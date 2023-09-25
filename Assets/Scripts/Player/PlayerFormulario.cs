using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerFormulario : MonoBehaviour
{
    public TMP_InputField input;
    public PlayerRequest playerRequest;

    public void CadastrarPlayer()
    {
        if (input.text == null || input.text == "") return;

        Player player = new Player() { nome = input.text };
        string json = JsonConvert.SerializeObject(player);

        Debug.Log("Json: " + json);
        playerRequest.SendPostRequest(json);
    }
}