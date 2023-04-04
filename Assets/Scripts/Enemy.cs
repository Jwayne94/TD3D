using UnityEngine;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector] //убрать лишние поля
	public float speed;

    public float health = 200;

    public int worth = 10;

    public GameObject deathEffect; //будет вызываться анимация смерти в функции Die()

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

        Destroy(gameObject);
    }



}
