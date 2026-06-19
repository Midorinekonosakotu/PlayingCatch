using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;
    [SerializeField] private BallController ball;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;
    private PlayerController lastThrowPlayer;
    private bool isBallFlying = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializeBall();
        UpdateScore();
    }

    void InitializeBall()
    {
        ball.OnCaught(player1.GetHoldPoint());
        player1.SetBall(ball);
        isBallFlying = false;
    }

    public void OnBallThrown(PlayerController throwPlayer)
    {
        lastThrowPlayer = throwPlayer;
        isBallFlying = true;
    }

    public void OnBallCaught(PlayerController catchPlayer)
    {
        if (isBallFlying && catchPlayer != lastThrowPlayer)
        {
            score++;
            UpdateScore();
        }

        isBallFlying = false;
    }

    // ★ ここが今回の修正ポイント
    public void OnBallMiss()
    {
        isBallFlying = false;

        // ★ スコアリセット
        score = 0;
        UpdateScore();

        PlayerController failedPlayer = GetOpponent(lastThrowPlayer);

        ball.OnCaught(failedPlayer.GetHoldPoint());
        failedPlayer.SetBall(ball);
    }

    public PlayerController GetOpponent(PlayerController player)
    {
        return player == player1 ? player2 : player1;
    }

    void UpdateScore()
    {
        scoreText.text = "Score : " + score;
    }
}