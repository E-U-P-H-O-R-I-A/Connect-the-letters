using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.ConnectLetters
{
    public class CrosswordFactory : ICrosswordFactory
    {
        private const char Empty = '_';
        private const int SizeMatrix = 50;

        private List<Vector2Int> _coordinatesLetter;
        private Variants[,] _tableVariants;
        private char[,] _tableWords;
        
        private int _minX;
        private int _maxX;
        private int _minY;
        private int _maxY;

        public char[,] CreateCrossword(string[] array)
        {
            InitializeField();

            string[] sortedWords = Sort(array);

            foreach (string word in sortedWords)
            {
                PositionWord foundedPlace = FindPlace(word);

                if (foundedPlace.Position != Vector2.zero)
                    PlaceWord(foundedPlace, word);
                else
                    Console.WriteLine($"Can't find place for word: {word}");
            }

            return GenerateResultMatrix();
        }

        private string[] Sort(string[] array) => 
            array.OrderByDescending(word => word.Length).ToArray();
        
        private bool CheckIsCellEmpty(Vector2Int coordinate) => 
            _tableWords[coordinate.x, coordinate.y] == Empty;

        private bool CheckIsCellFree(Vector2Int coordinate) => 
            _tableVariants[coordinate.x, coordinate.y] == Variants.Free;
        
        private bool CheckIsCellClose(Vector2Int coordinate) => 
            _tableVariants[coordinate.x, coordinate.y] == Variants.Close;
        
        private bool CheckIsCellOrientated(Vector2Int coordinate) => 
            _tableVariants[coordinate.x, coordinate.y] == Variants.Horizontal || _tableVariants[coordinate.x, coordinate.y] == Variants.Vertical;

        private void InitializeField()
        {
            _coordinatesLetter = new List<Vector2Int>();

            _tableVariants = new Variants[SizeMatrix, SizeMatrix];
            _tableWords = new char[SizeMatrix, SizeMatrix];

            for (int i = 0; i < _tableVariants.GetLength(0); i++)
            {
                for (int j = 0; j < _tableVariants.GetLength(1); j++)
                {
                    _tableVariants[i, j] = Variants.Free;
                }
            }

            for (int i = 0; i < _tableWords.GetLength(0); i++)
            {
                for (int j = 0; j < _tableWords.GetLength(1); j++)
                {
                    _tableWords[i, j] = Empty;
                }
            }
        }
        
        private void PlaceWord(PositionWord foundedPlace, string word)
        {
            SetWordToMatrix(foundedPlace, word);
            CloseNextAndPrevCell(foundedPlace, word);

            for (int index = 0; index < word.Length; index++)
            {
                if (foundedPlace.Orientation == Orientation.Horizontal)
                {
                    Vector2Int selectedCell = new(foundedPlace.Position.x , foundedPlace.Position.y + index);
                
                  //  CheckUpCell(new Vector2Int(selectedCell.x, selectedCell.y));
                  //  CheckDownCell(new Vector2Int(selectedCell.x, selectedCell.y));
                    CheckHorizontalCell(new Vector2Int(selectedCell.x, selectedCell.y));
                }
                else
                {
                    Vector2Int selectedCell = new(foundedPlace.Position.x + index, foundedPlace.Position.y);

                    //CheckLeftCell(new Vector2Int(selectedCell.x, selectedCell.y));
                   // CheckRightCell(new Vector2Int(selectedCell.x, selectedCell.y));
                    CheckVerticalCell(new Vector2Int(selectedCell.x, selectedCell.y));
                }
            }

            UpdateSizeMatrix(foundedPlace, word);
        }
        
        private PositionWord FindPlace(string word)
        {
            if (_coordinatesLetter.Count == 0)
            {
                int centerX = Convert.ToInt32(SizeMatrix / 2);
                int centerY = Convert.ToInt32(SizeMatrix / 2) - Convert.ToInt32(word.Length / 2);
                
                _minX = centerY;
                _maxX = centerY + word.Length - 1;
                _minY = centerX;
                _maxY = centerX;

                return new PositionWord(new Vector2Int(centerX, centerY), Orientation.Horizontal);
            }
            
            Dictionary<PositionWord, int> positions = new();

            for (int coordinate = 0; coordinate < _coordinatesLetter.Count; coordinate++)
            {
                char targetLetter = _tableWords[_coordinatesLetter[coordinate].x, _coordinatesLetter[coordinate].y];
                
                List<int> indices = new List<int>();

                for (int letter = 0; letter < word.Length; letter++)
                {
                    if (word[letter] == targetLetter) 
                        indices.Add(letter);
                }
                
                if (indices.Count == 0)
                    continue;

                foreach (var index in indices)
                {
                    PositionWord position = CalculatePosition(_coordinatesLetter[coordinate], index);
                    int coefficient = CalculateCoefficient(position, word);
                    
                    if (!CanSetWord(position, word))
                        continue;
                            
                    if (coefficient == 0)
                        return position;
                    
                    positions.Add(position, coefficient);
                }
                
                if (positions.Count != 0)
                    return positions.OrderBy(position => position.Value).First().Key;
            }
            
            return new PositionWord(new Vector2Int(0, 0), Orientation.Horizontal);
        }

        private void CheckUpCell(Vector2Int selectedPosition)
        {
            Vector2Int upCell = new(selectedPosition.x + 1, selectedPosition.y);
            
            if (_tableVariants[upCell.x, upCell.y] == Variants.Close) 
                return;
            
            Vector2Int upLeftCell = new(upCell.x, upCell.y - 1);
            Vector2Int upRightCell = new(upCell.x, upCell.y + 1);


            if (CheckIsCellClose(upLeftCell) && CheckIsCellClose(upRightCell))
            {
                CloseCell(upCell);
                return;
            }
            
            if (!CheckIsCellOrientated(upLeftCell) && !CheckIsCellOrientated(upRightCell) ) 
                return;
            
            CloseCell(upCell);
        }

        private void CloseCell(Vector2Int upCell)
        {
            _tableVariants[upCell.x, upCell.y] = Variants.Close;
            _coordinatesLetter.Remove(new Vector2Int(upCell.x, upCell.y));
        }

        private void CheckDownCell(Vector2Int selectedPosition)
        {
            Vector2Int downCell = new(selectedPosition.x - 1, selectedPosition.y);
            
            if (_tableVariants[downCell.x, downCell.y] == Variants.Close) 
                return;
            
            Vector2Int downLeftCell = new(downCell.x, downCell.y - 1);
            Vector2Int downRightCell = new(downCell.x, downCell.y + 1);

            if (CheckIsCellClose(downLeftCell) && CheckIsCellClose(downRightCell))
            {
                CloseCell(downCell);
                return;
            }
            
            if (!CheckIsCellOrientated(downLeftCell) && !CheckIsCellOrientated(downRightCell))
                return;

            CloseCell(downCell);
        }

        private void CheckLeftCell(Vector2Int selectedPosition)
        {
            Vector2Int leftCell = new(selectedPosition.x, selectedPosition.y - 1);
            
            if (_tableVariants[leftCell.x, leftCell.y] == Variants.Close) 
                return;
            
            Vector2Int leftUpCell = new(leftCell.x + 1, leftCell.y);
            Vector2Int leftDownCell = new(leftCell.x - 1, leftCell.y);

            if (CheckIsCellClose(leftUpCell) && CheckIsCellClose(leftUpCell))
            {
                CloseCell(leftCell);
                return;
            }
            
            if (!CheckIsCellOrientated(leftUpCell) && !CheckIsCellOrientated(leftDownCell)) 
                return;

            CloseCell(leftCell);
        }

        private void CheckRightCell(Vector2Int selectedPosition)
        {
            Vector2Int rightCell = new(selectedPosition.x , selectedPosition.y + 1);
            
            if (_tableVariants[rightCell.x, rightCell.y] == Variants.Close)
                return;
            
            Vector2Int rightUpCell = new(rightCell.x + 1, rightCell.y);
            Vector2Int rightDownCell = new(rightCell.x - 1, rightCell.y);

            if (CheckIsCellClose(rightUpCell) && CheckIsCellClose(rightDownCell))
            {
                CloseCell(rightCell);
                return;
            }
            
            if (!CheckIsCellOrientated(rightUpCell) && !CheckIsCellOrientated(rightDownCell))
                return;

            CloseCell(rightCell);
        }

        private void CheckHorizontalCell(Vector2Int selectedPosition)
        {
            if (_tableVariants[selectedPosition.x, selectedPosition.y] == Variants.Close)
                return;
            
            Vector2Int upCell = new(selectedPosition.x + 1, selectedPosition.y);
            Vector2Int downCell = new(selectedPosition.x - 1, selectedPosition.y);

            if (!CheckIsCellFree(upCell) && !CheckIsCellFree(downCell))
            {
                _tableVariants[selectedPosition.x, selectedPosition.y] = Variants.Close;
                _coordinatesLetter.Remove(new Vector2Int(selectedPosition.x, selectedPosition.y));
            }
            else
            {
                _tableVariants[selectedPosition.x, selectedPosition.y] = Variants.Vertical;
                _coordinatesLetter.Add(new Vector2Int(selectedPosition.x, selectedPosition.y));
            }
        }

        private void CheckVerticalCell(Vector2Int selectedPosition) 
        {
            if (_tableVariants[selectedPosition.x, selectedPosition.y] == Variants.Close)
                return;
            
            Vector2Int leftCell = new Vector2Int(selectedPosition.x, selectedPosition.y - 1);
            Vector2Int rightCell = new Vector2Int(selectedPosition.x , selectedPosition.y + 1);
            
            if (!CheckIsCellFree(leftCell) && !CheckIsCellFree(rightCell))
            {
                _tableVariants[selectedPosition.x, selectedPosition.y] = Variants.Close;
                _coordinatesLetter.Remove(new Vector2Int(selectedPosition.x, selectedPosition.y));
            }
            else
            {
                _tableVariants[selectedPosition.x, selectedPosition.y] = Variants.Horizontal;
                _coordinatesLetter.Add(new Vector2Int(selectedPosition.x, selectedPosition.y));
            }
        }

        private void CloseNextAndPrevCell(PositionWord foundedPlace, string word)
        {
            if (foundedPlace.Orientation == Orientation.Horizontal)
            {
                _tableVariants[foundedPlace.Position.x, foundedPlace.Position.y - 1] = Variants.Close;
                _tableVariants[foundedPlace.Position.x, foundedPlace.Position.y + word.Length] = Variants.Close;
            }
            else
            {
                _tableVariants[foundedPlace.Position.x - 1, foundedPlace.Position.y] = Variants.Close;
                _tableVariants[foundedPlace.Position.x + word.Length, foundedPlace.Position.y] = Variants.Close;
            }
        }

        private void SetWordToMatrix(PositionWord foundedPlace, string word)
        {
            if (foundedPlace.Orientation == Orientation.Horizontal)
            {
                for (int index = 0; index < word.Length; index++)
                    _tableWords[foundedPlace.Position.x, foundedPlace.Position.y + index] = word[index];
            }
            else
            {
                for (int index = 0; index < word.Length; index++)
                    _tableWords[foundedPlace.Position.x + index, foundedPlace.Position.y] = word[index];
            }
        }

        private bool CanSetWord(PositionWord position, string word)
        {
            for (int index = 0; index < word.Length; index++)
            {
                Vector2Int selectedCoordinate;
                
                if (position.Orientation == Orientation.Horizontal)
                    selectedCoordinate = new Vector2Int(position.Position.x, position.Position.y + index);
                else
                    selectedCoordinate = new Vector2Int(position.Position.x + index, position.Position.y);

                if (word[index] != _tableWords[selectedCoordinate.x, selectedCoordinate.y] && _tableWords[selectedCoordinate.x, selectedCoordinate.y] != Empty)
                    return false;
                
                switch (_tableVariants[selectedCoordinate.x, selectedCoordinate.y])
                {
                    case Variants.Free:
                        return true;
                    case Variants.Close:
                        return false;
                    case Variants.Vertical:
                        if (position.Orientation == Orientation.Horizontal)
                            return false;
                        break;
                    case Variants.Horizontal:
                        if (position.Orientation == Orientation.Vertical)
                            return false;
                        break;
                }
            }

            return true;
        }

        private void UpdateSizeMatrix(PositionWord foundedPlace, string word)
        {
            if (foundedPlace.Orientation == Orientation.Horizontal)
            {
                if (_maxX < foundedPlace.Position.y + word.Length - 1)
                    _maxX = foundedPlace.Position.y + word.Length - 1;
            
                if (_minX > foundedPlace.Position.y)
                    _minX = foundedPlace.Position.y;
            }
            else
            {
                if (_minY > foundedPlace.Position.x)
                    _minY = foundedPlace.Position.x;
            
                if (_maxY < foundedPlace.Position.x + word.Length - 1)
                    _maxY = foundedPlace.Position.x + word.Length - 1;
            }
        }

        private int CalculateCoefficient(PositionWord position, string word)
        {
            int result;

            if (position.Orientation == Orientation.Horizontal)
            {
                int newMaxX = _maxX;
                int newMinX = _minX;
                
                if (newMaxX < position.Position.y + word.Length - 1)
                    newMaxX = position.Position.y + word.Length - 1;
            
                if (newMinX > position.Position.y)
                    newMinX = position.Position.y;
                
                int currentWidth =(_maxX - _minX);
                int newWidth =(newMaxX - newMinX);
                
                result = newWidth - currentWidth;
            }
            else
            {
                int newMaxY = _maxY;
                int newMinY = _minY;
                
                if (newMinY > position.Position.x)
                    newMinY = position.Position.x;
            
                if (newMaxY < position.Position.x + word.Length - 1)
                    newMaxY = position.Position.x + word.Length - 1;
                
                int currentHeight = _maxY - _minY;
                int newHeight = newMinY - newMaxY;
                
                result = newHeight - currentHeight;
            }
            
            return result;
        }
        
        private PositionWord CalculatePosition(Vector2Int coordinate, int index)
        {
            Variants orientation = _tableVariants[coordinate.x, coordinate.y];
            
            PositionWord result = new();
            
            switch (orientation)
            {
                case Variants.Vertical:
                    result.Orientation = Orientation.Vertical;
                    result.Position = new Vector2Int(coordinate.x - index, coordinate.y);
                    break;
                case Variants.Horizontal:
                    result.Orientation = Orientation.Horizontal;
                    result.Position = new Vector2Int(coordinate.x, coordinate.y - index);
                    break;
                default:
                    Debug.LogError("Fail to calculate position");
                    break;
            }

            return result;
        }

        private char[,] GenerateResultMatrix()
        {
            char[,] result = new char[_maxY - _minY + 1, _maxX - _minX + 1];

            int indexY = 0;
            for (int y = _minY; y <= _maxY; y++)
            {
                int indexX = 0;
                for (int x = _minX; x <= _maxX; x++)
                {
                    result[indexY, indexX] = _tableWords[y, x];
                    indexX++;
                }

                indexY++;
            }
            
            return result;
        }

        private enum Variants
        {
            Free = 0,
            Close = 1,
            Vertical = 2,
            Horizontal = 3
        }
    }
}