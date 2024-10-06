using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // If you use an AudioSource component attached to the coin here, it would be destroyed before the sound is played.
        // This simple (but somewhat dirty) solution is to play a sound at this location in the Scene, not tied to the coin.
        AudioSource.PlayClipAtPoint(pickupSound, transform.position, 0.5f);

        GameManager.Instance.AddScore();

        Destroy(gameObject);
    }
}