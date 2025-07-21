using Unity.VisualScripting;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float elevatorSpeed;
    private bool activateElevator = false;
    private bool orderElevator = false;
    public bool playerOnElevator = false;
    public float playerY;

    public bool elevatorAtTop = true;
    public bool playerAtTop;
    public bool playerAtBottom;
    

    public GameObject bottomPoint;
    public GameObject topPoint;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        activateElevator = false;
        playerOnElevator = false;
        elevatorAtTop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && playerOnElevator == true)
        {
            activateElevator = true;
        }

        if (Input.GetKeyDown(KeyCode.X) && playerOnElevator == false)
        {
            orderElevator = true;

        }

        playerY = player.transform.position.y;

        if (playerY > (topPoint.transform.position.y / 2))
        {
            playerAtTop = true;
            // Debug.Log(topPoint.transform.position.y / 2);
            //Debug.Log("player at top");
        }

        // else if (playerY < (topPoint.transform.position.y / 2))
        else
        {
            playerAtTop = false;
            //Debug.Log("player at bottom");
            // Debug.Log(topPoint.transform.position.y / 2);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            playerOnElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            playerOnElevator = false;
            orderElevator = false;
            activateElevator = false;
        }
    }

    private void FixedUpdate()
    {
        if (activateElevator == true && playerOnElevator == true)
        {
            //moving up
            if (transform.position.y <= topPoint.transform.position.y && elevatorAtTop == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, topPoint.transform.position, elevatorSpeed * Time.deltaTime);
                // activateElevator = false;
                // playerOnElevator = false;
                // Debug.Log("yl�s");

                if (transform.position.y == topPoint.transform.position.y)
                {
                    // transform.position = Vector3.MoveTowards(transform.position, topPoint.transform.position, elevatorSpeed * Time.deltaTime);
                    activateElevator = false;
                    elevatorAtTop = true;
                    Debug.Log("elevator at top");

                }
                // elevatorAtTop = true;
            }

            // moving down
            if (transform.position.y >= bottomPoint.transform.position.y && elevatorAtTop == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, bottomPoint.transform.position, elevatorSpeed * Time.deltaTime);
                // Debug.Log("alas");

                if (transform.position.y == bottomPoint.transform.position.y)
                {
                    activateElevator = false;
                    elevatorAtTop = false;
                    // Debug.Log("Hissi alhaalla");
                }
                // elevatorAtTop = false;
            }
        }

        if (orderElevator == true && playerOnElevator == false && transform.position.y <= topPoint.transform.position.y)
        {        
                
                if (playerOnElevator == false && orderElevator == true && playerAtTop == true)
                {
                    
                    
                    //Debug.Log("Hissi tilattu yl�s");
                    if (transform.position.y >= topPoint.transform.position.y)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, topPoint.transform.position, elevatorSpeed * Time.deltaTime);
                        orderElevator = false;
                        elevatorAtTop = true;
                        Debug.Log(transform.position.y);

                    }
                }
                
                else if (playerOnElevator == false && orderElevator == true && playerAtTop == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position, bottomPoint.transform.position, elevatorSpeed * Time.deltaTime);
                    //Debug.Log("Hissi tilattu alas");
                    if (transform.position.y == bottomPoint.transform.position.y)
                    {
                        orderElevator = false;
                        elevatorAtTop = false;
                        // Debug.Log("Hissi alhaalla");
                    }


                }
        }
    }

    
    

}
