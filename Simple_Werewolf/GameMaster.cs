using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class GameMaster
    {
        private List<Person> Players;
        private List<string> MemberName;
        private int[] CastCount;


        public GameMaster()
        {
            Players = new List<Person>();
            MemberName = new List<string>();
            CastCount = new int[6];
        }
        
        /// <summary>
        /// 参加者の名前を入力させる
        /// </summary>
        public void JoinMember()
        {
            Console.WriteLine("参加者の名前を入力してください。");
            bool loop = true;
            do
            {
                Console.Write("> ");
                string member = Console.ReadLine();
                if(member == "")
                {
                    loop = false;
                }
                else
                {
                    MemberName.Add(member);
                }

            } while (loop);
        }

        /// <summary>
        /// 参加者の役職を決める。
        /// </summary>
        public void DecisionCast()
        {
            int MemberCount = MemberName.Count();
            //MemberCount cast = new MemberCount();
            bool CastCheck = false;

            string[] castlist = new string[] { "村人", "人狼", "占い師", "霊能力者", "狩人", "狂人" };

            while (!CastCheck)
            {
                int remaining = MemberName.Count;

                Console.WriteLine();

                foreach (var s in castlist.Select((v, i) => new { v, i }))
                {
                    bool check = false;
                    int n = 0;

                    if (remaining > 0)
                    {
                        Console.WriteLine("{0}の人数を入力してください。(残り人数:{1}人)", s.v, remaining);
                    }
                    else
                    {
                        check = true;
                    }

                    while (!check)
                    {
                        Console.Write("> ");
                        string input = Console.ReadLine();
                        try
                        {
                            n = int.Parse(input);
                            remaining -= n;
                            if (remaining < 0)
                            {
                                Console.WriteLine("参加人数よりも多い配役になります。");
                                remaining += n;
                            }
                            else
                            {
                                check = true;
                            }

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("数字を入力してください。");
                        }

                    }
                    CastCount[s.i] = n;

                }

                int WolfCount = CastCount[1] + CastCount[5];
                int VillagerCount = CastCount.Sum() - WolfCount;
                if(CastCount.Sum() == MemberName.Count)
                {
                    if (VillagerCount > WolfCount)
                    {
                        CastCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("人狼陣営が多いです。");
                    }
                }
                else
                {
                    Console.WriteLine("人数が合いません");
                }
            }

            Console.WriteLine();
            foreach (var s in castlist.Select((v, i) => new { v, i }))
            {
                Console.WriteLine("{0,-10:G10}:{1}人", s.v, CastCount[s.i]);
            }

            bool result = DisplayLibrary.YesOrNo(0, "\nこれでいいですか？");

            if (!result)
            {
                DecisionCast();
            }
        }
    }

    /*
    struct MemberCount
    {
        public int VillagerCount;   //村人の人数
        public int WerewolfCount;   //人狼の人数
        public int ProphetCount;    //占い師の人数
        public int PaychicCount;    //霊能力者の人数
        public int GuardmanCount;   //狩人の人数
        public int MadmanCount;     //狂人の人数

    }
    */
}
