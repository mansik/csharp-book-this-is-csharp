using System;

namespace WithExp
{

    // 송금 record
    // record 사용 이유 : 참조 형식이지만 값 형식처럼 깊은 복사 가능하다, 데이터 비교 메서드를 구현할 필요 없다.
    // 이 프로젝트는 그 중 깊은 복사의 예제이다.
    record RTransaction
    {
        // 초기화 전용(init-only) 프로퍼티
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
            RTransaction tr1 = new RTransaction() { From = "Alice", To = "Bob", Amount = 100 };
            // tr1을 깊은 복사
            RTransaction tr2 = tr1 with { To = "Charlie" };
            RTransaction tr3 = tr2 with { From = "Dave", Amount = 30 };

            // 깊은 복사로 인해서 tr1의 To = "Bob"그대로 유지
            Console.WriteLine(tr1); 
            Console.WriteLine(tr2);
            Console.WriteLine(tr3);
        }
    }

}
