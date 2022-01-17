using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage;
    public float range;
    public Transform weaponEnd;
    public Camera cam;
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    private void Shoot(){
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Vector3 mousePos = Input.mousePosition;

        if(Physics.Raycast(weaponEnd.position, -weaponEnd.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            ZombieScript zombieScript = hit.transform.GetComponent<ZombieScript>();
            if(zombieScript != null){
                zombieScript.TakeDamage(damage);
            }
        }
    }
}
