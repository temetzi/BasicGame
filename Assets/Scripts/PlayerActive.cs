using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerActive : MonoBehaviour
{
    public GameObject player;

    public GameObject GameOverScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            if (player.activeInHierarchy)
            {
                // Debug.Log("Player is active in the scene.");
            }
            else
            {
                GameOverScreen.SetActive(true);
            }
            
        }
            
    }
}
