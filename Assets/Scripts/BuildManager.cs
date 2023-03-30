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
	public GameObject missleLauncherPrefab;

	private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } } //Функция проверяет на наличие значения turretToBuld, возвращает значение истины 

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity); //постройка турели
        node.turret = turret;

        Debug.Log("Turret build! Money left: " + PlayerStats.Money);
    }

    public void SelectTurretToBuild (TurretBlueprint turret) //метод выбирает устанавливаемую туррель
    {
		turretToBuild = turret;
	}

}
