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

        public override bool IsWerewolf
        {
            get
            {
                return false;
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
            Console.Write("あなたは");
            DisplayLibrary.ColorConsole("占い師", Forground, Background);
            Console.WriteLine("です。");

            Person target = ListUpMember(people, "今日占う人を選んでください。");
            bool isWolf = target.IsWerewolf;

            Console.Write("{0}さんは",target.PlayerName);
            if (isWolf)
            {
                DisplayLibrary.ColorConsole("人狼", Wolf.Forground, Wolf.Background);
            }
            else
            {
                DisplayLibrary.ColorConsole("村人", Villager.Forground, Villager.Background);
            }
            Console.WriteLine("です。");

            Console.ReadKey();
        }
    }
}
