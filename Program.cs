using System.Collections;
using System.ComponentModel.Design;
using System.Net.Http.Headers;

namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            string[,] city = new string[25, 100];

            string[,] prison = new string[10, 20];

            string[,] poorhouse = new string[10, 50];

            Queue<string> queue = new Queue<string>();


            int index = 0;
            int robbedPeople = 0;
            int arrestedThieves = 0;
            int amountOfHello = 0;

            List<Person> persons = PersonMethods.CreatePerson();
            Jail jail = new Jail();

            PoorHouse house = new PoorHouse();
            string newPersonMessage = "";

            Console.CursorVisible = false;
            bool systemLog = false;

            while (true)
            {
                string message = null;

                CityMethods.PrepareCity(persons, city);
                CityMethods.PrepareCity(jail.JailedThieves, prison);
                CityMethods.PrepareCity(house.PoorPeople, poorhouse);

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
                                queue.Enqueue(message);

                                arrestedThieves = jail.BustedPerson(thief, arrestedThieves);


                            }
                            else if (person is Thief && otherPerson is Citizen)
                            {
                                city[person.Y, person.X] = "%";

                                message = person.MeetPerson(otherPerson);
                                queue.Enqueue(message);
                                robbedPeople++;
                            }

                            else if (person is Police && otherPerson is Citizen)
                            {
                                city[person.Y, person.X] = "&";
                                message = "Polisen säger HEJ HEJ till medborgaren";
                                queue.Enqueue(message);
                                amountOfHello++;
                            }
                        }
                    }

                    if(person is Citizen citizen)
                    {
                        house.PoorPerson(citizen);
                    }
                }

                foreach (Thief thief in jail.JailedThieves)
                {
                    persons.Remove(thief);
                }

                for (int i = jail.JailedThieves.Count - 1; i >= 0; i--)
                {
                    Thief thief = (Thief)jail.JailedThieves[i];

                    if (DateTime.Now >= thief.ReleaseTime)
                    {
                        jail.JailedThieves.Remove(thief);

                        thief.X = 1;
                        thief.Y = 1;

                        persons.Add(thief);
                    }
                }


                foreach (Citizen citizen1 in house.PoorPeople)
                {
                    persons.Remove(citizen1);
                }

                Console.SetCursorPosition(0, 0);


                if (!systemLog)
                {
                    CityMethods.DrawCity(city, "Staden");
                    CityMethods.DrawCity(prison, "Fängelse");
                    CityMethods.DrawCity(poorhouse, "Fattighuset");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("========== PERSONER I STADEN ==========");

                    int personIndex = 1;

                    foreach (Person person in persons)
                    {
                        if (person is Thief)
                        {
                            Console.WriteLine("Person " + personIndex + ": Tjuv - " + person.X + ", " + person.Y);
                        }
                        else if (person is Police)
                        {
                            Console.WriteLine("Person " + personIndex + ": Polis - " + person.X + ", " + person.Y);
                        }
                        else if (person is Citizen)
                        {
                            Console.WriteLine("Person " + personIndex + ": Medborgare - " + person.X + ", " + person.Y);
                        }

                        personIndex++;
                    }
                }

                index = PersonMethods.MovePersons(index, persons, city);
                PersonMethods.MovePersons(index, jail.JailedThieves, prison);
                PersonMethods.MovePersons(index, house.PoorPeople, poorhouse);

                PersonMethods.CountPeople(persons);

                queue = OutputMethods.OutputMsg(jail, queue, robbedPeople, arrestedThieves, amountOfHello);

                if (message != null)
                {
                    Thread.Sleep(2000);
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.G)
                    {
                        systemLog = !systemLog;
                        Console.Clear();
                    }
                    else
                    {
                        newPersonMessage = PersonMethods.AddPerson(persons, key);

                        if (newPersonMessage != "")
                        {
                            queue.Enqueue(newPersonMessage);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(200);
                }

                //ConsoleKey key = Console.ReadKey(true).Key;

                //newPersonMessage = Helpers.AddPerson(persons, key);


            }


        }
    }
}
