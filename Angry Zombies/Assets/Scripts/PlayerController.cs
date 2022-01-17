﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mh;
    public float mv;
    public float speed;
    public Rigidbody rb;
    public Camera cam;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
        Rotation();
    }

    private void Move(){
        mh = Input.GetAxis("Horizontal");
        mv = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(-mh, 0, -mv);
        rb.MovePosition(transform.position + move * Time.deltaTime * speed);
    }
    private void Rotation(){
        Vector3 playerPos = cam.WorldToScreenPoint(rb.transform.position);
        Vector3 mousePos = Input.mousePosition;

        Vector3 mouseLook = mousePos - playerPos;
        Vector3 finalVector = new Vector3(-mouseLook.x, 0, -mouseLook.y);

        rb.rotation = Quaternion.LookRotation(finalVector);
    }
}
