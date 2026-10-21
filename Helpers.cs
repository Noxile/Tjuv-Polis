using System;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class Helpers
    {
        public static List<Person> CreatePerson()
        {
            List<Person> persons = new List<Person>();

            for (int i = 0; i < 80; i++)
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

        public static int MovePersons(int index, List<Person> persons, string[,] city)
        {
            foreach (Person person in persons)
            { 
                if (index == 0 )
                {
                    int direction = Random.Shared.Next(1, 9);
                    switch (direction)
                    {
                        case 1:
                            person.Xdirection = -1;
                            break;
                        case 2:
                            person.Xdirection = 1;
                            break;
                        case 3:
                            person.Ydirection = -1;
                            break;
                        case 4:
                            person.Ydirection = 1;
                            break;
                        case 5:
                            person.Xdirection = -1;
                            person.Ydirection = -1;
                            break;
                        case 6:
                            person.Xdirection = 1;
                            person.Ydirection = 1;
                            break;
                        case 7:
                            person.Xdirection = -1;
                            person.Ydirection = 1;
                            break;
                        case 8:
                            person.Xdirection = 1;
                            person.Ydirection = -1;
                            break;
                    }

                }
                person.Y += person.Ydirection;
                person.X += person.Xdirection;

                if (person.X == city.GetLength(1) - 1)
                {
                    person.X = 1;
                }
                if (person.Y == city.GetLength(0) - 1)
                {
                    person.Y = 1;
                }
                if (person.X == 0)
                {
                    person.X = city.GetLength(1) - 2;
                }
                if (person.Y == 0)
                {
                    person.Y = city.GetLength(0) - 2;
                }

            }

            index++;
            if (index == 10)
            {
                index = 0;
            }
            return index;
        }

        public static string AddPerson(List<Person> persons, ConsoleKey key)
        {
            int x  = Random.Shared.Next(1, 99);
            int y = Random.Shared.Next(1, 24);

            int Xdirection = 0;
            int Ydirection = 0;

            switch(key)
            {
                case ConsoleKey.T:
                    persons.Add(new Thief(x, y, 'T', Xdirection, Ydirection));
                    return "En ny tjuv har anlänt till staden!";

                case ConsoleKey.P:
                    persons.Add(new Police(x, y, 'P', Xdirection, Ydirection));
                    return "En ny polis har anlänt till staden!";

                case ConsoleKey.C:
                    persons.Add(new Citizen(x, y, 'C', Xdirection, Ydirection));
                    return "En ny medborgare har anlänt till staden!";
            }

            return "";
        }

    }
}
