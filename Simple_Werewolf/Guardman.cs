using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class Guardman : Person
    {
        /// <summary>
        /// コンソールに表示する色
        /// </summary>
        public static ConsoleColor Forground = ConsoleColor.DarkYellow;
        public static ConsoleColor Background = ConsoleColor.Black;

        public Guardman(string name) : base(name)
        {
            //innerPlayerName = name;
        }

        public override PlayerPosition IsWerewolf
        {
            get
            {
                return PlayerPosition.Villager;
            }
        }

        public override PlayerPosition Position
        {
            get
            {
                return PlayerPosition.Guardman;
            }
        }

        public override bool IsWerewolfPosition
        {
            get
            {
                return false;
            }
        }

        override public void NightAction(List<Person> people)
        {
            DisplayLibrary.ChangeColorClear(GameMaster.OnePerson);
            Console.Write("あなたは");
            DisplayLibrary.ColorConsole(Position.DisplayName(), Forground, Background);
            Console.WriteLine("です。");

            Person target = ListUpMember(people, "今日の守る人を選んでください。");
            target.isProtect = true;
        }
    }
}
