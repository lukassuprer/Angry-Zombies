using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    public string weaponName;
    public float damage;
    public float fireRate;
    private float lastShot = 0f;
    public float range;
    public float currentAmmo;
    public float maxAmmo;
    public bool isReloading;
    public Transform weaponEnd;
    public Camera cam;
    public LineRenderer renderer;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Text ammo;

    private void Start(){
        renderer = GetComponent<LineRenderer>();
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        //particleSystemPlayed = false;
        Laser();
        if(Input.GetButton("Fire1") && isReloading == false){
            if( Time.time > fireRate + lastShot){
                Shoot();
                lastShot = Time.time;
                currentAmmo -= 1;
            }
        }
        else{   
            muzzleFlash.Stop(true);
        }
        Reload();
    }

    private void Shoot(){
        muzzleFlash.Play();

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Vector3 mousePos = Input.mousePosition;
        // Instantiate(muzzleFlash, weaponEnd.position, weaponEnd.rotation);
        // muzzleFlash.transform.SetParent(weaponEnd);
        // Destroy(muzzleFlash);

        if(Physics.Raycast(weaponEnd.position, -weaponEnd.forward, out hit, range)){
            ZombieScript zombieScript = hit.transform.GetComponent<ZombieScript>();
            if(zombieScript != null){
                zombieScript.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
            FindObjectOfType<SoundManager>().Play("M16 Shoot");
        }
    }

    private void Laser(){
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(weaponEnd.position, -weaponEnd.forward, out hit, range);

        renderer.enabled = true;
        renderer.SetPosition(0, weaponEnd.position);
        renderer.SetPosition(1, hit.point);
        
    }
    private void Reload(){
       if(currentAmmo <= 0 && !isReloading){   
           ammo.text = $"0/{maxAmmo}";
           StartCoroutine(WaitReload());
       }
       else{
           ammo.text = $"{currentAmmo}/{maxAmmo}";
       }
   }
    IEnumerator WaitReload(){
        isReloading = true;
        yield return new WaitForSeconds(3);
        isReloading = false;
        currentAmmo = maxAmmo;
    }
}
