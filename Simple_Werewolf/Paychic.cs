using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Simple_Werewolf
{
    class Paychic : Person
    {
        public Paychic(string name) : base()
        {
            innerPlayerName = name;
        }

        public override PlayerPosition Position
        {
            get
            {
                return PlayerPosition.Psychic;
            }
        }

        public override bool IsWerewolf
        {
            get
            {
                return false;
            }
        }

        public override bool IsWerewolfPosition
        {
            get
            {
                return false;
            }
        }


        /// <summary>
        /// 前日に処刑された人
        /// </summary>
        public Person Executioned;

        public override void NightAction(List<Person> JoinPlayers)
        {
            Console.Write("あなたは");
            DisplayLibrary.ColorConsole("霊能力者", ConsoleColor.Cyan, ConsoleColor.Black);
            Console.WriteLine("です。");
            //Console.WriteLine("10秒間待機してください。");
            Console.Write("先程処刑した{0}さんは", Executioned.PlayerName);

            if (Executioned.IsWerewolf)
            {
                DisplayLibrary.ColorConsole("人狼", ConsoleColor.Red, ConsoleColor.White);
            }
            else
            {
                DisplayLibrary.ColorConsole("村人", ConsoleColor.Blue, ConsoleColor.White);
            }
            Console.WriteLine("です。\n");

            for (int i = 10; i >= 0; i--)
            {
                Console.CursorLeft = 0;
                Console.Write("{0}秒間待機してください。", i);
                Thread.Sleep(1000);
            }
        }
    }
}
