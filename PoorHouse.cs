using System;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class PoorHouse
    {
        public List<Person> PoorPeople { get; set; } = new List<Person>();

        public void PoorPerson(Citizen citizen)
        {
            // Om medborgaren inte har några tillhörigheter, läggs medborgaren till i listan över fattiga personer och flyttas till fattighuset

            if (citizen.Belongings.Count == 0)
            {
                PoorPeople.Add(citizen);
                citizen.X = 1;
                citizen.Y = 1;
            }
        }
    }
}
