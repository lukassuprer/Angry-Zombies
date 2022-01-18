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

    private void Start(){
        renderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        Laser();
        if(Input.GetButton("Fire1") && Time.time > fireRate + lastShot){
            Shoot();
            lastShot = Time.time;
        }

    }

    private void Shoot(){
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Vector3 mousePos = Input.mousePosition;

        if(Physics.Raycast(weaponEnd.position, -weaponEnd.forward, out hit, range)){
            ZombieScript zombieScript = hit.transform.GetComponent<ZombieScript>();
            if(zombieScript != null){
                zombieScript.TakeDamage(damage);
            }
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
