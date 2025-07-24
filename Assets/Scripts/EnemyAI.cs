using NUnit;
using System.Collections;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;


public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    LookFor,
    Attack
}


public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;
    public Transform player; // Drag the player object into this field in the inspector
    private Vector3 playerPos;
    
    // enemy stuff
    public LayerMask layerMask;
    private bool hasLineOfSight = false;
    private bool isAtDistance = false;
    public float detectionRange = 0f;
    // public float chaseRange = 0f;
    // private bool minimumDistanceToPlayer = true;

    public float chaseDistance = 10f;
    public float enemySpeed = 1f;
    public float chaseSpeed = 4f;

    public float leftBoundary;
    public float rightBoundary;
    private int direction = 1;
    private float startingPosition;
    private float startPositionZ;
    private float startPositionY;
    
    private Renderer enemyColor;

    private void Start()
    {
        startingPosition = transform.position.x;
        startPositionZ = transform.position.z;
        startPositionY = transform.position.y;
        enemyColor = gameObject.GetComponent<Renderer>();
        
    }


    void Update()
    {
        gameObject.transform.position.Set(transform.position.x, startPositionY, startPositionZ);
        
        
        switch (currentState)
        {
            case EnemyState.Idle:
                // enemyColor.material.SetColor("_Color", Color.yellow);
                // enemyColor.material.SetColor("_Color", Color.red);
                // Implement Idle behavior here (e.g., do nothing)
                if (Vector3.Distance(transform.position, player.position) < detectionRange && player != null)
                {
                    Debug.Log("Patrol State");
                    Debug.Log("We are < color = green > green </ color > with envy");
                    currentState = EnemyState.Patrol;
                    
                }

                break;

            case EnemyState.Patrol:
                // Implement Patrol behavior here (e.g., walk a predefined path)
                // enemyColor.material.SetColor("_Color", Color.yellow);
                // enemyColor.material.SetColor("_Color", Color.red);

                transform.Translate(Vector3.right * direction * enemySpeed * Time.deltaTime);

                // 1. liikkuu vasemmalle 2. liikkuu oikealle
                if (transform.position.x < startingPosition - leftBoundary)
                {
                    direction = 1;
                    
                }
                else if (transform.position.x > startingPosition + rightBoundary)
                {
                    direction = -1;                  
                }

                if (hasLineOfSight && isAtDistance)
                {
                    Debug.Log("Chase State");
                    currentState = EnemyState.Chase;
                }

                // Back to Idle
                if (Vector3.Distance(transform.position, player.position) > detectionRange * 1.25)
                {
                    Debug.Log("Idle State");
                    currentState = EnemyState.Idle;
                }

                break;
         
            case EnemyState.Chase:
                // Implement Chase behavior here (e.g., move towards the player)
                // enemyColor.material.SetColor("_Color", Color.red);
                          
                if (transform.position.x == playerPos.x)
                {
                    currentState = EnemyState.Patrol;
                }

                if (Vector3.Distance(transform.position, player.position) > chaseDistance)
                {
                    currentState = EnemyState.Patrol; //Go back to patrol if player gets too far
                    Debug.Log("Patrol state");
                    // Debug.Log(playerPos);
                }

                if (hasLineOfSight == true)
                {
                    playerPos = new Vector3((player.transform.position.x - 0.5f), 1, player.transform.position.z);

                    transform.position = Vector2.MoveTowards(transform.position, playerPos, chaseSpeed * Time.deltaTime);                   
                }

                else
                {
                    if (Vector3.Distance(transform.position, player.position) < 4f)
                    {
                        if ((transform.position.y + 0.05f) < player.transform.position.y)
                        {
                            Debug.Log("LookFor State");
                            currentState = EnemyState.LookFor;
                        }
                        
                    }

                    else
                    {
                        Debug.Log("Patrol State");
                        currentState = EnemyState.Patrol;
                    }                  
                }
                break;

            case EnemyState.LookFor:
                // Debug.Log("look for state");

                if (hasLineOfSight && transform.position.y == player.position.y && isAtDistance)
                {
                    Debug.Log("Chase State");
                    currentState = EnemyState.Chase;
                }

                else if (hasLineOfSight == false && Vector3.Distance(transform.position, player.position) > chaseDistance / 2)
                {
                    Debug.Log("Patrol State");
                    currentState = EnemyState.Patrol;
                }

                else
                {
                    // currentState = EnemyState.Patrol;
                }

                break;

            case EnemyState.Attack:
                // Implement Attack behavior here (e.g., deal damage to the player)
                break;
        }
    }

    private void FixedUpdate()
    {
        Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, 100f, layerMask);
        // Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.white);
        // Debug.Log(hit.collider.gameObject.name);
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            hasLineOfSight = true;
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= chaseDistance)
            {
                isAtDistance = true;
            }

            else
            {
                isAtDistance = false;
            }        
        }

        else
        {
            hasLineOfSight = false;
            // Debug.Log("No hit");
        }
    }  
}
