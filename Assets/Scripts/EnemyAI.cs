using TMPro;
using UnityEngine;


public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack
}


public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;
    public Transform player; // Drag the player object into this field in the inspector

    // enemy stuff
    public LayerMask layerMask;
    private bool hasLineOfSight = false;
    private bool isAtDistance = false;
    public float detectionRange = 0f;
    // public float chaseRange = 0f;

    public float chaseDistance = 10f;
    public float enemySpeed = 1f;
    public float chaseSpeed = 4f;

    public float leftBoundary;
    public float rightBoundary;
    private int direction = 1;
    private float startingPosition;
    private Renderer enemyColor;

    private void Start()
    {
        startingPosition = transform.position.x;
        enemyColor = gameObject.GetComponent<Renderer>();
    }


    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                enemyColor.material.SetColor("_Color", Color.yellow);
                // Implement Idle behavior here (e.g., do nothing)
                if (Vector3.Distance(transform.position, player.position) < detectionRange && player != null)
                {
                    Debug.Log("Patrol State");
                    currentState = EnemyState.Patrol;
                    
                }

                break;

            case EnemyState.Patrol:
                // Implement Patrol behavior here (e.g., walk a predefined path)
                enemyColor.material.SetColor("_Color", Color.yellow);
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
                enemyColor.material.SetColor("_Color", Color.red);

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,chaseSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, player.position) > chaseDistance * 1.25)
                {
                    currentState = EnemyState.Patrol; //Go back to patrol if player gets too far
                    Debug.Log("Patrol state");
                }

                if (hasLineOfSight == false)
                {
                    currentState = EnemyState.Patrol; //Go back to patrol if player gets too far
                    Debug.Log("Lost sight, return to patrol");
                }

                break;

            case EnemyState.Attack:
                // Implement Attack behavior here (e.g., deal damage to the player)
                break;
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
