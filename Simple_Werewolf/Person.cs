﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Simple_Werewolf
{
    abstract class Person
    {
        public bool isTarget;
        public bool isProtect;
        public bool isDead;
        protected string innerPlayerName;


        protected Person(string name)
        {
            isTarget = false;
            isProtect = false;
            isDead = false;
            innerPlayerName = name;
        }
        /// <summary>
        /// プレイヤーの役職
        /// </summary>
        abstract public PlayerPosition Position
        {
            get;
        }

        /// <summary>
        /// プレイヤー名
        /// </summary>
        public string PlayerName
        {
            get
            {
                return innerPlayerName;
            }
        }

        /// <summary>
        /// 霊能力者・預言者に占われたときに人狼だった場合はtrue
        /// </summary>
        abstract public PlayerPosition IsWerewolf
        {
            get;
        }

        /// <summary>
        /// 人狼・村人陣営かどうか。人狼陣営の場合はtrue
        /// </summary>
        abstract public bool IsWerewolfPosition
        {
            get;
        }

        /// <summary>
        /// 夜の行動
        /// </summary>
        /// <param name="JoinPlayers">参加者</param>
        public abstract void NightAction(List<Person> JoinPlayers);

        /// <summary>
        /// 引数で与えられたプレイヤーを選択させる画面を表示する。(自分は表示しない)
        /// （生きてる人だけ）
        /// </summary>
        /// <param name="JoinPlayers">プレイヤー</param>
        /// <returns>選択されたプレイヤー</returns>
        public Person ListUpMember(List<Person> JoinPlayers, string message = "プレイヤーを選んでください。")
        {
            //Person output = null;
            //bool isDisplayed = false;

            List<Person> LivingPeople = JoinPlayers.Where(x => (x.isDead == false) && (x != this)).ToList();
            return CommonLibrarys.StaticListUpMember(LivingPeople, message);
        }

        /// <summary>
        /// 引数で与えられたプレイヤーを選択させる画面を表示する。
        /// /// </summary>
        /// <param name="JoinPlayers">表示するプレイヤー</param>
        /// <param name="message">メッセージ</param>
        /// <returns>選択されたプレイヤー</returns>
        /*
        public static Person StaticListUpMember(List<Person> JoinPlayers, string message = "プレイヤーを選んでください。")
        {
            Console.WriteLine(message + "\n");

            int p = DisplayLibrary.SelectDisplay(
                    JoinPlayers.Select(x => x.PlayerName).ToList(),
                    0
                );
            return JoinPlayers[p];
        }

        /// <summary>
        /// 次の人へ操作が変わるように指示をする画面を表示
        /// </summary>
        /// <param name="prevPerson">前の人</param>
        /// <param name="nextPerson">次の人</param>
        public static void ChangeDisplay(string prevPerson, string nextPerson)
        {
            Console.Clear();
            //日本語があやしい
            if (nextPerson != "")
            {
                if (prevPerson != "")
                {
                    Console.WriteLine("{0}さんから{1}さんに操作を変更してください。", prevPerson, nextPerson);
                    Console.WriteLine("{0}さんに代わったらEnterキーを押してください、", nextPerson);
                }
                else
                {
                    Console.WriteLine("{0}さんが操作をしたください。", nextPerson);
                    Console.WriteLine("{0}さんはEnterキーを押してください。", nextPerson);
                }
            }
            else
            {
                Console.WriteLine("全員で画面を見てください。");
            }
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// 待機する画面を出す。
        /// </summary>
        /// <param name="time">時間(秒)</param>
        public static void wait(int time)
        {
            for (int i = time; i >= 0; i--)
            {
                Console.CursorLeft = 0;
                Console.Write("{0}秒間待機してください。", i);
                Thread.Sleep(1000);
            }
        }
        */
    }
}
