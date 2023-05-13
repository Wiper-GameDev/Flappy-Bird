using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveObstacle : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameOver)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (!GameManager.Instance.IsGameStarted && gameObject.CompareTag("Pipe"))
        {
            return;
        }

        rb.velocity = new Vector2(-GameManager.Instance.Speed, 0f);
    }
}
