using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardRequest : MonoBehaviour
{
    protected string url = "http://ec2-15-228-77-162.sa-east-1.compute.amazonaws.com:3000/routes/leaderboard";

    private List<LeaderBoard> leaderBoards = new List<LeaderBoard>();
    public GameObject leaderBoardListPrefab;
    public Transform parentTransform;
    public List<GameObject> leaderBoardViewList = new();

    private List<LeaderBoardPlayer> leaderBoardPlayers = new List<LeaderBoardPlayer>();
    public GameObject leaderBoardPlayerListPrefab;
    public Transform parentLeaderBoardPlayerTransform;
    public List<GameObject> leaderBoardPlayerViewList = new();

    public void SendGetAllLeaderBoardsPlayer(string complemento)
    {
        StartCoroutine(GetAllLeaderBoardsPlayer(complemento));
    }

    private IEnumerator GetAllLeaderBoardsPlayer(string complemento)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(url + complemento))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro na requisição: " + uwr.error);
                Debug.Log("Detalhes do erro: " + uwr.downloadHandler.text);
            }
            else
            {
                Debug.Log("Resposta da API: " + uwr.downloadHandler.text);
                List<LeaderBoardPlayer> leaderBoards = JsonConvert.DeserializeObject<List<LeaderBoardPlayer>>(uwr.downloadHandler.text);
                ClearListLeaderBoardsPlayer();
                if (leaderBoards != null)
                {
                    foreach (LeaderBoardPlayer l in leaderBoards)
                    {
                        GameObject leaderBoardObj = Instantiate(leaderBoardPlayerListPrefab, parentLeaderBoardPlayerTransform);
                        LeaderBoardPlayerUI leaderBoardUI = leaderBoardObj.GetComponent<LeaderBoardPlayerUI>();
                        leaderBoardUI.SetLeaderBoard(l);
                        leaderBoardPlayerViewList.Add(leaderBoardObj);
                    }
                }
            }
        }
    }

    public void ClearListLeaderBoardsPlayer()
    {
        foreach (GameObject l in leaderBoardPlayerViewList)
        {
            Destroy(l);
        }
    }

    public void SendGetAllLeaderBoards(string complemento)
    {
        StartCoroutine(GetAllLeaderBoards(complemento));
    }

    private IEnumerator GetAllLeaderBoards(string complemento)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(url + complemento))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro na requisição: " + uwr.error);
                Debug.Log("Detalhes do erro: " + uwr.downloadHandler.text);
            }
            else
            {
                Debug.Log("Resposta da API: " + uwr.downloadHandler.text);
                List<LeaderBoard> leaderBoards = JsonConvert.DeserializeObject<List<LeaderBoard>>(uwr.downloadHandler.text);
                ClearList();
                if (leaderBoards != null)
                {
                    foreach (LeaderBoard l in leaderBoards)
                    {
                        GameObject leaderBoardObj = Instantiate(leaderBoardListPrefab, parentTransform);
                        LeaderBoardUI leaderBoardUI = leaderBoardObj.GetComponent<LeaderBoardUI>();
                        leaderBoardUI.SetLeaderBoard(l);
                        leaderBoardUI.SetRequest(this);
                        leaderBoardViewList.Add(leaderBoardObj);
                    }
                }
            }
        }
    }

    public void ClearList()
    {
        foreach (GameObject l in leaderBoardViewList)
        {
            Destroy(l);
        }
    }

    public void SendPostRequest(string jsonForm)
    {
        StartCoroutine(PostRequest(jsonForm));
    }

    private IEnumerator PostRequest(string json)
    {
        UnityWebRequest uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Erro na requisição: " + uwr.error);
            Debug.Log("Detalhes do erro: " + uwr.downloadHandler.text);
        }
        else
        {
            Debug.Log("Resposta da API: " + uwr.downloadHandler.text);
            LeaderBoard responseData = JsonConvert.DeserializeObject<LeaderBoard>(uwr.downloadHandler.text);
        }
    }

    public void SentDeleteRequest(int id)
    {
        StartCoroutine(DeleteRequest(id));
    }

    private IEnumerator DeleteRequest(int id)
    {
        UnityWebRequest uwr = UnityWebRequest.Delete(url + "?id=" + id);

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao deletar o leaderboard: " + uwr.error);
        }
        else
        {
            Debug.Log("Leaderboard deletado com sucesso!");
            ClearList();
            SendGetAllLeaderBoards("");
        }
    }
}