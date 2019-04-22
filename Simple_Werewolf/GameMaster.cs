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
        /// 参加者の役職の人数を決める
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

            //色を表示
            List<ConsoleColor> ForgroundList = new List<ConsoleColor>() {
                Villager.Forground,
                Wolf.Forground,
                Prophet.Forground,
                Paychic.Forground,
                Guardman.Forground,
                Madman.Forground,
            };

            List<ConsoleColor> BackgroundList = new List<ConsoleColor>() {
                Villager.Background,
                Wolf.Background,
                Prophet.Background,
                Paychic.Background,
                Guardman.Background,
                Madman.Background,
            };

            foreach (var s in castlist.Select((v, i) => new { v, i }))
            {
                DisplayLibrary.ColorConsole("{0}", ForgroundList[s.i], BackgroundList[s.i], s.v);

                int space = 5 - s.v.Length; //文字スペース
                Console.Write(new string(' ', space*2));

                Console.WriteLine("{0,5}人", CastCount[s.i]);
            }

            bool result = DisplayLibrary.YesOrNo(0, "\nこれでいいですか？");

            if (!result)
            {
                DecisionCast();
            }
        }

        /// <summary>
        /// 配役を行う。
        /// </summary>
        public void setCast()
        {
            Players.Clear();
            
            //カードに見立てて配役
            List<int> card = new List<int>();
            int CardNumber = 0;
            foreach(var c in CastCount)
            {
                for(int i = 0; i < c; i++)
                {
                    card.Add(CardNumber);
                }
                CardNumber++;
            }

            //カードをシャッフル
            Random r = new Random();
            card = card.OrderBy(a => r.Next(card.Count)).ToList();
            List<Wolf> wolflist = new List<Wolf>();

            foreach(var member in MemberName.Select((name,n) =>new {name,n}))
            {
                Person temp =null;
                switch (card[member.n])
                {
                    case 0:
                        temp = new Villager(member.name);
                        break;
                    case 1:
                        temp = new Wolf(member.name);
                        wolflist.Add(temp as Wolf);
                        break;
                    case 2:
                        temp = new Prophet(member.name);
                        break;
                    case 3:
                        temp = new Paychic(member.name);
                        break;
                    case 4:
                        temp = new Guardman(member.name);
                        break;
                    case 5:
                        temp = new Madman(member.name);
                        break;
                    default:
                        break;
                }

                Players.Add(temp);
            }

            foreach(Wolf w1 in wolflist)
            {
                foreach(Wolf w2 in wolflist)
                {
                    w1.Otherwolf.Add(w2);
                }
            }
        }

        /// <summary>
        /// 夜の行動後の処理
        /// </summary>
        /// <returns>犠牲者</returns>
        public List<Person> NightProcess()
        {
            List<Person> Victim = new List<Person>();

            foreach(var p in Players)
            {
                //人狼のターゲットか
                if(p.isTarget == true) {
                    if(p.isProtect == false)
                    {
                        //狩人にガードされてない村人は死
                        p.isDead = true;
                        Victim.Add(p);
                    }
                }
            }

            return Victim;
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
