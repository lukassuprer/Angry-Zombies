using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool forwardPressed = Input.GetKey("w");

        if(forwardPressed && !isRunning){
            animator.SetBool("isRunning", true);
        }
        if(!forwardPressed && isRunning){
            animator.SetBool("isRunning", false);
        }
    }
}
