using System.Collections;
using System.ComponentModel.Design;
using System.Net.Http.Headers;

namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Skapar array för att representera staden

            string[,] city = new string[25, 100];

            string[,] prison = new string[10, 20];

            string[,] poorhouse = new string[10, 50];

            Queue<string> queue = new Queue<string>();

            // Skapar variabler för att hålla reda på index.

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

            // Startar huvudloopen för programmet

            while (true)
            {
                string message = null;

                // Förbereder staden, fängelset och fattighuset för att ritas ut

                CityMethods.PrepareCity(persons, city);
                CityMethods.PrepareCity(jail.JailedThieves, prison);
                CityMethods.PrepareCity(house.PoorPeople, poorhouse);

                // Loopar igenom alla personer i staden och kollar om de möter varandra

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

                // Släpper tjuvar som har avtjänat sitt straff och flyttar fattiga personer till fattighuset

                persons = PersonMethods.ReleaseThiefs(persons, jail);

                persons = PersonMethods.MoveToPoorhouse(persons, house);

                Console.SetCursorPosition(0, 0);

                // Ritar ut staden, fängelset och fattighuset eller visar systemlogg beroende på användarens val

                if (!systemLog)
                {
                    CityMethods.DrawCity(city, "Staden");
                    CityMethods.DrawCity(prison, "Fängelse");
                    CityMethods.DrawCity(poorhouse, "Fattighuset");
                }
                else
                {
                    OutputMethods.ShowSystemLog(persons);
                }

                // Flyttar personer till sina nya positioner och räknar antalet personer i staden

                index = PersonMethods.MovePersons(index, persons, city);
                PersonMethods.MovePersons(index, jail.JailedThieves, prison);
                PersonMethods.MovePersons(index, house.PoorPeople, poorhouse);

                PersonMethods.CountPeople(persons);

                queue = OutputMethods.OutputMsg(jail, queue, robbedPeople, arrestedThieves, amountOfHello);

                if (message != null)
                {
                    Thread.Sleep(2000);
                }

                // Kollar om användaren trycker på en tangent och hanterar input

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

            }


        }
    }
}
