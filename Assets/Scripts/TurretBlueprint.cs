using System.Collections;
using UnityEngine;
[System.Serializable] //Сериализация //Данный класс будет сохранять переменные 
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject[] upgradedPrefab; // Массив префабов улучшенных башен
    public int[] upgradeCost; // Массив стоимостей улучшения башен

    public int GetSellAmount () //функция будет вычислять стоимость продажи построенной турели 
    {
        return cost / 4;
    }
}
