using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform target;

	[Header("General")]

	public float range = 15f; //настройка радиуса обзора

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab; //префаб снаряда
    public float fireRate = 1f; //Выстрелы в секунду
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;

    [Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate; //объявление вращаемой части модели
    public float turnSpeed = 10f; //скорость поворота

    public Transform firePoint; //место спауна снаряда

    // Use this for initialization
    void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity; //наименьшее расстояние
        GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //возвращает расстояние до цели
            if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform; //определение ближайшей цели?
        } else
		{
			target = null; //потеря цели из поля зрения
        }

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                    lineRenderer.enabled = false; //условие прекращает отображения лазера, если он включен
            }

            return; //если не находит цель, ничего не делает
        }


        LockOnTarget();

        if (useLaser)
        {
            Laser(); 
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

        }


        fireCountdown -= Time.deltaTime; //каждую секунду fireCountdown будет умньшаться на 1

    }

    void LockOnTarget()
    {
        //Target lock on
        Vector3 dir = target.position - transform.position; //направление в сторону цели по вектору
        Quaternion lookRotation = Quaternion.LookRotation(dir); //вращение к цели
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; //определение угла вращения, в отличии от значения lookRotation.eulerAngels, настощая фунцкия обеспечит более плавное вращение
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //вращение строго по оси y

    }

    void Laser() //вектор направления луча
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true; //включает лазер

        lineRenderer.SetPosition(0, firePoint.position); //начальная точка
        lineRenderer.SetPosition(1, target.position);   //конечная точка
    }

    void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //создание объекта
        Bullet bullet = bulletGO.GetComponent<Bullet>(); //поиск компонента

        if (bullet != null) //если существует снаряд, используется метод
            bullet.Seek(target);
	}

	void OnDrawGizmosSelected () //отображение радиуса действия
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
