using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float speed = 70f;
    public float explosionRadius = 0f;
	public GameObject impactEffect; //частицы

    public void Seek (Transform _target)
	{
		target = _target;
	}

	// Update is called once per frame
	void Update () {

		if (target == null) //если снаряд не находит свою цель
        {
			Destroy(gameObject); //унитожается, возвращает код в предыдущую фазу
            return;
		}

		Vector3 dir = target.position - transform.position; //определение траектории между позицией цели и исходной позицией
        float distanceThisFrame = speed * Time.deltaTime; //путь снаряда?

        if (dir.magnitude <= distanceThisFrame) //сравнение пути снаряда с вектором направления, проверка на столкновение
        {
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

	}

	void HitTarget ()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation); //Создаят эффект попадания
		Destroy(effectIns, 5f); //длительность существования эффекта

        if (explosionRadius > 0f) //если есть радиус взрыва снаряда
        {
            Explode();
        }
        else
        {
            Damage(target); //просто наносится урон
        }

		Destroy(gameObject);
	}

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); //генерация массива коллайдеров
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy") //тэг объекта
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
