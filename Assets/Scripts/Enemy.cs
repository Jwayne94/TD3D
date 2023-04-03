using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 10f;

    public int health = 200;

    public int value = 10;

    public GameObject deathEffect; //будет вызываться анимация смерти в функции Die()

	private Transform target;
	private int wavepointIndex = 0;

	void Start ()
	{
		target = Waypoints.points[0];
	}

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            PlayerStats.Money += value;

            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);

            Die();
        }
    }

    void Die()
    {

        Destroy(gameObject);
    }

	void Update ()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
            EndPath(); 
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }

}
