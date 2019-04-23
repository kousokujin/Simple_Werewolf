﻿using System;
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
            DisplayLibrary.ChangeColorClear(GameMaster.OnePerson);
            Console.Write("あなたは");
            DisplayLibrary.ColorConsole(Position.DisplayName(), Forground, Background);
            Console.WriteLine("です。");
            //Console.WriteLine("10秒間待機してください。");
            Console.Write("先程処刑した{0}さんは", Executioned.PlayerName);
            DisplayLibrary.ColorConsole(Executioned.IsWerewolf.DisplayName(), IsWerewolf.ForgroundColor(), IsWerewolf.BackgroundColor());

            /*
            if (Executioned.IsWerewolf)
            {
                DisplayLibrary.ColorConsole("人狼", Wolf.Forground, Wolf.Background);
            }
            else
            {
                DisplayLibrary.ColorConsole("村人", Villager.Forground,  Villager.Background);
            }
            */
            Console.WriteLine("です。\n");
            CommonLibrarys.wait(10);
        }
    }
}
