using System;
using System.IO;
using System.Net.NetworkInformation;

namespace Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            char[,] map = ReadMap("map.txt");
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            int pacmanX = 1;
            int pacmanY = 1;

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write("@");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(32, 0);
                Console.Write(pressedKey.KeyChar);

                pressedKey = Console.ReadKey(true);

                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map);
            }
        }
        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("map.txt");

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int y = 0; y < file.Length; y++)
                for (int x = 0; x < file[y].Length; x++)
                    map[x, y] = file[y][x];

            return map;
        }
        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }
        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];

            if (nextPacmanPositionX >= 0 && nextPacmanPositionX < map.GetLength(0) &&
                nextPacmanPositionY >= 0 && nextPacmanPositionY < map.GetLength(1) &&
                map[nextPacmanPositionX, nextPacmanPositionY] == ' ')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;
            }
        }
        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
                direction[1] = -1;
            else if (pressedKey.Key == ConsoleKey.DownArrow)
                direction[1] = 1;
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
                direction[0] = -1;
            else if (pressedKey.Key == ConsoleKey.RightArrow)
                direction[0] = 1;

            return direction;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
                if (line.Length > maxLength)
                    maxLength = line.Length;

            return maxLength;
        }
    }
}