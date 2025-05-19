namespace SudokuRowExtension

{
    class Program
    {
        public static int[,] ReadBoard()
        {
            int[,] board = new int[9, 9];

            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine($"\nEnter 9 numbers for Row {row + 1} (1 to 9):");

                for (int col = 0; col < 9; col++)
                {
                    int value;
                    Console.Write($"Row {row + 1}, Column {col + 1}: ");
                    while (!int.TryParse(Console.ReadLine(), out value) || value < 1 || value > 9)
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
                        Console.Write($"Row {row + 1}, Column {col + 1}: ");
                    }
                    board[row, col] = value;
                }
            }

            return board;
        }

        public static bool IsValidSet(int[] group)
        {
            if (group.Length != 9) return false;
            int[] counts = new int[9];
            foreach (int num in group)
            {
                if (num < 1 || num > 9) return false;
                counts[num - 1]++;
                if (counts[num - 1] > 1) return false;
            }
            return true;
        }

        public static bool ValidateSudokuBoard(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                int[] row = new int[9];
                for (int j = 0; j < 9; j++)
                    row[j] = board[i, j];
                if (!IsValidSet(row))
                {
                    Console.WriteLine($"Row {i + 1} is invalid.");
                    return false;
                }
                int[] column = new int[9];
                for (int j = 0; j < 9; j++)
                    column[j] = board[j, i];
                if (!IsValidSet(column))
                {
                    Console.WriteLine($"Column {i + 1} is invalid.");
                    return false;
                }
            }

            for (int row = 0; row < 9; row += 3)
            {
                for (int col = 0; col < 9; col += 3)
                {
                    int[] block = new int[9];
                    int index = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            block[index++] = board[row + i, col + j];
                        }
                    }

                    if (!IsValidSet(block))
                    {
                        Console.WriteLine($"Block starting at ({row + 1},{col + 1}) is invalid.");
                        return false;
                    }
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            //int[,] board = ReadBoard();

            int[,] board = {
                { 7,9,2,1,5,4,3,8,6},
                {6,4,3,8,2,7,1,5,9 },
                {8,5,1,3,9,6,7,2,4 },
                {2,6,5,9,7,3,8,4,1 },
                {4,8,9,5,6,1,2,7,3 },
                {3,1,7,4,8,2,9,6,5 },
                {1,3,6,7,4,8,5,9,2 },
                {9,7,4,2,1,5,6,3,8 },
                {5,2,8,6,3,9,4,1,7}
            };

            Console.WriteLine("Validating entire Sudoku board...\n");

            if (ValidateSudokuBoard(board))
                Console.WriteLine("\nThe given Sudoku board is VALID.");
            else
                Console.WriteLine("\nThe given Sudoku board is INVALID.");
        }
    }
}