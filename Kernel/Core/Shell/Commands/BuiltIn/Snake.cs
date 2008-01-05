// Authors:
//	Aaron Lindsay <Aaron@rivalsource.net>

// This was just a test to get my feet wet with the kernel and did help me quite a lot.
// I decided to finish it up to a playable state to have something to show
// Notes...
// 1) This game will move at different speeds at different clock speeds until I find a way to make it consistent :/
//    (suggestions welcome, I'm new to all this)
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
		private static SnakeBody* apple;
		private static Random* random;
		private static int snakeLength;
		private static int toAdd;
		private static bool randomizeApple;

		private const int DEFAULT_SPEED = 6;
		private const int ADD_PER_APPLE = 4;
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

		[Label(SNAKE_TIMER_HANDLER)]
		public static void Timer(uint ticks)
		{
			if (!playing)
				return;

			else if ((int)ticks % speed == 0) {
				Update(ticks);

				if (playing)
					RenderScreen(ticks);
			}
		}

		[Label(SNAKE_KEYDOWN_HANDLER)]
		public static unsafe void KeyDown (uint scancode)
		{
			if (waiting || playing)
				keyCount++;

			if (waiting) {
				playing = true;
				waiting = false;

				TextMode.ClearScreen();

				return;
			}

			//No unregister key handler?
			if (!playing)
				return;

			Keys key = (Keys)scancode;

			// Escape
			if (scancode == 1) {

				QuitGame();
			} else if (key == Keys.LeftArrow) {
				if (!IsDirectionVertical())
					return;

				direction = Direction.Left;
			} else if (key == Keys.RightArrow) {
				if (!IsDirectionVertical())
					return;

				direction = Direction.Right;
			} else if (key == Keys.UpArrow) {
				if (IsDirectionVertical())
					return;

				direction = Direction.Up;
			} else if (key == Keys.DownArrow) {
				if (IsDirectionVertical())
					return;

				direction = Direction.Down;
			}
		}

		public static bool IsDirectionVertical ()
		{
			return direction == Direction.Up || direction == Direction.Down;
		}

		public unsafe static void Update (uint ticks)
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

			Space nextSpace = map[ToFlatIndex(next.x, next.y)];
			if (nextSpace == Space.Apple) {
				//Yay point
				points++;

				randomizeApple = true;

				toAdd += ADD_PER_APPLE;
			} else if (nextSpace != Space.Empty) {
				//Game Over!
				QuitGame();
				return;
			}

			//Move
			SnakeBody oldTail = snake[snakeLength - 1];
			for (int i = snakeLength - 1; i >= 0; i--) {
				snake[i] = snake[i - 1];
			}

			snake [0] = next;
			//Draw changes to map (erase last position; draw head at new position; replace old head with body)
			map [ToFlatIndex (oldTail.x, oldTail.y)] = Space.Empty;
			map [ToFlatIndex (next.x, next.y)] = Space.SnakeHead;
			map [ToFlatIndex (snake [1].x, snake [1].y)] = Space.SnakeBody;

			//Elongate the snake
			if (toAdd > 0) {
				snakeLength++;
				SnakeBody* newSnake = (SnakeBody*)MemoryManager.Allocate((
					uint)(sizeof(SnakeBody) * snakeLength));

				for (int i = 0; i < snakeLength; i++) {
					if (i == snakeLength - 1)
						newSnake [i] = oldTail;
					else
						newSnake [i] = snake[i];
				}

				MemoryManager.Free ((void*)snake);
				snake = newSnake;

				toAdd--;
			}
		}

		public static void RandomizeApple (uint ticks)
		{
			if ((uint) random == 0) {
				random = (Random*)MemoryManager.Allocate ((uint)sizeof(Random));
				random->CREATE ((int)ticks);
			}

			int position;

			do {
				position = random->Next(0, ToFlatIndex(mapWidth - 1, mapHeight - 1));
			} while (map [position] != Space.Empty);

			map [position] = Space.Apple;

			apple->x = position % mapWidth;
			apple->y = (position - apple->x) / mapWidth;

			randomizeApple = false;
		}

		public unsafe static void RenderScreen(uint ticks)
		{
			TextMode.MoveTo (0, 0);
			TextMode.ClearToEndOfLine ();

			TextMode.Write ("Snake# v0.1 - ");
			TextMode.Write (points);
			TextMode.WriteLine (" points");

			TextMode.SaveAttributes ();

			for (int y = 0; y < mapHeight; y++) {
				for (int x = 0; x < mapWidth; x++) {
					int index = ToFlatIndex(x, y);
					Space space = map[index];

					//I read that switches are borked?
					if (space == Space.Empty) {
						TextMode.SetAttributes(EMPTY_COLOUR_FG, EMPTY_COLOUR_BG);
						TextMode.Write(EMPTY_SPACE);
					} else if (space == Space.Wall) {
						TextMode.SetAttributes(WALL_COLOUR_FG, WALL_COLOUR_BG);
						TextMode.Write(WALL_SPACE);
					} else if (space == Space.Apple) {
						TextMode.SetAttributes(APPLE_COLOUR_FG, APPLE_COLOUR_BG);
						TextMode.Write(APPLE_SPACE);
					} else if (space == Space.SnakeBody) {
						TextMode.SetAttributes(BODY_COLOUR_FG, BODY_COLOUR_BG);
						TextMode.Write(BODY_SPACE);
					} else if (space == Space.SnakeHead) {
						TextMode.SetAttributes(HEAD_COLOUR_FG, HEAD_COLOUR_BG);
						TextMode.Write(HEAD_SPACE);
					} else {
						TextMode.SetAttributes(EMPTY_COLOUR_FG, EMPTY_COLOUR_BG);
						TextMode.Write(EMPTY_SPACE);
					}
				}

				//Only do so if mapWidth < TextMode.GetScreenSize()'s width:
				//TextMode.WriteLine();
			}

			TextMode.RestoreAttributes ();

			TextMode.ClearToEndOfLine ();
		}

		public static void QuitGame ()
		{
			MemoryManager.Free ((void*)map);
			MemoryManager.Free ((void*)snake);
			MemoryManager.Free ((void*)apple);

			if ((uint)random != 0) {
				random->DISPOSE();
				MemoryManager.Free((void*)random);
				random = (Random*)0;
			}

			playing = false;

			TextMode.MoveTo (keyCount, mapHeight);

			// Erase anything the keypressing might have done (lotta 'f's if you don't)...
			for (int i = 0; i < keyCount; i++) {
				// Calling Console.KeyDown() directly makes the AOT compiler
				// error out, so I trick it :P
				SharpOS.Kernel.ADC.Memory.Call ((void*)Stubs.GetFunctionPointer (
					Console.CONSOLE_KEY_DOWN_HANDLER), (void*)(uint)Keys.Backspace);
			}

			keyCount = 0;

			TextMode.MoveTo (0, mapHeight);

			TextMode.ClearToEndOfLine();
			WriteInfo();
			TextMode.ClearToEndOfLine();
			TextMode.Write("Game Over - Your score was ");
			TextMode.Write(points);
			TextMode.WriteLine(" points.");

			TextMode.WriteLine();

			//Show the prompt. This and the backspacing above is just a hack to make it integrate more cleanly.
			Prompter.WritePrompt();
		}

		public static void WriteInfo()
		{
			TextMode.WriteLine("Snake# v0.1");
			TextMode.WriteLine("(C)opyright 2008 Aaron \"AerialX\" Lindsay");
		}

		public unsafe static void SetupMap()
		{
			int width;
			int height;

			TextMode.GetScreenSize (&width, &height);

			//Account for the tick counter and the points header
			mapHeight = height - 2 - 1;
			mapWidth = width;

			map = (Space*)MemoryManager.Allocate (((uint)(sizeof(Space) * mapHeight * mapWidth)));

			// For now, we'll start with a basic level with walls around the outside
			for (int y = 0; y < mapHeight; y++) {
				for (int x = 0; x < mapWidth; x++) {
					int index = ToFlatIndex (x, y);

					if (y == 0 || x == 0 || y == mapHeight - 1 || x == mapWidth - 1)
						map[index] = Space.Wall;
					else
						map[index] = Space.Empty;
				}
			}

			//Snake starts in the middle
			snakeLength = 4;
			snake = (SnakeBody*)MemoryManager.Allocate ((uint)(sizeof(SnakeBody) * snakeLength));
			for (int i = 0; i < snakeLength; i++) {
				SnakeBody body;

				body.x = mapWidth / 2;
				body.y = mapHeight / 2 - i;

				int index = ToFlatIndex (body.x, body.y);
				if (i == 0)
					map [index] = Space.SnakeHead;
				else
					map [index] = Space.SnakeBody;

				snake [i] = body;
			}

			direction = Direction.Down;
		}

		public static int ToFlatIndex (int x, int y)
		{
			return y * mapWidth + x;
		}

		[Label (lblExecute)]
		public static void Execute (CommandExecutionContext* context)
		{
			TextMode.ClearScreen ();
			WriteInfo ();
			TextMode.WriteLine ("Use the arrow keys to move around, and hit escape to quit.");
			TextMode.WriteLine ("Press any key to continue...");

			speed = DEFAULT_SPEED;
			playing = false;
			waiting = true;
			randomizeApple = true;
			apple = (SnakeBody*)MemoryManager.Allocate ((uint)sizeof(SnakeBody));
			toAdd = 0;
			points = 0;

			//Whee direct kernel integration :P
			SharpOS.Kernel.ADC.Keyboard.RegisterKeyDownEvent (
				Stubs.GetFunctionPointer (SNAKE_KEYDOWN_HANDLER));
			SharpOS.Kernel.ADC.Timer.RegisterTimerEvent (
				Stubs.GetFunctionPointer (SNAKE_TIMER_HANDLER));

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
				(uint)sizeof(CommandTableEntry));

			entry->name = (CString8*)SharpOS.Kernel.Stubs.CString(name);
			entry->shortDescription = (CString8*)SharpOS.Kernel.Stubs.CString(shortDescription);
			entry->func_Execute = (void*)SharpOS.Kernel.Stubs.GetLabelAddress(lblExecute);
			entry->func_GetHelp = (void*)SharpOS.Kernel.Stubs.GetLabelAddress(lblGetHelp);

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

		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct SnakeBody {
			public int x;
			public int y;

			public void DISPOSE()
			{
			}
		}

		// Taken from Mono and adapted for these limited conditions
		// Magic numbers galore o_O
		[StructLayout(LayoutKind.Sequential)]
		public struct Random {
			const int MBIG = int.MaxValue;
			const int MSEED = 161803398;
			const int MZ = 0;

			int inext, inextp;
			int* SeedArray;

			public void CREATE(int seed) {
				//SeedArray = new int[56];
				SeedArray = (int*)MemoryManager.Allocate (sizeof(int) * 56);

				int ii;
				int mj, mk;

				// Numerical Recipes in C online @
				// http://www.library.cornell.edu/nr/bookcpdf/c7-1.pdf
				mj = MSEED - seed;
				SeedArray[55] = mj;
				mk = 1;
				for (int i = 1; i < 55; i++) {
					//  [1, 55] is special (Knuth)
					ii = (21 * i) % 55;
					SeedArray[ii] = mk;
					mk = mj - mk;
					if (mk < 0)
						mk += MBIG;
					mj = SeedArray[ii];
				}

				for (int k = 1; k < 5; k++) {
					for (int i = 1; i < 56; i++) {
						SeedArray[i] -= SeedArray[1 + (i + 30) % 55];
						if (SeedArray[i] < 0)
							SeedArray[i] += MBIG;
					}
				}
				inext = 0;
				inextp = 31;
			}

			// Normally returns from 0 to 1; due to an apparent lack of
			// floating-point math, let's make it 0 to 100
			int Sample()
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
				return (int)(Sample () * maxValue / 100);
			}

			public int Next (int minValue, int maxValue)
			{
				int diff = (maxValue - minValue);
				if (diff == 0)
					return minValue;

				int result = (int)(Sample () * diff / 100 + minValue);
				return ((result != maxValue) ? result : (result - 1));
			}

			public void DISPOSE ()
			{
				MemoryManager.Free((void*)SeedArray);
			}
		}
	}
}