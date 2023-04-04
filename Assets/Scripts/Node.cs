using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
    public Color notEnoughMoneyColor;

    public Vector3 positionOffset;

    [Header("Optional")]
	public GameObject turret;

	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;

	void Start ()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color; //переменной цвета присваиваем текущую текстуру

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

	void OnMouseDown () //функци€ срабатывает при клике на коллайдер
    {
		if (EventSystem.current.IsPointerOverGameObject()) //использу€ пространство имен EventSystem не дает наводить мышь на клетку, если над ней элемент интерфейса
            return;

		if (turret != null)
		{
            buildManager.SelectNode(this);
			return;
		}

        if (!buildManager.CanBuild) //см BuildManager.cs
            return;

        buildManager.BuildTurretOn(this); //ѕостройка турели
    }

	void OnMouseEnter () //функци€ срабатывает при наведении курсора на коллайер
    {
		if (EventSystem.current.IsPointerOverGameObject()) //использу€ пространство имен EventSystem не дает наводить мышь на клетку, если над ней элемент интерфейса
            return;

		if (!buildManager.CanBuild) //см BuildManager.cs
            return;
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor; //смена текстуры на заданную параметром
        } else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

	void OnMouseExit () //функци€ срабатывает, когда курсор выходит за пределы коллайдера
    {
		rend.material.color = startColor; //смена текстуры на изначальную
    }

}
