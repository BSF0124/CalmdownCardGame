using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum NNG_Difficulty{Easy, Normal, Hard};

    public NNG_Difficulty current_Difficulty;
    public int current_Stage;
    
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

        SceneManager.LoadScene("MainMenu");
    }
}
