using System;
using System.IO;

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] map = ReadMap("map1");
            DrawMap(map);
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

        static char[,] ReadMap(string mapName)
        {
            string mapFilePath = $"../../maps/{mapName}.txt";
            string[] fileLines = File.ReadAllLines(mapFilePath);

            char[,] map = new char[fileLines.Length, fileLines[0].Length];

            for (int i = 0, mapRows = map.GetLength(0); i < mapRows; i++)
            {
                for (int j = 0, mapCols = map.GetLength(1); j < mapCols; j++)
                {
                    map[i, j] = fileLines[i][j];
                }
            }

            return map;
        }
    }
}
