using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
    {
        myRigidbody.velocity = moveInput;
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        print(value);
    }



}
