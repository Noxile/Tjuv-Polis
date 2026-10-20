using System.Net.Http.Headers;

namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] city = new string[25, 100];

            List<Person> persons = Helpers.CreatePerson();
            Jail jail = new Jail();

            while (true)
            {
                
                string message = "";
                string message2 = "";

                Helpers.PrepareCity(persons, city);

                foreach (Person person in persons)
                {
                    foreach (Person otherPerson in persons)
                    {
                        if (person != otherPerson && person.X == otherPerson.X && person.Y == otherPerson.Y)
                        {
                            if (person is Police police && otherPerson is Thief thief)
                            {
                                city[person.Y, person.X] = "#";

                                message = person.MeetPerson(otherPerson);
                                jail.BustedPerson(thief, persons);

                                //jail.jailedThieves.Add(thief);
                                //persons.Remove(thief);
                                //jail.Prisoners++;
                                

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

                                    message = "Tjuven tog medborgarens " + item.Name;
                                    
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

                Helpers.DrawCity(city);

                //foreach (Thief thief in theivesToJail)
                //{
                //    jail.Add(thief);
                //    persons.Remove(thief);
                //}

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

                int policeCount = 0;
                int thiefCount = 0;
                int citizenCount = 0;

                foreach (Person person in persons)
                {

                    if (person is Police)
                    {
                        policeCount++;
                    }
                    else if(person is Thief)
                    {
                        thiefCount++;
                    }
                    else if (person is Citizen)
                    {
                        citizenCount++;
                    }
                }
                Console.WriteLine($"Antal poliser: {policeCount}");
                Console.WriteLine($"Antal tjuvar: {thiefCount}");
                Console.WriteLine($"Antal medborgare: {citizenCount}");
                Console.WriteLine( "Antal tjuvar i fängelse:" + jail.jailedThieves.Count);
                Console.WriteLine("=========================================================");

                Console.WriteLine(message);
                Console.WriteLine(message2);
                Console.ReadKey();
                Console.Clear();
            }


        }
    }
}
