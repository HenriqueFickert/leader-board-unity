using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardPlayerUI : MonoBehaviour
{
    public LeaderBoardPlayer leaderBoard;

    public TextMeshProUGUI idText;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI criadoEmText;

    public void SetLeaderBoard(LeaderBoardPlayer leaderboard)
    {
        this.leaderBoard = leaderboard;
        SetUI();
    }

    private void SetUI()
    {
        idText.text = leaderBoard.id.ToString();
        playerName.text = leaderBoard.nome.ToString();
        scoreText.text = leaderBoard.score.ToString();
        criadoEmText.text = leaderBoard.criadoEm.ToString();
    }
}
