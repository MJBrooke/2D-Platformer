using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        Destroy(gameObject);
    }
}
