using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class LeaderBoardFormulario : MonoBehaviour
{
    public TMP_InputField playerId;
    public TMP_InputField score;
    public LeaderBoardRequest leaderBoardRequest;

    public void CadastrarLeaderBoard()
    {
        if (score.text == null || score.text == "" || playerId.text == null || playerId.text == "") return;

        LeaderBoard leaderBoard = new LeaderBoard()
        {
            playerId = int.Parse(playerId.text),
            score = int.Parse(score.text)
        };

        string json = JsonConvert.SerializeObject(leaderBoard);

        Debug.Log("Json: " + json);
        leaderBoardRequest.SendPostRequest(json);
    }
}