using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class Prophet : Person
    {
        /// <summary>
        /// コンソールに表示する色
        /// </summary>
        public static ConsoleColor Forground = ConsoleColor.Cyan;
        public static ConsoleColor Background = ConsoleColor.Black;

        public Prophet(string name) : base(name)
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
                return PlayerPosition.Madman;
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

            Person target = ListUpMember(people, "今日占う人を選んでください。");
            PlayerPosition isWolf = target.IsWerewolf;

            Console.Write("{0}さんは",target.PlayerName);
            DisplayLibrary.ColorConsole(isWolf.DisplayName(), isWolf.ForgroundColor(), isWolf.BackgroundColor());

            /*
            if (isWolf == PlayerPosition.Werewolf)
            {
                DisplayLibrary.ColorConsole(PlayerPosition.Werewolf.DisplayName(), Wolf.Forground, Wolf.Background);
            }
            else
            {
                DisplayLibrary.ColorConsole(PlayerPosition.Villager.DisplayName(), Villager.Forground, Villager.Background);
            }
            */

            Console.WriteLine("です。");

            Console.ReadKey();
        }
    }
}
