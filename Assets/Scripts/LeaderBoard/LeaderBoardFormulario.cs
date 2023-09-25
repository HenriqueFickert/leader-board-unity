using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class LeaderBoardFormulario : MonoBehaviour
{
    public TMP_Text playerId;
    public TMP_InputField score;
    public PlayerRequest playerRequest;

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
        playerRequest.SendPostRequest(json);
    }
}