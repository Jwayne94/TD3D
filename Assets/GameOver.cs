using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    void OnEnable() //метод работает при включении объекта
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); //запускает текущую сцену//запускает сцену по имени //можно использовать строкой название сцены или просто цифру индекса
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

}
