using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    // public string weaponName;
    // public float damage;
    // public float fireRate;
    // public float range;
    // public float currentAmmo;
    // public float maxAmmo;
    //public Text ammo;
    // public Transform weaponEnd;
    // public ParticleSystem muzzleFlash;
    private float lastShot = 0f;
    public bool isReloading;
    public Camera cam;
    // public LineRenderer renderer;
    // public GameObject impactEffect;

    // public string soundName;

    public Gun currentGun;

    private void Start(){
        currentGun.currentAmmo = currentGun.maxAmmo;
        //currentGun.weaponName = name;
    }
    void Update()
    {
        //particleSystemPlayed = false;
        Laser();
        if(Input.GetButton("Fire1") && isReloading == false){
            if( Time.time > currentGun.fireRate + lastShot){
                Shoot();
                lastShot = Time.time;
                currentGun.currentAmmo -= 1;
            }
        }
        else{   
            currentGun.muzzleFlash.Stop(true);
        }
        Reload();
    }

    private void Shoot(){
        currentGun.muzzleFlash.Play();

        for(int i = 0; i < currentGun.shots; i++){
            float randomOffSet = Random.Range(-currentGun.offset, currentGun.offset);
            Vector3 offset = randomOffSet * currentGun.weaponEnd.right;
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(currentGun.weaponEnd.position, -currentGun.weaponEnd.forward + offset, out hit, currentGun.range)){
                ZombieScript zombieScript = hit.transform.GetComponent<ZombieScript>();
                if(zombieScript != null){
                    zombieScript.TakeDamage(currentGun.damage);
                }

                GameObject impactGO = Instantiate(currentGun.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
                FindObjectOfType<SoundManager>().Play(currentGun.soundName);
            }
        }
    }

    private void Laser(){
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(currentGun.weaponEnd.position, -currentGun.weaponEnd.forward, out hit, currentGun.range);

        currentGun.renderer.enabled = true;
        currentGun.renderer.SetPosition(0, currentGun.weaponEnd.position);
        currentGun.renderer.SetPosition(1, hit.point);
        
    }
    private void Reload(){
       if(currentGun.currentAmmo <= 0 && !isReloading){   
           currentGun.ammoText.text = $"0/{currentGun.maxAmmo}";
           StartCoroutine(WaitReload());
       }
       else{
           currentGun.ammoText.text = $"{currentGun.currentAmmo}/{currentGun.maxAmmo}";
       }
   }
    IEnumerator WaitReload(){
        isReloading = true;
        yield return new WaitForSeconds(3);
        isReloading = false;
        currentGun.currentAmmo = currentGun.maxAmmo;
    }
}
