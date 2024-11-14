using System.Collections.Generic;
using System.Linq;
using CodeBase.ConnectLetters;
using CodeBase.Services.LogService;
using ConnectLetters.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Crossword : MonoBehaviour
{
    [Space, Header("Components")] 
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    [Space] 
    
    [SerializeField] private CellCrossword cellCrosswordPrefab;
    [SerializeField] private GameObject emptyPrefab;

    private ICrosswordFactory _crosswordFactory;
    private ILogService _logService;
    
    private CellCrossword[,] _crosswordsCells;
    private CrosswordData _crosswordData;

    [Inject]
    public void Construct(ICrosswordFactory crosswordFactory, ILogService logService)
    {
        _crosswordFactory = crosswordFactory;
        _logService = logService;
    }

    public void Initialize(List<string> words)
    {
        _crosswordData = _crosswordFactory.CreateCrossword(words.ToArray());

        InitializeSizeGrid(_crosswordData.Matrix);
        initializeCells(_crosswordData.Matrix);
    }

    public void OpenWord(string keyWord)
    {
        if (!_crosswordData.Words.ContainsKey(keyWord.ToUpper()))
        {
            _logService.LogError($"Not Find Word: {keyWord}");
            return;
        }

        PositionWord positionWord = _crosswordData.Words.FirstOrDefault(word => word.Key.ToUpper() == keyWord).Value;

        if (positionWord.Orientation == Orientation.Horizontal)
        {
            for (int index = positionWord.Position.y; index < positionWord.Position.y + positionWord.Length; index++)
                _crosswordsCells[positionWord.Position.x, index].Show();
        }
        else
        {
            for (int index = positionWord.Position.x; index < positionWord.Position.x + positionWord.Length; index++)
                _crosswordsCells[index, positionWord.Position.y].Show();
        }
    }

    private void CreateEmptyCell() =>
        Instantiate(emptyPrefab, gridLayoutGroup.transform);

    private CellCrossword CreateCell() =>
        Instantiate(cellCrosswordPrefab, gridLayoutGroup.transform);

    private void InitializeSizeGrid(char[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        float sizeCell = rectTransform.rect.width * 0.85f / Mathf.Max(rows, columns);
        float spacing = rectTransform.rect.height * 0.15f / Mathf.Max(rows, columns);

        gridLayoutGroup.cellSize = Vector2.one * sizeCell;
        gridLayoutGroup.spacing = Vector2.one * spacing;
        
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columns;
    }

    private void initializeCells(char[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        
        _crosswordsCells = new CellCrossword[rows, columns];

        for (int indexY = 0; indexY < rows; indexY++)
        {
            for (int indexX = 0; indexX < columns; indexX++)
            {
                if (matrix[indexY, indexX] == '_')
                {
                    CreateEmptyCell();
                }
                else
                {
                    CellCrossword cell = CreateCell();
                    cell.Initialize(matrix[indexY, indexX]);
                    
                    _crosswordsCells[indexY, indexX] = cell;
                }
            }
        }
    }
}