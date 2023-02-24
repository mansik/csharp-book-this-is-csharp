using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


// list<NameCard> 형식의 객체 직렬화
namespace SerializingCollection
{
    [Serializable]
    class NameCard
    {
        public string Name;
        public string Phone;
        public int Age;

        public NameCard(string name, string phone, int age)
        {
            this.Name = name;
            this.Phone = phone;
            this.Age = age;
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {

            
            using (var ws = new FileStream("a.dat", FileMode.Create))
            {
                var serializer = new BinaryFormatter();

                List<NameCard> list = new List<NameCard>();
                list.Add(new NameCard("홍길동", "001-0000-1111", 33));
                list.Add(new NameCard("이순신", "000-1122-3333", 44));
                list.Add(new NameCard("강감찬", "111-9999-9900", 55));

                serializer.Serialize(ws, list);
            }

            using (var rs = new FileStream("a.dat", FileMode.Open))
            {
                var deserializer = new BinaryFormatter();

                var list2 = new List<NameCard>();
                list2 = deserializer.Deserialize(rs) as List<NameCard>;

                if (list2 == null)
                    return;

                foreach (NameCard nc in list2)
                {
                    Console.WriteLine($"Name; {nc.Name}, Phone: {nc.Phone}, Age: {nc.Age}");
                }
            }
        }
    }
}