using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private const float OnDeathWaitSeconds = 3f;

    [SerializeField] private int playerLives = 3;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

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

    private void Start()
    {
        livesText.text = playerLives.ToString();
    }

    public void ProcessPlayerDeath() => StartCoroutine(playerLives > 1 ? SoftDeath() : HardDeath());

    private IEnumerator SoftDeath()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        yield return new WaitForSeconds(OnDeathWaitSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator HardDeath()
    {
        livesText.text = "(×_×)";
        yield return new WaitForSeconds(OnDeathWaitSeconds);
        SceneManager.LoadScene("Scene 1");
        Destroy(gameObject); // This allows the GameManager to be recreated with initial values on SceneLoad
    }
}