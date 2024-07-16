using UnityEngine;

public class Camera_Nonogram : MonoBehaviour
{
    public GridManager gridManager;
    private Camera camera;
    private float scrollSpeed = 2000.0f;

    private void Start()
    {
        camera = GetComponent<Camera>();
        int x = gridManager.rowHintSize;
        int y = gridManager.columnHintSize;

        switch(gridManager.columns)
        {
            case 10:
                transform.position = new Vector2(24-2*x, 1+5*y);
                break;

            case 15:
                transform.position = new Vector2(42-3*x, 2+5*y);
                break;

            case 20:
                transform.position = new Vector2(56-3*x, -1+5.5f*y);
                break;
        }
    }

    private void Update()
    {
        float scroollWheel = Input.GetAxis("Mouse ScrollWheel");
        camera.fieldOfView += scroollWheel * Time.deltaTime * scrollSpeed;
    }
}