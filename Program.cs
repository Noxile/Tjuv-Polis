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

            int Xdirection = 0;
            int Ydirection = 0;
            // Ska flyttas till egen metod
            for (int i = 0; i < 50; i++)
            {
                int nr = Random.Shared.Next(0, 3);

                int x = Random.Shared.Next(1, 99);
                int y = Random.Shared.Next(1, 24);


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
                string message = "";
                string message2 = "";

                foreach (Person person in persons)
                {
                    city[person.Y, person.X] = person.Symbol.ToString();
                }

                foreach (Person person in persons)
                {
                    foreach (Person otherPerson in persons)
                    {
                        if (person != otherPerson && person.X == otherPerson.X && person.Y == otherPerson.Y)
                        {
                            if (person is Police police && otherPerson is Thief thief)
                            {
                                city[person.Y, person.X] = "#";
                                if (thief.StolenGoods.Count > 0)
                                {
                                    police.ConfiscatedGoods.AddRange(thief.StolenGoods);
                                    thief.StolenGoods.Clear();
                                    message = "Polisen grep en tjuv och beslagtog alla stulna föremål.";
                                }

                            }
                            else if (person is Thief thief2 && otherPerson is Citizen citizen)
                            {
                                city[person.Y, person.X] = "%";

                                if (citizen.Belongings.Count > 0)
                                {
                                    int randomIndex = Random.Shared.Next(0, citizen.Belongings.Count);

                                    Item item = citizen.Belongings[randomIndex];

                                    thief2.StolenGoods.Add(citizen.Belongings[randomIndex]);

                                    citizen.Belongings.RemoveAt(randomIndex);

                                    message = "Tjuven tog medborgarens" + item.Name;
                                }

                               
                            }
                            else if (person is Police && otherPerson is Citizen)
                            {
                                city[person.Y, person.X] = "&";
                                message2 = "HEJ HEJ";
                            }
                        }
                    }
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
                        }
                    }

                    Console.WriteLine();
                    
                }
                
                foreach (Person person in persons)
                {
                    person.Xdirection = 0;
                    person.Ydirection = 0;

                    int choosenNr = Random.Shared.Next(0, 6);

                    switch (choosenNr)
                    {
                        case 0:
                            person.Xdirection = -1;
                            break;
                        case 1:
                            person.Ydirection = -1;
                            break;
                        case 2:
                            person.Xdirection = 1;
                            break;
                        case 3:
                            person.Ydirection = 1;
                            break;
                        case 4:
                            person.Xdirection = -1;
                            person.Ydirection = -1;
                            break;
                        case 5:
                            person.Xdirection = 1;
                            person.Ydirection = 1;
                            break;
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
                Console.WriteLine(message );
                Console.WriteLine( message2);


                //Console.ReadKey( );
                Console.Clear();
            }


        }
    }
}
