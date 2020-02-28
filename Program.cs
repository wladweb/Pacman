using System;
using System.IO;
using System.Threading;

namespace Pacman
{
    class Program
    {
        private const char PACMAN_SYMBOL = '@';
        private const char GHOST_SYMBOL = '$';

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool isPlaying = true;

            int allDots = 0;
            int collectedDots = 0;

            int pacmanX, pacmanY;
            int pacmanDX = 0, pacmanDY = 1;
            bool isAlive = true;

            int ghostX, ghostY;
            int ghostDX = 0, ghostDY = -1;

            char[,] map = ReadMap("map1", out pacmanX, out pacmanY, out ghostX, out ghostY, ref allDots);
            
            DrawMap(map);

            while (isPlaying) 
            {
                Console.SetCursorPosition(0, 20);
                Console.WriteLine($"Collected: {collectedDots}/{allDots}");

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ChangeDirection(keyInfo, ref pacmanDX, ref pacmanDY);                    
                }

                if (map[pacmanX + pacmanDX, pacmanY + pacmanDY] != '#')
                {
                    Move(' ', PACMAN_SYMBOL, ref pacmanX, ref pacmanY, pacmanDX, pacmanDY);
                    CollectDots(map, pacmanX, pacmanY, ref collectedDots);
                }

                if (map[ghostX + ghostDX, ghostY + ghostDY] != '#')
                {
                    Move('.', GHOST_SYMBOL, ref ghostX, ref ghostY, ghostDX, ghostDY);
                }
                else 
                {
                    //
                }

                Thread.Sleep(200);

                if (collectedDots == allDots) 
                {
                    isPlaying = false;
                }
            }

            Console.SetCursorPosition(0, 21);

            if (collectedDots == allDots)
            {
                Console.WriteLine("You win");
            }
        }

        static void CollectDots(char[,] map, int pacmanX, int pacmanY, ref int collectedDots)
        {
            if (map[pacmanX, pacmanY] == '.')
            {
                collectedDots++;
                map[pacmanX, pacmanY] = ' ';
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

        static void Move(char previousSymbol, char symbol, ref int x, ref int y, int dx, int dy)
        {
            Console.SetCursorPosition(y, x);
            Console.Write(previousSymbol);

            x += dx;
            y += dy;

            Console.SetCursorPosition(y, x);
            Console.Write(symbol);
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

        static char[,] ReadMap(string mapName, out int pacmanX, out int pacmanY, out int ghostX, out int ghostY, ref int allDots)
        {
            pacmanX = 0;
            pacmanY = 0;
            ghostX = 0;
            ghostY = 0;

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
                        map[i, j] = '.';
                    }
                    else if (map[i, j] == '$')
                    {
                        ghostX = i;
                        ghostY = j;
                        map[i, j] = '.';
                    }
                    else if (map[i, j] == ' ')
                    {
                        map[i, j] = '.';
                        allDots++;
                    }
                }
            }
            return map;
        }
    }
}
