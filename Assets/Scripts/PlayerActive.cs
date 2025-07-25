using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerActive : MonoBehaviour
{
    public GameObject player;
    public CameraController cameraController;

    public GameObject GameOverScreen;
    public GameObject WinScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        if (player != null) {
            if (player.activeInHierarchy)
            {
                // Debug.Log("Player is active in the scene.");
            }
            else
            {
                Death();
            }
            
        }
            
    }

    public void Win()
    {
        // Debug.Log("win");
        // player.SetActive(false);
        AudioManager.Instance.playSFX("Win");
        Time.timeScale = 0;
        WinScreen.SetActive(true);
    }

    public void Death()
    {
        // Debug.Log("dead");
        cameraController.UnParentCamera();
        // player.SetActive(false);
        Destroy(player);
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
