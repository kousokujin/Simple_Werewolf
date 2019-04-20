using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    abstract class Person
    {
        public bool isTarget;
        public bool isProtect;
        public bool isDead;
        protected string innerPlayerName;
        public string PlayerName
        {
            get
            {
                return innerPlayerName;
            }
        }


        public abstract void NightAction(List<Person> JoinPlayers);

        /// <summary>
        /// 引数で与えられたプレイヤーを選択させる画面を表示する。
        /// （生きてる人だけ）
        /// </summary>
        /// <param name="JoinPlayers">プレイヤー</param>
        /// <returns>選択されたプレイヤー</returns>
        public static Person ListUpMember(List<Person> JoinPlayers)
        {
            //Person output = null;
            //bool isDisplayed = false;

            List<Person> LivingPeople = JoinPlayers.Where(x => x.isDead == false).ToList();

            Console.WriteLine("プレイヤーを選んでください。\n");

            int p = DisplayLibrary.SelectDisplay(
                    LivingPeople.Select(x => x.PlayerName).ToList(),
                    4
                );
            return LivingPeople[p];

            /*
            do
            {
                Console.WriteLine("プレイヤーを選んでください。");

                int p = DisplayLibrary.SelectDisplay(
                        LivingPeople.Select(x => x.PlayerName).ToList()
                    );
                if (isDisplayed == false)
                {
                    byte count = 1;
                    Console.WriteLine("プレイヤーを選んでください。");
                    foreach (Person Player in LivingPeople)
                    {
                            Console.WriteLine("[{0}]:{1}", count, Player.PlayerName);
                            count++;
                    }
                    isDisplayed = true;
                }
                else
                {
                    Console.WriteLine("1～{0}の数字を入力してください。",LivingPeople.Count);
                }
                string KeyboardInput = Console.ReadLine();
                try
                {
                    int point = int.Parse(KeyboardInput);

                    if(LivingPeople.Count() >= point && point > 0)
                    {

                        output = LivingPeople[point-1];
                    }
                }
                catch (FormatException)
                {
                    output = null;
                }
            } while (output == null);
            return output;
            */
        }
    }
}
