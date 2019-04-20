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

            //persons[2].isDead = true;

            Person per = Person.ListUpMember(persons);
            //List<string> str = persons.Select(x => x.PlayerName).ToList();
            //Console.WriteLine(per.PlayerName);
            //int count = DisplayLibrary.SelectDisplay(str,3);
            Console.WriteLine(per.PlayerName);
            Console.ReadKey();
        }
    }
}
