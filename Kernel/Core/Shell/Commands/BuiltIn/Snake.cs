// Authors:
//	Aaron Lindsay <Aaron@rivalsource.net>

// This was just a test to get my feet wet with the kernel and did help me quite a lot.
// I decided to finish it up to a playable state to have something to show
// Notes...
// 1) This game will move at different speeds at different clock speeds until I
//    find a way to make it consistent :/ (suggestions welcome, I'm new to all this)
// 2) It currently just plays one level forever... I plan on adding level support later :)
// 3) Enjoy your first genuine kernel game :)

//Regarding the Random struct, most of it was borrowed from Mono:
//
// System.Random.cs
//
// Authors:
//   Bob Smith (bob@thestuff.net)
//   Ben Maurer (bmaurer@users.sourceforge.net)
//
// (C) 2001 Bob Smith.  http://www.thestuff.net
// (C) 2003 Ben Maurer
//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Runtime.InteropServices;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Shell.Commands.BuiltIn {
	public unsafe static class Snake {
		private const string name = "snake";
		private const string shortDescription = "Plays a quick game of Snake#";
		private const string lblExecute = "COMMANDS.snake.Execute";
		private const string lblGetHelp = "COMMANDS.snake.GetHelp";

		private const string SNAKE_KEYDOWN_HANDLER = "SNAKE_KEYDOWN_HANDLER";
		private const string SNAKE_TIMER_HANDLER = "SNAKE_TIMER_HANDLER";

		private static int keyCount = 0;
		private static bool playing = false;
		private static bool waiting = false;
		private static int mapWidth;
		private static int mapHeight;
		private static int speed;
		private static int points;
		private static Space* map;
		private static SnakeBody* snake;
		private static Direction direction;
		private static Direction nextDirection;
		private static SnakeBody* apple;
		private static Random* random;
		private static int snakeLength;
		private static int toAdd;
		private static bool randomizeApple;
		private static int goal;
		private static int level;
		private static int lives;

		private const int DEFAULT_LIVES = 3;
		private const int MAX_LEVELS = 5;
		private const int DEFAULT_SPEED = 8;
		private const int ADD_PER_APPLE = 3;
		private const string EMPTY_SPACE = " ";
		private const string WALL_SPACE = " ";
		private const string APPLE_SPACE = "*";
		private const string HEAD_SPACE = "@";
		private const string BODY_SPACE = "o";
		private const TextColor EMPTY_COLOUR_BG = TextColor.Black;
		private const TextColor EMPTY_COLOUR_FG = TextColor.White;
		private const TextColor WALL_COLOUR_BG = TextColor.Brown;
		private const TextColor WALL_COLOUR_FG = TextColor.Brown;
		private const TextColor APPLE_COLOUR_BG = EMPTY_COLOUR_BG;
		private const TextColor APPLE_COLOUR_FG = TextColor.Red;
		private const TextColor BODY_COLOUR_BG = EMPTY_COLOUR_BG;
		private const TextColor BODY_COLOUR_FG = TextColor.Green;
		private const TextColor HEAD_COLOUR_BG = BODY_COLOUR_BG;
		private const TextColor HEAD_COLOUR_FG = BODY_COLOUR_FG;
		private const Keys DIRECTION_LEFT = Keys.LeftArrow;
		private const Keys DIRECTION_RIGHT = Keys.RightArrow;
		private const Keys DIRECTION_UP = Keys.UpArrow;
		private const Keys DIRECTION_DOWN = Keys.DownArrow;
		private const Keys PAUSE_KEY = Keys.Backspace;
		private const Keys QUIT_KEY = Keys.Escape;

		[Label (SNAKE_TIMER_HANDLER)]
		public static void Timer (uint ticks)
		{
			if (!playing)
				return;

			int newSpeed;

			if (IsDirectionVertical (nextDirection))
				newSpeed = speed * 6 / 5;
			else
				newSpeed = speed;

			if ((int) ticks % newSpeed == 0) {
				direction = nextDirection;

				Update (ticks);

				if (playing)
					RenderScreen (ticks);
			}
		}

		[Label (SNAKE_KEYDOWN_HANDLER)]
		public static unsafe void KeyDown (uint scancode)
		{
			if (waiting || playing)
				keyCount++;

			if (waiting) {
				playing = true;
				waiting = false;

				TextMode.ClearScreen ();

				return;
			}

			//No unregister key handler?
			if (!playing)
				return;

			Keys key = (Keys) scancode;

			if (key == QUIT_KEY) {
				QuitGame ();
			} else if (key == PAUSE_KEY) {
				TextMode.MoveTo (0, 0);
				TextMode.ClearToEndOfLine ();
				TextMode.Write ("Game paused, press any key to continue...");
				waiting = true;
				playing = false;
			} else if (key == DIRECTION_LEFT) {
				if (IsDirectionVertical ())
					nextDirection = Direction.Left;
			} else if (key == DIRECTION_RIGHT) {
				if (IsDirectionVertical ())
					nextDirection = Direction.Right;
			} else if (key == DIRECTION_UP) {
				if (!IsDirectionVertical ())
					nextDirection = Direction.Up;
			} else if (key == DIRECTION_DOWN) {
				if (!IsDirectionVertical ())
					nextDirection = Direction.Down;
			}
		}

		private static bool IsDirectionVertical ()
		{
			return IsDirectionVertical (direction);
		}

		private static bool IsDirectionVertical (Direction direction)
		{
			return direction == Direction.Up || direction == Direction.Down;
		}

		private unsafe static void Update (uint ticks)
		{
			if (randomizeApple)
				RandomizeApple (ticks);

			SnakeBody head = snake [0];

			SnakeBody next;
			if (direction == Direction.Left) {
				next.y = head.y;
				next.x = head.x - 1;
			} else if (direction == Direction.Right) {
				next.y = head.y;
				next.x = head.x + 1;
			} else if (direction == Direction.Up) {
				next.x = head.x;
				next.y = head.y - 1;
			} else /* if (direction == Direction.Down) */ {
				next.x = head.x;
				next.y = head.y + 1;
			}

			Space nextSpace = map [ToFlatIndex (next.x, next.y)];
			if (nextSpace == Space.Apple) {
				//Yay point
				points++;

				randomizeApple = true;

				if (points == goal) {
					// Next level
					points = 0;
					level++;
					SetupMap ();

					TextMode.MoveTo (0, 0);
					TextMode.ClearToEndOfLine ();
					TextMode.Write ("Congratulations, you cleared the level! Press any key to continue...");
					waiting = true;
					playing = false;

					return;
				} else
					toAdd += ADD_PER_APPLE;
			} else if (nextSpace != Space.Empty) {
				//Ohnoes!

				if (lives - 1 == 0) {
					QuitGame ();
				} else {
					lives--;
					EraseSnake ();
					SetupSnake (ToModulusLevel (level));

					TextMode.MoveTo (0, 0);
					TextMode.ClearToEndOfLine ();
					TextMode.Write ("Argh, you lost a life! :( Press any key to continue...");
					waiting = true;
					playing = false;
				}
				return;
			}

			//Move
			SnakeBody oldTail = snake [snakeLength - 1];
			for (int i = snakeLength - 1; i >= 0; i--) {
				snake [i] = snake [i - 1];
			}

			snake [0] = next;
			//Draw changes to map (erase last position; draw head at new position;
			//                     replace old head with body)
			map [ToFlatIndex (oldTail.x, oldTail.y)] = Space.Empty;
			map [ToFlatIndex (next.x, next.y)] = Space.SnakeHead;
			map [ToFlatIndex (snake [1].x, snake [1].y)] = Space.SnakeBody;

			//Elongate the snake
			if (toAdd > 0) {
				snakeLength++;
				SnakeBody* newSnake = (SnakeBody*) MemoryManager.Allocate ((
					uint) (sizeof (SnakeBody) * snakeLength));

				for (int i = 0; i < snakeLength; i++) {
					if (i == snakeLength - 1)
						newSnake [i] = oldTail;
					else
						newSnake [i] = snake [i];
				}

				MemoryManager.Free ((void*) snake);
				snake = newSnake;

				toAdd--;
			}
		}

		private static void EraseSnake ()
		{
			for (int i = 0; i < snakeLength; i++) {
				map [ToFlatIndex (snake [i].x, snake [i].y)] = Space.Empty;
			}
		}

		private static void RandomizeApple (uint ticks)
		{
			if ((uint) random == 0) {
				random = (Random*) MemoryManager.Allocate ((uint) sizeof (Random));
				random->CREATE ((int) ticks);
			}

			int position;

			do {
				position = random->Next (0, ToFlatIndex (mapWidth - 1, mapHeight - 1));
			} while (map [position] != Space.Empty);

			map [position] = Space.Apple;

			apple->x = position % mapWidth;
			apple->y = (position - apple->x) / mapWidth;

			randomizeApple = false;
		}

		private static void RenderScreen (uint ticks)
		{
			TextMode.MoveTo (0, 0);
			TextMode.ClearToEndOfLine ();

			TextMode.Write ("Snake# v0.2 - Lives: ");
			TextMode.Write (lives);
			TextMode.Write (" - Level ");
			TextMode.Write (level);
			TextMode.Write (" - ");
			TextMode.Write (points);
			TextMode.Write (" / ");
			TextMode.Write (goal);
			TextMode.WriteLine (" Points");

			TextMode.SaveAttributes ();

			for (int y = 0; y < mapHeight; y++) {
				for (int x = 0; x < mapWidth; x++) {
					int index = ToFlatIndex (x, y);
					Space space = map [index];

					//I read that switches are borked?
					if (space == Space.Empty) {
						TextMode.SetAttributes (EMPTY_COLOUR_FG, EMPTY_COLOUR_BG);
						TextMode.Write (EMPTY_SPACE);
					} else if (space == Space.Wall) {
						TextMode.SetAttributes (WALL_COLOUR_FG, WALL_COLOUR_BG);
						TextMode.Write (WALL_SPACE);
					} else if (space == Space.Apple) {
						TextMode.SetAttributes (APPLE_COLOUR_FG, APPLE_COLOUR_BG);
						TextMode.Write (APPLE_SPACE);
					} else if (space == Space.SnakeBody) {
						TextMode.SetAttributes (BODY_COLOUR_FG, BODY_COLOUR_BG);
						TextMode.Write (BODY_SPACE);
					} else if (space == Space.SnakeHead) {
						TextMode.SetAttributes (HEAD_COLOUR_FG, HEAD_COLOUR_BG);
						TextMode.Write (HEAD_SPACE);
					} else {
						TextMode.SetAttributes (EMPTY_COLOUR_FG, EMPTY_COLOUR_BG);
						TextMode.Write (EMPTY_SPACE);
					}
				}

				//Only do so if mapWidth < TextMode.GetScreenSize()'s width:
				//TextMode.WriteLine();
			}

			TextMode.RestoreAttributes ();

			TextMode.ClearToEndOfLine ();
		}

		private static void QuitGame ()
		{
			MemoryManager.Free ((void*) map);
			MemoryManager.Free ((void*) snake);
			MemoryManager.Free ((void*) apple);

			if ((uint) random != 0) {
				random->DISPOSE ();
				MemoryManager.Free ((void*) random);
				random = (Random*) 0;
			}

			playing = false;

			TextMode.MoveTo (keyCount, mapHeight);

			// Erase anything the keypressing might have done to the prompter
			// (lotta 'f's if you don't)... If up/down history is introduced
			// this'll break (it won't backspace/erase enough).
			for (int i = 0; i < keyCount; i++) {
				// Calling Console.KeyDown() directly makes the AOT compiler
				// error out, so I trick it
				SharpOS.Kernel.ADC.MemoryUtil.Call ((void*) Stubs.GetFunctionPointer (
					Console.CONSOLE_KEY_DOWN_HANDLER), (void*) (uint) Keys.Backspace);
			}

			keyCount = 0;

			TextMode.MoveTo (0, mapHeight);

			TextMode.ClearToEndOfLine ();
			WriteInfo ();
			TextMode.ClearToEndOfLine ();

			TextMode.Write ("Game Over - You reached level ");
			TextMode.Write (level);
			TextMode.Write (" - at ");
			TextMode.Write (points);
			TextMode.Write (" / ");
			TextMode.Write (goal);
			TextMode.WriteLine (" points.");

			TextMode.WriteLine ();

			//Show the prompt. This and the backspacing above is just a hack to make it integrate more cleanly.
			Prompter.WritePrompt ();
		}

		private static void WriteInfo ()
		{
			TextMode.WriteLine ("Snake# v0.2");
			TextMode.WriteLine ("(C)opyright 2008 Aaron \"AerialX\" Lindsay");
		}

		private static void SetupMap ()
		{
			SetupMap (level);
		}

		private unsafe static void SetupMap (int level)
		{
			// Start with a basic level with walls around the outside
			for (int y = 0; y < mapHeight; y++) {
				for (int x = 0; x < mapWidth; x++) {
					int index = ToFlatIndex (x, y);

					if (y == 0 || x == 0 || y == mapHeight - 1 || x == mapWidth - 1)
						map [index] = Space.Wall;
					else
						map [index] = Space.Empty;
				}
			}

			// Let the level add any walls it wishes to
			SetupLevel (level);
		}

		private static void SetupLevel ()
		{
			SetupLevel (level);
		}

		// TODO: I welcome others to add their own levels :)
		//       Just ensure it increases in difficulty (the SharpOS one should
		//       remain last) and don't forget to increment the MAX_LEVELS const.
		public static void SetupLevel (int level)
		{
			int mapSpeed;

			// Go back to level one if they reach higher than what we have, but
			// still have the game regard it as a higher level
			// (Up the speed after each cycle through all levels)
			level = ToModulusLevel (level);

			if (level == Snake.level)
				mapSpeed = DEFAULT_SPEED;
			else
				mapSpeed = DEFAULT_SPEED - (Snake.level - level) / (MAX_LEVELS + 1);

			if (level == 1) {
				speed = mapSpeed;
				goal = 10;
			} else if (level == 2) {
				// Put a line of walls right through the vertical middle
				// (half the map height centered)
				int x = mapWidth / 2;
				for (int y = mapHeight * 1 / 4; y < mapHeight * 3 / 4; y++) {
					map [ToFlatIndex (x, y)] = Space.Wall;
				}

				SetupSnake (level);
				speed = mapSpeed;
				goal = 10;
			} else if (level == 3) {
				map [ToFlatIndex (13, 1)] = Space.Wall;
				map [ToFlatIndex (39, 1)] = Space.Wall;
				map [ToFlatIndex (65, 1)] = Space.Wall;
				map [ToFlatIndex (13, 2)] = Space.Wall;
				map [ToFlatIndex (39, 2)] = Space.Wall;
				map [ToFlatIndex (65, 2)] = Space.Wall;
				map [ToFlatIndex (13, 3)] = Space.Wall;
				map [ToFlatIndex (39, 3)] = Space.Wall;
				map [ToFlatIndex (65, 3)] = Space.Wall;
				map [ToFlatIndex (13, 4)] = Space.Wall;
				map [ToFlatIndex (39, 4)] = Space.Wall;
				map [ToFlatIndex (65, 4)] = Space.Wall;
				map [ToFlatIndex (13, 5)] = Space.Wall;
				map [ToFlatIndex (39, 5)] = Space.Wall;
				map [ToFlatIndex (65, 5)] = Space.Wall;
				map [ToFlatIndex (13, 6)] = Space.Wall;
				map [ToFlatIndex (65, 6)] = Space.Wall;
				map [ToFlatIndex (13, 7)] = Space.Wall;
				map [ToFlatIndex (39, 7)] = Space.Wall;
				map [ToFlatIndex (65, 7)] = Space.Wall;
				map [ToFlatIndex (13, 8)] = Space.Wall;
				map [ToFlatIndex (39, 8)] = Space.Wall;
				map [ToFlatIndex (65, 8)] = Space.Wall;
				map [ToFlatIndex (13, 9)] = Space.Wall;
				map [ToFlatIndex (39, 9)] = Space.Wall;
				map [ToFlatIndex (65, 9)] = Space.Wall;
				map [ToFlatIndex (13, 10)] = Space.Wall;
				map [ToFlatIndex (26, 10)] = Space.Wall;
				map [ToFlatIndex (39, 10)] = Space.Wall;
				map [ToFlatIndex (52, 10)] = Space.Wall;
				map [ToFlatIndex (65, 10)] = Space.Wall;
				map [ToFlatIndex (26, 11)] = Space.Wall;
				map [ToFlatIndex (52, 11)] = Space.Wall;
				map [ToFlatIndex (26, 12)] = Space.Wall;
				map [ToFlatIndex (52, 12)] = Space.Wall;
				map [ToFlatIndex (26, 13)] = Space.Wall;
				map [ToFlatIndex (52, 13)] = Space.Wall;
				map [ToFlatIndex (26, 14)] = Space.Wall;
				map [ToFlatIndex (52, 14)] = Space.Wall;
				map [ToFlatIndex (26, 15)] = Space.Wall;
				map [ToFlatIndex (52, 15)] = Space.Wall;
				map [ToFlatIndex (26, 16)] = Space.Wall;
				map [ToFlatIndex (52, 16)] = Space.Wall;
				map [ToFlatIndex (26, 17)] = Space.Wall;
				map [ToFlatIndex (52, 17)] = Space.Wall;
				map [ToFlatIndex (26, 18)] = Space.Wall;
				map [ToFlatIndex (52, 18)] = Space.Wall;
				map [ToFlatIndex (26, 19)] = Space.Wall;
				map [ToFlatIndex (52, 19)] = Space.Wall;
				map [ToFlatIndex (26, 20)] = Space.Wall;
				map [ToFlatIndex (52, 20)] = Space.Wall;
				goal = 10;
				speed = mapSpeed;
			} else if (level == 4) {
				map [ToFlatIndex (2, 2)] = Space.Wall;
				map [ToFlatIndex (4, 2)] = Space.Wall;
				map [ToFlatIndex (5, 2)] = Space.Wall;
				map [ToFlatIndex (6, 2)] = Space.Wall;
				map [ToFlatIndex (7, 2)] = Space.Wall;
				map [ToFlatIndex (8, 2)] = Space.Wall;
				map [ToFlatIndex (9, 2)] = Space.Wall;
				map [ToFlatIndex (10, 2)] = Space.Wall;
				map [ToFlatIndex (11, 2)] = Space.Wall;
				map [ToFlatIndex (12, 2)] = Space.Wall;
				map [ToFlatIndex (13, 2)] = Space.Wall;
				map [ToFlatIndex (14, 2)] = Space.Wall;
				map [ToFlatIndex (15, 2)] = Space.Wall;
				map [ToFlatIndex (16, 2)] = Space.Wall;
				map [ToFlatIndex (17, 2)] = Space.Wall;
				map [ToFlatIndex (18, 2)] = Space.Wall;
				map [ToFlatIndex (19, 2)] = Space.Wall;
				map [ToFlatIndex (20, 2)] = Space.Wall;
				map [ToFlatIndex (21, 2)] = Space.Wall;
				map [ToFlatIndex (22, 2)] = Space.Wall;
				map [ToFlatIndex (23, 2)] = Space.Wall;
				map [ToFlatIndex (24, 2)] = Space.Wall;
				map [ToFlatIndex (25, 2)] = Space.Wall;
				map [ToFlatIndex (26, 2)] = Space.Wall;
				map [ToFlatIndex (27, 2)] = Space.Wall;
				map [ToFlatIndex (28, 2)] = Space.Wall;
				map [ToFlatIndex (29, 2)] = Space.Wall;
				map [ToFlatIndex (30, 2)] = Space.Wall;
				map [ToFlatIndex (31, 2)] = Space.Wall;
				map [ToFlatIndex (32, 2)] = Space.Wall;
				map [ToFlatIndex (47, 2)] = Space.Wall;
				map [ToFlatIndex (49, 2)] = Space.Wall;
				map [ToFlatIndex (50, 2)] = Space.Wall;
				map [ToFlatIndex (51, 2)] = Space.Wall;
				map [ToFlatIndex (52, 2)] = Space.Wall;
				map [ToFlatIndex (53, 2)] = Space.Wall;
				map [ToFlatIndex (54, 2)] = Space.Wall;
				map [ToFlatIndex (55, 2)] = Space.Wall;
				map [ToFlatIndex (56, 2)] = Space.Wall;
				map [ToFlatIndex (57, 2)] = Space.Wall;
				map [ToFlatIndex (58, 2)] = Space.Wall;
				map [ToFlatIndex (59, 2)] = Space.Wall;
				map [ToFlatIndex (60, 2)] = Space.Wall;
				map [ToFlatIndex (61, 2)] = Space.Wall;
				map [ToFlatIndex (62, 2)] = Space.Wall;
				map [ToFlatIndex (63, 2)] = Space.Wall;
				map [ToFlatIndex (64, 2)] = Space.Wall;
				map [ToFlatIndex (65, 2)] = Space.Wall;
				map [ToFlatIndex (66, 2)] = Space.Wall;
				map [ToFlatIndex (67, 2)] = Space.Wall;
				map [ToFlatIndex (68, 2)] = Space.Wall;
				map [ToFlatIndex (69, 2)] = Space.Wall;
				map [ToFlatIndex (70, 2)] = Space.Wall;
				map [ToFlatIndex (71, 2)] = Space.Wall;
				map [ToFlatIndex (72, 2)] = Space.Wall;
				map [ToFlatIndex (73, 2)] = Space.Wall;
				map [ToFlatIndex (74, 2)] = Space.Wall;
				map [ToFlatIndex (75, 2)] = Space.Wall;
				map [ToFlatIndex (76, 2)] = Space.Wall;
				map [ToFlatIndex (77, 2)] = Space.Wall;
				map [ToFlatIndex (2, 3)] = Space.Wall;
				map [ToFlatIndex (47, 3)] = Space.Wall;
				map [ToFlatIndex (2, 4)] = Space.Wall;
				map [ToFlatIndex (32, 4)] = Space.Wall;
				map [ToFlatIndex (47, 4)] = Space.Wall;
				map [ToFlatIndex (77, 4)] = Space.Wall;
				map [ToFlatIndex (2, 5)] = Space.Wall;
				map [ToFlatIndex (32, 5)] = Space.Wall;
				map [ToFlatIndex (47, 5)] = Space.Wall;
				map [ToFlatIndex (77, 5)] = Space.Wall;
				map [ToFlatIndex (2, 6)] = Space.Wall;
				map [ToFlatIndex (32, 6)] = Space.Wall;
				map [ToFlatIndex (47, 6)] = Space.Wall;
				map [ToFlatIndex (77, 6)] = Space.Wall;
				map [ToFlatIndex (2, 7)] = Space.Wall;
				map [ToFlatIndex (32, 7)] = Space.Wall;
				map [ToFlatIndex (47, 7)] = Space.Wall;
				map [ToFlatIndex (77, 7)] = Space.Wall;
				map [ToFlatIndex (2, 8)] = Space.Wall;
				map [ToFlatIndex (47, 8)] = Space.Wall;
				map [ToFlatIndex (2, 9)] = Space.Wall;
				map [ToFlatIndex (4, 9)] = Space.Wall;
				map [ToFlatIndex (5, 9)] = Space.Wall;
				map [ToFlatIndex (6, 9)] = Space.Wall;
				map [ToFlatIndex (7, 9)] = Space.Wall;
				map [ToFlatIndex (8, 9)] = Space.Wall;
				map [ToFlatIndex (9, 9)] = Space.Wall;
				map [ToFlatIndex (10, 9)] = Space.Wall;
				map [ToFlatIndex (11, 9)] = Space.Wall;
				map [ToFlatIndex (12, 9)] = Space.Wall;
				map [ToFlatIndex (13, 9)] = Space.Wall;
				map [ToFlatIndex (14, 9)] = Space.Wall;
				map [ToFlatIndex (15, 9)] = Space.Wall;
				map [ToFlatIndex (16, 9)] = Space.Wall;
				map [ToFlatIndex (17, 9)] = Space.Wall;
				map [ToFlatIndex (18, 9)] = Space.Wall;
				map [ToFlatIndex (19, 9)] = Space.Wall;
				map [ToFlatIndex (20, 9)] = Space.Wall;
				map [ToFlatIndex (21, 9)] = Space.Wall;
				map [ToFlatIndex (22, 9)] = Space.Wall;
				map [ToFlatIndex (23, 9)] = Space.Wall;
				map [ToFlatIndex (24, 9)] = Space.Wall;
				map [ToFlatIndex (25, 9)] = Space.Wall;
				map [ToFlatIndex (26, 9)] = Space.Wall;
				map [ToFlatIndex (27, 9)] = Space.Wall;
				map [ToFlatIndex (28, 9)] = Space.Wall;
				map [ToFlatIndex (29, 9)] = Space.Wall;
				map [ToFlatIndex (30, 9)] = Space.Wall;
				map [ToFlatIndex (31, 9)] = Space.Wall;
				map [ToFlatIndex (32, 9)] = Space.Wall;
				map [ToFlatIndex (47, 9)] = Space.Wall;
				map [ToFlatIndex (49, 9)] = Space.Wall;
				map [ToFlatIndex (50, 9)] = Space.Wall;
				map [ToFlatIndex (51, 9)] = Space.Wall;
				map [ToFlatIndex (52, 9)] = Space.Wall;
				map [ToFlatIndex (53, 9)] = Space.Wall;
				map [ToFlatIndex (54, 9)] = Space.Wall;
				map [ToFlatIndex (55, 9)] = Space.Wall;
				map [ToFlatIndex (56, 9)] = Space.Wall;
				map [ToFlatIndex (57, 9)] = Space.Wall;
				map [ToFlatIndex (58, 9)] = Space.Wall;
				map [ToFlatIndex (59, 9)] = Space.Wall;
				map [ToFlatIndex (60, 9)] = Space.Wall;
				map [ToFlatIndex (61, 9)] = Space.Wall;
				map [ToFlatIndex (62, 9)] = Space.Wall;
				map [ToFlatIndex (63, 9)] = Space.Wall;
				map [ToFlatIndex (64, 9)] = Space.Wall;
				map [ToFlatIndex (65, 9)] = Space.Wall;
				map [ToFlatIndex (66, 9)] = Space.Wall;
				map [ToFlatIndex (67, 9)] = Space.Wall;
				map [ToFlatIndex (68, 9)] = Space.Wall;
				map [ToFlatIndex (69, 9)] = Space.Wall;
				map [ToFlatIndex (70, 9)] = Space.Wall;
				map [ToFlatIndex (71, 9)] = Space.Wall;
				map [ToFlatIndex (72, 9)] = Space.Wall;
				map [ToFlatIndex (73, 9)] = Space.Wall;
				map [ToFlatIndex (74, 9)] = Space.Wall;
				map [ToFlatIndex (75, 9)] = Space.Wall;
				map [ToFlatIndex (76, 9)] = Space.Wall;
				map [ToFlatIndex (77, 9)] = Space.Wall;
				map [ToFlatIndex (2, 12)] = Space.Wall;
				map [ToFlatIndex (3, 12)] = Space.Wall;
				map [ToFlatIndex (4, 12)] = Space.Wall;
				map [ToFlatIndex (5, 12)] = Space.Wall;
				map [ToFlatIndex (6, 12)] = Space.Wall;
				map [ToFlatIndex (7, 12)] = Space.Wall;
				map [ToFlatIndex (8, 12)] = Space.Wall;
				map [ToFlatIndex (9, 12)] = Space.Wall;
				map [ToFlatIndex (10, 12)] = Space.Wall;
				map [ToFlatIndex (11, 12)] = Space.Wall;
				map [ToFlatIndex (12, 12)] = Space.Wall;
				map [ToFlatIndex (13, 12)] = Space.Wall;
				map [ToFlatIndex (14, 12)] = Space.Wall;
				map [ToFlatIndex (15, 12)] = Space.Wall;
				map [ToFlatIndex (16, 12)] = Space.Wall;
				map [ToFlatIndex (17, 12)] = Space.Wall;
				map [ToFlatIndex (18, 12)] = Space.Wall;
				map [ToFlatIndex (19, 12)] = Space.Wall;
				map [ToFlatIndex (20, 12)] = Space.Wall;
				map [ToFlatIndex (21, 12)] = Space.Wall;
				map [ToFlatIndex (22, 12)] = Space.Wall;
				map [ToFlatIndex (23, 12)] = Space.Wall;
				map [ToFlatIndex (24, 12)] = Space.Wall;
				map [ToFlatIndex (25, 12)] = Space.Wall;
				map [ToFlatIndex (26, 12)] = Space.Wall;
				map [ToFlatIndex (27, 12)] = Space.Wall;
				map [ToFlatIndex (28, 12)] = Space.Wall;
				map [ToFlatIndex (29, 12)] = Space.Wall;
				map [ToFlatIndex (30, 12)] = Space.Wall;
				map [ToFlatIndex (32, 12)] = Space.Wall;
				map [ToFlatIndex (47, 12)] = Space.Wall;
				map [ToFlatIndex (48, 12)] = Space.Wall;
				map [ToFlatIndex (49, 12)] = Space.Wall;
				map [ToFlatIndex (50, 12)] = Space.Wall;
				map [ToFlatIndex (51, 12)] = Space.Wall;
				map [ToFlatIndex (52, 12)] = Space.Wall;
				map [ToFlatIndex (53, 12)] = Space.Wall;
				map [ToFlatIndex (54, 12)] = Space.Wall;
				map [ToFlatIndex (55, 12)] = Space.Wall;
				map [ToFlatIndex (56, 12)] = Space.Wall;
				map [ToFlatIndex (57, 12)] = Space.Wall;
				map [ToFlatIndex (58, 12)] = Space.Wall;
				map [ToFlatIndex (59, 12)] = Space.Wall;
				map [ToFlatIndex (60, 12)] = Space.Wall;
				map [ToFlatIndex (61, 12)] = Space.Wall;
				map [ToFlatIndex (62, 12)] = Space.Wall;
				map [ToFlatIndex (63, 12)] = Space.Wall;
				map [ToFlatIndex (64, 12)] = Space.Wall;
				map [ToFlatIndex (65, 12)] = Space.Wall;
				map [ToFlatIndex (66, 12)] = Space.Wall;
				map [ToFlatIndex (67, 12)] = Space.Wall;
				map [ToFlatIndex (68, 12)] = Space.Wall;
				map [ToFlatIndex (69, 12)] = Space.Wall;
				map [ToFlatIndex (70, 12)] = Space.Wall;
				map [ToFlatIndex (71, 12)] = Space.Wall;
				map [ToFlatIndex (72, 12)] = Space.Wall;
				map [ToFlatIndex (73, 12)] = Space.Wall;
				map [ToFlatIndex (74, 12)] = Space.Wall;
				map [ToFlatIndex (75, 12)] = Space.Wall;
				map [ToFlatIndex (77, 12)] = Space.Wall;
				map [ToFlatIndex (32, 13)] = Space.Wall;
				map [ToFlatIndex (77, 13)] = Space.Wall;
				map [ToFlatIndex (2, 14)] = Space.Wall;
				map [ToFlatIndex (32, 14)] = Space.Wall;
				map [ToFlatIndex (47, 14)] = Space.Wall;
				map [ToFlatIndex (77, 14)] = Space.Wall;
				map [ToFlatIndex (2, 15)] = Space.Wall;
				map [ToFlatIndex (32, 15)] = Space.Wall;
				map [ToFlatIndex (47, 15)] = Space.Wall;
				map [ToFlatIndex (77, 15)] = Space.Wall;
				map [ToFlatIndex (2, 16)] = Space.Wall;
				map [ToFlatIndex (32, 16)] = Space.Wall;
				map [ToFlatIndex (47, 16)] = Space.Wall;
				map [ToFlatIndex (77, 16)] = Space.Wall;
				map [ToFlatIndex (2, 17)] = Space.Wall;
				map [ToFlatIndex (32, 17)] = Space.Wall;
				map [ToFlatIndex (47, 17)] = Space.Wall;
				map [ToFlatIndex (77, 17)] = Space.Wall;
				map [ToFlatIndex (32, 18)] = Space.Wall;
				map [ToFlatIndex (77, 18)] = Space.Wall;
				map [ToFlatIndex (2, 19)] = Space.Wall;
				map [ToFlatIndex (3, 19)] = Space.Wall;
				map [ToFlatIndex (4, 19)] = Space.Wall;
				map [ToFlatIndex (5, 19)] = Space.Wall;
				map [ToFlatIndex (6, 19)] = Space.Wall;
				map [ToFlatIndex (7, 19)] = Space.Wall;
				map [ToFlatIndex (8, 19)] = Space.Wall;
				map [ToFlatIndex (9, 19)] = Space.Wall;
				map [ToFlatIndex (10, 19)] = Space.Wall;
				map [ToFlatIndex (11, 19)] = Space.Wall;
				map [ToFlatIndex (12, 19)] = Space.Wall;
				map [ToFlatIndex (13, 19)] = Space.Wall;
				map [ToFlatIndex (14, 19)] = Space.Wall;
				map [ToFlatIndex (15, 19)] = Space.Wall;
				map [ToFlatIndex (16, 19)] = Space.Wall;
				map [ToFlatIndex (17, 19)] = Space.Wall;
				map [ToFlatIndex (18, 19)] = Space.Wall;
				map [ToFlatIndex (19, 19)] = Space.Wall;
				map [ToFlatIndex (20, 19)] = Space.Wall;
				map [ToFlatIndex (21, 19)] = Space.Wall;
				map [ToFlatIndex (22, 19)] = Space.Wall;
				map [ToFlatIndex (23, 19)] = Space.Wall;
				map [ToFlatIndex (24, 19)] = Space.Wall;
				map [ToFlatIndex (25, 19)] = Space.Wall;
				map [ToFlatIndex (26, 19)] = Space.Wall;
				map [ToFlatIndex (27, 19)] = Space.Wall;
				map [ToFlatIndex (28, 19)] = Space.Wall;
				map [ToFlatIndex (29, 19)] = Space.Wall;
				map [ToFlatIndex (30, 19)] = Space.Wall;
				map [ToFlatIndex (32, 19)] = Space.Wall;
				map [ToFlatIndex (47, 19)] = Space.Wall;
				map [ToFlatIndex (48, 19)] = Space.Wall;
				map [ToFlatIndex (49, 19)] = Space.Wall;
				map [ToFlatIndex (50, 19)] = Space.Wall;
				map [ToFlatIndex (51, 19)] = Space.Wall;
				map [ToFlatIndex (52, 19)] = Space.Wall;
				map [ToFlatIndex (53, 19)] = Space.Wall;
				map [ToFlatIndex (54, 19)] = Space.Wall;
				map [ToFlatIndex (55, 19)] = Space.Wall;
				map [ToFlatIndex (56, 19)] = Space.Wall;
				map [ToFlatIndex (57, 19)] = Space.Wall;
				map [ToFlatIndex (58, 19)] = Space.Wall;
				map [ToFlatIndex (59, 19)] = Space.Wall;
				map [ToFlatIndex (60, 19)] = Space.Wall;
				map [ToFlatIndex (61, 19)] = Space.Wall;
				map [ToFlatIndex (62, 19)] = Space.Wall;
				map [ToFlatIndex (63, 19)] = Space.Wall;
				map [ToFlatIndex (64, 19)] = Space.Wall;
				map [ToFlatIndex (65, 19)] = Space.Wall;
				map [ToFlatIndex (66, 19)] = Space.Wall;
				map [ToFlatIndex (67, 19)] = Space.Wall;
				map [ToFlatIndex (68, 19)] = Space.Wall;
				map [ToFlatIndex (69, 19)] = Space.Wall;
				map [ToFlatIndex (70, 19)] = Space.Wall;
				map [ToFlatIndex (71, 19)] = Space.Wall;
				map [ToFlatIndex (72, 19)] = Space.Wall;
				map [ToFlatIndex (73, 19)] = Space.Wall;
				map [ToFlatIndex (74, 19)] = Space.Wall;
				map [ToFlatIndex (75, 19)] = Space.Wall;
				map [ToFlatIndex (77, 19)] = Space.Wall;
				goal = 10;
				speed = mapSpeed;
			} else if (level == 5) {
				map [ToFlatIndex (15, 3)] = Space.Wall;
				map [ToFlatIndex (16, 3)] = Space.Wall;
				map [ToFlatIndex (7, 4)] = Space.Wall;
				map [ToFlatIndex (8, 4)] = Space.Wall;
				map [ToFlatIndex (9, 4)] = Space.Wall;
				map [ToFlatIndex (10, 4)] = Space.Wall;
				map [ToFlatIndex (15, 4)] = Space.Wall;
				map [ToFlatIndex (16, 4)] = Space.Wall;
				map [ToFlatIndex (59, 4)] = Space.Wall;
				map [ToFlatIndex (60, 4)] = Space.Wall;
				map [ToFlatIndex (61, 4)] = Space.Wall;
				map [ToFlatIndex (67, 4)] = Space.Wall;
				map [ToFlatIndex (68, 4)] = Space.Wall;
				map [ToFlatIndex (69, 4)] = Space.Wall;
				map [ToFlatIndex (70, 4)] = Space.Wall;
				map [ToFlatIndex (6, 5)] = Space.Wall;
				map [ToFlatIndex (7, 5)] = Space.Wall;
				map [ToFlatIndex (11, 5)] = Space.Wall;
				map [ToFlatIndex (15, 5)] = Space.Wall;
				map [ToFlatIndex (16, 5)] = Space.Wall;
				map [ToFlatIndex (56, 5)] = Space.Wall;
				map [ToFlatIndex (57, 5)] = Space.Wall;
				map [ToFlatIndex (61, 5)] = Space.Wall;
				map [ToFlatIndex (62, 5)] = Space.Wall;
				map [ToFlatIndex (66, 5)] = Space.Wall;
				map [ToFlatIndex (67, 5)] = Space.Wall;
				map [ToFlatIndex (71, 5)] = Space.Wall;
				map [ToFlatIndex (5, 6)] = Space.Wall;
				map [ToFlatIndex (6, 6)] = Space.Wall;
				map [ToFlatIndex (15, 6)] = Space.Wall;
				map [ToFlatIndex (16, 6)] = Space.Wall;
				map [ToFlatIndex (56, 6)] = Space.Wall;
				map [ToFlatIndex (57, 6)] = Space.Wall;
				map [ToFlatIndex (61, 6)] = Space.Wall;
				map [ToFlatIndex (62, 6)] = Space.Wall;
				map [ToFlatIndex (65, 6)] = Space.Wall;
				map [ToFlatIndex (66, 6)] = Space.Wall;
				map [ToFlatIndex (5, 7)] = Space.Wall;
				map [ToFlatIndex (6, 7)] = Space.Wall;
				map [ToFlatIndex (15, 7)] = Space.Wall;
				map [ToFlatIndex (16, 7)] = Space.Wall;
				map [ToFlatIndex (17, 7)] = Space.Wall;
				map [ToFlatIndex (18, 7)] = Space.Wall;
				map [ToFlatIndex (19, 7)] = Space.Wall;
				map [ToFlatIndex (20, 7)] = Space.Wall;
				map [ToFlatIndex (21, 7)] = Space.Wall;
				map [ToFlatIndex (27, 7)] = Space.Wall;
				map [ToFlatIndex (28, 7)] = Space.Wall;
				map [ToFlatIndex (29, 7)] = Space.Wall;
				map [ToFlatIndex (30, 7)] = Space.Wall;
				map [ToFlatIndex (31, 7)] = Space.Wall;
				map [ToFlatIndex (36, 7)] = Space.Wall;
				map [ToFlatIndex (37, 7)] = Space.Wall;
				map [ToFlatIndex (38, 7)] = Space.Wall;
				map [ToFlatIndex (39, 7)] = Space.Wall;
				map [ToFlatIndex (40, 7)] = Space.Wall;
				map [ToFlatIndex (41, 7)] = Space.Wall;
				map [ToFlatIndex (42, 7)] = Space.Wall;
				map [ToFlatIndex (45, 7)] = Space.Wall;
				map [ToFlatIndex (46, 7)] = Space.Wall;
				map [ToFlatIndex (48, 7)] = Space.Wall;
				map [ToFlatIndex (49, 7)] = Space.Wall;
				map [ToFlatIndex (50, 7)] = Space.Wall;
				map [ToFlatIndex (51, 7)] = Space.Wall;
				map [ToFlatIndex (55, 7)] = Space.Wall;
				map [ToFlatIndex (56, 7)] = Space.Wall;
				map [ToFlatIndex (62, 7)] = Space.Wall;
				map [ToFlatIndex (63, 7)] = Space.Wall;
				map [ToFlatIndex (65, 7)] = Space.Wall;
				map [ToFlatIndex (66, 7)] = Space.Wall;
				map [ToFlatIndex (5, 8)] = Space.Wall;
				map [ToFlatIndex (6, 8)] = Space.Wall;
				map [ToFlatIndex (7, 8)] = Space.Wall;
				map [ToFlatIndex (15, 8)] = Space.Wall;
				map [ToFlatIndex (16, 8)] = Space.Wall;
				map [ToFlatIndex (17, 8)] = Space.Wall;
				map [ToFlatIndex (21, 8)] = Space.Wall;
				map [ToFlatIndex (22, 8)] = Space.Wall;
				map [ToFlatIndex (26, 8)] = Space.Wall;
				map [ToFlatIndex (30, 8)] = Space.Wall;
				map [ToFlatIndex (31, 8)] = Space.Wall;
				map [ToFlatIndex (32, 8)] = Space.Wall;
				map [ToFlatIndex (36, 8)] = Space.Wall;
				map [ToFlatIndex (37, 8)] = Space.Wall;
				map [ToFlatIndex (38, 8)] = Space.Wall;
				map [ToFlatIndex (42, 8)] = Space.Wall;
				map [ToFlatIndex (43, 8)] = Space.Wall;
				map [ToFlatIndex (45, 8)] = Space.Wall;
				map [ToFlatIndex (46, 8)] = Space.Wall;
				map [ToFlatIndex (51, 8)] = Space.Wall;
				map [ToFlatIndex (52, 8)] = Space.Wall;
				map [ToFlatIndex (55, 8)] = Space.Wall;
				map [ToFlatIndex (56, 8)] = Space.Wall;
				map [ToFlatIndex (62, 8)] = Space.Wall;
				map [ToFlatIndex (63, 8)] = Space.Wall;
				map [ToFlatIndex (65, 8)] = Space.Wall;
				map [ToFlatIndex (66, 8)] = Space.Wall;
				map [ToFlatIndex (67, 8)] = Space.Wall;
				map [ToFlatIndex (6, 9)] = Space.Wall;
				map [ToFlatIndex (7, 9)] = Space.Wall;
				map [ToFlatIndex (8, 9)] = Space.Wall;
				map [ToFlatIndex (9, 9)] = Space.Wall;
				map [ToFlatIndex (15, 9)] = Space.Wall;
				map [ToFlatIndex (16, 9)] = Space.Wall;
				map [ToFlatIndex (21, 9)] = Space.Wall;
				map [ToFlatIndex (22, 9)] = Space.Wall;
				map [ToFlatIndex (31, 9)] = Space.Wall;
				map [ToFlatIndex (32, 9)] = Space.Wall;
				map [ToFlatIndex (36, 9)] = Space.Wall;
				map [ToFlatIndex (37, 9)] = Space.Wall;
				map [ToFlatIndex (42, 9)] = Space.Wall;
				map [ToFlatIndex (43, 9)] = Space.Wall;
				map [ToFlatIndex (45, 9)] = Space.Wall;
				map [ToFlatIndex (46, 9)] = Space.Wall;
				map [ToFlatIndex (52, 9)] = Space.Wall;
				map [ToFlatIndex (53, 9)] = Space.Wall;
				map [ToFlatIndex (55, 9)] = Space.Wall;
				map [ToFlatIndex (56, 9)] = Space.Wall;
				map [ToFlatIndex (62, 9)] = Space.Wall;
				map [ToFlatIndex (63, 9)] = Space.Wall;
				map [ToFlatIndex (66, 9)] = Space.Wall;
				map [ToFlatIndex (67, 9)] = Space.Wall;
				map [ToFlatIndex (68, 9)] = Space.Wall;
				map [ToFlatIndex (69, 9)] = Space.Wall;
				map [ToFlatIndex (8, 10)] = Space.Wall;
				map [ToFlatIndex (9, 10)] = Space.Wall;
				map [ToFlatIndex (10, 10)] = Space.Wall;
				map [ToFlatIndex (11, 10)] = Space.Wall;
				map [ToFlatIndex (15, 10)] = Space.Wall;
				map [ToFlatIndex (16, 10)] = Space.Wall;
				map [ToFlatIndex (21, 10)] = Space.Wall;
				map [ToFlatIndex (22, 10)] = Space.Wall;
				map [ToFlatIndex (31, 10)] = Space.Wall;
				map [ToFlatIndex (32, 10)] = Space.Wall;
				map [ToFlatIndex (36, 10)] = Space.Wall;
				map [ToFlatIndex (37, 10)] = Space.Wall;
				map [ToFlatIndex (45, 10)] = Space.Wall;
				map [ToFlatIndex (46, 10)] = Space.Wall;
				map [ToFlatIndex (52, 10)] = Space.Wall;
				map [ToFlatIndex (53, 10)] = Space.Wall;
				map [ToFlatIndex (55, 10)] = Space.Wall;
				map [ToFlatIndex (56, 10)] = Space.Wall;
				map [ToFlatIndex (62, 10)] = Space.Wall;
				map [ToFlatIndex (63, 10)] = Space.Wall;
				map [ToFlatIndex (68, 10)] = Space.Wall;
				map [ToFlatIndex (69, 10)] = Space.Wall;
				map [ToFlatIndex (70, 10)] = Space.Wall;
				map [ToFlatIndex (71, 10)] = Space.Wall;
				map [ToFlatIndex (10, 11)] = Space.Wall;
				map [ToFlatIndex (11, 11)] = Space.Wall;
				map [ToFlatIndex (12, 11)] = Space.Wall;
				map [ToFlatIndex (15, 11)] = Space.Wall;
				map [ToFlatIndex (16, 11)] = Space.Wall;
				map [ToFlatIndex (21, 11)] = Space.Wall;
				map [ToFlatIndex (22, 11)] = Space.Wall;
				map [ToFlatIndex (26, 11)] = Space.Wall;
				map [ToFlatIndex (27, 11)] = Space.Wall;
				map [ToFlatIndex (28, 11)] = Space.Wall;
				map [ToFlatIndex (29, 11)] = Space.Wall;
				map [ToFlatIndex (31, 11)] = Space.Wall;
				map [ToFlatIndex (32, 11)] = Space.Wall;
				map [ToFlatIndex (36, 11)] = Space.Wall;
				map [ToFlatIndex (37, 11)] = Space.Wall;
				map [ToFlatIndex (45, 11)] = Space.Wall;
				map [ToFlatIndex (46, 11)] = Space.Wall;
				map [ToFlatIndex (52, 11)] = Space.Wall;
				map [ToFlatIndex (53, 11)] = Space.Wall;
				map [ToFlatIndex (55, 11)] = Space.Wall;
				map [ToFlatIndex (56, 11)] = Space.Wall;
				map [ToFlatIndex (62, 11)] = Space.Wall;
				map [ToFlatIndex (63, 11)] = Space.Wall;
				map [ToFlatIndex (70, 11)] = Space.Wall;
				map [ToFlatIndex (71, 11)] = Space.Wall;
				map [ToFlatIndex (72, 11)] = Space.Wall;
				map [ToFlatIndex (11, 12)] = Space.Wall;
				map [ToFlatIndex (12, 12)] = Space.Wall;
				map [ToFlatIndex (15, 12)] = Space.Wall;
				map [ToFlatIndex (16, 12)] = Space.Wall;
				map [ToFlatIndex (21, 12)] = Space.Wall;
				map [ToFlatIndex (22, 12)] = Space.Wall;
				map [ToFlatIndex (25, 12)] = Space.Wall;
				map [ToFlatIndex (26, 12)] = Space.Wall;
				map [ToFlatIndex (31, 12)] = Space.Wall;
				map [ToFlatIndex (32, 12)] = Space.Wall;
				map [ToFlatIndex (36, 12)] = Space.Wall;
				map [ToFlatIndex (37, 12)] = Space.Wall;
				map [ToFlatIndex (45, 12)] = Space.Wall;
				map [ToFlatIndex (46, 12)] = Space.Wall;
				map [ToFlatIndex (52, 12)] = Space.Wall;
				map [ToFlatIndex (53, 12)] = Space.Wall;
				map [ToFlatIndex (55, 12)] = Space.Wall;
				map [ToFlatIndex (56, 12)] = Space.Wall;
				map [ToFlatIndex (62, 12)] = Space.Wall;
				map [ToFlatIndex (63, 12)] = Space.Wall;
				map [ToFlatIndex (71, 12)] = Space.Wall;
				map [ToFlatIndex (72, 12)] = Space.Wall;
				map [ToFlatIndex (11, 13)] = Space.Wall;
				map [ToFlatIndex (12, 13)] = Space.Wall;
				map [ToFlatIndex (15, 13)] = Space.Wall;
				map [ToFlatIndex (16, 13)] = Space.Wall;
				map [ToFlatIndex (21, 13)] = Space.Wall;
				map [ToFlatIndex (22, 13)] = Space.Wall;
				map [ToFlatIndex (25, 13)] = Space.Wall;
				map [ToFlatIndex (26, 13)] = Space.Wall;
				map [ToFlatIndex (31, 13)] = Space.Wall;
				map [ToFlatIndex (32, 13)] = Space.Wall;
				map [ToFlatIndex (36, 13)] = Space.Wall;
				map [ToFlatIndex (37, 13)] = Space.Wall;
				map [ToFlatIndex (45, 13)] = Space.Wall;
				map [ToFlatIndex (46, 13)] = Space.Wall;
				map [ToFlatIndex (52, 13)] = Space.Wall;
				map [ToFlatIndex (53, 13)] = Space.Wall;
				map [ToFlatIndex (56, 13)] = Space.Wall;
				map [ToFlatIndex (57, 13)] = Space.Wall;
				map [ToFlatIndex (61, 13)] = Space.Wall;
				map [ToFlatIndex (62, 13)] = Space.Wall;
				map [ToFlatIndex (71, 13)] = Space.Wall;
				map [ToFlatIndex (72, 13)] = Space.Wall;
				map [ToFlatIndex (5, 14)] = Space.Wall;
				map [ToFlatIndex (10, 14)] = Space.Wall;
				map [ToFlatIndex (11, 14)] = Space.Wall;
				map [ToFlatIndex (15, 14)] = Space.Wall;
				map [ToFlatIndex (16, 14)] = Space.Wall;
				map [ToFlatIndex (21, 14)] = Space.Wall;
				map [ToFlatIndex (22, 14)] = Space.Wall;
				map [ToFlatIndex (25, 14)] = Space.Wall;
				map [ToFlatIndex (26, 14)] = Space.Wall;
				map [ToFlatIndex (31, 14)] = Space.Wall;
				map [ToFlatIndex (32, 14)] = Space.Wall;
				map [ToFlatIndex (36, 14)] = Space.Wall;
				map [ToFlatIndex (37, 14)] = Space.Wall;
				map [ToFlatIndex (45, 14)] = Space.Wall;
				map [ToFlatIndex (46, 14)] = Space.Wall;
				map [ToFlatIndex (51, 14)] = Space.Wall;
				map [ToFlatIndex (52, 14)] = Space.Wall;
				map [ToFlatIndex (56, 14)] = Space.Wall;
				map [ToFlatIndex (57, 14)] = Space.Wall;
				map [ToFlatIndex (61, 14)] = Space.Wall;
				map [ToFlatIndex (62, 14)] = Space.Wall;
				map [ToFlatIndex (65, 14)] = Space.Wall;
				map [ToFlatIndex (70, 14)] = Space.Wall;
				map [ToFlatIndex (71, 14)] = Space.Wall;
				map [ToFlatIndex (6, 15)] = Space.Wall;
				map [ToFlatIndex (7, 15)] = Space.Wall;
				map [ToFlatIndex (8, 15)] = Space.Wall;
				map [ToFlatIndex (9, 15)] = Space.Wall;
				map [ToFlatIndex (10, 15)] = Space.Wall;
				map [ToFlatIndex (15, 15)] = Space.Wall;
				map [ToFlatIndex (16, 15)] = Space.Wall;
				map [ToFlatIndex (21, 15)] = Space.Wall;
				map [ToFlatIndex (22, 15)] = Space.Wall;
				map [ToFlatIndex (26, 15)] = Space.Wall;
				map [ToFlatIndex (27, 15)] = Space.Wall;
				map [ToFlatIndex (28, 15)] = Space.Wall;
				map [ToFlatIndex (29, 15)] = Space.Wall;
				map [ToFlatIndex (31, 15)] = Space.Wall;
				map [ToFlatIndex (32, 15)] = Space.Wall;
				map [ToFlatIndex (36, 15)] = Space.Wall;
				map [ToFlatIndex (37, 15)] = Space.Wall;
				map [ToFlatIndex (45, 15)] = Space.Wall;
				map [ToFlatIndex (46, 15)] = Space.Wall;
				map [ToFlatIndex (48, 15)] = Space.Wall;
				map [ToFlatIndex (49, 15)] = Space.Wall;
				map [ToFlatIndex (50, 15)] = Space.Wall;
				map [ToFlatIndex (51, 15)] = Space.Wall;
				map [ToFlatIndex (57, 15)] = Space.Wall;
				map [ToFlatIndex (59, 15)] = Space.Wall;
				map [ToFlatIndex (60, 15)] = Space.Wall;
				map [ToFlatIndex (66, 15)] = Space.Wall;
				map [ToFlatIndex (67, 15)] = Space.Wall;
				map [ToFlatIndex (68, 15)] = Space.Wall;
				map [ToFlatIndex (69, 15)] = Space.Wall;
				map [ToFlatIndex (70, 15)] = Space.Wall;
				map [ToFlatIndex (45, 16)] = Space.Wall;
				map [ToFlatIndex (46, 16)] = Space.Wall;
				map [ToFlatIndex (45, 17)] = Space.Wall;
				map [ToFlatIndex (46, 17)] = Space.Wall;
				map [ToFlatIndex (45, 18)] = Space.Wall;
				map [ToFlatIndex (46, 18)] = Space.Wall;
				map [ToFlatIndex (45, 19)] = Space.Wall;
				map [ToFlatIndex (46, 19)] = Space.Wall;
				goal = 10;
				speed = mapSpeed;
			}

			SetupSnake (level);
		}

		public static void SetupSnake (int level)
		{
			if (level == 1)
				SetupSnake (4, Direction.Down);
			else if (level == 2)
				SetupSnake (mapWidth / 4, mapHeight / 2, 10, Direction.Down);
			else if (level == 3)
				SetupSnake (6, mapHeight / 2, 10, Direction.Down);
			else if (level == 4)
				SetupSnake (10, Direction.Down);
			else if (level == 5)
				SetupSnake (2, mapHeight / 4, 2, Direction.Down);
		}

		public static void SetupSnake (int length, Direction direction)
		{
			//Snake starts in the middle
			SetupSnake (mapWidth / 2, mapHeight / 2, length, direction);
		}

		public static void SetupSnake (int x, int y, int length, Direction direction)
		{
			snakeLength = length;
			snake = (SnakeBody*) MemoryManager.Allocate ((uint) (sizeof (SnakeBody) * snakeLength));
			for (int i = 0; i < snakeLength; i++) {
				SnakeBody body;

				body.x = x;
				body.y = y;

				if (direction == Direction.Up)
					body.y += i;
				else if (direction == Direction.Down)
					body.y -= i;
				else if (direction == Direction.Left)
					body.x += i;
				else if (direction == Direction.Right)
					body.x -= i;

				int index = ToFlatIndex (body.x, body.y);
				if (i == 0)
					map [index] = Space.SnakeHead;
				else
					map [index] = Space.SnakeBody;

				snake [i] = body;
			}

			nextDirection = direction;
		}

		private static void InitializeMap ()
		{
			int width;
			int height;

			TextMode.GetScreenSize (out width, out height);

			//Account for the tick counter and the points header
			mapHeight = height - 3;
			mapWidth = width;

			map = (Space*) MemoryManager.Allocate (((uint) (sizeof (Space) * mapHeight * mapWidth)));
		}

		public static int ToFlatIndex (int x, int y)
		{
			return y * mapWidth + x;
		}

		public static int ToModulusLevel (int level)
		{
			level = level % (MAX_LEVELS + 1);
			if (level != Snake.level)
				level++;

			return level;
		}

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			TextMode.ClearScreen ();
			WriteInfo ();
			TextMode.WriteLine ("Use the arrow keys to move around and eat the apples. Hit escape to quit and backspace to pause.");
			TextMode.WriteLine ("Press any key to continue...");

			playing = false;
			waiting = true;
			randomizeApple = true;
			apple = (SnakeBody*) MemoryManager.Allocate ((uint) sizeof (SnakeBody));
			toAdd = 0;
			points = 0;
			level = 1;
			lives = DEFAULT_LIVES;

			//Whee direct kernel integration :P
			SharpOS.Kernel.ADC.Keyboard.RegisterKeyDownEvent (
				Stubs.GetFunctionPointer (SNAKE_KEYDOWN_HANDLER));
			SharpOS.Kernel.ADC.Timer.RegisterTimerEvent (
				Stubs.GetFunctionPointer (SNAKE_TIMER_HANDLER));

			InitializeMap ();

			SetupMap ();
		}

		[Label (lblGetHelp)]
		public static void GetHelp (CommandExecutionContext* context)
		{
			TextMode.WriteLine ("Syntax: ");
			TextMode.WriteLine ("     snake");
			TextMode.WriteLine ();
			TextMode.WriteLine ("Plays a quick game of Snake#");
		}

		public static CommandTableEntry* CREATE ()
		{
			CommandTableEntry* entry = (CommandTableEntry*) MemoryManager.Allocate (
				(uint) sizeof (CommandTableEntry));

			entry->name = (CString8*) SharpOS.Kernel.Stubs.CString (name);
			entry->shortDescription = (CString8*) SharpOS.Kernel.Stubs.CString (shortDescription);
			entry->func_Execute = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblExecute);
			entry->func_GetHelp = (void*) SharpOS.Kernel.Stubs.GetLabelAddress (lblGetHelp);

			return entry;
		}

		public enum Space {
			Empty,
			Wall,
			Apple,
			SnakeBody,
			SnakeHead
		}

		public enum Direction {
			Up,
			Down,
			Left,
			Right
		}

		[StructLayout (LayoutKind.Sequential)]
		public unsafe struct SnakeBody {
			public int x;
			public int y;

			public void DISPOSE ()
			{
			}
		}

		// Taken from Mono and adapted for these limited conditions
		// Magic numbers galore o_O
		[StructLayout (LayoutKind.Sequential)]
		public struct Random {
			const int MBIG = int.MaxValue;
			const int MSEED = 161803398;
			const int MZ = 0;

			int inext, inextp;
			int* SeedArray;

			public void CREATE (int seed)
			{
				//SeedArray = new int[56];
				SeedArray = (int*) MemoryManager.Allocate (sizeof (int) * 56);

				int ii;
				int mj, mk;

				// Numerical Recipes in C online @
				// http://www.library.cornell.edu/nr/bookcpdf/c7-1.pdf
				mj = MSEED - seed;
				SeedArray [55] = mj;
				mk = 1;
				for (int i = 1; i < 55; i++) {
					//  [1, 55] is special (Knuth)
					ii = (21 * i) % 55;
					SeedArray [ii] = mk;
					mk = mj - mk;
					if (mk < 0)
						mk += MBIG;
					mj = SeedArray [ii];
				}

				for (int k = 1; k < 5; k++) {
					for (int i = 1; i < 56; i++) {
						SeedArray [i] -= SeedArray [1 + (i + 30) % 55];
						if (SeedArray [i] < 0)
							SeedArray [i] += MBIG;
					}
				}
				inext = 0;
				inextp = 31;
			}

			// Normally returns from 0 to 1; due to an apparent lack of
			// floating-point math, let's make it 0 to 100
			int Sample ()
			{
				int retVal;

				if (++inext >= 56)
					inext = 1;
				if (++inextp >= 56)
					inextp = 1;

				retVal = SeedArray [inext] - SeedArray [inextp];

				if (retVal < 0)
					retVal += MBIG;

				SeedArray [inext] = retVal;

				//return retVal * (1.0 / MBIG);
				return retVal / (MBIG / 100);
			}

			// Won't work because it'll overflow and long math doesn't work either
			/*
			public int Next()
			{
				return (int)(Sample() * int.MaxValue / 100);
			}
			*/

			public int Next (int maxValue)
			{
				return (int) (Sample () * maxValue / 100);
			}

			public int Next (int minValue, int maxValue)
			{
				int diff = (maxValue - minValue);
				if (diff == 0)
					return minValue;

				int result = (int) (Sample () * diff / 100 + minValue);
				return ((result != maxValue) ? result : (result - 1));
			}

			public void DISPOSE ()
			{
				MemoryManager.Free ((void*) SeedArray);
			}
		}
	}
}