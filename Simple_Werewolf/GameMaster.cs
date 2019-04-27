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

        public GameMaster()
        {
            Players = new List<Person>();
            MemberName = new List<string>();
            //CastCount = new int[6];
            CastCount = new Dictionary<PlayerPosition, int>();
        }

        
        public void MainGame()
        {
            bool isloop = true;
            JoinMember();
            DecisionCast();

            while (isloop)
            {
                OneGame();
                Console.WriteLine();
                isloop = DisplayLibrary.YesOrNo(0,"もう一度やりますか？");
            }
        }
        
        //ゲーム1回
        public void OneGame()
        {
            bool isGameRun = true;
            bool firstloop = true;
            //参加者の名前を入力
            setCast();

            while (isGameRun)
            {
                /*
                EachChackCast();
                execution();
                
                if (ContinueDisplay())
                {
                    break;
                }
                */
                
                if (!firstloop) {
                    execution();
                    if (ContinueDisplay())
                    {
                        //isGameRun = false;
                        break;
                    }
                }
                else
                {
                    firstloop = false;
                }
                
                NightAction();
                NightProcess();
                isGameRun = !ContinueDisplay();
                
            }
        }
        
        /// <summary>
        /// 参加者の名前を入力させる
        /// </summary>
        public void JoinMember()
        {
            //DisplayLibrary.ChangeColorClear(CommonLibrary.AllPerson);
            CommonLibrary.ChangeDisplayColor(0);

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
            //DisplayLibrary.ChangeColorClear(CommonLibrary.AllPerson);
            CommonLibrary.ChangeDisplayColor(0);

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

            int maxWidth = CastEnum.AllCastList().Select(x => (DisplayLibrary.StringCount(x.DisplayName()))).Max();
            foreach (var s in CastEnum.AllCastList().Select((v, i) => new { v, i }))
            {
                //DisplayLibrary.ColorConsole("{0}", s.v.ForgroundColor(), s.v.BackgroundColor(), s.v.DisplayName());
                CommonLibrary.WriteCastColor(s.v);

                int space = maxWidth - DisplayLibrary.StringCount(s.v.DisplayName()); //文字スペース
                Console.Write(new string(' ', space));

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
        public void NightProcess()
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

                //フラグを戻す
                p.isProtect = false;
                p.isTarget = false;
            }

            CommonLibrary.ChangeDisplayColor(0);

            Console.WriteLine("夜が明けました。");
            if (Victim.Count == 0)
            {
                Console.WriteLine("昨晩の犠牲者はいませんでした。");
            }
            else
            {
                Console.WriteLine("昨晩の犠牲者は");
                foreach(Person p in Victim)
                {
                    Console.WriteLine("{0}さん", p.PlayerName);
                }
                Console.WriteLine("の{0}人でした。",Victim.Count());
            }
            Console.WriteLine("Enterキーを押してください。");
            Console.ReadKey();
            //return Victim;
        }

        /// <summary>
        /// 処刑を実行する
        /// </summary>
        public void execution()
        {
            CommonLibrary.ChangeDisplayColor(0);
            Person p = CommonLibrary.StaticListUpMember(Players.Where(x => (x.isDead == false)).ToList(),"今夜処刑する人を選んでください。");
            p.isDead = true;

            //霊能力者に通知
            foreach(Paychic psy in Players.Where(x=>x.Position == PlayerPosition.Psychic).ToList())
            {
                psy.Executioned = p;
            }

            Console.WriteLine("{0}さんが処刑されました。Enterキーを押してください。",p.PlayerName);
            Console.ReadKey();
        }

        /// <summary>
        /// 夜の行動
        /// </summary>
        public void NightAction()
        {

            foreach (var item in Players.Where(x => x.isDead == false).Select((play,count)=>new { play, count }).ToList())
            {
                if (item.count == 0)
                {
                    CommonLibrary.ChangeDisplay("", item.play.PlayerName,"夜の行動に移ります。");

                }
                else
                {
                    CommonLibrary.ChangeDisplay(Players[item.count - 1].PlayerName, item.play.PlayerName);
                }

                item.play.NightAction(Players);
            }
        }

        /// <summary>
        /// 勝敗判定
        /// </summary>
        /// <returns>村人が勝ちの場合はtrue,人狼が勝ちの場合はfalse、それ以外はnull</returns>
        private bool? ContinueGame()
        {
            int village_side = Players.Where(x => (x.isDead == false) && (x.IsWerewolfPosition == false)).ToList().Count();
            int wolf_side= Players.Where(x => (x.isDead == false) && (x.IsWerewolfPosition == true)).ToList().Count();
            int wolf = Players.Where(x => (x.isDead == false) && (x.Position == PlayerPosition.Werewolf)).ToList().Count();

            if(wolf == 0)
            {
                return true;
            }
            if(village_side <= wolf_side)
            {
                return false;
            }

            return null;

        }

        /// <summary>
        /// 次の週に入る画面
        /// </summary>
        /// <returns>ゲームが終わって次の週に入らない場合はtrue</returns>
        public bool ContinueDisplay()
        {
            bool? isContinue = ContinueGame();

            //勝敗が決まった
            if(isContinue != null)
            {
                CommonLibrary.ChangeDisplayColor(0);
                bool VillegeVictory = (bool)isContinue;

                //村人の勝ち
                if (VillegeVictory)
                {
                    DisplayLibrary.ColorConsole("村人", Villager.Forground, Villager.Background);
                }
                else　//人狼の勝ち
                {
                    DisplayLibrary.ColorConsole("人狼", Wolf.Forground, Wolf.Background);
                }
                Console.WriteLine("の勝利。\n");
                Console.WriteLine("役職一覧");
                DisplayCast();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 配役表示
        /// </summary>
        public void DisplayCast()
        {
            int width = Players.Select(x => DisplayLibrary.StringCount(x.PlayerName)).ToList().Max() + 4;
            foreach(Person p in Players)
            {
                string displayname = p.PlayerName + new string(' ', width - DisplayLibrary.StringCount(p.PlayerName));
                Console.Write(displayname);
                CommonLibrary.WriteCastColor(p.Position);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 各参加者に役職を確認させる。
        /// </summary>
        public void EachChackCast()
        {
            //CommonLibrary.ChangeDisplay("",Players[0].PlayerName,"まず最初に、各自が自分の役職を確認します。");

            foreach(var EachPlayer in Players.Select((x,n)=>new {x,n}).ToList())
            {
                if (EachPlayer.n == 0)
                {
                    CommonLibrary.ChangeDisplay("", Players[0].PlayerName, "まず最初に、各自が自分の役職を確認します。");
                }
                else
                {
                    CommonLibrary.ChangeDisplay(Players[EachPlayer.n - 1].PlayerName, EachPlayer.x.PlayerName);
                }
                CommonLibrary.ChangeDisplayColor(1);
                EachPlayer.x.DisplayThisCast();
                CommonLibrary.wait(0);
            }
        }


    }
}
