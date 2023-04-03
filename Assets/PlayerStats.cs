using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money; //если присовить перменной значение, то они будут бесконечно накапливаться от сцены к сцене
    public int startMoney = 120;

    public static int Lives;
    public int startLives = 20;

    public static int Income;
    public int startIncome = 100; // доход игрока за 10 секунд
    public static float Timer = 10.0f; // таймер для отслеживания прошедшего времени


    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Income = startIncome;
    }


    void Update()
    {
        Timer -= Time.deltaTime; // уменьшаем таймер на время, прошедшее с последнего обновления

        if (Timer <= 0.0f) // если прошло 10 секунд
        {
            Money += Income; // начисляем доход игроку
            Timer = 10.0f; // сбрасываем таймер
        }
    }
}
