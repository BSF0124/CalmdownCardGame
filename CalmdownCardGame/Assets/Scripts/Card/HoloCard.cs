using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class HoloCard : MonoBehaviour
{
    // public Sprite[] sprites;
    // public Vector3 cardRotateValue;     // 카드 회전 값

    // private Camera mainCamera;          // 메인 카메라
    // private Bounds cardBounds;
    // private Transform cardObject;       // 회전할 카드 오브젝트
    // private Renderer _renderer;
    // private bool isMouseOver = false;   // 커서가 카드 위에 있는지 여부

    private string cardName;
    private CardRarity cardRarity;
    private CardType cardType;

    // private void Awake()
    // {
    //     mainCamera = Camera.main;
    //     cardBounds = GetComponent<Renderer>().bounds;
    //     cardObject = transform.GetChild(0).transform;
    //     _renderer = transform.GetChild(0).GetComponent<Renderer>();

    //     GetComponent<SpriteRenderer>().sprite = sprites[0];
    //     transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
    //     _renderer.material.SetTexture("_mask", SpriteToTexture2D(sprites[2]));
    // }

    public void Init(int cardID)
    {
        CardData cardData = CardDataManager.instance.GetcardByID(cardID);
        if(cardData != null)
        {
            cardName = cardData.cardName;
            cardRarity = cardData.cardRarity;
            cardType = cardData.cardType;
            transform.GetComponent<Image>().sprite = cardData.cardSprite;
        }
    }

    // private void OnMouseEnter()
    // {
    //     isMouseOver = true;

    // }

    // private void OnMouseExit()
    // {
    //     isMouseOver = false;
    //     CardRotateReset();
    // }

    // private void OnMouseOver()
    // {
    //     // 커서가 카드 위에 있을 때 실행
    //     if (isMouseOver)
    //     {
    //         // 마우스 위치를 카드의 로컬 좌표로 변환
    //         Vector3 mousePos = Input.mousePosition;
    //         mousePos.z = Vector3.Distance(mainCamera.transform.position, transform.position); // 카메라와의 거리를 계산

    //         Vector3 localCursor = transform.InverseTransformPoint(mainCamera.ScreenToWorldPoint(mousePos));
    //         float pivotX = (localCursor.x - cardBounds.min.x) / cardBounds.size.x;
    //         float pivotY = (localCursor.y - cardBounds.min.y) / cardBounds.size.y;

    //         // X, Y 회전 값을 선형 보간을 통해 계산
    //         float rotateX = Mathf.Lerp(-cardRotateValue.x, cardRotateValue.x, pivotY);
    //         float rotateY = Mathf.Lerp(cardRotateValue.y, -cardRotateValue.y, pivotX);

    //         // 카드 오브젝트의 회전 값 설정
    //         cardObject.rotation = Quaternion.Euler(rotateX, rotateY, 0);
    //     }
    // }

    // // 카드 Rotate 리셋 (포커스 이후)
    // private void CardRotateReset()
    // {
    //     cardObject.DORotate(Vector3.zero, 0.5f);
    // }

    // private Texture2D SpriteToTexture2D(Sprite sprite)
    // {
    //     if(sprite == null) return null;

    //     Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
    //     Color[] pixels = sprite.texture.GetPixels(
    //         (int)sprite.textureRect.x,
    //         (int)sprite.textureRect.y,
    //         (int)sprite.textureRect.width,
    //         (int)sprite.textureRect.height
    //     );

    //     texture.SetPixels(pixels);
    //     texture.Apply();

    //     return texture;
    // }
}