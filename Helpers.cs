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


        public static void PrepareCity(List<Person> persons, string[,] city)
        {
            for (int r = 0; r < city.GetLength(0); r++)
            {
                for (int c = 0; c < city.GetLength(1); c++)
                {
                    city[r, c] = " ";
                }
            }

            foreach (Person person in persons)
            {
                city[person.Y, person.X] = person.Symbol.ToString();
            }
        }

        public static void DrawCity(string[,] city)
        {
            //for (int r = 0; r < city.GetLength(0); r++)
            //{
            //    for (int c = 0; c < city.GetLength(1); c++)
            //    {
            //        city[r, c] = " ";
            //    }
            //}

            //foreach (Person person in persons)
            //{
            //    city[person.Y, person.X] = person.Symbol.ToString();
            //}

            for (int row = 0; row < city.GetLength(0); row++)
            {

                for (int col = 0; col < city.GetLength(1); col++)
                {

                    if (row == 0 || row == city.GetLength(0) - 1)
                    {
                        Console.Write("=");
                    }
                    else if (col == 0 || col == city.GetLength(1) - 1)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(city[row, col]);
                    }
                }

                Console.WriteLine();

            }
        }

    }
}
