using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerRequest request;
    public Player player;

    public TextMeshProUGUI idText;
    public TextMeshProUGUI nomeText;
    public TextMeshProUGUI criadoEmText;
    public Button deleteButton;

    public void SetPlayer(Player player)
    {
        this.player = player;
        SetUI();
    }

    public void SetRequest(PlayerRequest request)
    {
        this.request = request;
        deleteButton.onClick.AddListener(DeletePlayer);
    }

    private void DeletePlayer()
    {
        Debug.Log(player.id);
        request.SentDeleteRequest(player.id);
    }

    private void SetUI()
    {
        idText.text = player.id.ToString();
        nomeText.text = player.nome;
        criadoEmText.text = player.criadoEm.ToString();
    }
}