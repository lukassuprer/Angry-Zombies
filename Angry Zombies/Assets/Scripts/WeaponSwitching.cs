using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public static int selectedWeapon = 0;
    public WeaponScript weaponScript;
    public List<Gun> guns = new List<Gun>();
    void Start()
    {
        //SelectWeapon();
        selectedWeapon = 0;
        weaponScript.currentGun = guns[0];
        guns[0].ammoContainer.SetActive(true);
        guns[1].ammoContainer.SetActive(false);
        guns[2].ammoContainer.SetActive(false);
    }
    private void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            weaponScript.currentGun = guns[0];
            guns[0].ammoContainer.SetActive(true);
            guns[1].ammoContainer.SetActive(false);
            guns[2].ammoContainer.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
            weaponScript.currentGun = guns[1];
            guns[0].ammoContainer.SetActive(false);
            guns[1].ammoContainer.SetActive(true);
            guns[2].ammoContainer.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
            weaponScript.currentGun = guns[2];
            guns[2].ammoContainer.SetActive(true);
            guns[0].ammoContainer.SetActive(false);
            guns[1].ammoContainer.SetActive(false);
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
