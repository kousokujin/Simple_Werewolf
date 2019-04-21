using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class Guardman : Person
    {
        public Guardman(string name) : base(name)
        {
            //innerPlayerName = name;
        }

        public override bool IsWerewolf
        {
            get
            {
                return false;
            }
        }

        public override PlayerPosition Position
        {
            get
            {
                return PlayerPosition.Guardman;
            }
        }

        public override bool IsWerewolfPosition
        {
            get
            {
                return false;
            }
        }

        override public void NightAction(List<Person> people)
        {
            Console.Write("あなたは");
            DisplayLibrary.ColorConsole("狩人", ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine("です。");

            Person target = ListUpMember(people, "今日の守る人を選んでください。");
            target.isProtect = true;
        }
    }
}
