using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    static class CastEnum
    {
        /// <summary>
        /// 役職の日本語名を返す
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static string DisplayName(this PlayerPosition pos)
        {
            string[] name = { "村人", "人狼", "占い師", "霊能力者", "狩人", "狂人"};
            return name[(int)pos];

        }

        /// <summary>
        /// 役職を表示する文字色を返す
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static ConsoleColor ForgroundColor(this PlayerPosition pos)
        {
            ConsoleColor[] cls = {
                Villager.Forground,
                Wolf.Forground,
                Prophet.Forground,
                Paychic.Forground,
                Guardman.Forground,
                Madman.Forground
            };
            return cls[(int)pos];
        }

        /// <summary>
        /// 役職を表示する背景色を返す
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static ConsoleColor BackgroundColor(this PlayerPosition pos)
        {
            ConsoleColor[] cls = {
                Villager.Background,
                Wolf.Background,
                Prophet.Background,
                Paychic.Background,
                Guardman.Background,
                Madman.Background
            };
            return cls[(int)pos];
        }

        /// <summary>
        /// すべての役職のenumをリストに入れたやつを返す
        /// </summary>
        /// <returns></returns>
        public static List<PlayerPosition> AllCastList()
        {
              return  new List<PlayerPosition> {
                PlayerPosition.Villager,
                PlayerPosition.Werewolf,
                PlayerPosition.Prophet,
                PlayerPosition.Psychic,
                PlayerPosition.Guardman,
                PlayerPosition.Madman
            };
        }
    }

    /// <summary>
    /// 各役職の列挙型
    /// </summary>
    enum PlayerPosition
    {
        Villager,   //村人
        Werewolf,   //人狼
        Prophet,    //占い師
        Psychic,    //霊能力者
        Guardman,   //狩人
        Madman      //狂人
    }

}
