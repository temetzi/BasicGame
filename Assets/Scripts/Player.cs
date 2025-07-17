// using System;

using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool jumpKeyWasPressed;
    public float horizontalInput;
    public Rigidbody rigidbodyComponent;
    public Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    
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
        rigidbodyComponent.linearVelocity = new Vector3(horizontalInput, rigidbodyComponent.linearVelocity.y, 0);
            
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }
            
        
        if (jumpKeyWasPressed) 
        {
            rigidbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        
        
    }

    
}
