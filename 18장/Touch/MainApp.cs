using System;
using System.IO;

// 파일과 디렉터리가 없으면 생성, 있으면 최종수정시간만 갱신
namespace Touch
{
	class MainApp
	{
		static void OnWrongPathType(string type)
		{
			Console.WriteLine($"{type} is wrong type");
			return;
        }

		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Usage : Touch.exe <Path> [Type:File/Directory]");
				return;
			}

			string path = args[0];
			string type = "File";
			if (args.Length> 1)
				type = args[1];

			if (File.Exists(path) || Directory.Exists(path))
			{
				if (type == "File")
					File.SetLastWriteTime(path, DateTime.Now);
				else if (type == "Directory")
					Directory.SetLastWriteTime(path, DateTime.Now);
				else {
					OnWrongPathType(type);
					return;
				}
				Console.WriteLine($"Updated {path} {type}");
			}
			else
			{
				if (type == "File")
					File.Create(path).Close();
				else if (type == "Directory")
					Directory.CreateDirectory(path);
				else
				{
					OnWrongPathType(type);
					return;
				}
				Console.WriteLine($"Created {path} {type}");
			}
			
		}
	}
}