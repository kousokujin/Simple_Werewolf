﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class Wolf : Person
    {
        /// <summary>
        /// コンソールに表示する色
        /// </summary>
        public static ConsoleColor Forground = ConsoleColor.White;
        public static ConsoleColor Background = ConsoleColor.DarkRed;

        /// <summary>
        /// 他の人狼
        /// </summary>
        public List<Wolf> Otherwolf;

        public Wolf(string name) : base(name)
        {
            //innerPlayerName = name;
            Otherwolf = new List<Wolf>();
        }

        public override PlayerPosition IsWerewolf
        {
            get
            {
                return PlayerPosition.Werewolf;
            }
        }

        public override PlayerPosition Position
        {
            get
            {
                return PlayerPosition.Werewolf;
            }
        }

        public override bool IsWerewolfPosition
        {
            get
            {
                return true;
            }
        }

        override public void NightAction(List<Person> people)
        {
            //DisplayLibrary.ChangeColorClear(CommonLibrary.OnePerson);
            CommonLibrary.ChangeDisplayColor(1);
            DisplayThisCast();

            if (Otherwolf.Where(x => (x != this)&&(x.isDead == false)).ToList().Count != 0)
            {
                Console.Write("他の人狼は");

                foreach (Wolf w in Otherwolf.Where(x => (x != this)&&(x.isDead == false)))
                {
                    Console.Write("{0}さん　", w.PlayerName);
                }
                Console.WriteLine("です。");
            }

            DateTime start = DateTime.Now;
            Person target = ListUpMember(people.Where(x=>x.Position != PlayerPosition.Werewolf).ToList(),"今日のターゲットを選んでください。");
            target.isTarget = true;
            DateTime end = DateTime.Now;

            Console.WriteLine("{0}さんを襲います。", target.PlayerName);
            CommonLibrary.TimeSpanWait(start, end);
        }
    }
}
