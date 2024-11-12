using CodeBase.ConnectLetters;
using ConnectLetters.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Crossword : MonoBehaviour
{
    [Space, Header("Components")] [SerializeField]
    private RectTransform rectTransform;

    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    [Space] [SerializeField] private CellCrossword cellCrosswordPrefab;
    [SerializeField] private GameObject emptyPrefab;

    private ICrosswordFactory _crosswordFactory;
    private string[] Words = { "КАРТА", "КАПОТ", "ТРОПА", "ПАРА", "ЛАК", "ПАРК", "ТАРА", "ЛАПА" };

    [Inject]
    public void Construct(ICrosswordFactory crosswordFactory) =>
        _crosswordFactory = crosswordFactory;

    public void Start()
    {
        char[,] crossword = _crosswordFactory.CreateCrossword(Words);

        InitializeSizeGrid(crossword);
        initializeCells(crossword);
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
                }
            }
        }
    }
}