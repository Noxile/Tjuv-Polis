using System.Net.Http.Headers;

namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] city = new string[25, 100];

            string profession = "";
            List<Person> persons = new List<Person>();


            // Ska flyttas till egen metod
            for(int i = 0; i < 50; i++)
            {
                int nr = Random.Shared.Next(0, 3);

                int x = Random.Shared.Next(1, 99);
                int y = Random.Shared.Next(1, 24);


                int Xdirection = 0;
                int Ydirection = 0;

                int choosenNr = Random.Shared.Next(0, 6);

                switch (choosenNr)
                {
                    case 0:
                        Xdirection = -1;
                        break;
                    case 1:
                        Ydirection = -1;
                        break;
                    case 2:
                        Xdirection = 1;
                        break;
                    case 3:
                        Ydirection = 1;
                        break;
                    case 4:
                        Xdirection = -1;
                        Ydirection = -1;
                        break;
                    case 5:
                        Xdirection = 1;
                        Ydirection = 1;
                        break;
                }



                switch (nr)
                {
                    case 0:
                        profession = "Tjuv";
                        Thief thief = new Thief(x, y, 'T', Xdirection, Ydirection);
                        persons.Add(thief);
                        break;
                    case 1:
                        profession = "Polis";
                        Police police = new Police(x, y, 'P', Xdirection, Ydirection);
                        persons.Add(police);
                        break;
                    case 2:
                        profession = "Medborgare";
                        Citizen citizen = new Citizen(x, y, 'C', Xdirection, Ydirection);
                        persons.Add(citizen);
                        break;
                }

                Console.WriteLine(profession);
                
            }

            while (true)
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

                            //    if (city[row, col] != "")
                            //    {
                            //        Console.Write(city[row, col]);
                            //    }
                            //    else if (city[row, col] == "")
                            //    {
                            //        Console.Write(' ');
                            //    }
                        }
                    }

                    Console.WriteLine();
                    
                }
                Console.ReadLine();
                foreach (Person person in persons)
                {
                   

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
                Console.Clear();
            }


        }
    }
}
