using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardUI : MonoBehaviour
{
    public LeaderBoardRequest request;
    public LeaderBoard leaderBoard;

    public TextMeshProUGUI idText;
    public TextMeshProUGUI playerIdText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI criadoEmText;
    public Button deleteButton;

    public void SetLeaderBoard(LeaderBoard leaderboard)
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

    public void SetRequest(LeaderBoardRequest request)
    {
        this.request = request;
        deleteButton.onClick.AddListener(DeleteScore);
    }

    private void DeleteScore()
    {
        request.SentDeleteRequest(leaderBoard.id);
    }
}