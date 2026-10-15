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
    }


}
