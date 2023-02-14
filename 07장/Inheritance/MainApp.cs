using System;

namespace Inheritance
{
    class Base
    {
        protected string Name;

        public Base(string name)
        {
            this.Name = name;
            Console.WriteLine($"{this.Name}.Base()");
        }
        
        ~Base()
        {
            Console.WriteLine($"{this.Name}.~Base()");
        }

        public void BaseMethod()
        {
            Console.WriteLine($"{Name}.BaseMethod()");
        }
    }

    class Drived : Base
    {
        // Drived 클래스에는 Name 필드가 없기 때문에 this.Name 및 Name는 Base 클래스의 Name 필드를 의미 한다.
        // Drived Class(파생클래스)는 Base Class(기반 클래스)로 부터 물려받은(public, protected, internal, protected internal, private protected) 멤버를 가지고 있다.
        public Drived(string name) : base(name)
        {
            Console.WriteLine($"{this.Name}.Drived()");
        }

        ~Drived()
        {
            Console.WriteLine($"{this.Name}.~Drived()");
        }

        public void DrivedMethod()
        {
            Console.WriteLine($"{Name}.DrivedMethod()");
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Base a = new Base("a");
            a.BaseMethod();

            Drived b = new Drived("b");
            b.BaseMethod();
            b.DrivedMethod();

        }
    }

}

