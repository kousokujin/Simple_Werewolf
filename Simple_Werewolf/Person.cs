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

        public static Person ListUpMember(List<Person> JoinPlayers)
        {
            Person output = null;
            bool isDisplayed = false;
            byte LivingPeopleCount = 0;

            List<Person> LivingPeople = new List<Person>();
            foreach(Person p in JoinPlayers)
            {
                if(p.isDead == false)
                {
                    LivingPeople.Add(p);
                }
            }

            do
            {
                if (isDisplayed == false)
                {
                    byte count = 1;
                    Console.WriteLine("プレイヤーを選んでください。");
                    foreach (Person Player in JoinPlayers)
                    {
                        if (Player.isDead == false)
                        {
                            LivingPeopleCount++;
                            Console.WriteLine("[{0}]:{1}", count, Player.PlayerName);
                            count++;
                        }
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

                    if(LivingPeopleCount >= point && point > 0)
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
        }
    }
}
