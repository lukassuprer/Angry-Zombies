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
    public float smoothBlend = 0.1f;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool backPressed = Input.GetKey("s");

        if(forwardPressed && velocityZ < 0.5){
            velocityZ += Time.deltaTime * acceleration;
        }
        if(leftPressed && velocityX > -0.5){
            velocityX -= Time.deltaTime * acceleration;
        }
        if(rightPressed && velocityX < 0.5){
            velocityX += Time.deltaTime * acceleration;
        }
        if(backPressed && velocityZ > -0.5){
            velocityZ -= Time.deltaTime * acceleration;
        }

        if(!forwardPressed && !backPressed && velocityZ > 0.0){
            velocityZ -= Time.deltaTime * deceleration;
        }
        if(!forwardPressed && !backPressed && velocityZ < 0.0){
            velocityZ = 0;
        }

        if(!leftPressed && velocityX < 0.0){
            velocityX += Time.deltaTime * deceleration;
        }
        if(!rightPressed && velocityX > 0.0){
            velocityX -= Time.deltaTime * deceleration;
        }

        if(!leftPressed && !rightPressed && velocityX != 0 && (velocityX > -0.05 && velocityX < 0.05)){
            velocityX = 0;
        }

        // float x = Input.GetAxis("Horizontal");
        // float y = Input.GetAxis("Vertical");
        // velocityZ = Mathf.Clamp(y, -0.5f, 0.5f);
        // velocityX = Mathf.Clamp(x, -0.5f, 0.5f);

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
}
