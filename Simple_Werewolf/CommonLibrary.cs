using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Simple_Werewolf
{
    /// <summary>
    /// 使い回す関数群
    /// </summary>
    static class CommonLibrary
    {
        /// <summary>
        /// 1人だけが操作するときの文字色
        /// </summary>
        public static ConsoleColor OnePerson = ConsoleColor.White;

        /// <summary>
        /// 全員で操作するときの文字色
        /// </summary>
        public static ConsoleColor AllPerson = ConsoleColor.Cyan;

        /// <summary>
        /// 人が変わるときの文字色
        /// </summary>
        public static ConsoleColor ChangePerson = ConsoleColor.Green;

        /// <summary>
        /// 引数で与えられたプレイヤーを選択させる画面を表示する。
        /// /// </summary>
        /// <param name="JoinPlayers">表示するプレイヤー</param>
        /// <param name="message">メッセージ</param>
        /// <returns>選択されたプレイヤー</returns>
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
            DisplayLibrary.ChangeColorClear();
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

        /// <summary>
        /// 役職をいい感じに表示する(改行なし)
        /// </summary>
        /// <param name="cast">役職</param>
        public static void WriteCastColor(PlayerPosition cast)
        {
            DisplayLibrary.ColorConsole(cast.DisplayName(), cast.ForgroundColor(), cast.BackgroundColor());
        }
    }
}
