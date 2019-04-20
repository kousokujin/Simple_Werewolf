using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Werewolf
{
    class Wolf : Person
    {
        public Wolf(string name)
        {
            innerPlayerName = name;
        }
        override public void NightAction(List<Person> people)
        {
           
        }
    }
}
