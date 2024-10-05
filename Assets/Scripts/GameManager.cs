using UnityEngine;
using UnityEngine.SceneManagement;

// TODO - add some GameStates to this
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    // TODO - show this life-count on the UI
    [SerializeField] private int playerLives = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1) SoftDeath();
        else HardDeath();
    }

    private void SoftDeath()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void HardDeath()
    {
        SceneManager.LoadScene("Scene 1");
        Destroy(gameObject); // This allows the GameManager to be recreated with initial values on SceneLoad
    }
}
