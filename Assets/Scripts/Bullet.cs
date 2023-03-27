using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public GameObject impactEffect; //частицы 
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) //если снаряд не находит свою цель
        {
            Destroy(gameObject); //унитожается, вовзрает код в предыдущую фазу
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


    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation); //условия при попадании в цель, создаются частицы
        Destroy(effectIns, 2f);

        Destroy(target.gameObject); //цель уничтожена
        Destroy(gameObject);
    }
}

