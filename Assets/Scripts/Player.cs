// using System;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool jumpKeyWasPressed;
    public int moveSpeed;

    public GameObject winScreen;
    public GameObject gameOverScreen;
    public int score = 0;
    public GameObject scoreText;
    public float jumpPower = 10f;
    
    public float horizontalInput;
    public Rigidbody rigidbodyComponent;
    public Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    

    public int superJumpsRemaining;
    public bool grounded = true;
    public bool wallJump = false;

    public float maxSpeed = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
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
            grounded = false;
            //Debug.Log("notGrounded");
            
        }

        

        else
        {
            grounded = true;
            //Debug.Log("IsGrounded");
        }
     

        

        if (jumpKeyWasPressed && grounded == true)
        {

            
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
            wallJump = false;

        }

        else if (jumpKeyWasPressed && wallJump == true)
        {
                        
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
            wallJump = false;
        }

        // if (rigidbodyComponent.linearVelocity.magnitude > maxSpeed)
        //{
            // rigidbodyComponent.linearVelocity = Vector3.ClampMagnitude(rigidbodyComponent.linearVelocity, maxSpeed);
            // Debug.Log("speed");
        //}



    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 7) 
        {
            gameOverScreen.SetActive(true);
            gameObject.SetActive(false);
            
        }
        
        if (other.gameObject.layer == 9) 
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
        
        if (other.gameObject.layer == 11) 
        {
            Destroy(other.gameObject);
            score++;
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
            // Debug.Log(score);
            
        }
        
        // GameOver Screen
        if (other.gameObject.layer == 10) 
        {
            Destroy(other.gameObject);
            winScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        
        

        if (other.gameObject.layer == 14)
        {
            wallJump = true;
            Debug.Log("jump = true");
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (wallJump == true)
        {
            wallJump = false;
        }
    }
}
