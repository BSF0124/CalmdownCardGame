using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GridManager gridManager;
    private Image image;
    private GameObject text;
    public Sprite[] sprites;

    // 0:빈 셀, 1:체크 셀, 2:X셀
    public int state;

    [HideInInspector] public int column;
    [HideInInspector] public int row;

    private void Start()
    {
        gridManager = transform.parent.GetComponent<GridManager>();
        image = GetComponent<Image>();
        text = transform.GetChild(0).gameObject;
        state = 0;
        UpdateCell();
    }

    private void UpdateCell()
    {
        switch(state)
        {
            case 0:
                text.SetActive(false);
                image.sprite = sprites[0];
                break;

            case 1:
                text.SetActive(false);
                image.sprite = sprites[1];
                break;

            case 2:
                text.SetActive(true);
                image.sprite = sprites[0];
                break;
        }
    }

    public bool IsFilled()
    {
        return state==1;
    }

    public void Clear(bool solution)
    {
        text.SetActive(false);

        if(solution)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = Color.white;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if(!gridManager.stageClear)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                gridManager.isClicking = true;
                if(state == 1)
                {
                    state = 0;
                    gridManager.clickType = 0;
                }
                else
                {
                    state = 1;
                    gridManager.clickType = 1;
                }
            }

            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                gridManager.isClicking = true;
                if(state == 2)
                {
                    state = 0;
                    gridManager.clickType = 3;
                }
                else
                {
                    state = 2;
                    gridManager.clickType = 2;
                }

            }
            UpdateCell();
            gridManager.HintAutoCheck(column, row);
            gridManager.CheckSolution();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!gridManager.stageClear)
        {
            gridManager.CellHighlight(column, row);

            if(gridManager.isClicking == true)
            {
                switch(gridManager.clickType)
                {
                    case 0:
                        if(state == 1)
                            state = gridManager.clickType;
                        break;

                    case 1:
                    case 2:
                        if(state == 0)
                            state = gridManager.clickType;
                        break;

                    case 3:
                        if(state == 2)
                        state = 0;
                        break;
                }
            }
            UpdateCell();
            gridManager.CheckSolution();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gridManager.isClicking = false;
        gridManager.clickType = -1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!gridManager.stageClear)
        {
            gridManager.CellHighlight(-1, -1);
        }
    }
}