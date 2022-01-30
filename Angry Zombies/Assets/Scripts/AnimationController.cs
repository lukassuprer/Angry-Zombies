using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private float velocityX = 0f;
    private float velocityZ = 0f;
    public float acceleration = 2f;
    public float deceleration = 2f;

    private bool forwardPressed;
    private bool rightPressed;
    private bool leftPressed;
    private bool backPressed;

    private Vector3 mousePos;
    private Vector3 posDif;
    public Camera cam;
    private void Update()
    {   
        GetInput();

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);

        Vertical();
        Horizontal();
    }

    //Move forward and backward
    private void Vertical(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
 
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = mousePos - transform.position;

        if(forwardPressed && velocityZ < 0.5 && Vector3.Dot(forward, toOther) < 0){
            velocityZ += Time.deltaTime * acceleration;
        }
        else if(forwardPressed && velocityZ > -0.5 && Vector3.Dot(forward, toOther) > 0){
            velocityZ -= Time.deltaTime * acceleration;
        }
        else if(backPressed && velocityZ > -0.5 && Vector3.Dot(forward, toOther) < 0){
            velocityZ -= Time.deltaTime * acceleration;
        }
        else if(backPressed && velocityZ < 0.5 && Vector3.Dot(forward, toOther) > 0){
            velocityZ += Time.deltaTime * acceleration;
        }

        if(!forwardPressed && !backPressed && velocityZ > 0.0){
            velocityZ -= Time.deltaTime * deceleration;
        }
        else if(!forwardPressed && !backPressed && velocityZ < 0.0){
            velocityZ = 0;
        }
    }
    //Move right and left
    private void Horizontal(){
        if(leftPressed && velocityX > -0.5){
            velocityX -= Time.deltaTime * acceleration;
        }
        else if(rightPressed && velocityX < 0.5){
            velocityX += Time.deltaTime * acceleration;
        }

        if(!leftPressed && velocityX < 0.0){
            velocityX += Time.deltaTime * deceleration;
        }
        else if(!rightPressed && velocityX > 0.0){
            velocityX -= Time.deltaTime * deceleration;
        }

        if(!leftPressed && !rightPressed && velocityX != 0 && (velocityX > -0.05 && velocityX < 0.05)){
            velocityX = 0;
        }
    }
    //Get horizontal and vertical input and assign it to booleans
    private void GetInput(){
        float vertical = Input.GetAxis("Vertical");
        if(vertical > 0.2f){
            forwardPressed = true;
        }
        else if(vertical < -0.2f){
            backPressed = true;
        }
        else if(vertical <= 0.15f && vertical >= -0.15f){
            forwardPressed = false;
            backPressed = false;
        }

        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal > 0.2f){
            rightPressed = true;
        }
        else if(horizontal < -0.2f){
            leftPressed = true;
        }
        else if(horizontal <= 0.15f && horizontal >= -0.15f){
            rightPressed = false;
            leftPressed = false;
        }
    }
}
