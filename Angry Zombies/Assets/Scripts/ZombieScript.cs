using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float health = 100f;
    public GameObject zombie;
    public void TakeDamage(float amount){
        health -= amount;
        if(health <= 0){
            Die();
        }
    }
    private void Die(){
        zombie.SetActive(false);
    }
}
