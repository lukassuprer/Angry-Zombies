using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveHorizontal;
    public float moveVertical;
    public float speed;
    public Rigidbody rb;
    public Camera cam;
    public Animator animator;
    public GameObject weaponSwitch;
    public float health = 100f;
    private void Awake(){
        GameManager.playerInstance = this;
    }
    void FixedUpdate()
    {
        GameManager.playerInstance = this;
        Move();
        Rotation();

        if(health <= 0){
            Dead();
        }
    }
    //Handles movement
    private void Move(){
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(-moveHorizontal, 0, -moveVertical);
        rb.MovePosition(transform.position + move * Time.deltaTime * speed);
    }
    //Handles rotation towards mouse
    private void Rotation(){
        Vector3 playerPos = cam.WorldToScreenPoint(rb.transform.position);
        Vector3 mousePos = Input.mousePosition;

        Vector3 mouseLook = mousePos - playerPos;
        Vector3 finalVector = new Vector3(-mouseLook.x, 0, -mouseLook.y);

        rb.rotation = Quaternion.LookRotation(finalVector);
    }
    //Called when dead
    private void Dead(){
        animator.SetBool("isDead", true);
        this.enabled = false;
        weaponSwitch.SetActive(false);
    }
}
