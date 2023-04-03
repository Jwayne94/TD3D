using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money; //���� ��������� ��������� ��������, �� ��� ����� ���������� ������������� �� ����� � �����
    public int startMoney = 120;

    public static int Lives;
    public int startLives = 20;

    public static int Income;
    public int startIncome = 100; // ����� ������ �� 10 ������
    public static float Timer = 10.0f; // ������ ��� ������������ ���������� �������


    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Income = startIncome;
    }


    void Update()
    {
        Timer -= Time.deltaTime; // ��������� ������ �� �����, ��������� � ���������� ����������

        if (Timer <= 0.0f) // ���� ������ 10 ������
        {
            Money += Income; // ��������� ����� ������
            Timer = 10.0f; // ���������� ������
        }
    }
}
