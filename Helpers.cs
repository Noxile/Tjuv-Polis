using System;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class Helpers
    {
        public static List<Person> SkapaPerson()
        {
            List<Person> persons = new List<Person>();

            for (int i = 0; i < 50; i++)
            {
                int nr = Random.Shared.Next(0, 3);

                int x = Random.Shared.Next(1, 99);
                int y = Random.Shared.Next(1, 24);

                int Xdirection = 0;
                int Ydirection = 0;

                switch (nr)
                {
                    case 0:
                        Thief thief = new Thief(x, y, 'T', Xdirection, Ydirection);
                        persons.Add(thief);
                        break;
                    case 1:
                        Police police = new Police(x, y, 'P', Xdirection, Ydirection);
                        persons.Add(police);
                        break;
                    case 2:
                        Citizen citizen = new Citizen(x, y, 'C', Xdirection, Ydirection);
                        persons.Add(citizen);
                        break;
                }
            }
            return persons;
        }
    }
}
