using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standartTurret;
    public TurretBlueprint missleLauncher;


    BuildManager buildManager;

	void Start ()
	{
		buildManager = BuildManager.instance; //����� ����� ������� �� ��� 
	}

	public void SelectStandardTurret ()
	{
		Debug.Log("Standard Turret Selected");
		buildManager.SelectTurretToBuild(standartTurret);
	}

	public void SelectMissleLauncher()
	{
		Debug.Log("Missle Launcher Selected");
		buildManager.SelectTurretToBuild(missleLauncher);
	}

}
