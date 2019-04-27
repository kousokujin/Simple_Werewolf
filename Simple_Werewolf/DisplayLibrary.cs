using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    static class DisplayLibrary
    {

        static ConsoleColor DefaultBackground;
        static ConsoleColor DefaultForground;

        static DisplayLibrary()
        {
            DefaultBackground = Console.BackgroundColor;
            DefaultForground = Console.ForegroundColor;
        }

        /// <summary>
        /// 引数で与えられた文字列の配列を選択させる画面を表示する
        /// </summary>
        /// <param name="choices">選択肢として表示する文字列</param>
        /// <param name="shift">左側の余白</param>
        /// <returns>選ばれた選択肢</returns>
        public static int SelectDisplay(List<string> choices,int shift = 0)
        {
            //Console.WriteLine(Console.ForegroundColor.ToString());
            ConsoleColor beforeForground = Console.ForegroundColor;
            ConsoleColor beforeBackground = Console.BackgroundColor;


            int width = choices.Select(x => StringCount(x)).ToList().Max();
            List<string> FixChoices = new List<string>();
            foreach (string s in choices)
            {
                FixChoices.Add(fixSpace(s, width));
            }


            Console.CursorVisible = false;
            int point = 0;
            //int NowPosition = Console.CursorTop;
            const ConsoleColor PointColor = ConsoleColor.DarkBlue;
            const ConsoleColor PointForground = ConsoleColor.White;
            ConsoleColor NornalFoground = beforeForground;
            //ConsoleColor NormalBackground = Console.BackgroundColor;
            ConsoleColor NormalBackground = ConsoleColor.DarkGray;

            foreach (var item in FixChoices.Select((x,i)=> new { x, i }))
            {
                if (point == item.i)
                {
                    Console.ForegroundColor = PointForground;
                    Console.BackgroundColor = PointColor;

                }
                else
                {
                    Console.BackgroundColor = NormalBackground;
                    Console.ForegroundColor = NornalFoground;
                }
                Console.SetCursorPosition(shift, Console.CursorTop);
                Console.WriteLine(item.x);
            }

            Console.SetCursorPosition(shift, Console.CursorTop - FixChoices.Count());

            bool isSelect = false;
            while (!isSelect)
            {
                ConsoleKeyInfo c = Console.ReadKey(true);
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (point > 0)
                        {
                            Console.SetCursorPosition(shift, Console.CursorTop);
                            Console.Write(FixChoices[point]);
                            Console.SetCursorPosition(shift, Console.CursorTop - 1);
                            Console.BackgroundColor = PointColor;
                            Console.ForegroundColor = PointForground;
                            point--;
                            Console.Write(FixChoices[point]);
                            Console.BackgroundColor = NormalBackground;
                            Console.ForegroundColor = NornalFoground;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (point < choices.Count() - 1)
                        {
                            Console.SetCursorPosition(shift, Console.CursorTop);
                            Console.Write(FixChoices[point]);
                            Console.SetCursorPosition(shift, Console.CursorTop + 1);
                            Console.BackgroundColor = PointColor;
                            Console.ForegroundColor = PointForground;
                            point++;
                            Console.Write(FixChoices[point]);
                            Console.BackgroundColor = NormalBackground;
                            Console.ForegroundColor = NornalFoground;
                        }
                        break;
                    case ConsoleKey.Enter:
                        isSelect = true;
                        break;
                    default:
                        break;
                }
            }

            Console.CursorVisible = true;
            Console.SetCursorPosition(0, Console.CursorTop + (choices.Count() - point));
            Console.ForegroundColor = beforeForground;
            Console.BackgroundColor = beforeBackground;
            return point;

            string fixSpace(string str,int str_width)
            {
                int strLength = StringCount(str);
                if(strLength < str_width)
                {
                    int d = str_width - strLength;
                    return str + new string(' ', d);
                }
                else
                {
                    return str;
                }
            }
        }

        /// <summary>
        /// はいかいいえを問う
        /// </summary>
        /// <param name="shift">左のマージン</param>
        /// <param name="message">メッセージ</param>
        /// <returns>はいを選択したらtrue</returns>
        public static bool YesOrNo(int shift=0,string message = "")
        {
            if(message != "")
            {
                Console.WriteLine(message);
            }

            List<string> select = new List<string>() { "はい", "いいえ" };
            int result = SelectDisplay(select, shift);

            return result == 0;
        }

        /// <summary>
        /// コンソールに色を使って表示（改行なし)
        /// </summary>
        /// <param name="str">表示する文字</param>
        /// <param name="Foreground">文字色</param>
        /// <param name="Background">背景色</param>
        /// <param name="args">引数</param>
        public static void ColorConsole(string str, ConsoleColor Foreground,ConsoleColor Background,params string[] args)
        {
            ConsoleColor preForground = Console.ForegroundColor;
            ConsoleColor preBackground = Console.BackgroundColor;

            ConsoleColor Forgreound_def;
            ConsoleColor Background_def;

            //文字色
            if (Foreground == DefaultForground)
            {
                Forgreound_def = preForground;
            }
            else
            {
                Forgreound_def = Foreground;
            }

            if(Background == DefaultBackground)
            {
                Background_def = preBackground;
            }
            else
            {
                Background_def = Background;
            }

            Console.BackgroundColor = Background_def;
            Console.ForegroundColor = Forgreound_def;
            Console.Write(str, args);
            Console.BackgroundColor = preBackground;
            Console.ForegroundColor = preForground;
        }
        
        /// <summary>
        /// コンソールに色を使って表示（改行あり)
        /// </summary>
        /// <param name="str">表示する文字</param>
        /// <param name="Foreground">文字色</param>
        /// <param name="Background">背景色</param>
        /// <param name="args">引数</param>
        public static void ColorConsoleLine(string str, ConsoleColor Foreground, ConsoleColor Background,params string[] args)
        {
            DisplayLibrary.ColorConsole(str, Foreground, Background, args);
            Console.WriteLine();
        }

        /// <summary>
        /// 文字色を指定して画面クリア
        /// </summary>
        /// <param name="TextColor">新しく設定する文字色</param>
        public static void ChangeColorClear(ConsoleColor TextColor = ConsoleColor.White,ConsoleColor Background = ConsoleColor.Black)
        {
            Console.ForegroundColor = TextColor;
            Console.BackgroundColor = Background;
            Console.Clear();
            //Console.WriteLine(Console.ForegroundColor.ToString());
        }

        /// <summary>
        /// 文字列を表示するのに使う桁数を表示
        /// </summary>
        /// <param name="str">調べたい文字数</param>
        public static int StringCount(string str)
        {
            Encoding sjis = Encoding.GetEncoding("Shift_JIS");
            return sjis.GetByteCount(str);
        }
    }
}
