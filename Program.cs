using System.Net.Http.Headers;

namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] city = new string[25, 100];

            string[,] prison = new string[10, 40];
            int index = 0;
            List<Person> persons = Helpers.CreatePerson();
            Jail jail = new Jail();
            string newPersonMessage = "";

            while (true)
            {
                
                string message = "";
                string message2 = "";

                Helpers.PrepareCity(persons, city, prison, jail);

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
                                jail.BustedPerson(thief);


                            }
                            else if (person is Thief && otherPerson is Citizen)
                            {
                                city[person.Y, person.X] = "%";

                                message = person.MeetPerson(otherPerson);
                            }

                            else if (person is Police && otherPerson is Citizen)
                            {
                                city[person.Y, person.X] = "&";
                                message2 = "HEJ HEJ";
                            }
                        }
                    }
                }

                foreach (Thief thief in jail.JailedThieves)
                {
                    persons.Remove(thief);
                }

                Helpers.DrawCity(city, prison);

                index = Helpers.MovePersons(index, persons,city, prison, jail);
                

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
                Console.WriteLine("Antal tjuvar i fängelset: " + jail.JailedThieves.Count);
                Console.WriteLine("=========================================================");

                Console.WriteLine(message);
                Console.WriteLine(message2);
                Console.WriteLine(newPersonMessage);
                Console.WriteLine("För att lägga till tjuv tryck 'T', polis 'P', medborgare 'C'");
                ConsoleKey key = Console.ReadKey(true).Key;

                newPersonMessage = Helpers.AddPerson(persons, key);
                //Console.ReadKey();
                Console.Clear();
            }


        }
    }
}
