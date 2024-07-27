using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardOwnership
{
    public int cardID;
    public int quantity;

    public CardOwnership(int cardID, int quantity)
    {
        this.cardID = cardID;
        this.quantity = quantity;
    }
}

public class PlayerCardData
{
    public List<CardOwnership> cardOwnerships = new List<CardOwnership>();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerCardData playerCardData = new PlayerCardData();
    private string jsonFilePath;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        jsonFilePath = Path.Combine(Application.persistentDataPath, "/PlayerData.json");

    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(playerCardData, true);
        File.WriteAllText(jsonFilePath, jsonData);
        print("Data Save");
    }

    public void LoadData()
    {
        if(File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            playerCardData = JsonUtility.FromJson<PlayerCardData>(jsonData);
            print("Data Load");
        }
        else
        {
            print("No Data");
        }
    }
}
