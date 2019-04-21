using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class Prophet : Person
    {
        public Prophet(string name)
        {
            innerPlayerName = name;
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
            DisplayLibrary.ColorConsole("占い師", ConsoleColor.DarkGreen, ConsoleColor.White);
            Console.WriteLine("です。");

            Person target = ListUpMember(people, "今日占う人を選んでください。");
            bool isWolf = target.IsWerewolf;

            Console.Write("{0}さんは",target.PlayerName);
            if (isWolf)
            {
                DisplayLibrary.ColorConsole("人狼", ConsoleColor.Red, ConsoleColor.White);
            }
            else
            {
                DisplayLibrary.ColorConsole("村人", ConsoleColor.Blue, ConsoleColor.White);
            }
            Console.WriteLine("です。");

            Console.ReadKey();
        }
    }
}
