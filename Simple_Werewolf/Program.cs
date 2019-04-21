using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format("player{0}", i);
                Wolf temp= new Wolf(name);
                persons.Add(temp);
            }
            Paychic v = new Paychic("player5");
            v.Executioned = persons[0];
            persons.Add(v);

            //persons[2].isDead = true;

            persons[5].NightAction(persons);
            Person.ChangeDisplay(persons[5].PlayerName, persons[1].PlayerName);

            Console.ReadKey();
        }
    }
}
