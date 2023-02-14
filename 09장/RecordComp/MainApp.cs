using System;

namespace RecordComp
{
    class CTransaction
    {
        public string From { get; set; }
        public string To { get; set; }
        public int Amount { get; set; }

        public override string ToString()
        {
            return $"{From,-10} -> {To,-10} : ${Amount}";
        }

        // 클래스의 Equals 구현
        //public override bool Equals(Object? obj)
        //{
        //    CTransaction? target = obj as CTransaction;

        //    if (target != null)
        //    {
        //        if (this.From == target.From && this.To == target.To && this.Amount == target.Amount)
        //            return true;
        //    }
        //    return false;
        //}
    }

    // record 객체 사용 이유 중 2번째인 객체 비교시 Equals를 구현하지 않아도 컴파일러가 Equals() 메소드를 구현하여 결과는 True가 된다.
    record RTransaction
    {
        public string From { get; init; }
        public string To { get; init; }
        public int Amount { get; init; }

        public override string ToString()
        {
            return $"{From,-10} -> {To,-10} : ${Amount}";
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            CTransaction trA = new CTransaction { From = "Alice", To = "Bob", Amount = 100 };
            CTransaction trB = new CTransaction { From = "Alice", To = "Bob", Amount = 100 };

            Console.WriteLine(trA);
            Console.WriteLine(trB);
            // 클래스의 Equals()는 참조를 비교하므로 결과가 False
            Console.WriteLine($"trA equals to trB : {trA.Equals(trB)}"); 

            RTransaction tr1 = new RTransaction { From = "Alice", To = "Bob", Amount = 100 };
            RTransaction tr2 = new RTransaction { From = "Alice", To = "Bob", Amount = 100 };

            Console.WriteLine(tr1);
            Console.WriteLine(tr2);
            // record의 Equals()는 값을 비교하므로 결과가 True
            Console.WriteLine($"tr1 equals to tr2 : {tr1.Equals(tr2)}"); 

        }
    }

}