using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Vector3 positionOffset;

	private GameObject turret;

	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;

	void Start ()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color; //переменной цвета присваиваем текущую текстуру

        buildManager = BuildManager.instance;
    }

	void OnMouseDown () //функци€ срабатывает при клике на коллайдер
    {
		if (EventSystem.current.IsPointerOverGameObject()) //использу€ пространство имен EventSystem не дает наводить мышь на клетку, если над ней элемент интерфейса
            return;

		if (buildManager.GetTurretToBuild() == null)
			return;

		if (turret != null)
		{
			Debug.Log("Can't build there! - TODO: Display on screen.");
			return;
		}

		GameObject turretToBuild = buildManager.GetTurretToBuild(); //ѕостройка турели
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
	}

	void OnMouseEnter () //функци€ срабатывает при наведении курсора на коллайер
    {
		if (EventSystem.current.IsPointerOverGameObject()) //использу€ пространство имен EventSystem не дает наводить мышь на клетку, если над ней элемент интерфейса
            return;

		if (buildManager.GetTurretToBuild() == null)
			return;

		rend.material.color = hoverColor; //смена текстуры на заданную параметром
    }

	void OnMouseExit () //функци€ срабатывает, когда курсор выходит за пределы коллайдера
    {
		rend.material.color = startColor; //смена текстуры на изначальную
    }

}
