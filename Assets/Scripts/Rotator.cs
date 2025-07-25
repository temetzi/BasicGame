using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    public float rotateX = 0f;
    public float rotateY = 10f;
    public float rotateZ = 0f;
    void Update()
    {
        
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime, Space.World);
    }
}
