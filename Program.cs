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

            List<Person> persons = Helpers.CreatePerson();
            Jail jail = new Jail();

            PoorHouse poorhouse1 = new PoorHouse();
            string newPersonMessage = "";

            Console.CursorVisible = false;
            bool systemLog = false;

            while (true)
            {
                string message = null;

                Helpers.PrepareCity(persons, city);
                Helpers.PrepareCity(jail.JailedThieves, prison);
                Helpers.PrepareCity(poorhouse1.PoorPeople, poorhouse);

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

                                jail.BustedPerson(thief);


                            }
                            else if (person is Thief && otherPerson is Citizen)
                            {
                                city[person.Y, person.X] = "%";

                                message = person.MeetPerson(otherPerson);
                                queue.Enqueue(message);
                            }

                            else if (person is Police && otherPerson is Citizen)
                            {
                                city[person.Y, person.X] = "&";
                                message = "HEJ HEJ";
                                queue.Enqueue(message);
                            }
                        }
                    }

                    if(person is Citizen citizen)
                    {
                        poorhouse1.PoorPerson(citizen);
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


                foreach (Citizen citizen1 in poorhouse1.PoorPeople)
                {
                    persons.Remove(citizen1);
                }
                Console.SetCursorPosition(0, 0);



                Console.SetCursorPosition(0, 0);

                if (!systemLog)
                {
                    Helpers.DrawCity(city);
                    Helpers.DrawCity(prison);
                    Helpers.DrawCity(poorhouse);
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






                index = Helpers.MovePersons(index, persons, city);
                Helpers.MovePersons(index, jail.JailedThieves, prison);
                Helpers.MovePersons(index, poorhouse1.PoorPeople, poorhouse);

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
                Console.WriteLine($"Antal poliser: {policeCount}".PadRight(Console.WindowWidth - 1));
                Console.WriteLine($"Antal tjuvar: {thiefCount}".PadRight(Console.WindowWidth - 1));
                Console.WriteLine($"Antal medborgare: {citizenCount}".PadRight(Console.WindowWidth - 1));
                Console.WriteLine("Antal tjuvar i fängelset: " + jail.JailedThieves.Count);
                Console.WriteLine("=========================================================");


                while (queue.Count > 10)
                {
                    queue.Dequeue();
                }
                if (queue.Count > 0)
                {
                    foreach (var msg in queue.Reverse())
                    {
                        Console.WriteLine(msg.PadRight(Console.WindowWidth - 1));
                    }
                }

                //Console.WriteLine(newPersonMessage);
                Console.WriteLine("För att lägga till tjuv tryck 'T', polis 'P', medborgare 'C'");

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
                        newPersonMessage = Helpers.AddPerson(persons, key);

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
