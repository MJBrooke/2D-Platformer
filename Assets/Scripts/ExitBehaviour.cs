using UnityEngine;

public class ExitBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem exitParticles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        // TODO - add some sound
        exitParticles.Play();
        GameManager.Instance.LoadNextScene();
    }
}