using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class RayCast : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask nonDestroyableMask;
    public Vector3 mousePos;
    public Camera mainCamera;
    public Transform player;

    public LineRenderer laserLine;
    public Transform laserOrigin;
    public float laserDuration = 0.05f;
    private Vector3 actualMousePosition;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {       

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            AudioManager.Instance.playSFX("Shoot");
            // Shoot();
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition -= transform.position;
            mouseWorldPosition.y -= 3;
            mouseWorldPosition.z = 0f;

            if (mouseWorldPosition.y > 2f)
                mouseWorldPosition.y += 0.2f;
            {
                if (mouseWorldPosition.y > 3f)
                {
                    mouseWorldPosition.y += 0.30f;
                    if (mouseWorldPosition.y > 4f)
                    {
                        mouseWorldPosition.y += 0.25f;


                    }
                }
            }

            laserLine.SetPosition(0, laserOrigin.position);

            laserLine.startWidth = 0.1f;
            laserLine.endWidth = 0.1f;

            // Debug.DrawRay(transform.position, mouseWorldPosition, Color.white);
            if (Physics.Raycast(transform.position, mouseWorldPosition, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
            {
                int groundLayer = LayerMask.NameToLayer("Default");
                int destroyableLayer = LayerMask.NameToLayer("Destroyable");
                int boxLayer = LayerMask.NameToLayer("Box");

                


                if (hitInfo.transform.gameObject.layer == groundLayer)
                {
                    //Debug.Log(hitInfo.collider.gameObject.name + " was hit");
                    // hitInfo.collider.gameObject.SetActive(false);

                    laserLine.SetPosition(1, hitInfo.point);
                    
                }

                else if (hitInfo.transform.gameObject.layer == boxLayer)
                {
                    //Debug.Log(hitInfo.collider.gameObject.name + " was hit");
                    // hitInfo.collider.gameObject.SetActive(false);
                    hitInfo.transform.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 5f, 0f), ForceMode.VelocityChange);
                    laserLine.SetPosition(1, hitInfo.point);

                }

                else if (hitInfo.transform.gameObject.layer == destroyableLayer)
                {
                    //Debug.Log(hitInfo.collider.gameObject.name + " was hit");
                    hitInfo.collider.gameObject.SetActive(false);

                    laserLine.SetPosition(1, hitInfo.point);
                }

                else
                {
                    laserLine.SetPosition(1, hitInfo.point);
                    // Debug.Log("Hit nothing");
                }


            }

            else
            {
                // Debug.Log("Missed");
            }
            StartCoroutine(ShootLaser());
        }

        
        // Ensure the camera is assigned
        if (mainCamera == null)
        {
            // Debug.LogError("Main camera not assigned!");
            return;
        }      

    }
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    private void FixedUpdate()
    {
        
    }




    void Shoot()
    {
        
        //Physics.Raycast(transform.position, mouseWorldPosition, out hitInfo);

        // Debug.Log(hitInfo.point);
        // Debug.Log(mouseWorldPosition);
        //actualMousePosition = mouseWorldPosition;
        actualMousePosition += transform.position;


        if (actualMousePosition.y > 3f)
        {
            actualMousePosition.y += 0.25f;
            if (actualMousePosition.y > 4f)
            {
                actualMousePosition.y += 0.25f;


            }
        }

        laserLine.SetPosition(1, actualMousePosition);
        // Debug.Log(actualMousePosition);
    }
}
