using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage;
    public float fireRate;
    private float lastShot = 0f;
    public float range;
    public Transform weaponEnd;
    public Camera cam;
    public LineRenderer renderer;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private void Start(){
        renderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        //particleSystemPlayed = false;
        Laser();
        if(Input.GetButton("Fire1")){
            if( Time.time > fireRate + lastShot){
                Shoot();
                lastShot = Time.time;
            }
        }
        else{   
            muzzleFlash.Stop(true);
        }
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
}
