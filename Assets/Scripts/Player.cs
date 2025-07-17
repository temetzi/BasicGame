// using System;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool jumpKeyWasPressed;
    public int moveSpeed;
    
    public float horizontalInput;
    public Rigidbody rigidbodyComponent;
    public Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    public int superJumpsRemaining;
    
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

        if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("StartScene");
        }
        
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.linearVelocity = new Vector3(horizontalInput * moveSpeed, rigidbodyComponent.linearVelocity.y, 0);
        // Debug.Log(rigidbodyComponent.linearVelocity.y);
            
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }
            
        
        if (jumpKeyWasPressed) {
            
            float jumpPower = 5;
            if (superJumpsRemaining > 0) {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) 
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
        
        if (other.gameObject.layer == 10) 
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("StartScene");
        }
    }
}
