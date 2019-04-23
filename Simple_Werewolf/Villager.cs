using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Simple_Werewolf
{
    class Villager : Person
    {
        /// <summary>
        /// コンソールに表示する色
        /// </summary>
        public static ConsoleColor Forground = ConsoleColor.Green;
        public static ConsoleColor Background = ConsoleColor.Black;

        public Villager(string name) :base(name)
        {
            //innerPlayerName = name;
        }
        public override PlayerPosition Position
        {
            get
            {
                return PlayerPosition.Villager;
            }
        }

        public override PlayerPosition IsWerewolf
        {
            get
            {
                return PlayerPosition.Villager;
            }
        }

        public override bool IsWerewolfPosition
        {
            get
            {
                return false;
            }
        }

        public override void NightAction(List<Person> JoinPlayers)
        {
            DisplayLibrary.ChangeColorClear(CommonLibrary.OnePerson);
            DisplayThisCast();

            CommonLibrary.wait(10);
        }
    }
}
