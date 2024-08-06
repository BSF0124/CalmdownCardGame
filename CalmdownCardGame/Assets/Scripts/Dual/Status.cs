using UnityEngine;

public class Status : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if(rectTransform.rotation.z != 0)
        {
            rectTransform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
