using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public SceneFader sceneFader; //класс объекта затухания 

    public string menuSceneName = "MainMenu";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf); //переключает состояние меню вкл/выкл

        if (ui.activeSelf)  //останавливает время в игре
        {
            Time.timeScale = 0f; //буквально умножает на ноль
        } else
        {
            Time.timeScale = 1f; //возвращает время к обычному ходу
        }
    }

    public void Retry()
    {
        Toggle(); //будет отключать меню паузы
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); //перезапускает сцену
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }

}
