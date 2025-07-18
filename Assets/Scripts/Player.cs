// using System;
using System;
using TMPro;
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
    
    public float horizontalInput;
    public Rigidbody rigidbodyComponent;
    public Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    public int superJumpsRemaining;
    
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

    public void Death()
    {
        if (gameObject == null) {
            return;
        }
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
    }
}
