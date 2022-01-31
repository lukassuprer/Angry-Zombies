using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponSwitching : MonoBehaviour
{
    [System.Serializable]
    public class Gun
    {
        //Just a list that holds info about every gun
        public string weaponName;
        public float currentAmmo;
        public float maxAmmo;
        public float range;
        public float damage;
        public float fireRate;
        public Transform weaponEnd;
        public ParticleSystem muzzleFlash;
        public GameObject weapon;
        public GameObject impactEffect;
        public LineRenderer renderer;
        public Text ammoText;
        public GameObject ammoContainer;
        public float shots;
        public float offset;
        public string soundName;
    }
    public static int selectedWeapon = 0;
    public WeaponScript weaponScript;
    public List<Gun> guns = new List<Gun>();
    void Start()
    {
        //Selects M4A4 as starting weapon
        selectedWeapon = 0;
        weaponScript.currentGun = guns[0];
        guns[0].ammoContainer.SetActive(true);
        guns[1].ammoContainer.SetActive(false);
        guns[2].ammoContainer.SetActive(false);
    }
    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        //Selects M4A4
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            weaponScript.currentGun = guns[0];
            guns[0].ammoContainer.SetActive(true);
            guns[1].ammoContainer.SetActive(false);
            guns[2].ammoContainer.SetActive(false);
        }
        //Selects shotgun
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && Pickable_Gun.shotgunUnlocked == true)
        {
            selectedWeapon = 1;
            weaponScript.currentGun = guns[1];
            guns[0].ammoContainer.SetActive(false);
            guns[1].ammoContainer.SetActive(true);
            guns[2].ammoContainer.SetActive(false);
        }
        //Selects UMP
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && Pickable_Gun.umpUnlocked == true)
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
