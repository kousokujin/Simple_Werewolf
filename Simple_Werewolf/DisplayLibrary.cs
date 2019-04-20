using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    static class DisplayLibrary
    {
        /// <summary>
        /// 引数で与えられた文字列の配列を選択させる画面を表示する
        /// </summary>
        /// <param name="choices">選択肢として表示する文字列</param>
        /// <param name="shift">左側の余白</param>
        /// <returns>選ばれた選択肢</returns>
        public static int SelectDisplay(List<string> choices,int shift = 0)
        {
            Console.CursorVisible = false;
            int point = 0;
            //int NowPosition = Console.CursorTop;
            const ConsoleColor PointColor = ConsoleColor.DarkBlue;
            foreach(var item in choices.Select((x,i)=> new { x, i }))
            {
                if (point == item.i)
                {
                    Console.BackgroundColor = PointColor;

                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(shift, Console.CursorTop);
                Console.WriteLine(item.x);
            }

            Console.SetCursorPosition(shift, Console.CursorTop - choices.Count());

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
                            Console.Write(choices[point]);
                            Console.SetCursorPosition(shift, Console.CursorTop - 1);
                            Console.BackgroundColor = PointColor;
                            point--;
                            Console.Write(choices[point]);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (point < choices.Count() - 1)
                        {
                            Console.SetCursorPosition(shift, Console.CursorTop);
                            Console.Write(choices[point]);
                            Console.SetCursorPosition(shift, Console.CursorTop + 1);
                            Console.BackgroundColor = PointColor;
                            point++;
                            Console.Write(choices[point]);
                            Console.BackgroundColor = ConsoleColor.Black;
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
            return point;
        }
    }
}
