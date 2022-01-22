﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public float health = 100f;
    public float stopDistance;
    public float damage = 0.0f;
    public float damageRate;
    private float lastShot = 0f;
    public GameObject zombie;
    public NavMeshAgent agent;
    private Vector3 player;
    public LayerMask layerPlayer;
    public BoxCollider boxCol;
    public Animator animator;
    public LineRenderer renderer;
    private PlayerController playerController;
    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<LineRenderer>();
        animator.SetBool("isRunning", true);
        playerController = FindObjectOfType<PlayerController>();
        player = GameManager.playerInstance.transform.position;
        renderer.startWidth = 0.15f;
        renderer.endWidth = 0.15f;
        renderer.positionCount = 0;

        agent.SetDestination(player = GameManager.playerInstance.transform.position);
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
            Stop();
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            agent.ResetPath();
            transform.GetComponent<ZombieScript>().enabled = false;
        }
        DrawPath();
        FindPlayer();
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
        transform.GetComponent<ZombieScript>().enabled = false;
        animator.SetBool("isDead", true);
        SetAllCollidersStatus(false);
        StartCoroutine("wait");
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        zombie.SetActive(false);
    }

    private void Stop()
    {
        bool distance = agent.remainingDistance > agent.stoppingDistance;
        if (Vector3.Distance(player, transform.position) <= 3)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);
            agent.isStopped = true;
            agent.SetDestination(player = GameManager.playerInstance.transform.position);
        }
        else if (agent.isStopped == true)
        {
            agent.enabled = true;
            agent.isStopped = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", true);
            agent.SetDestination(player = GameManager.playerInstance.transform.position);
        }
        else if (Vector3.Distance(player, transform.position) >= 3 && agent.isStopped == true)
        {
            agent.enabled = true;
            agent.isStopped = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", true);
            agent.SetDestination(player = GameManager.playerInstance.transform.position);
        }
    }
    private void DealDamage()
    {
        if (Time.time > damageRate + lastShot)
        {
            // PlayerController playerController = gameObject.GetComponent<PlayerController>();
            playerController.health -= damage;
            lastShot = Time.time;
            agent.SetDestination(player = GameManager.playerInstance.transform.position);
        }
    }
    private void FindPlayer()
    {
        // if (Vector3.Distance(player, transform.position) <= 50)
        // {
        //     agent.SetDestination(player = GameManager.playerInstance.transform.position);
        // }
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
}
