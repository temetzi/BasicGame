using System;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public Vector3 mousePos;
    public Camera mainCamera;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            Debug.Log(Input.mousePosition);
            
            Vector3 mousePosScreen = Input.mousePosition;

            mousePosScreen.z = 10f;

            // Vector3 mousePosWorld = mainCamera.ScreenToWorldPoint(mousePosScreen);
        }
        
        
        
        // Ensure the camera is assigned
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not assigned!");
            return;
        }

    }

    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 5f, layerMask))

        { 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow); 
            // Debug.Log("Did Hit"); 
        }
        else
        { 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 5, Color.white); 
            // Debug.Log("Did not Hit"); 
        }

    }
}
