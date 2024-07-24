using UnityEngine;
using UnityEngine.SceneManagement;

public class Nonogram : MonoBehaviour
{
    public void SelectDifficulty(int difficulty)
    {
        GameManager.instance.current_Difficulty = (GameManager.NNG_Difficulty)difficulty;
        SceneManager.LoadScene("Nonogram");
    }
}
