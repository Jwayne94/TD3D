using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
    public Color notEnoughMoneyColor;

    public Vector3 positionOffset;

    private Vector3 initialPosition;
    private bool isMoving = false;
    private Coroutine moveBulder; // ������ �� ���������� ��������

    GameObject builder;

    [HideInInspector]
	public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    private int currentUpgradeLevel = 0;
    // ��������� �������� ��� ������� � �������� ������ ���������
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
		startColor = rend.material.color; //���������� ����� ����������� ������� ��������

        buildManager = BuildManager.instance;

        builder = GameObject.Find("Builder");
        initialPosition = builder.transform.position; // ��������� �������� ������� Builder
    }
        
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

	void OnMouseDown () //������� ����������� ��� ����� �� ���������
    {
        if (EventSystem.current.IsPointerOverGameObject()) //��������� ������������ ���� EventSystem �� ���� �������� ���� �� ������, ���� ��� ��� ������� ����������
            return;

		if (turret != null)
		{
            buildManager.SelectNode(this);
			return;
		}

        if (!buildManager.CanBuild) //�� BuildManager.cs
            return;

        if (isMoving)
        {
            StopCoroutine(moveBulder); // ������������� ������� �������� �����������
            isMoving = false; // ���������� ���� ��������� �����������
        }


        Vector3 targetPosition = GetBuildPosition();// ���������� ������ Builder � ������� �������
        moveBulder = StartCoroutine(MoveBuilderToTargetPosition(targetPosition)); //���������� StartCoroutine ��� ������ ������� � ���������

        float delayTime = .5f;
        Invoke("BuildTurretAfterDelay", delayTime); //��������� ������
        isMoving = true;
    }
    void BuildTurretAfterDelay()
    {
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)  //������� ��������� ������
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity); //��������� ������
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity); //������ ��� ��������
        Destroy(effect, 5f);

        Debug.Log("Turret build!");
    }
    IEnumerator MoveBuilderToTargetPosition(Vector3 targetPosition)
    {
        float time = .5f; // ����� �����������
        float elapsedTime = 0f; // ��������� �����

        Vector3 startPosition = builder.transform.position; // ��������� �������
        float initialY = builder.transform.position.y; // �������� Y �� ������ �����������

        while (elapsedTime < time)
        {
            Vector3 newPosition = new Vector3(Mathf.Lerp(startPosition.x, targetPosition.x, elapsedTime / time),
                                             initialY, // ���������� ��������� �������� Y
                                             Mathf.Lerp(startPosition.z, targetPosition.z, elapsedTime / time));
            builder.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        builder.transform.position = targetPosition; // ������������� ������ �������� �������

        // ���������� Builder �� �������� �������
        elapsedTime = 0f; // �������� ��������� �����
        Vector3 returnPosition = builder.transform.position; // ������� ������� Builder

        while (elapsedTime < time)
        {
            Vector3 newPosition = new Vector3(Mathf.Lerp(returnPosition.x, initialPosition.x, elapsedTime / time),
                                             initialY, // ���������� ��������� �������� Y
                                             Mathf.Lerp(returnPosition.z, initialPosition.z, elapsedTime / time));
            builder.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        builder.transform.position = initialPosition; // ������������� ������ �������� ������� Builder
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

        // ���������� �� ������ ������
        Destroy(turret);

        // ��������� ����� ������
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab[currentUpgradeLevel], GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        currentUpgradeLevel++; // ��������� ������� ������� ���������
        if (currentUpgradeLevel >= turretBlueprint.upgradedPrefab.Length)
        {
            isUpgraded = true; // ���������� ����, ��� ����� �������� ������������� ������ ���������
        }

        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity); //������ ��� ��������
        Destroy(effect, 5f);                                                                                                                //������ �������

        Destroy(turret);
        turretBlueprint = null;
        currentUpgradeLevel = 0;
        isUpgraded = false;
    }

    void OnMouseEnter () //������� ����������� ��� ��������� ������� �� ��������
    {
		if (EventSystem.current.IsPointerOverGameObject()) //��������� ������������ ���� EventSystem �� ���� �������� ���� �� ������, ���� ��� ��� ������� ����������
            return;

		if (!buildManager.CanBuild) //�� BuildManager.cs
            return;
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor; //����� �������� �� �������� ����������
        } else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

	void OnMouseExit () //������� �����������, ����� ������ ������� �� ������� ����������
    {
		rend.material.color = startColor; //����� �������� �� �����������
    }

}
