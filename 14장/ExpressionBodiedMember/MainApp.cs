using System;
using System.Collections.Generic;

// 식으로 구현된 멤버 = Expression-Bodied Member 
// 문법 : 멤버 => 식;
// 메소드, 생성자, 종료자, 속성, 인덱서는  Expression-Bodied Member를 구현할 수 있다.
namespace ExpressionBodiedMember
{
    class FriendList
    {
        private List<string> list = new List<string>();

        
        // 메소드를 Expression-Bodied Member로 구현
        public void Add(string name) => list.Add(name);
        public void Remove(string name) => list.Remove(name);

        // 일반 메소드
        public void PrintAll()
        { 
            foreach(var s in list)
            {
                Console.WriteLine(s);
            }
        }

        // 생성자를 Expression-Bodied Member로 구현
        public FriendList() => Console.WriteLine("FriendList()");

        // 종료자를 Expression-Bodied Member로 구현
        ~FriendList() => Console.WriteLine("~FriendList()");

        // 속성(property)를 Expression-Bodied Member로 구현
        // 읽기 전용
        // public int Capacity => list.Capacity;

        public int Capacity
        { 
            get => list.Capacity;
            set => list.Capacity = value;
        }

        // 인덱서(indexer)를 Expression-Bodied Member로 구현
        // 읽기 전용
        // public string this[int index] => list[index]; 
        public string this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            FriendList obj = new FriendList();            
            obj.Add("Envy");            
            obj.Add("Meeny");
            obj.Add("Miny");
            obj.Remove("Envy");
            obj.PrintAll();

            Console.WriteLine($"{obj.Capacity}");
            obj.Capacity = 10;
            Console.WriteLine($"{obj.Capacity}");

            Console.WriteLine($"{obj[0]}");
            obj[0] = "Moe";
            obj.PrintAll();
        }
    }
}