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

    public GameObject buildEffect;

	private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } } //Функция проверяет на наличие значения turretToBuld, возвращает значение истины 
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } } //Функция проверяет на наличие денег, возвращает значение истины 

    public void SelectNode(Node node) //метод выбирает туррель, которая установлена на платформе
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode() //функция помогает закрыть элемент интерфейса
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild (TurretBlueprint turret) //метод выбирает устанавливаемую туррель
    {
		turretToBuild = turret;
        DeselectNode();
	}

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
