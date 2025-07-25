using System;
using TMPro;
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
    public AudioSource audioSource;
    public GameObject AudioManagerObject;
    
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
    public bool IsMoving = false;
    
    bool mybool;
    bool checkit;
    
    public bool doubleJump = true;
    
    private void Start()
    {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        // audioSource = GameObject.FindGameObjectWithTag("WalkAudioSource");
        // audioSource = audioSource.GetComponent<AudioSource>();

        AudioManagerObject = GameObject.FindGameObjectWithTag("AudioSourceTag");
        audioSource = AudioManagerObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>();
        // Debug.Log(audioSource.gameObject.name);

    }

    void Update()
    {
        

        if (rb.linearVelocity.y < 0.1f && rb.linearVelocity.x > 0.1f || rb.linearVelocity.x < -0.1f) {
            IsMoving = true;
        }

        else {
            IsMoving = false;
        }
        
        switch (currentState) {
            case PlayerState.Idle:
                
                if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)) {
                    currentState = PlayerState.Jump;
                    // Debug.Log("PlayerState Jump");
                }
                
                if (isGrounded == false && doubleJump && Input.GetKeyDown(KeyCode.Space)) {
                    currentState = PlayerState.DoubleJump;
                    // Debug.Log("PlayerState DoubleJump");
                }

                if (isGrounded) {
                    doubleJump = true;
                }
                
                if (Input.GetKey(KeyCode.A)) {                 
                    rb.AddForce(Vector3.left * (moveSpeed * Time.deltaTime), ForceMode.Acceleration);                  
                    // Debug.Log("Move left");
                }

                else if (Input.GetKey(KeyCode.D)) 
                {                 
                    rb.AddForce(Vector3.right * (moveSpeed * Time.deltaTime), ForceMode.Acceleration);
                    // Debug.Log("Move right");
                }
                
                else 
                {                                                   
                    Vector3 movementDirection = rb.linearVelocity;
                    movementDirection.Normalize();
                    movementDirection.y = 0f;
                    
                   
                    //if (rb.linearVelocity.x > 0.001 || rb.linearVelocity.x < -0.001) {
                    //    rb.AddForce(-movementDirection * stopForce);                     
                    //}

                    if (rb.linearVelocity.x > 0.1)
                    {
                        // rb.AddForce(-movementDirection * stopForce);
                        rb.AddForce(Vector3.left * (stopForce * Time.deltaTime), ForceMode.VelocityChange);
                    }

                    else if (rb.linearVelocity.x < -0.1)
                    {
                        // rb.AddForce(-movementDirection * stopForce);
                        rb.AddForce(Vector3.right * (stopForce * Time.deltaTime), ForceMode.VelocityChange);
                    }

                    else
                    {
                        // rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
                        // Debug.Log("Stopped");
                    }
                }

                
                
                break;
            
            case PlayerState.Jump:
                // checkit = true;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                // checkit = true;
                currentState = PlayerState.Idle;
                // Debug.Log("PlayerState Idle");               
                break;
            
            case PlayerState.DoubleJump:               
                rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.VelocityChange);                            
                doubleJump = false;
                currentState = PlayerState.Idle;
                // Debug.Log("PlayerState Idle");
                break;
            
            case PlayerState.Attack:
                // Debug.Log("PlayerState = Attack");
                // Implement Attack behavior here (e.g., deal damage to the player)
                break;
        }
    }

    void FixedUpdate()
    {      
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) 
        {
            isGrounded = false;                    
        }

        else
        {
            isGrounded = true;
            
            if (IsMoving && isGrounded) {
            
                if (!audioSource.isPlaying && IsMoving && rb.linearVelocity.magnitude > 0.5f)
                {
                    // AudioManager.Instance.playSFX("Walk");
                    AudioManager.Instance.playWalk("Walk");
                }

                else {
                    // audioSource.Stop();
                }
            }

            else {

                if (audioSource != null) {
                    audioSource.Stop();
                }
            }
            
            
        }
                       
        if (rb.linearVelocity.magnitude > 0) {
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x, -maxMoveSpeed , maxMoveSpeed), rb.linearVelocity.y);          
        }
        
        // Debug.Log(rb.linearVelocity.y);
        
        // landing sound
        if (isGrounded != checkit) {
            checkit = isGrounded;
            if (isGrounded == true) {
                
                if (rb.linearVelocity.y < -3f || rb.linearVelocity.y > 3f) {
                    AudioManager.Instance.playSFX("Jump");
                }

                else {
                    // Debug.Log("Didnt land");
                }

            }
        }
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
            AudioManager.Instance.playSFX("CoinPickUp");
            // Debug.Log(score);
            
        }
                            
        if (other.gameObject.layer == 14)
        {
            // wallJump = true;
            Debug.Log("jump = true");
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        
    }
}
