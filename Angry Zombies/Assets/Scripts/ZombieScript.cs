using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class ZombieScript : MonoBehaviour, IPooledObject
{
    public float health = 100f;
    public float stopDistance;
    public float damage = 0.0f;
    public float damageRate;
    private float lastShot = 0f;
    public GameObject zombie;
    public NavMeshAgent agent;
    public Transform playerPos;
    public LayerMask layerPlayer;
    public Animator animator;
    public LineRenderer renderer;
    public PlayerController playerController;
    public GameObject[] droppableItem;
    public void OnObjectSpawn()
    {
        playerPos = GameObject.FindObjectOfType<PlayerController>().transform;
        playerController = GameObject.FindObjectOfType<PlayerController>();
        animator.SetBool("isRunning", true);
        renderer.startWidth = 0.15f;
        renderer.endWidth = 0.15f;
        renderer.positionCount = 0;
        agent.SetDestination(playerPos.position);
        InvokeRepeating("FindPlayer", 2f, 2f);
    }

    private void Update()
    {
        if (playerController != null && playerController.health > 0 && agent.isStopped == false && agent.enabled == true)
        {
            //agent.SetDestination(player = GameManager.playerInstance.transform.position);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }
        if (agent.isStopped == false)
        {
        }
        if (playerController.health > 0)
        {
            StopAtPlayer();
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            agent.ResetPath();
            //transform.GetComponent<ZombieScript>().enabled = false;
            this.enabled = false;
        }
        DrawPath();

    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        //transform.GetComponent<ZombieScript>().enabled = false;
        this.enabled = false;
        CancelInvoke();
        animator.SetBool("isDead", true);
        SetAllCollidersStatus(false);
        StartCoroutine("wait");
    }
    private void DropItem()
    {
        int i = Random.Range(0, 21);
        int x = Random.Range(1, 3);
        if(i == 5 || i == 10 || i == 15 || i == 20){
            Instantiate(droppableItem[0], this.transform.position, Quaternion.identity);
        }
        else if(i == 1 || i == 9 || i == 13 || i == 16){
            if(Pickable_Gun.gunsUnlocked == false && x == 1){
                if(Pickable_Gun.shotgunUnlocked == false)
                Instantiate(droppableItem[1], this.transform.position, Quaternion.identity);
            }
            else if(Pickable_Gun.gunsUnlocked == false && x == 2){
                if(Pickable_Gun.umpUnlocked == false)
                Instantiate(droppableItem[2], this.transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        GameManager.score += 10;
        zombie.SetActive(false);
        DropItem();
    }

    private void StopAtPlayer()
    {
        bool distance = agent.remainingDistance > agent.stoppingDistance;
        if (Vector3.Distance(playerPos.position, transform.position) <= 3)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);
            agent.isStopped = true;
            agent.SetDestination(playerPos.position);
        }
        else if (agent.isStopped == true)
        {
            agent.enabled = true;
            agent.isStopped = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", true);
            agent.SetDestination(playerPos.position);
        }
        else if (Vector3.Distance(playerPos.position, transform.position) >= 3 && agent.isStopped == true)
        {
            agent.enabled = true;
            agent.isStopped = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", true);
            agent.SetDestination(playerPos.position);
        }
    }
    private void DealDamage()
    {
        if (Time.time > damageRate + lastShot)
        {
            // PlayerController playerController = gameObject.GetComponent<PlayerController>();
            playerController.health -= damage;
            FindObjectOfType<SoundManager>().Play("zombie bite");
            lastShot = Time.time;
            agent.SetDestination(playerPos.position);
        }
    }

    void DrawPath()
    {
        renderer.positionCount = agent.path.corners.Length;
        renderer.SetPosition(0, transform.position);

        if (agent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < agent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(agent.path.corners[i].x, agent.path.corners[i].y, agent.path.corners[i].z);
            renderer.SetPosition(1, pointPosition);
        }
    }
    public void SetAllCollidersStatus(bool active)
    {
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = active;
        }
    }
    private void FindPlayer()
    {
        if (agent.isStopped == false)
        {
            agent.ResetPath();
            agent.SetDestination(playerPos.position);
        }
    }
}
