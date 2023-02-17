using System;

namespace StatementLambda
{
    class MainApp
    {
        delegate string Concatenate(string[] args);

        static void Main(string[] args)
        {
            // statement lambda(문 형식의 람다 식) : (매개변수_목록) => { 문장; 문장; ...}
            //
            // lambda식으로 만드는 익명 메서드(anonymous method)를 무명 함수(anonymous function)라고 한다.
            // lambda식은 deletegate가 반드시 필요
            // 매개변수_목록은 형식 유추(Type Inference) 사용 (매개변수의 형식을 생략하여 작성하고, 컴파일러가 delegate의 매개변수 형식을 이용하여 유추함.)
            Concatenate concat = (arr) =>
            {
                string result = "";
                foreach (string arg in arr)
                {
                    result += arg;
                }
                return result;
            };

            // StatementLambda 아버지가 방에 들어가신다.
            // 인자값이 공백으로 구분되어 배열로 할당된다.
            // args[3] ->  아버지가, 방에, 들어가신다.
            Console.WriteLine(concat(args));
        }
    }
}