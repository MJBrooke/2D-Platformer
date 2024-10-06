using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*
 * Things to add for learning:
 *   - Smooth Scene transitions
 *   - Music and audio
 *   - Shooting animation with bow
 *   - Event-driven actions
 *   - Start and pause menus
 *     - Persistent settings
 *   - Save/Load game
 *   - Quality movement
 *   - Fuller TileMap for platforming
 *   - Use Finite State Machine for Player status
 *   - Extra life on 1k points
 *   - Enemies that can shoot at you
 *   - Enemies that can navigate to you via pathfinding
 *
 * Fixes:
 *   - Fix climbing animation when using controller
 */

public class GameManager : PersistentSingleton<GameManager>
{
    private const float OnDeathWaitSeconds = 3f;

    [SerializeField] private int playerLives = 3;

    // TODO - move these to their own components listening to events
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int _score;

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = _score.ToString();
    }

    public void LoadNextScene() => StartCoroutine(LoadNextSceneAsync());

    private static IEnumerator LoadNextSceneAsync()
    {
        yield return new WaitForSeconds(1f);
        ScenePersist.Instance.ResetScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ProcessPlayerDeath() => StartCoroutine(playerLives > 1 ? SoftDeath() : HardDeath());

    public void AddScore(int points = 100)
    {
        _score += points;
        scoreText.text = _score.ToString();
    }

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
        ScenePersist.Instance.ResetScene();
        SceneManager.LoadScene("Scene 1");
        Destroy(gameObject); // This allows the GameManager to be recreated with initial values on SceneLoad
    }
}