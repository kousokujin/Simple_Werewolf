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
        abstract public bool IsWerewolf
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
        /// 引数で与えられたプレイヤーを選択させる画面を表示する。
        /// （生きてる人だけ）
        /// </summary>
        /// <param name="JoinPlayers">プレイヤー</param>
        /// <returns>選択されたプレイヤー</returns>
        public Person ListUpMember(List<Person> JoinPlayers,string message = "プレイヤーを選んでください。")
        {
            //Person output = null;
            //bool isDisplayed = false;

            List<Person> LivingPeople = JoinPlayers.Where(x => (x.isDead == false)&&(x != this)).ToList();

            Console.WriteLine(message + "\n");

            int p = DisplayLibrary.SelectDisplay(
                    LivingPeople.Select(x => x.PlayerName).ToList(),
                    4
                );
            return LivingPeople[p];
        }

        /// <summary>
        /// 次の人へ操作が変わるように指示をする画面を表示
        /// </summary>
        /// <param name="prevPerson">前の人</param>
        /// <param name="nextPerson">次の人</param>
        public static void ChangeDisplay(string prevPerson,string nextPerson)
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
