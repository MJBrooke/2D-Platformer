using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private Rigidbody2D myRigidbody2D;

    private void Update()
    {
        myRigidbody2D.velocity = new Vector2(movementSpeed, myRigidbody2D.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ChangeMovementDirection();
        FlipSprite();
    }

    private void ChangeMovementDirection() => movementSpeed *= -1f;

    private void FlipSprite() => transform.localScale = new Vector2(Mathf.Sign(movementSpeed), 1f);
}