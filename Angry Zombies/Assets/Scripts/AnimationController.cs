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

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        forwardPressed = Input.GetKey("w");
        rightPressed = Input.GetKey("d");
        leftPressed = Input.GetKey("a");
        backPressed = Input.GetKey("s");

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);

        Vertical();
        Horizontal();
    }

    private void Vertical(){
        if(forwardPressed && velocityZ < 0.5){
            velocityZ += Time.deltaTime * acceleration;
        }
        else if(backPressed && velocityZ > -0.5){
            velocityZ -= Time.deltaTime * acceleration;
        }

        if(!forwardPressed && !backPressed && velocityZ > 0.0){
            velocityZ -= Time.deltaTime * deceleration;
        }
        else if(!forwardPressed && !backPressed && velocityZ < 0.0){
            velocityZ = 0;
        }
    }
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
}
