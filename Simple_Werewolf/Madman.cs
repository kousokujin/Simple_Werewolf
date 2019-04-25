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
        /// <summary>
        /// コンソールに表示する色
        /// </summary>
        public new static ConsoleColor Forground = ConsoleColor.White;
        public new static ConsoleColor Background = ConsoleColor.Magenta;

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
            CommonLibrary.ChangeDisplayColor(1);
            DisplayThisCast();
            //Console.WriteLine("10秒間待機してください。");

            CommonLibrary.wait(10);
        }
    }
}
