using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab;

    public int rows = 10;
    public int columns = 10;
    public bool[] solutions; 

    public int current_Column;
    public int current_Row;

    // 0:체크 해제, 1:체크, 2:X 체크, 3:X 해제
    [HideInInspector] public bool stageClear = false;
    [HideInInspector] public bool isClicking = false;
    [HideInInspector] public int clickType = -1;

    private void Start()
    {
        stageClear = false;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<columns; j++)
            {
                GameObject cell = Instantiate(cellPrefab, transform);
                cell.GetComponent<Cell>().column = j;
                cell.GetComponent<Cell>().row = i;
            }
        }
    }

    public void CheckSolution()
    {
        bool isCorrect = true;
        for(int i=0; i<solutions.Length; i++)
        {
            Cell cell = transform.GetChild(i).GetComponent<Cell>();
            if (cell.IsFilled() != solutions[i])
            {
                isCorrect = false;
                break;
            }
        }

        if(isCorrect)
        {
            stageClear = true;
            for(int i=0; i<solutions.Length; i++)
            {

            }
        }
    }

    public void CellHighlight(int _column, int _row)
    {
        current_Column = _column;
        current_Row = _row;
        
        for(int i=0; i<solutions.Length; i++)
        {
            Cell cell = transform.GetChild(i).GetComponent<Cell>();
            if(cell.column == current_Column || cell.row == current_Row)
            {
                cell.GetComponent<Image>().color = Color.gray;
            }

            else
            {
                cell.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
