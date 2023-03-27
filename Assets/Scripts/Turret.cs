using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;

    [Header("Attributes")]
    
    public float range = 15f;  //настройка радиуса обзора
    public float fireRate = 1f; //¬ыстрелы в секунду
    private float fireCountdown = 0;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate; //объ€вление вращаемой части модели
    public float turnSpeed = 10f; //скорость поворота

    public GameObject bulletPrefab; //префаб снар€да
    public Transform firePoint; //место спауна снар€да

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() //поиск цели
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; //наименьшее рассто€ние
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //возвращает рассто€ние до цели
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform; //определение ближайшей цели?
        }
        else
        {
            target = null; //потер€ цели из пол€ зрени€
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;     //если не находит цель, ничего не делает

        //Ќаведение на цель
        Vector3 dir = target.position - transform.position; //направление в сторону цели по вектору
        Quaternion lookRotation = Quaternion.LookRotation(dir); //вращение к цели
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; //определение угла вращени€, в отличии от значени€ lookRotation.eulerAngels, настоща€ фунцки€ обеспечит более плавное вращение
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //вращение строго по оси y

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime; //каждую секунду fireCountdown будет умньшатьс€ на 1

    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //создание объекта
        Bullet bullet = bulletGo.GetComponent<Bullet>(); //поиск компонента

        if (bullet != null) //если существует снар€д, используетс€ метод 
            bullet.Seek(target);
        
    }



    void OnDrawGizmosSelected()            //отображение радиуса действи€
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
