using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard
{
    public int id { get; set; }

    public int playerId { get; set; }

    public int score { get; set; }

    public DateTime criadoEm { get; set; }
}

public class LeaderBoardPlayer
{
    public int id { get; set; }

    public int playerId { get; set; }

    public int score { get; set; }

    public DateTime criadoEm { get; set; }

    public string nome { get; set; }
}