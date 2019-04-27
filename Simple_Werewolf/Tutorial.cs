using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{

    /// <summary>
    /// チュートリアルをやる
    /// </summary>
    static class Tutorial
    {

        /// <summary>
        /// チュートリアルのエントリポイント的な
        /// </summary>
        public static void TutorialRun()
        {
            DisplayColor();
            AllPerson();
            ChangePerson();
            OnePerson();
        }

        /// <summary>
        /// 画面の色による説明
        /// </summary>
        static void DisplayColor()
        {
            CommonLibrary.ChangeDisplayColor(0);
            Console.WriteLine("この人狼アプリケーションは、画面の色によって操作する人が異なります。");
            Console.WriteLine("画面の色は3色あり、全員で操作したり見たりするとき、操作する人を代えるとき、一人で操作するときで異なります。");
            Console.ReadKey();
        }
        /// <summary>
        /// すべての人のチュートリアル
        /// </summary>
        static void AllPerson()
        {
            CommonLibrary.ChangeDisplayColor(0);
            Console.WriteLine("画面がこの色のときは、参加者全員が画面を見るときです。");
            Console.WriteLine("この色の画面のときは、みんなで画面を確認してください。");
            Console.ReadKey();
        }

        /// <summary>
        /// 1人の操作のチュートリアル
        /// </summary>
        static void OnePerson()
        {
            CommonLibrary.ChangeDisplayColor(1);
            Console.WriteLine("画面がこの色のときは、特定の参加者のみが画面を見るときです。");
            Console.WriteLine("他の人は画面を見てはいけません。");
            Console.WriteLine("画面を見る人は前の緑色の画面で指示されるので、その人のみ画面を見てください。");
            Console.ReadKey();
        }

        /// <summary>
        /// 操作変更のチュートリアル
        /// </summary>
        static void ChangePerson()
        {
            CommonLibrary.ChangeDisplayColor(2);
            Console.WriteLine("画面がこの色のときは、参加者のうち、画面を見て操作する人を変更するときです。");
            Console.WriteLine("この画面を見た人は、画面に出ている次の人に、操作を代わってください。");
            Console.WriteLine("代わったら、その人はEnterキーを押してください。");
            Console.ReadKey();
        }
    }
}
