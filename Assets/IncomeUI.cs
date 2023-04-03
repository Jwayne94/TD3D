using UnityEngine;
using UnityEngine.UI;


public class IncomeUI : MonoBehaviour
{
    public Text incomeText;


    // Update is called once per frame
    void Update()
    {
        incomeText.text = "Income " + PlayerStats.Income + "$ in: " + string.Format("{0:0}", PlayerStats.Timer);

    }
}
