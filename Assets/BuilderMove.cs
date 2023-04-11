using System.Collections;
using UnityEngine;

public class BuilderMove : MonoBehaviour
{
    public Vector3 newPosition; // ����� ������� ��� �����������
    public float time = 1f; // ����� ����������� � ��������

    void Start()
    {
        // ��������� �������� ����������� Builder'�
        StartCoroutine(MoveBuilder(newPosition, time));
    }

    IEnumerator MoveBuilder(Vector3 newPosition, float time)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // ��������� ������� ������� Builder'� � ����������� �� �������
            transform.position = Vector3.Lerp(startPosition, newPosition, elapsedTime / time);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���������� Builder'� �� �������� �������
        transform.position = startPosition;
    }
}