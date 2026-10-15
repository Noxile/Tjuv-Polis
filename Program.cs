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
                

                switch (nr)
                {
                    case 0:
                        profession = "Tjuv";
                        Thief thief = new Thief(x, y, 'T', 0, 0);
                        persons.Add(thief);
                        break;
                    case 1:
                        profession = "Polis";
                        Police police = new Police(x, y, 'P', 0, 0);
                        persons.Add(police);
                        break;
                    case 2:
                        profession = "Medborgare";
                        Citizen citizen = new Citizen(x, y, 'C', 0, 0);
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
            }


        }
    }
}
