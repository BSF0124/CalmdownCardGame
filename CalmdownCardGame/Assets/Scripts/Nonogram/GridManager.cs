using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GridLayoutGroup mainGrid;
    public GridLayoutGroup columnGrid;
    public GridLayoutGroup rowGrid;
    public RectTransform preview;
    public GameObject cellPrefab;
    public GameObject hintPrefab;

    public int rows = 10;
    public int columns = 10;
    public bool[] solutions;

    [HideInInspector] public int current_Column;
    [HideInInspector] public int current_Row;
    [HideInInspector] public bool stageClear = false;
    [HideInInspector] public bool isClicking = false;
    // 0:체크 해제, 1:체크, 2:X 체크, 3:X 해제
    [HideInInspector] public int clickType = -1;

    [HideInInspector] public List<List<int>> columnHints = new List<List<int>>();
    [HideInInspector] public List<List<int>> rowHints = new List<List<int>>();
    private int columnHintSize;
    private int rowHintSize;

    private void Start()
    {
        stageClear = false;
        solutions = new bool[rows * columns];

        for(int i=0; i<solutions.Length; i++)
        {
            int rand = Random.Range(0, 2);
            solutions[i] = rand == 0? false : true;
        }

        GridSetting();
        GenerateGrid();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Nonogram");
        }
    }

    // 그리드 세팅
    private void GridSetting()
    {
        ColumnHintCalculate();
        RowHintCalculate();
        preview.localScale = new Vector3(rowGrid.constraintCount, columnGrid.transform.childCount / columnGrid.constraintCount, 1);
    }
    // 컬럼 그리드 설정
    private void ColumnHintCalculate()
    {
        for(int i=0; i<rows; i++)
        {
            List<int> temp = new List<int>(); // column hint 임시 저장 리스트
            int count = 0; // 연속되는 hint의 수

            // 한 column씩 hint 계산
            for(int j=0; j<columns; j++)
            {
                if(solutions[j*rows + i])
                {
                    count++;
                }

                // 연속되던 hint가 끊기면 이전의 값을 리스트에 저장
                else
                {
                    if(count != 0)
                    {
                        temp.Add(count);
                        count = 0;
                    }
                }
            }

            // 한 column이 모두 끝났을 때

            // 마지막 연속되는 hint를 리스트에 추가
            if(count != 0)
            {
                temp.Add(count);
            }

            // hint가 아예 없는 column일 경우 0을 추가
            if(temp.Count == 0)
            {
                temp.Add(0);
            }

            // 한 컬럼에 대한 힌트 리스트를 columnHints에 추가
            columnHints.Add(temp);

            // 컬럼 힌트 크기 갱신
            if(columnHintSize < temp.Count)
            {
                columnHintSize = temp.Count;
            }
        }

        // 각 컬럼 힌트 리스트에서 빈 공간을 0으로 채움
        for(int i=0; i<columns; i++)
        {
            int num = columnHintSize-columnHints[i].Count;
            for(int j=0; j<num; j++)
            {
                columnHints[i].Insert(0, 0);
            }
        }

        columnGrid.constraintCount = columns;
        mainGrid.spacing = new Vector2(mainGrid.spacing.x, 200 + (columnHintSize-1)*50);

        // 힌트 오브젝트 생성
        for(int i=0; i<columns; i++)
        {
            for(int j=0; j<columnHintSize; j++)
            {
                GameObject hint = Instantiate(hintPrefab, columnGrid.transform);
                
                if(columnHints[i][j] == 0)
                {
                    hint.transform.GetChild(0).gameObject.SetActive(false);
                }

                else
                {
                    hint.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = columnHints[i][j].ToString();
                }
            }
        }
        
    }
    // 로우 그리드 설정
    private void RowHintCalculate()
    {
        for(int i=0; i<columns; i++)
        {
            List<int> temp = new List<int>(); // row hint 임시 저장 리스트
            int count = 0; // 연속되는 hint의 수

            // 한 row씩 hint 계산
            for(int j=0; j<rows; j++)
            {
                if(solutions[i*columns + j])
                {
                    count++;
                }

                // 연속되던 hint가 끊기면 이전의 값을 리스트에 저장
                else
                {
                    if(count != 0)
                    {
                        temp.Add(count);
                        count = 0;
                    }
                }
            }

            // 한 row가 모두 끝났을 때

            // 마지막 연속되는 hint를 리스트에 추가
            if(count != 0)
            {
                temp.Add(count);
            }

            // hint가 아예 없는 row일 경우 0을 추가
            if(temp.Count == 0)
            {
                temp.Add(0);
            }

            // 한 로우에 대한 힌트 리스트를 rowwHints에 추가
            rowHints.Add(temp);

            // 로우 힌트 크기 갱신
            if(rowHintSize < temp.Count)
            {
                rowHintSize = temp.Count;
            }
        }

        for(int i=0; i<rows; i++)
        {
            int num = rowHintSize-rowHints[i].Count;
            for(int j=0; j<num; j++)
            {
                rowHints[i].Insert(0, 0);
            }
        }

        rowGrid.constraintCount = rowHintSize;
        mainGrid.spacing = new Vector2(200 + (rowHintSize-1)*50, mainGrid.spacing.y);

        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<rowHintSize; j++)
            {
                GameObject hint = Instantiate(hintPrefab, rowGrid.transform);
                
                if(rowHints[i][j] == 0)
                {
                    hint.transform.GetChild(0).gameObject.SetActive(false);
                }

                else
                {
                    hint.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rowHints[i][j].ToString();
                }
            }
        }
    }

    // 셀 생성
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

    // 정답 확인
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
            print("Clear");
            stageClear = true;
            for(int i=0; i<solutions.Length; i++)
            {
                transform.GetChild(i).GetComponent<Cell>().Clear(solutions[i]);
            }
        }
    }

    // 셀 강조 표시
    public void CellHighlight(int _column, int _row)
    {
        current_Column = _column;
        current_Row = _row;
        
        for(int i=0; i<solutions.Length; i++)
        {
            Cell cell = transform.GetChild(i).GetComponent<Cell>();
            if(cell.state == 1)
                continue;
            
            else if (cell.column == current_Column && cell.row == current_Row)
                cell.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f);

            else if(cell.column == current_Column || cell.row == current_Row)
                cell.GetComponent<Image>().color = Color.gray;
            
            else
                cell.GetComponent<Image>().color = Color.white;
            
        }

        for(int i=0; i<columnHintSize*columns; i++)
        {
            if(i >= current_Column * columnHintSize &&  i < (current_Column+1) * columnHintSize)
                columnGrid.transform.GetChild(i).GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f);
            else
                columnGrid.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
        }

        for(int i=0; i<rowHintSize*rows; i++)
        {
            if(i >= current_Row * rowHintSize &&  i < (current_Row+1) * rowHintSize)
                rowGrid.transform.GetChild(i).GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f);
            else
                rowGrid.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
        }
    }

    public void HintAutoCheck(int _column, int _row)
    {
        // Column hints check
        bool columnCorrect = true;
        int columnTemp = 0;
        for(int i=0; i<columnHintSize; i++)
        {
            int count = 0;
            if(columnHints[_column][i] == 0)
            {
                continue;
            }

            for(int j=0; j<=columns; j++)
            {
                Cell cell = transform.GetChild(j*columns+_column).GetComponent<Cell>();
                if(cell.IsFilled())
                {    
                    count++;
                    print(columnTemp);
                    print(count);
                }

                else
                {
                    if(count != columnHints[_column][i] && count != 0)
                        columnCorrect = false;
                    
                    else
                        columnTemp = j+1;

                    break;
                }
            }
            if(!columnCorrect)
                break;
        }

        bool isColumnEmpty = true;
        for(int i=0; i<columnHintSize; i++)
        {
            Cell cell = transform.GetChild(i*columns+_column).GetComponent<Cell>();
            
            if(!cell.IsFilled())
            {
                isColumnEmpty = false;
                break;
            }
        }
        columnCorrect = isColumnEmpty==true? false: columnCorrect;

        print(columnCorrect);
        if(columnCorrect)
        {
            for(int i=0; i<columnHintSize; i++)
            {
                Hint hint = columnGrid.transform.GetChild(_column*columnHintSize+i).GetComponent<Hint>();
                hint.AutoCheck(true);
            }
        }

        else
        {
            for(int i=0; i<columnHintSize; i++)
            {
                Hint hint = columnGrid.transform.GetChild(_column*columnHintSize+i).GetComponent<Hint>();
                hint.AutoCheck(false);
            }
        }
    }
}