using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hanoi
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet run -Recursive <n> OR dotnet run -Iterative <n>");
                return;
            }

            string mode = args[0];
            if (!int.TryParse(args[1], out int n) || n <= 0)
            {
                Console.WriteLine("Please provide a valid positive integer for <n>.");
                return;
            }

            if (mode.Equals("-Recursive", StringComparison.OrdinalIgnoreCase))
            {
                SolveRecursive(n);
            }
            else if (mode.Equals("-Iterative", StringComparison.OrdinalIgnoreCase))
            {
                SolveIterative(n);
            }
            else
            {
                Console.WriteLine("Usage: dotnet run -Recursive <n> OR dotnet run -Iterative <n>");
            }
        }

        static void PrintState(int n, Stack<int> L, Stack<int> M, Stack<int> R)
        {
            Console.WriteLine();
            for (int i = n; i > 0; i--)
            {
                DrawLevel(i, L, M, R, n);
            }
            int pad = n - 1;
            string spaceL = pad > 0 ? new string(' ', pad) : "";
            string spaceBetween = new string(' ', pad * 2 + 1);
            Console.WriteLine($"{spaceL}(L){spaceBetween}(M){spaceBetween}(R)");
            Console.WriteLine();
        }

        static void DrawLevel(int level, Stack<int> L, Stack<int> M, Stack<int> R, int maxN)
        {
            string DrawDisk(Stack<int> stack, int lvl)
            {
                int[] arr = stack.ToArray();
                if (lvl <= arr.Length)
                {
                    int diskSize = arr[arr.Length - lvl];
                    string left = "<";
                    string right = ">";
                    string mid = new string('+', diskSize * 2 - 1);
                    string disk = left + mid + right;
                    int pad = maxN - diskSize;
                    return new string(' ', pad) + disk + new string(' ', pad);
                }
                else
                {
                    return new string(' ', maxN) + "|" + new string(' ', maxN);
                }
            }

            Console.WriteLine($"{DrawDisk(L, level)} {DrawDisk(M, level)} {DrawDisk(R, level)}");
        }

        static void SolveRecursive(int n)
        {
            Stack<int> L = new Stack<int>();
            Stack<int> M = new Stack<int>();
            Stack<int> R = new Stack<int>();

            for (int i = n; i >= 1; i--) L.Push(i);

            PrintState(n, L, M, R);
            MoveRecursive(n, L, R, M, "L", "R", "M", n, L, M, R);
        }

        static void MoveRecursive(int k, Stack<int> source, Stack<int> dest, Stack<int> aux, string sName, string dName, string aName, int totalN, Stack<int> L, Stack<int> M, Stack<int> R)
        {
            if (k == 1)
            {
                int disk = source.Pop();
                dest.Push(disk);
                Console.WriteLine($"Disk {disk} moved from ({sName}) to ({dName})");
                PrintState(totalN, L, M, R);
                Thread.Sleep(200);
                return;
            }

            MoveRecursive(k - 1, source, aux, dest, sName, aName, dName, totalN, L, M, R);
            
            int d = source.Pop();
            dest.Push(d);
            Console.WriteLine($"Disk {d} moved from ({sName}) to ({dName})");
            PrintState(totalN, L, M, R);
            Thread.Sleep(200);

            MoveRecursive(k - 1, aux, dest, source, aName, dName, sName, totalN, L, M, R);
        }

        static void SolveIterative(int n)
        {
            Stack<int> L = new Stack<int>();
            Stack<int> M = new Stack<int>();
            Stack<int> R = new Stack<int>();

            for (int i = n; i >= 1; i--) L.Push(i);

            PrintState(n, L, M, R);

            int totalMoves = (int)Math.Pow(2, n) - 1;
            
            Stack<int> src = L; string sName = "L";
            Stack<int> aux = M; string aName = "M";
            Stack<int> dest = R; string dName = "R";

            if (n % 2 == 0)
            {
                for (int i = 1; i <= totalMoves; i++)
                {
                    if (i % 3 == 1) MoveIterativeStep(src, aux, sName, aName, n, L, M, R);
                    else if (i % 3 == 2) MoveIterativeStep(src, dest, sName, dName, n, L, M, R);
                    else if (i % 3 == 0) MoveIterativeStep(aux, dest, aName, dName, n, L, M, R);
                }
            }
            else
            {
                for (int i = 1; i <= totalMoves; i++)
                {
                    if (i % 3 == 1) MoveIterativeStep(src, dest, sName, dName, n, L, M, R);
                    else if (i % 3 == 2) MoveIterativeStep(src, aux, sName, aName, n, L, M, R);
                    else if (i % 3 == 0) MoveIterativeStep(aux, dest, aName, dName, n, L, M, R);
                }
            }
        }

        static void MoveIterativeStep(Stack<int> s1, Stack<int> s2, string n1, string n2, int totalN, Stack<int> L, Stack<int> M, Stack<int> R)
        {
            if (s1.Count == 0)
            {
                int disk = s2.Pop();
                s1.Push(disk);
                Console.WriteLine($"Disk {disk} moved from ({n2}) to ({n1})");
            }
            else if (s2.Count == 0)
            {
                int disk = s1.Pop();
                s2.Push(disk);
                Console.WriteLine($"Disk {disk} moved from ({n1}) to ({n2})");
            }
            else
            {
                int top1 = s1.Peek();
                int top2 = s2.Peek();

                if (top1 < top2)
                {
                    int disk = s1.Pop();
                    s2.Push(disk);
                    Console.WriteLine($"Disk {disk} moved from ({n1}) to ({n2})");
                }
                else
                {
                    int disk = s2.Pop();
                    s1.Push(disk);
                    Console.WriteLine($"Disk {disk} moved from ({n2}) to ({n1})");
                }
            }
            
            PrintState(totalN, L, M, R);
            Thread.Sleep(200);
        }
    }
}
