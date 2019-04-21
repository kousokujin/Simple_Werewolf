using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Simple_Werewolf
{
    class Madman : Villager
    {
        public Madman(string name) : base(name)
        {
        }

        public override PlayerPosition Position
        {
            get
            {
                return PlayerPosition.Madman;
            }
        }

        public override bool IsWerewolfPosition
        {
            get
            {
                return true;
            }
        }

        public override void NightAction(List<Person> JoinPlayers)
        {
            Console.Write("あなたは");
            DisplayLibrary.ColorConsole("狂人", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine("です。");
            //Console.WriteLine("10秒間待機してください。");

            for (int i = 10; i >= 0; i--)
            {
                Console.CursorLeft = 0;
                Console.Write("{0}秒間待機してください。", i);
                Thread.Sleep(1000);
            }
        }
    }
}
