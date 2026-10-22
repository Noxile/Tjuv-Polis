using System;
using System.Collections.Generic;
using System.Text;

namespace Tjuv_Polis
{
    internal class CityMethods
    {
        public static void PrepareCity(List<Person> persons, string[,] city)
        {
            for (int r = 0; r < city.GetLength(0); r++)
            {
                for (int c = 0; c < city.GetLength(1); c++)
                {
                    city[r, c] = " ";
                }
            }

            foreach (Person person in persons)
            {
                city[person.Y, person.X] = person.Symbol.ToString();
            }
        }


        public static void DrawCity(string[,] city, string building)
        {

            for (int row = 0; row < city.GetLength(0); row++)
            {

                for (int col = 0; col < city.GetLength(1); col++)
                {
                    if(row  == 0 && col == 1)
                    {
                        Console.Write(building);
                        col = col + building.Length-1;

                    }
                    else if (row == 0 || row == city.GetLength(0) - 1)
                    {
                        Console.Write("=");
                    }
                    else if (col == 0 || col == city.GetLength(1) - 1)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(city[row, col]);
                    }
                }

                Console.WriteLine();

            }
        }
    }
}
