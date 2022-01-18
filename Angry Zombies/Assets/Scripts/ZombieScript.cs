using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public float health = 100f;
    public float stopDistance;
    public float damage = 0.0f;
    public GameObject zombie;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask layerPlayer;
    public BoxCollider boxCol;

    private void Start() {
        boxCol = GetComponent<BoxCollider>();
    }

    private void Update(){
        agent.SetDestination(player.position);
        DealDamage();
    }
    public void TakeDamage(float amount){
        health -= amount;
        if(health <= 0){
            Die();
        }
    }
    private void Die(){
        zombie.SetActive(false);
    }

    private void DealDamage()
    {
        //přes vzdálenosti
        bool distance = agent.remainingDistance > agent.stoppingDistance;
        if(!distance){ 
            agent.isStopped = true;
            // PlayerController playerController = GetComponent<PlayerController>();
            // Debug.Log(playerController.health);
            //agent.isStopped = true;
            //playerController.health -= damage;
        }
        else{
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }
}
