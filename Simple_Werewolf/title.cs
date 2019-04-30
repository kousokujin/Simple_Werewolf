using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    static class title
    {

        static public void MainTitle()
        {
            DisplayLibrary.ChangeColorClear();
            Console.Title = "シンプル人狼";
            writetitle();
            int select = WriteGameMenu(3);

            switch(select)
            {
                case 0:
                    GameMaster master = new GameMaster();
                    master.MainGame();
                    MainTitle();
                    break;
                case 1:
                    Tutorial.TutorialRun();
                    MainTitle();
                    break;
                default:
                    break;
                
            }
        }
        /// <summary>
        /// タイトルを出力
        /// </summary>
        static void writetitle()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Sinple Werewolf Game");
            Console.WriteLine("Version {0}", getAssemblyVersion());
            Console.WriteLine("Copyright (c) 2019 kousokujin.");
            Console.WriteLine("Released under the MIT license.");
            Console.WriteLine("-------------------------------");

        }

        /// <summary>
        /// ゲームのチュートリアルとかを選択するやつを表示
        /// </summary>
        /// <param name="margin">縦方向の空白</param>
        static int WriteGameMenu(int margin)
        {
            string space = new string('\n', margin);
            Console.Write(space);

            List<string> selectStrs = new List<string>() { "ゲームを始める", "チュートリアル","終了" };
            return DisplayLibrary.SelectDisplay(selectStrs);
        }

        /// <summary>
        /// バージョン取得
        /// </summary>
        /// <returns></returns>
        static public string getAssemblyVersion()
        {
            System.Diagnostics.FileVersionInfo ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            string version = ver.ProductVersion;

            return version;
        }
    }
}
