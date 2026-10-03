# Tower of Hanoi

A C# console implementation of the classic **Tower of Hanoi** problem with both recursive and iterative solving strategies.

The program visualizes each move directly in the terminal, making it possible to compare the two approaches step by step.

## Features

- Recursive Tower of Hanoi solver
- Iterative Tower of Hanoi solver
- ASCII visualization of all three towers
- Move-by-move console output
- Input validation for solver mode and disk count
- Automatic handling of odd and even disk counts in the iterative algorithm

## Tech stack

- C#
- .NET 10

## Run the project

The repository includes a `global.json` pinned to .NET SDK 10.0.103.

### Recursive solution

```bash
dotnet run -- -Recursive 4
```

### Iterative solution

```bash
dotnet run -- -Iterative 4
```

Replace `4` with any positive number of disks.

## How it works

The recursive implementation follows the standard divide-and-conquer approach:

1. Move the top `n - 1` disks to the auxiliary tower.
2. Move the largest disk to the destination tower.
3. Move the `n - 1` disks from the auxiliary tower to the destination.

The iterative solver performs the same legal sequence without recursive calls. It calculates the total number of required moves as:

```text
2^n - 1
```

It then cycles through legal moves between tower pairs, with the order adjusted depending on whether the number of disks is odd or even.

## Example output

```text
Disk 1 moved from (L) to (R)
Disk 2 moved from (L) to (M)
Disk 1 moved from (R) to (M)
```

After each move, the current tower state is redrawn in the console.

## Project structure

```text
.
├── Program.cs
├── Hanoi.csproj
└── global.json
```

This project is a compact example of recursion, stacks, iterative algorithm design, and console visualization in C#.
