using UnityEngine;

public class BallController : MonoBehaviour
{
    public enum BallState
    {
        Idle,
        Flying,
        Caught
    }

    public BallState State { get; private set; }

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnThrown(Vector2 velocity, float gravityScale)
    {
        State = BallState.Flying;

        rb.simulated = true;
        rb.gravityScale = gravityScale;
        rb.linearVelocity = velocity;

        transform.SetParent(null);
    }

    public void OnCaught(Transform holdPoint)
    {
        State = BallState.Caught;

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        rb.simulated = false;

        transform.position = holdPoint.position;
        transform.SetParent(holdPoint);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.OnBallMiss();
        }
    }
}