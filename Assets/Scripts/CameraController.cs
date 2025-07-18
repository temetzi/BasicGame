using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public GameObject player;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        if (player != null) {
            offset = transform.position - player.transform.position;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null) {
            transform.position = player.transform.position + offset;
        }
    }
}
