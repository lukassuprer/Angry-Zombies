using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Item : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter(Collider player)
    {
        if (playerController.health > 0 && playerController.health <= 80)
        {
            playerController.health += 20;
            Debug.Log("player");
            Destroy(this.gameObject);
        }
        else
        {

        }
    }
}
