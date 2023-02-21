using System;

namespace ListSort
{
    class Profile
    {
        public string Name { get; set; }
        public int Height { get; set; }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Profile[] arrProfile = {
                //new Profile() {Name = "이문세", Height = 178}, // ()는 생략가능
                // 개체 이니셜라이저를 이용한 초기화
                new Profile {Name = "정우성", Height = 186}, 
                new Profile {Name = "김태희", Height = 158},
                new Profile {Name = "고현정", Height = 172},
                new Profile {Name = "이문세", Height = 178},
                new Profile {Name = "하동훈", Height = 171}
            };


            #region 1. Linq를 이용하지 않는 Sort
            // arrProfile 안에 있는 각 데이터로부터 Height가 175 미만인 객체만 골라 profiles에 넣은 후            
            List<Profile> profiles = new List<Profile>();
            foreach (Profile profile in arrProfile)
            {
                if (profile.Height < 175)
                    profiles.Add(profile);
            }

            // 키의 오름차순으로 정렬
            // List<T>.Sort(Comparison<T>) 
            profiles.Sort(
                (profile1, profile2) =>
                {
                    return profile1.Height - profile2.Height;
                });

            foreach (var profile in profiles)
                Console.WriteLine($"{profile.Name}, {profile.Height}");
            #endregion




            #region 2. Linq를 이용한 Sort
            
            var linqProfiles = from profile in arrProfile
                               where profile.Height < 175
                               orderby profile.Height
                               select profile;

            foreach (var profile in linqProfiles)
                Console.WriteLine($"{profile.Name}, {profile.Height}, Linq");

            #endregion

        }


    }
}