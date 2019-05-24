using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handleTextUi : MonoBehaviour
{
    public gameManager gm;
    public Text displayer;
    public towerScript Turret1;
    public towerScript Turret2;
    public towerScript Turret3;
    // Update is called once per frame
    void Start()
    {
    }
    void Update()
    {
        switch (displayer.name)
        {
            case "Hp":
                displayer.text = gm.playerHp.ToString();
                break;
            case "Energy":
                displayer.text = gm.playerEnergy.ToString();
                break;
            case "FireRateT1":
                displayer.text = Turret1.fireRate.ToString();
                break;
            case "FireRateT2":
                displayer.text = Turret2.fireRate.ToString();
                break;
            case "FireRateT3":
                displayer.text = Turret3.fireRate.ToString();
                break;
            case "DamageT1":
                displayer.text = Turret1.damage.ToString();
                break;
            case "DamageT2":
                displayer.text = Turret2.damage.ToString();
                break;
            case "DamageT3":
                displayer.text = Turret3.damage.ToString();
                break;
            case "RangeT1":
                displayer.text = Turret1.range.ToString();
                break;
            case "RangeT2":
                displayer.text = Turret2.range.ToString();
                break;
            case "RangeT3":
                displayer.text = Turret3.range.ToString();
                break;
            case "EnergyCostT1":
                displayer.text = Turret1.energy.ToString();
                break;
            case "EnergyCostT2":
                displayer.text = Turret2.energy.ToString();
                break;
            case "EnergyCostT3":
                displayer.text = Turret3.energy.ToString();
                break;
            default:
                break;
        }
    }
}
