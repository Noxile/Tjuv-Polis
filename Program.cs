namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] city = new string[25, 100];

            string profession = "";

            // Ska flyttas till egen metod
            for(int i = 0; i < 50; i++)
            {
                int nr = Random.Shared.Next(0, 3);

                switch (nr)
                {
                    case 0:
                        profession = "Tjuv";
                        break;
                    case 1:
                        profession = "Polis";
                        break;
                    case 2:
                        profession = "Medborgare";
                        break;
                }

                Console.WriteLine(profession);
                
            }

            while (true)
            {
                for (int r = 0; r < city.GetLength(0); r++)
                {
                    for (int c = 0; c < city.GetLength(1); c++)
                    {
                        city[r, c] = " ";
                    }

                }

                for (int row = 0; row < city.GetLength(0); row++)
                {
                    for (int col = 0; col < city.GetLength(1); col++)
                    {

                        if (row == 0 || row == city.GetLength(0) - 1)
                        {
                            Console.Write("=");
                        }
                        else if (col == 0 || col == city.GetLength(1) - 1)
                        {
                            Console.Write("X");
                        }
                        else
                        {
                            if (city[row, col] != "")
                            {
                                Console.Write(city[row, col]);
                            }
                            else if (city[row, col] == "")
                            {
                                Console.Write(' ');
                            }

                        }
                    }
                    Console.WriteLine();
                    
                }
                Console.ReadLine();
            }


        }
    }
}
