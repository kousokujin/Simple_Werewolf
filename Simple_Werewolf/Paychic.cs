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

        /// <summary>
        /// コンソールに表示する色
        /// </summary>
        public static ConsoleColor Forground = ConsoleColor.Blue;
        public static ConsoleColor Background = ConsoleColor.Black;

        public Paychic(string name) : base(name)
        {
            //innerPlayerName = name;
            Executioned = null;
        }

        public override PlayerPosition Position
        {
            get
            {
                return PlayerPosition.Psychic;
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


        /// <summary>
        /// 前日に処刑された人
        /// </summary>
        public Person Executioned;

        public override void NightAction(List<Person> JoinPlayers)
        {
            CommonLibrary.ChangeDisplayColor(1);
            DisplayThisCast();

            if (Executioned != null)
            {
                //Console.WriteLine("10秒間待機してください。");
                Console.Write("先程処刑した{0}さんは", Executioned.PlayerName);
                CommonLibrary.WriteCastColor(Executioned.IsWerewolf);
                //DisplayLibrary.ColorConsole(Executioned.IsWerewolf.DisplayName(), IsWerewolf.ForgroundColor(), IsWerewolf.BackgroundColor());

                Console.WriteLine("です。\n");
            }
            CommonLibrary.wait(10);
        }
    }
}
