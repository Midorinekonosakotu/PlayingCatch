using UnityEngine;

public class BallThrower : MonoBehaviour
{
    [Header("弾道設定（角度強化版）")]
    [SerializeField] private float baseArcHeight = 2.5f;      // 最低角度保証
    [SerializeField] private float arcHeightByDistance = 0.25f; // 距離で増える高さ
    [SerializeField] private float upwardBoost = 1.1f;        // 上方向補正
    [SerializeField] private float gravityScale = 1f;

    [SerializeField] private float minFlightTime = 0.4f;
    [SerializeField] private float maxFlightTime = 1.0f;

    private float Gravity => Physics2D.gravity.y * gravityScale;

    public void Throw(BallController ball, Vector2 target, Vector2 startPosition)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        float dx = target.x - startPosition.x;
        float dy = target.y - startPosition.y;

        float distance = Mathf.Abs(dx);

        // ★ 時間を距離で決定（これが重要）
        float t = Mathf.Lerp(minFlightTime, maxFlightTime, distance / 10f);

        // ★ X速度は「必ず届く値」
        float vx = dx / t;

        // ★ Y速度は重力から逆算
        float vy = (dy - 0.5f * Gravity * t * t) / t;
        vy *= 0.8f;

        Vector2 velocity = new Vector2(vx, vy);

        ball.OnThrown(velocity, gravityScale);
    }
}