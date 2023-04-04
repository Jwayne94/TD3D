using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    void OnEnable() //метод работает при включении объекта
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex); //запускает индекс сцены //можно использовать строкой название сцены или просто цифру
    }

    public void Menu()
    {
        Debug.Log("Go to menu.");
    }

}
