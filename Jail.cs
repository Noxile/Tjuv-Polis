using System;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class Jail
    {
        public List<Thief> JailedThieves { get; set; } = new List<Thief>();

        public void BustedPerson(Thief thief)
        {
            if (thief.StolenGoods.Count > 0)
            {
                JailedThieves.Add(thief);
                thief.StolenGoods.Clear();
            }
        }
    }
}
