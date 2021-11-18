using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Data Data;
    public GameObject UpgradesPanel;

    public Text ReproductionCurrentLvl;
    public Text UnitSpeedCurrentLvl;
    
    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenUpgradePanel()
    {
        UpgradesPanel.SetActive(true);
        ReproductionCurrentLvl.text = Data.ReproductionLvl.ToString();
        //UnitSpeedCurrentLvl.text = Data.MoveSpeedLvl.ToString();
    }
    
    public void CloseUpgradePanel()
    {
        UpgradesPanel.SetActive(false);
    }

    public void Upgrade(string id)
    {
        if (id.Equals("Rep"))
        {
            Data.ReproductionTime -= 0.02f;
            Data.ReproductionLvl += 1;
            ReproductionCurrentLvl.text = Data.ReproductionLvl.ToString();
        }
    }
}
