using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject optionPanel;

    private void Start()
    {
        
    }

    // 게임 시작
    public void GameStart()
    {
        print("Game Start");
    }

    // 카드 도감
    public void Collection()
    {
        print("Collection");
    }

    // 옵션
    public void Option()
    {
        optionPanel.SetActive(true);
    }

    // 게임 종료
    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        #else
        Application.Quit();
        
        #endif
    }

    // 버튼 텍스트 색 변경
    public void ButtonEnter(int index)
    {
        buttons[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
    }
    public void ButtonExit(int index)
    {
        buttons[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.gray;
    }
}
