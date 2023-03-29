using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance; //следующая функция будет создавать общую копию класса от любого объекта

    void Awake ()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}

	public GameObject standardTurretPrefab;
	public GameObject anotherTurretPrefab;

	private GameObject turretToBuild;

	public GameObject GetTurretToBuild ()
	{
		return turretToBuild;
	}

	public void SetTurretToBuild (GameObject turret) //метод выбирает устанавливаемую туррель
    {
		turretToBuild = turret;
	}

}
