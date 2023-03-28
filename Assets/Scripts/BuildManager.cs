using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //следующая функция будет создавать общую копию класса от любого объекта

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;

    void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    private GameObject turretToBuild;
    
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
