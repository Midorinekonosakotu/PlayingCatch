using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerID { Player1, Player2 }

    [Header("設定")]
    public PlayerID playerID;
    public KeyCode throwKey;
    public KeyCode catchKey;

    [Header("参照")]
    [SerializeField] private BallThrower thrower;
    [SerializeField] private Transform holdPoint;

    [Header("投擲補正")]
    [SerializeField] private float targetHeightOffset = 1.0f;
    [SerializeField] private float aimRandomRange = 0.3f;

    private BallController currentBall;

    void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            if (currentBall != null)
            {
                Vector2 target = GetTargetPosition();

                thrower.Throw(currentBall, target, transform.position);

                GameManager.Instance.OnBallThrown(this);
                currentBall = null;
            }
        }
    }

    public bool IsCatchInput()
    {
        return Input.GetKey(catchKey);
    }

    public void CatchBall(BallController ball)
    {
        currentBall = ball;
        ball.OnCaught(holdPoint);

        GameManager.Instance.OnBallCaught(this);
    }

    public void SetBall(BallController ball)
    {
        currentBall = ball;
    }

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    // ★ 改良版ターゲット計算
    Vector2 GetTargetPosition()
    {
        var opponent = GameManager.Instance.GetOpponent(this);

        Vector2 target = opponent.transform.position;

        // 高さ補正（手の位置）
        target.y += targetHeightOffset;

        // ランダムブレ
        target.x += Random.Range(-aimRandomRange, aimRandomRange);

        return target;
    }
}