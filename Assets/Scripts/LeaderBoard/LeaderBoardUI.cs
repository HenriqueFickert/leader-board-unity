using TMPro;
using UnityEngine;

public class LeaderBoardUI : MonoBehaviour
{
    public LeaderBoard leaderBoard;

    public TextMeshProUGUI idText;
    public TextMeshProUGUI playerIdText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI criadoEmText;

    public void SetPlayer(LeaderBoard leaderboard)
    {
        this.leaderBoard = leaderboard;
        SetUI();
    }

    private void SetUI()
    {
        idText.text = leaderBoard.id.ToString();
        playerIdText.text = leaderBoard.playerId.ToString();
        scoreText.text = leaderBoard.score.ToString();
        criadoEmText.text = leaderBoard.criadoEm.ToString();
    }
}