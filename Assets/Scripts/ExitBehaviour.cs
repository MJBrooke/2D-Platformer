using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem exitParticles;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        // TODO - add some sound
        exitParticles.Play();
        StartCoroutine(LoadNextScene());
    }

    private static IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}