using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerRequest : MonoBehaviour
{
    protected string url = "http://localhost:3000/routes/player";

    private List<Player> players = new List<Player>();

    public GameObject playerListPrefab;
    public Transform parentTransform;
    public List<GameObject> playerViewList = new();

    public void SendGetAllPlayer(string complemento)
    {
        StartCoroutine(GetAllPlayer(complemento));
    }

    private IEnumerator GetAllPlayer(string complemento)
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
                List<Player> players = JsonConvert.DeserializeObject<List<Player>>(uwr.downloadHandler.text);
                ClearList();
                if (players != null)
                {
                    foreach (Player p in players)
                    {
                        GameObject playerObj = Instantiate(playerListPrefab, parentTransform);
                        PlayerUI playerUi = playerObj.GetComponent<PlayerUI>();
                        playerUi.SetPlayer(p);
                        playerUi.SetRequest(this);
                        playerViewList.Add(playerObj);
                    }
                }
            }
        }
    }

    public void ClearList()
    {
        foreach (GameObject p in playerViewList)
        {
            Destroy(p);
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
            Player responseData = JsonConvert.DeserializeObject<Player>(uwr.downloadHandler.text);
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
            Debug.LogError("Erro ao deletar o jogador: " + uwr.error);
        }
        else
        {
            Debug.Log("Jogador deletado com sucesso!");
            ClearList();
            SendGetAllPlayer("");
        }
    }
}