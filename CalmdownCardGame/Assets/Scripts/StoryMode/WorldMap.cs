using UnityEngine;

public class WorldMap : MonoBehaviour
{
    public GameObject[] worldObjects;

    public GameObject deckBulider;

    private int worldIndex = 0;
    private int maxWorldIndex = 5;

    private void Start()
    {
        SetWorld();
    }

    public void NextWorld()
    {
        worldIndex = (worldIndex + 1) % maxWorldIndex;
        SetWorld();
    }

    public void PreviousWorld()
    {
        worldIndex = worldIndex-1 == -1 ? maxWorldIndex-1 : (worldIndex-1);
        SetWorld();
    }

    private void SetWorld()
    {
        for(int i=0; i<maxWorldIndex; i++)
        {
            if(i == worldIndex)
                worldObjects[i].SetActive(true);
            else
                worldObjects[i].SetActive(false);
        }
    }

    public void SelectStage(int stageIndex)
    {
        GameManager.instance.current_Stage = stageIndex;
        deckBulider.SetActive(true);
    }

    public void CloseDeckBuilder()
    {
        deckBulider.SetActive(false);
    }
}
