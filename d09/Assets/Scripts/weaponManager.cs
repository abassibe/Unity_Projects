using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    public GameObject gun;

    public GameObject rifle;

    private GameObject equipedWeapon;

    void Start()
    {
        equipedWeapon = gun;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            rifle.SetActive(true);
            gun.SetActive(false);
            equipedWeapon = rifle;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gun.SetActive(true);
            rifle.SetActive(false);
            equipedWeapon = gun;
        }
    }
}
