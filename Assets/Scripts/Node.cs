using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
    public Color notEnoughMoneyColor;

    public Vector3 positionOffset;

    [HideInInspector]
	public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

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

	void OnMouseDown () //функция срабатывает при клике на коллайдер
    {
		if (EventSystem.current.IsPointerOverGameObject()) //используя пространство имен EventSystem не дает наводить мышь на клетку, если над ней элемент интерфейса
            return;

		if (turret != null)
		{
            buildManager.SelectNode(this);
			return;
		}

        if (!buildManager.CanBuild) //см BuildManager.cs
            return;

        BuildTurret(buildManager.GetTurretToBuild()); //Постройка турели
    }

    void BuildTurret(TurretBlueprint blueprint)  //функция постройки турели
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity); //установка турели
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity); //эффект при создании
        Destroy(effect, 5f);

        Debug.Log("Turret build!");
    }

    public void UpgradeTurret() //Улучшение турели
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;


        //Избавиться от старой турели
        Destroy(turret);

        //Построить новую турель
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity); //установка префаба новой турели
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity); //эффект при создании
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");

    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity); //эффект при создании
        Destroy(effect, 5f);                                                                                                                //эффект продажи

        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter () //функция срабатывает при наведении курсора на коллайер
    {
		if (EventSystem.current.IsPointerOverGameObject()) //используя пространство имен EventSystem не дает наводить мышь на клетку, если над ней элемент интерфейса
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

	void OnMouseExit () //функция срабатывает, когда курсор выходит за пределы коллайдера
    {
		rend.material.color = startColor; //смена текстуры на изначальную
    }

}
