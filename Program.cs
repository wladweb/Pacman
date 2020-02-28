using System;
using System.IO;

namespace Pacman
{
    class Program
    {
        private const char PACMAN_SYMBOL = '@';

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool isPlaying = true;

            int pacmanX, pacmanY;
            int pacmanDX = 1, pacmanDY = 0;

            char[,] map = ReadMap("map1", out pacmanX, out pacmanY);
            
            DrawMap(map);

            while (isPlaying) 
            {
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write(PACMAN_SYMBOL);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            pacmanDY = -1;
                            pacmanDX = 0;
                            break;
                        case ConsoleKey.DownArrow:
                            pacmanDY = 1;
                            pacmanDX = 0;
                            break;
                        case ConsoleKey.LeftArrow:
                            pacmanDY = 0;
                            pacmanDX = -1;
                            break;
                        case ConsoleKey.RightArrow:
                            pacmanDY = 0;
                            pacmanDX = 1;
                            break;
                    }
                }
            }
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0, lenOuter = map.GetLength(0); i < lenOuter; i++)
            {
                for (int j = 0, lenInner = map.GetLength(1); j < lenInner; j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static char[,] ReadMap(string mapName, out int pacmanX, out int pacmanY)
        {
            pacmanX = 0;
            pacmanY = 0;

            string mapFilePath = $"../../maps/{mapName}.txt";
            string[] fileLines = File.ReadAllLines(mapFilePath);
            char[,] map = new char[fileLines.Length, fileLines[0].Length];

            for (int i = 0, mapRows = map.GetLength(0); i < mapRows; i++)
            {
                for (int j = 0, mapCols = map.GetLength(1); j < mapCols; j++)
                {
                    char currentChar = fileLines[i][j];
                    map[i, j] = currentChar;

                    if (currentChar == PACMAN_SYMBOL)
                    {
                        pacmanX = j;
                        pacmanY = i;
                    }
                }
            }
            return map;
        }
    }
}
