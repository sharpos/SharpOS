/*
	*  (C) 2007 Johann MacDonagh <johann [at] macdonaghs [dot] com>
	* 
	*  Licensed under the terms of the GNU GPL License version 2.
	*
*/

.assembly extern mscorlib
{
}

.namespace SharpOS.TestKernel
{
	.class public auto ansi abstract sealed beforefieldinit TestRunner extends [mscorlib]System.Object
	{
		.method public static void Run() cil managed
		{
			.maxstack 3