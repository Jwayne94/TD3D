using System.Collections;
using UnityEngine;

public class BuilderMove : MonoBehaviour
{
    public Vector3 newPosition; // Новая позиция для перемещения
    public float time = 1f; // Время перемещения в секундах

    void Start()
    {
        // Запускаем корутину перемещения Builder'а
        StartCoroutine(MoveBuilder(newPosition, time));
    }

    IEnumerator MoveBuilder(Vector3 newPosition, float time)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Вычисляем текущую позицию Builder'а в зависимости от времени
            transform.position = Vector3.Lerp(startPosition, newPosition, elapsedTime / time);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Возвращаем Builder'а на исходную позицию
        transform.position = startPosition;
    }
}