using System;
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
            return CommonLibrary.StaticListUpMember(LivingPeople, message);
        }

        /// <summary>
        /// 「あなたは{役職名}です」を表示
        /// </summary>
        public void DisplayThisCast()
        {
            Console.Write("あなたは");
            CommonLibrary.WriteCastColor(this.Position);
            Console.WriteLine("です。");
        }
    }
}
