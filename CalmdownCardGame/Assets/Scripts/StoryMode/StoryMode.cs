using TMPro;
using UnityEngine;

public class StoryMode : MonoBehaviour
{
    public TextMeshProUGUI explainTMP;
    private string[] explainTexts = new string[3]{
        "월드맵\n 다양한 상대와 카드 대결",
        "내 카드\n 보유한 카드 보기 및 카드팩 뽑기",
        "노노그램\n 노노그램을 클리어하여 카드팩 획득"
    };

    [HideInInspector] public int buttonType;

    void Update()
    {
        explainTMP.text = buttonType!=-1? explainTexts[buttonType] : "";
    }

    public void ButtonClick()
    {
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

    private void WorldMap()
    {
        print("WorldMap");
    }

    private void  Dictionary()
    {
        print("Dictionary");
    }

    private void Nonogram()
    {
        print("Nonogram");
    }
}
