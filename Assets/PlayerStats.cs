using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money; //если присовить перменной значение, то они будут бесконечно накапливаться от сцены к сцене
    public int startMoney = 120;

    void Start()
    {
        Money = startMoney;
    }
}
