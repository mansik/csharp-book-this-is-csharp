﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
	[Serializable]
	class NameCard
	{
		public string Name;
		public string Phone;
		public int Age;
	}

	class MainApp
	{
		static void Main(string[] args)
		{
			using (Stream ws = new FileStream("a.dat", FileMode.Create))
			{
				BinaryFormatter serializer = new BinaryFormatter();

				NameCard nc = new NameCard()
				{
					Name = "홍길동",
					Phone = "000-1111-0000",
					Age = 33
				};

				serializer.Serialize(ws, nc);
			}

			using Stream rs = new FileStream("a.dat", FileMode.Open);
			BinaryFormatter deserializer = new BinaryFormatter();

			NameCard nc2;
			nc2 = (NameCard)deserializer.Deserialize(rs);

			Console.WriteLine($"Name : {nc2.Name}");
			Console.WriteLine($"Phone : {nc2.Phone}");
			Console.WriteLine($"Age : {nc2.Age}");

		}
	}
}