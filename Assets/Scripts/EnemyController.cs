using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask layerMask;
    
    private GameObject player;
    public bool hasLineOfSight = false;
    public bool isAtDistance = false;
    public float detectionRange = 5f;
    public float enemySpeed = 2f;

    public float leftBoundary;
    public float rightBoundary;
    private int direction = 1;
    public float startingPosition;
    
        
        
    public float enemyRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLineOfSight && isAtDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }

        else {
            
            EnemyPatrol();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true) 
        {
            other.gameObject.SetActive(false);
        }    
    }

    private void EnemyPatrol()
    {
        // moving left
         
        transform.Translate(Vector3.right * direction * enemySpeed * Time.deltaTime);

        // Check for boundaries and reverse direction if needed
        if (transform.position.x < startingPosition - leftBoundary)
        {
            direction = 1;
            // Debug.Log("vasemalla");
            // Debug.Log(direction);
        }
        else if (transform.position.x > startingPosition + rightBoundary)
        {
            direction = -1;
            //Debug.Log("oikealla");
            //Debug.Log(direction);
        }
        
        
    }

    private void FixedUpdate()
    {
        Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, layerMask);
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.white);
        // Debug.Log(hit.collider.gameObject.name);
        
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            hasLineOfSight = true;
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= detectionRange)
            {
                isAtDistance = true;
            }

            else {
                isAtDistance = false;
            }
        }

        else {
            hasLineOfSight = false;
            // Debug.Log("No hit");
        }
        

    }

}


