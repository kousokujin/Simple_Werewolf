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
                return PlayerPosition.Prophet;
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
            CommonLibrary.ChangeDisplayColor(1);
            DisplayThisCast();

            DateTime start = DateTime.Now;
            Person target = ListUpMember(people, "今日占う人を選んでください。");
            PlayerPosition isWolf = target.IsWerewolf;
            DateTime end = DateTime.Now;
            Console.Write("{0}さんは",target.PlayerName);
            CommonLibrary.WriteCastColor(isWolf);


            Console.WriteLine("です。");
            CommonLibrary.TimeSpanWait(start, end);
        }
    }
}
