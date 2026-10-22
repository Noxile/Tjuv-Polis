using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class OutputMethods
    {
        public static Queue<string> OutputMsg(Jail jail, Queue<string> queue, int robbedPeople, int arrestedThieves, int amountOfHello)
        {
            Console.WriteLine($"Antal rånade invånare: {robbedPeople}");
            Console.WriteLine($"Antal gripna tjuvar: {arrestedThieves}");
            Console.WriteLine($"Antal HEJ HEJ hälsningar: {amountOfHello}");
            Console.WriteLine("Antal tjuvar i fängelset: " + jail.JailedThieves.Count);
            Console.WriteLine("=====================Nyhetsflöde=========================");

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

            Console.WriteLine("======================Kommandon==========================".PadRight(Console.WindowWidth - 1));

            Console.WriteLine("För att lägga till tjuv tryck 'T', polis 'P', medborgare 'C'. För att byta vy tryck 'G'.");

            return queue;
        }
    }
}
