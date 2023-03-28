using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;


    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color; //переменной цвета присваиваем текущую текстуру
    }

    void OnMouseDown() //функция срабатывает при клике на коллайдер
    {
        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen.");
            return;
        }

        GameObject turretTobuild = BuildManager.instance.GetTurretToBuild();//Постройка туррели
        turret = (GameObject)Instantiate(turretTobuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter() //функция срабатывает при наведении курсора на коллайер
    {
        rend.material.color = hoverColor; //смена текстуры на заданную параметром
    }

    void OnMouseExit() //функция срабатывает, когда курсор выходит за пределы коллайдера
    {
        rend.material.color = startColor; //смена текстуры на изначальную
    }
}
