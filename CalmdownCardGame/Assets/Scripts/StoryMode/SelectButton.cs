using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Sprite[] sprites;

    private StoryMode storyMode;
    private Image image;

    private void Start()
    {
        storyMode = transform.parent.GetComponent<StoryMode>();
        image = GetComponent<Image>();
    }

    public void PointerEnter(int buttonType)
    {
        storyMode.buttonType = buttonType;
        image.sprite = sprites[1];
    }

    public void PointerExit()
    {
        storyMode.buttonType = -1;
        image.sprite = sprites[0];
    }

    // public void OnPointerClick()
    // {
    //     storyMode.ButtonClick();
    // }
}
