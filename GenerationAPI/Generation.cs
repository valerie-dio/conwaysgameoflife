namespace GenerationAPI
{
    public class Cells
    {
        public int x { get; set; }
        public int y { get; set; }
        public int value { get; set; }
    }


    public static class Generation
    {
        public const int HEIGHT = 15;
        public const int WIDTH = 15;

        public static bool Start { get; set; } = false;
        public static bool Reset { get; set; } = false;

        private static int[,]? _currentGenerationCells;
        public static int[,] CurrentGenerationCells
        {
            get
            {
                if (_currentGenerationCells == null)
                {
                    _currentGenerationCells = new int[HEIGHT, WIDTH];
                }
                return _currentGenerationCells;
            }
            set
            {
                if(_currentGenerationCells == null)
                {
                    _currentGenerationCells = new int[HEIGHT, WIDTH];
                }
                value = _currentGenerationCells;
            }
        }

        private static int[,]? _nextGenerationCells;
        public static int[,] NextGenerationCells
        {
            get
            {
                if (_nextGenerationCells == null)
                {
                    _nextGenerationCells = new int[HEIGHT, WIDTH];
                }
                return _nextGenerationCells;
            }
            private set
            {
                if (_nextGenerationCells == null)
                {
                    _nextGenerationCells = new int[HEIGHT, WIDTH];
                }
                value = _nextGenerationCells;
            }
        }

        private static List<Cells>? _cellList;
        public static List<Cells> CellList
        {
            get
            {
                if (_cellList == null)
                {
                    _cellList = new List<Cells>();
                }
                return _cellList;
            }
            private set
            {
                if (_cellList == null)
                {
                    _cellList = new List<Cells>();
                }
                value = _cellList;
            }
        }


        /// <summary>
        /// Initializes a new Game of Life.
        /// </summary>
        /// <param name="heigth">Heigth of the cell field.</param>
        /// <param name="width">Width of the cell field.</param>
        
        public static void Generate()
        {
            GenerateNextGeneration();
            System.Threading.Thread.Sleep(300);
        }

        /// <summary>
        /// Initializes the field with random boolean values
        /// </summary>
        public static void GenerateInitialCells()
        {
            Random generator = new Random();
            int cell;
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    //30% live cells
                    if (generator.Next(1, 101) < 70)
                        cell = 0;
                    else
                        cell = 1;
                    CurrentGenerationCells[i, j] = cell;
                }
            }
        }

        /// <summary>
        /// Converts 2D array to List
        /// </summary>
        /// <param name="generation"></param>
        /// <returns></returns>
        public static List<Cells> ConvertToList(int[,] generation)
        {
            CellList.Clear();

            for (int i = 0; i < generation.GetLength(0); i++)
            {
                for(int j = 0; j < generation.GetLength(1); j++)
                {
                    Cells cell = new Cells()
                    {
                        x = i,
                        y = j,
                        value = generation[i, j]
                    };

                    CellList.Add(cell);
                }
            }

            return CellList;
        }

        /// <summary>
        /// Advances the game by one generation
        /// </summary>
        private static void GenerateNextGeneration()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    int numOfAliveNeighbors = GetNeighbors(i, j);

                    if (CurrentGenerationCells[i, j] == 1)
                    {
                        // Any live cell with fewer than two live neighbors dies, as if caused by under-population.
                        if (numOfAliveNeighbors < 2)
                        {
                            NextGenerationCells[i, j] = 0;
                        }

                        // Any live cell with more than three live neighbors dies, as if by overpopulation.
                        if (numOfAliveNeighbors > 3)
                        {
                            NextGenerationCells[i, j] = 0;
                        }

                        // Any live cell with two or three live neighbors lives on to the next generation.
                        if (numOfAliveNeighbors == 2 || numOfAliveNeighbors == 3)
                        {
                            NextGenerationCells[i, j] = 1;
                        }
                    }
                    else
                    {
                        //Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                        if (numOfAliveNeighbors == 3)
                        {
                            NextGenerationCells[i, j] = 1;
                        }
                    }
                }
            }

            TransferNextGenerations();
        }

        /// <summary>
        /// Transfer next generation to current generation 
        /// </summary>
        private static void TransferNextGenerations()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    CurrentGenerationCells[i, j] = NextGenerationCells[i, j];
                }
            }
        }

        /// <summary>
        /// Checks how many alive neighbors are in the vicinity of a cell
        /// </summary>
        /// <param name="x">x-coordinate of the cell.</param>
        /// <param name="y">y-coordinate of the cell.</param>
        /// <returns>The number of alive neighbors.</returns>
        private static int GetNeighbors(int x, int y)
        {
            int numOfAliveNeighbors = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i < 0 || j < 0 || i >= HEIGHT || j >= WIDTH)
                        continue; //out of bounds
                    if (i == x && j == y)
                        continue; //same cell

                    if (CurrentGenerationCells[i, j] == 1)
                        numOfAliveNeighbors++;
                }
            }
            return numOfAliveNeighbors;
        }

        /// <summary>
        /// Draws the game to the console for testing
        /// </summary>
        private static void DrawCells()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    Console.Write(CurrentGenerationCells[i, j] == 1 ? "O " : "  ");
                    if (j == WIDTH - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }

    }
}
