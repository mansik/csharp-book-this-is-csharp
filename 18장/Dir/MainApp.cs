using System;
using System.Linq;
using System.IO;

// 인수를 입력하지 않으면 현재 디렉터리, 인수를 입력하면 입력한 디렉터리 경로에 대해
// 하위 디렉터리 및 파일의 목록을 조회하고, 디렉터리 명과 속성, 파일명과 크기, 속성을 출력
namespace Dir
{
	class MainApp
	{
		static void Main(string[] args)
		{
			string directory;
			if (args.Length == 0)
				directory = ".";
			else
				directory = args[0];

			Console.WriteLine($"{directory} directory Info");
			Console.WriteLine("- Directory :");
			var directories = (from dir in Directory.GetDirectories(directory) // 하위 디렉터리 목록 조회
							   let info = new DirectoryInfo(dir)  // let은 LINQ안에서 변수 선언용. var과 같은 기능
							   select new
							   {
								   Name = info.Name,
								   Attributes = info.Attributes
							   }).ToList(); // ToList() : IEnumerable<T>(반복만) -> List<T>(반복, 수정, 정렬 가능) 로 변환, ICollection<T> (반복, 수정 가능)
			
			foreach ( var d in directories)
				Console.WriteLine($"{d.Name} : {d.Attributes}");

			Console.WriteLine("- Files :");
			var files = (from file in Directory.GetFiles(directory) // 하위 파일 목록 조회
						 let info = new FileInfo(file)
						 select new
						 {
							 Name = info.Name,
							 FileSize = info.Length,
							 Attributes = info.Attributes
						 }); //  IEnumerable<T>(반복만)
            foreach (var f in files)
				Console.WriteLine($"{f.Name} : {f.FileSize}, {f.Attributes}");
		}
	}
}