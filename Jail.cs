using System;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class Jail
    {
        public List<Person> JailedThieves { get; set; } = new List<Person>();

        public int BustedPerson(Thief thief, int arrestedThieves)
        {
            if (thief.StolenGoods.Count > 0)
            {
                thief.ReleaseTime = DateTime.Now.AddSeconds(thief.StolenGoods.Count * 10);

                JailedThieves.Add(thief);

                thief.StolenGoods.Clear();
                thief.X = 1;
                thief.Y = 1;
                arrestedThieves++;
            }
            return arrestedThieves;
        }
    }
}
