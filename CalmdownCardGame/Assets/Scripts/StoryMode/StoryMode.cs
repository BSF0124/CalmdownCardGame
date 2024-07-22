using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StoryMode : MonoBehaviour
{
    public RawImage background;
    public TextMeshProUGUI explainTMP;
    public float duration = 1f;

    [HideInInspector] public int buttonType;

    private string[] explainTexts = new string[3]
    {
        "월드맵\n 다양한 상대와 카드 대결",
        "내 카드\n 보유한 카드 보기 및 카드팩 뽑기",
        "노노그램\n 노노그램을 클리어하여 카드팩 획득"
    };
    private RectTransform rectTransform;
    private bool isSequnceActivate = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        explainTMP.text = buttonType!=-1? explainTexts[buttonType] : "";
    }

    public void ButtonClick()
    {
        if(!isSequnceActivate)
        {
            isSequnceActivate = true;
            switch(buttonType)
            {
                case 0:
                    WorldMap();
                    break;

                case 1:
                    Dictionary();
                    break;

                case 2:
                    Nonogram();
                    break;
            }
        }
    }

    private void WorldMap()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPos(new Vector2(0, -1080), duration))
        .Join(background.DOColor(new Color(0.4f, 0.7f, 1),duration))
        .OnComplete(()=> isSequnceActivate = false);
    }

    private void Dictionary()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPos(new Vector2(1920, 0), duration))
        .Join(background.DOColor(new Color(0.4f, 1, 0.7f),duration))
        .OnComplete(()=> isSequnceActivate = false);
    }

    private void Nonogram()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPos(new Vector2(-1920, 0), duration))
        .Join(background.DOColor(new Color(0.7f, 0.4f, 1),duration))
        .OnComplete(()=> isSequnceActivate = false);
    }

    public void Back()
    {
        if(!isSequnceActivate)
        {
            isSequnceActivate = true;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(rectTransform.DOAnchorPos(new Vector2(0, 0), duration))
            .Join(background.DOColor(new Color(1, 0.7f, 0.4f),duration))
            .OnComplete(()=> isSequnceActivate = false);
        }
    }
}
