using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool jumpKeyWasPressed;
    public float horizontalInput;
    private Rigidbody rigidbodyComponent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpKeyWasPressed = true;
        }
        
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (jumpKeyWasPressed) 
        {
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);
    }
}
