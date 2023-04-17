using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
    public Color notEnoughMoneyColor;

    public Vector3 positionOffset;

    private Vector3 initialPosition;
    private bool isMoving = false;
    private Coroutine moveBulder; // Ссылка на запущенную корутину

    GameObject builder;

    [HideInInspector]
	public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    private int currentUpgradeLevel = 0;
    // Публичное свойство для доступа к текущему уровню улучшения
    public int CurrentUpgradeLevel
    {
        get { return currentUpgradeLevel; }
    }
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

        builder = GameObject.Find("Builder");
        initialPosition = builder.transform.position; // Сохраняем исходную позицию Builder
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

        if (isMoving)
        {
            StopCoroutine(moveBulder); // Останавливаем текущую корутину перемещения
            isMoving = false; // Сбрасываем флаг состояния перемещения
        }


        Vector3 targetPosition = GetBuildPosition();// Перемещаем объект Builder в целевую позицию
        moveBulder = StartCoroutine(MoveBuilderToTargetPosition(targetPosition)); //используем StartCoroutine для вызова функции с корутином

        float delayTime = .5f;
        Invoke("BuildTurretAfterDelay", delayTime); //Постройка турели
        isMoving = true;
    }
    void BuildTurretAfterDelay()
    {
        BuildTurret(buildManager.GetTurretToBuild());
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
    IEnumerator MoveBuilderToTargetPosition(Vector3 targetPosition)
    {
        float time = .5f; // Время перемещения
        float elapsedTime = 0f; // Прошедшее время

        Vector3 startPosition = builder.transform.position; // Начальная позиция
        float initialY = builder.transform.position.y; // Значение Y на начало перемещения

        while (elapsedTime < time)
        {
            Vector3 newPosition = new Vector3(Mathf.Lerp(startPosition.x, targetPosition.x, elapsedTime / time),
                                             initialY, // Используем начальное значение Y
                                             Mathf.Lerp(startPosition.z, targetPosition.z, elapsedTime / time));
            builder.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        builder.transform.position = targetPosition; // Устанавливаем точную конечную позицию

        // Возвращаем Builder на исходную позицию
        elapsedTime = 0f; // Обнуляем прошедшее время
        Vector3 returnPosition = builder.transform.position; // Текущая позиция Builder

        while (elapsedTime < time)
        {
            Vector3 newPosition = new Vector3(Mathf.Lerp(returnPosition.x, initialPosition.x, elapsedTime / time),
                                             initialY, // Используем начальное значение Y
                                             Mathf.Lerp(returnPosition.z, initialPosition.z, elapsedTime / time));
            builder.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        builder.transform.position = initialPosition; // Устанавливаем точную исходную позицию Builder
        isMoving = false;
    }
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost[currentUpgradeLevel])
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost[currentUpgradeLevel];

        // Избавиться от старой турели
        Destroy(turret);

        // Построить новую турель
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab[currentUpgradeLevel], GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        currentUpgradeLevel++; // Увеличить текущий уровень улучшения
        if (currentUpgradeLevel >= turretBlueprint.upgradedPrefab.Length)
        {
            isUpgraded = true; // Установить флаг, что башня достигла максимального уровня улучшения
        }

        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity); //эффект при создании
        Destroy(effect, 5f);                                                                                                                //эффект продажи

        Destroy(turret);
        turretBlueprint = null;
        currentUpgradeLevel = 0;
        isUpgraded = false;
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
