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
        //private int[] CastCount;
        private Dictionary<PlayerPosition, int> CastCount;

        /*
        /// <summary>
        /// 1人だけが操作するときの文字色
        /// </summary>
        public static ConsoleColor OnePerson = ConsoleColor.White;

        /// <summary>
        /// 全員で操作するときの文字色
        /// </summary>
        public static ConsoleColor AllPerson = ConsoleColor.Cyan;

        /// <summary>
        /// 人が変わるときの文字色
        /// </summary>
        public static ConsoleColor ChangePerson = ConsoleColor.Green;
        */

        public GameMaster()
        {
            Players = new List<Person>();
            MemberName = new List<string>();
            //CastCount = new int[6];
            CastCount = new Dictionary<PlayerPosition, int>();
        }
        
        /// <summary>
        /// 参加者の名前を入力させる
        /// </summary>
        public void JoinMember()
        {
            DisplayLibrary.ChangeColorClear(CommonLibrary.AllPerson);

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
            DisplayLibrary.ChangeColorClear(CommonLibrary.AllPerson);

            int MemberCount = MemberName.Count();
            //MemberCount cast = new MemberCount();
            bool CastCheck = false;

            /*
            PlayerPosition[] castlist = new PlayerPosition[] {
                PlayerPosition.Villager,
                PlayerPosition.Werewolf, 
                PlayerPosition.Prophet,
                PlayerPosition.Psychic,
                PlayerPosition.Guardman,
                PlayerPosition.Madman
            };
            */

            while (!CastCheck)
            {
                int remaining = MemberName.Count;

                Console.WriteLine();

                foreach (var s in CastEnum.AllCastList().Select((v, i) => new { v, i }))
                {
                    bool check = false;
                    int n = 0;

                    if (remaining > 0)
                    {
                        Console.WriteLine("{0}の人数を入力してください。(残り人数:{1}人)", s.v.DisplayName(), remaining);
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
                    //CastCount[s.i] = n;
                    CastCount[s.v] = n;

                }

                int sum = CastCount.Select(x => x.Value).Sum();
                int WolfCount = CastCount[PlayerPosition.Werewolf] + CastCount[PlayerPosition.Madman];
                //int VillagerCount = CastCount[PlayerPosition.Villager] + CastCount[PlayerPosition.Psychic] + CastCount[PlayerPosition.Prophet] + CastCount[PlayerPosition.Guardman];
                int VillagerCount = sum - WolfCount;
                if (sum == MemberName.Count)
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

            foreach (var s in CastEnum.AllCastList().Select((v, i) => new { v, i }))
            {
                //DisplayLibrary.ColorConsole("{0}", s.v.ForgroundColor(), s.v.BackgroundColor(), s.v.DisplayName());
                CommonLibrary.WriteCastColor(s.v);

                int space = 5 - s.v.DisplayName().Length; //文字スペース
                Console.Write(new string(' ', space*2));

                Console.WriteLine("{0,5}人", CastCount[s.v]);
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
            List<PlayerPosition> card = new List<PlayerPosition>();
            int CardNumber = 0;
            foreach(var c in CastEnum.AllCastList())
            {
                int n = CastCount[c];
                for(int i = 0; i < n; i++)
                {
                    card.Add(c);
                }
                CardNumber++;
            }

            //カードをシャッフル
            Random r = new Random();
            card = card.OrderBy(a => r.Next(card.Count)).ToList();
            //List<Wolf> wolflist = new List<Wolf>();

            foreach(var member in MemberName.Select((name,n) =>new {name,n}))
            {
                Person temp =null;
                switch (card[member.n])
                {
                    case PlayerPosition.Villager:
                        temp = new Villager(member.name);
                        break;
                    case PlayerPosition.Werewolf:
                        temp = new Wolf(member.name);
                        break;
                    case PlayerPosition.Prophet:
                        temp = new Prophet(member.name);
                        break;
                    case PlayerPosition.Psychic:
                        temp = new Paychic(member.name);
                        break;
                    case PlayerPosition.Guardman:
                        temp = new Guardman(member.name);
                        break;
                    case PlayerPosition.Madman:
                        temp = new Madman(member.name);
                        break;
                    default:
                        break;
                }

                Players.Add(temp);
            }

            //人狼の処理
            foreach(Wolf wx in Players.Where(x => (x.Position == PlayerPosition.Werewolf)).ToList()){
                foreach (Wolf wy in Players.Where(x => (x.Position == PlayerPosition.Werewolf)).ToList())
                {
                    wx.Otherwolf.Add(wy);
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
}
