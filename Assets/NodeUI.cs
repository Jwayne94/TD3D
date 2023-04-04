using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Node target;

    public void SetTarget(Node _target) //функция выбирает нод
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();

        ui.SetActive(true);     //будет включать отображение элемента интерфейса
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

}
