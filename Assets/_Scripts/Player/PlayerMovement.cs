using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    private float moveSpeed = 1.5f;
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
    {
        myRigidbody.velocity = moveInput * moveSpeed;
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnDisable() 
    {
        myRigidbody.velocity = Vector2.zero;
    }



}
