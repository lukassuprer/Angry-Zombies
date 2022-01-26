using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Gun
{
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
