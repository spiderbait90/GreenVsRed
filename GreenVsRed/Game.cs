using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenVsRed
{
    class Game
    {
        private static List<List<char>> _grid;

        private static int _coordinateX;
        private static int _coordinateY;
        private static int _generationsCount;
        private static int _timesGreen;

        private const char Green = '1';
        private const char Red = '0';

        private InputOutput Helper { get; }

        public Game(InputOutput helper)
        {
            this.Helper = helper;
        }

        public void Start()
        {
            AcceptInputAndGenerateInitialGrid();
            GenerateResult();

            Helper.PrintResult(_timesGreen);
            Helper.WaitForKeyPress();
        }

        private void GenerateResult()
        {
            //each generation
            for (int n = 0; n < _generationsCount; n++)
            {
                var nextGridGeneration = new List<List<char>>();

                for (int height = 0; height < _grid.Count; height++)
                {
                    //each row of cells
                    var row = new List<char>();

                    for (int width = 0; width < _grid[height].Count; width++)
                    {
                        //the current cell
                        var currentCell = _grid[height][width];

                        //list that is going to be filled with all neighbors of the current cell
                        var neighborColors = new List<char>();

                        CheckingCurrentCellNeighbors(width, height, neighborColors);

                        var allGreenNeighbors = neighborColors.Count(x => x == '1');

                        currentCell = GenerateCellNextGeneration(allGreenNeighbors, currentCell);

                        row.Add(currentCell);
                    }

                    nextGridGeneration.Add(row);
                }

                _grid = nextGridGeneration;

                CheckIfGreen();
            }
        }

        //alters the current cell for the next generation
        private char GenerateCellNextGeneration(int allGreenNeighbors, char currentCell)
        {
            var newCellValue = ' ';

            if (currentCell == Red)
            {
                if (allGreenNeighbors == 3 || allGreenNeighbors == 6)
                {
                    newCellValue = Green;
                }
                else
                {
                    newCellValue = Red;
                }
            }
            else if (currentCell == Green)
            {
                if (allGreenNeighbors != 2 && allGreenNeighbors != 3 && allGreenNeighbors != 6)
                {
                    newCellValue = Red;
                }
                else
                {
                    newCellValue = Green;
                }
            }

            return newCellValue;
        }

        //checks if the the current sell has a neighbors
        private void CheckingCurrentCellNeighbors(int width, int height, List<char> neighborColors)
        {
            var widthBegin = width - 1 >= 0;
            var widthEnd = width + 1 <= _grid[height].Count - 1;
            var heightBegin = height - 1 >= 0;
            var heightEnd = height + 1 <= _grid.Count - 1;

            //filling list with neighbor cells if it is not outside of the array
            if (widthBegin)
            {
                neighborColors.Add(_grid[height][width - 1]);
            }

            if (widthEnd)
            {
                neighborColors.Add(_grid[height][width + 1]);
            }

            if (heightBegin)
            {
                neighborColors.Add(_grid[height - 1][width]);
            }

            if (heightEnd)
            {
                neighborColors.Add(_grid[height + 1][width]);
            }

            if (heightEnd && widthBegin)
            {
                neighborColors.Add(_grid[height + 1][width - 1]);
            }

            if (heightEnd && widthEnd)
            {
                neighborColors.Add(_grid[height + 1][width + 1]);
            }

            if (heightBegin && widthBegin)
            {
                neighborColors.Add(_grid[height - 1][width - 1]);
            }

            if (heightBegin && widthEnd)
            {
                neighborColors.Add(_grid[height - 1][width + 1]);
            }
        }

        private void AcceptInputAndGenerateInitialGrid()
        {
            var sizeOfGridTokens = Helper.GetInputFromUser();

            //grid size
            var height = sizeOfGridTokens[1];

            _grid = new List<List<char>>();

            //filling the grid
            for (var i = 0; i < height; i++)
            {
                var rowOfColors = Console.ReadLine().ToCharArray();
                _grid.Add(rowOfColors.ToList());
            }

            var lastLineTokens = Helper.GetInputFromUser();

            _coordinateX = lastLineTokens[0];
            _coordinateY = lastLineTokens[1];
            _generationsCount = lastLineTokens[2];

            CheckIfGreen();
        }

        //checks if the traced cell is green for each generation
        private void CheckIfGreen()
        {
            if (_grid[_coordinateY][_coordinateX] == Green)
            {
                _timesGreen++;
            }
        }
    }
}
