using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector] //убрать лишние поля
	public float speed;

    public float health = 200;

    public int worth = 10;

    public GameObject deathEffect; //будет вызываться анимация смерти в функции Die()

    public GameObject RewardTextCanvas;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            PlayerStats.Money += worth;

            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);

            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        ShowRewardText();
        Destroy(gameObject);
    }

    public void ShowRewardText()
    {
        // Создаем копию объекта Canvas RewardText и сохраняем его в переменной
        GameObject rewardTextCanvas = (GameObject)Instantiate(RewardTextCanvas, transform.position, Quaternion.identity);

        // Получаем объект TextMeshPro из копии Canvas
        TextMeshProUGUI rewardText = rewardTextCanvas.GetComponentInChildren<TextMeshProUGUI>();

        // Задаем текст для объекта TextMeshPro
        rewardText.text = "+" + worth.ToString() + "$";

        // Запускаем анимацию появления объекта Canvas
        Animator animator = rewardTextCanvas.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Show");

            // Уничтожаем объект Canvas после окончания анимации
            Destroy(rewardTextCanvas, animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            // Уничтожаем объект Canvas через 1 секунду после его появления
            Destroy(rewardTextCanvas, 1f);
        }
    }

}
