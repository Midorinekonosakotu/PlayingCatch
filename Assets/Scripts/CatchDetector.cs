using UnityEngine;

public class CatchDetector : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        BallController ball = collision.GetComponent<BallController>();

        if (ball != null && ball.State == BallController.BallState.Flying)
        {
            if (player.IsCatchInput())
            {
                player.CatchBall(ball);
            }
        }
    }
}