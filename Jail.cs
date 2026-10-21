using System;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class Jail
    {
        public List<Person> JailedThieves { get; set; } = new List<Person>();

        public void BustedPerson(Thief thief)
        {
            if (thief.StolenGoods.Count > 0)
            {
                JailedThieves.Add(thief);

                // thief.JailTime = thief.StolenGoods.Count * 10; ;

                thief.StolenGoods.Clear();
                thief.X = 1;
                thief.Y = 1;
            }
        }
    }
}
