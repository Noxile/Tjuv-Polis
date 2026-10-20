using System;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class Person
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public int Xdirection { get; set; }
        public int Ydirection { get; set; }



        public Person(int x, int y, char symbol, int xDirection, int yDirection)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Xdirection = xDirection;
            Ydirection = yDirection;

           
        }

        public virtual string MeetPerson(Person otherPerson)
        {
            return "";
        }
    }
    class Thief : Person
    {
        public List<Item> StolenGoods { get; set; } = new List<Item>();
        public Thief(int x, int y, char symbol, int xDirection, int yDirection) : base(x, y, symbol, xDirection, yDirection)
        {



        }
        public override string MeetPerson(Person otherPerson)
        {
            if (otherPerson is Citizen citizen && citizen.Belongings.Count > 0)
            {
                int randomIndex = Random.Shared.Next(0, citizen.Belongings.Count);

                Item item = citizen.Belongings[randomIndex];

                StolenGoods.Add(item);
                citizen.Belongings.RemoveAt(randomIndex);

                return "Thief stole the citizen's " + item.Name;
            }

            return "";
        }
    }
    class Police : Person
    {
        public List<Item> ConfiscatedGoods { get; set; } = new List<Item>();
        public Police(int x, int y, char symbol, int xDirection, int yDirection) : base(x, y, symbol, xDirection, yDirection)
        {
        }

        public override string MeetPerson(Person otherPerson)
        {
            if (otherPerson is Thief thief && thief.StolenGoods.Count > 0)
            {
                ConfiscatedGoods.AddRange(thief.StolenGoods);
                
                return "Polisen grep en tjuv och beslagtog alla stulna föremål.";
            }
            else
            {
                return "Polisen möter en tjuv men fanns inget att beslagta.";
            }


        }
    }
    class Citizen : Person
    {
        public List<Item> Belongings { get; set; } = new List<Item>();
        public Citizen(int x, int y, char symbol, int xDirection, int yDirection) : base(x, y, symbol, xDirection, yDirection)
        {
            Belongings.Add(new Item("Nycklar"));
            Belongings.Add(new Item("Mobiltelefon"));
            Belongings.Add(new Item("Pengar"));
            Belongings.Add(new Item("Klocka"));
        }
    }

}
