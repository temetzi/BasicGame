using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Jump,
    DoubleJump,
    Attack
}


public class PlayerController : MonoBehaviour
{
    // Objects
    public PlayerState currentState = PlayerState.Idle;
    public LayerMask playerMask;
    public GameObject player; // Drag the player object into this field in the inspector
    private Rigidbody rb;
    public Transform groundCheckTransform;
    public PlayerActive playerActive;
    
    // Score
    public int score = 0;
    public GameObject scoreText;
    public GameObject scoreUI;
    
    // Movement
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 1f;
    public float jumpForce = 5f;
    public float doubleJumpForce = 5f;
    public float stopForce = 5f;
    
    // public bool moveDirection;
    public bool isGrounded = true;
    public bool doubleJump = true;
    


    private void Start()
    {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
    }


    void Update()
    {
        switch (currentState) {
            case PlayerState.Idle:
                // Debug.Log("PlayerState = Idle");
                
                if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)) {
                    currentState = PlayerState.Jump;
                }
                
                if (isGrounded == false && doubleJump && Input.GetKeyDown(KeyCode.Space)) {
                    currentState = PlayerState.DoubleJump;
                }

                if (isGrounded) {
                    doubleJump = true;
                }
                
                if (Input.GetKey(KeyCode.A)) {
                    // currentState = PlayerState.Idle;
                    rb.AddForce(Vector3.left * (moveSpeed * Time.deltaTime), ForceMode.VelocityChange);
                    
                    // Debug.Log("Move left");
                }

                else if (Input.GetKey(KeyCode.D)) {
                    // currentState = PlayerState.Idle;
                    // moveDirection = false;
                    // Debug.Log("Move Right");
                    rb.AddForce(Vector3.right * (moveSpeed * Time.deltaTime), ForceMode.VelocityChange);
                }
                
                else {
                    

                    // if (rb.linearVelocity.magnitude > 0) // Check if the object is moving
                    
                    Vector3 movementDirection = rb.linearVelocity;
                    movementDirection.Normalize();
                    movementDirection.y = 0f;
                    // Debug.Log(movementDirection);
                    if (rb.linearVelocity.x > 0.1 || rb.linearVelocity.x < -0.1) {
                        rb.AddForce(-movementDirection * stopForce);
                        // Debug.Log(rb.linearVelocity.x);
                    }


                } 
                break;
            
            case PlayerState.Jump:
                // Debug.Log("PlayerState = Jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                currentState = PlayerState.Idle;
                // currentState = PlayerState.Idle;
                break;
            
            case PlayerState.DoubleJump:
                // Debug.Log("PlayerState = DoubleJump");
                rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.VelocityChange);
                
                // rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.y, 1f, 15f), rb.linearVelocity.x);
                // Debug.Log(rb.linearVelocity.y);
                doubleJump = false;
                currentState = PlayerState.Idle;
                break;
            
            case PlayerState.Attack:
                // Debug.Log("PlayerState = Attack");
                // Implement Attack behavior here (e.g., deal damage to the player)
                break;
        }
    }

    void FixedUpdate()
    {
        // Debug.Log("notGrounded");
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            isGrounded = false;
            // Debug.Log("notGrounded");
            
        }

        else
        {
            isGrounded = true;
            // Debug.Log("IsGrounded");
        }
        
        
        // rb.linearVelocity = new Vector2 (Mathf.Clamp(rb.linearVelocity.x, 0 - moveSpeed, maxMoveSpeed), rb.linearVelocity.y);

        if (rb.linearVelocity.magnitude > 0) {
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x, -maxMoveSpeed , maxMoveSpeed), rb.linearVelocity.y);
            // Debug.Log(rb.linearVelocity.x);
        }
        // Debug.Log(rb.linearVelocity.x);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // layer 6 = Enemy
        if (other.gameObject.layer == 6)
        {
            playerActive.Death();

        }

        // layer 7 == fell out of map
        if (other.gameObject.layer == 7) 
        {
            playerActive.Death();
            
        }
        
        if (other.gameObject.layer == 9) 
        {
            Destroy(other.gameObject);
            // superJumpsRemaining++;
        }

        // level complete object
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            playerActive.Win();
        }

        // Score layer
        if (other.gameObject.layer == 11) 
        {
            Destroy(other.gameObject);
            score++;
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
            scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
            // Debug.Log(score);
            
        }
                            
        if (other.gameObject.layer == 14)
        {
            // wallJump = true;
            Debug.Log("jump = true");
        }
        
    }
}
