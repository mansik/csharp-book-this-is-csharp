﻿using System;
using System.Threading;

namespace BasicThread
{
	class MainApp
	{
		static void DoSomething()
		{
			for (int i= 0; i < 5; i++)
			{
				Console.WriteLine($"DoSomething : {i}");
				Thread.Sleep(1000);
			}
		}
		static void Main(string[] args)
		{
			Thread t1 = new Thread(new ThreadStart(DoSomething));

			Console.WriteLine("Starting thread...");
			t1.Start();

			for ( int i = 0; i< 5; i++)
			{
				Console.WriteLine($"Main : {i}");
				Thread.Sleep(1000);
			}

			Console.WriteLine("Waiting until thread stop...");
			t1.Join();

			Console.WriteLine("Finished");

		}
	}
}