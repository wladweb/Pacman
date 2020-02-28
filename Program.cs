using System;
using System.IO;
using System.Threading;

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
            int pacmanDX = 0, pacmanDY = 1;

            char[,] map = ReadMap("map1", out pacmanX, out pacmanY);
            
            DrawMap(map);

            while (isPlaying) 
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ChangeDirection(keyInfo, ref pacmanDX, ref pacmanDY);                    
                }

                if (map[pacmanX + pacmanDX, pacmanY + pacmanDY] != '#')
                {
                    Move(ref pacmanX, ref pacmanY, pacmanDX, pacmanDY);
                }
                Thread.Sleep(200);
            }
        }

        static void ChangeDirection(ConsoleKeyInfo keyInfo, ref int dx, ref int dy) 
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    dx = -1;
                    dy = 0;
                    break;
                case ConsoleKey.DownArrow:
                    dx = 1;
                    dy = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    dx = 0;
                    dy = -1;
                    break;
                case ConsoleKey.RightArrow:
                    dx = 0;
                    dy = 1;
                    break;
            }
        }

        static void Move(ref int x, ref int y, int dx, int dy)
        {
            Console.SetCursorPosition(y, x);
            Console.Write(" ");

            x += dx;
            y += dy;

            Console.SetCursorPosition(y, x);
            Console.Write(PACMAN_SYMBOL);
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
                        pacmanX = i;
                        pacmanY = j;
                    }
                }
            }
            return map;
        }
    }
}
