using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class Wolf : Person
    {
        public Wolf(string name) : base(name)
        {
            //innerPlayerName = name;
        }

        public override bool IsWerewolf
        {
            get
            {
                return true;
            }
        }

        public override PlayerPosition Position
        {
            get
            {
                return PlayerPosition.Werewolf;
            }
        }

        public override bool IsWerewolfPosition
        {
            get
            {
                return true;
            }
        }

        override public void NightAction(List<Person> people)
        {
            Console.Write("あなたは");
            DisplayLibrary.ColorConsole("人狼", ConsoleColor.White, ConsoleColor.Red);
            Console.WriteLine("です。");

            Person target = ListUpMember(people,"今日のターゲットを選んでください。");
            target.isTarget = true;
        }
    }
}
