using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    private float lastShot = 0f;
    public bool isReloading;
    public Camera cam;
    public Gun currentGun;
    
    private void Start(){
        //Just setting guns ammo to one that is set in list
        currentGun.currentAmmo = currentGun.maxAmmo;
    }
    void Update()
    {
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
    //Called when pressing shoot button, shots ray from gun end towards mouse. If this is shotgun it has spread.
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
    //For debbuging and visual effect
    private void Laser(){
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(currentGun.weaponEnd.position, -currentGun.weaponEnd.forward, out hit, currentGun.range);

        currentGun.renderer.enabled = true;
        currentGun.renderer.SetPosition(0, currentGun.weaponEnd.position);
        currentGun.renderer.SetPosition(1, hit.point);
        
    }
    //Called when current ammo drops to 0
    private void Reload(){
       if(currentGun.currentAmmo <= 0 && !isReloading){   
           currentGun.ammoText.text = $"0/{currentGun.maxAmmo}";
           StartCoroutine(WaitReload());
       }
       else{
           currentGun.ammoText.text = $"{currentGun.currentAmmo}/{currentGun.maxAmmo}";
       }
   }
   //Called when reloading to wait some time
    IEnumerator WaitReload(){
        isReloading = true;
        yield return new WaitForSeconds(3);
        isReloading = false;
        currentGun.currentAmmo = currentGun.maxAmmo;
    }
}
