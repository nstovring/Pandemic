
using System;
using UnityEngine;

public class City : MonoBehaviour
{

    private Player[] players = new Player[4];
    public int cityId;
    public int[] connectedCityIDs;
    public string color;
    public Color cityColor;
    public string name;
    public bool hasResearchCenter = false;
    public SpriteRenderer[] diseaseCubes;
    public SpriteRenderer researchCenter;

    public bool locked;
    public int diseaseSpread;

    public void Initialize(int cityId, int[] connectedCityIDs, string color, string name)
    {
        this.cityId = cityId;
        this.connectedCityIDs = connectedCityIDs;
        this.color = color;
        this.name = name;
        transform.name = name;
        hasResearchCenter = name == "Atlanta";
        switch (color)
        {
            case "Blue":
                cityColor = Color.blue;
                break;
            case "Yellow":
                cityColor = Color.yellow;
                break;
            case "Black":
                cityColor = Color.black;
                break;
            case "Red":
                cityColor = Color.red;
                break;
        }
        GetComponent<Renderer>().material.color = cityColor;
    }

    private void Update()
    {
        for (int i = 0; i < diseaseCubes.Length; i++)
        {
            diseaseCubes[i].enabled = i + 1 <= diseaseSpread;
        }

        researchCenter.enabled = hasResearchCenter;
    }


    internal void removePlayer(Player player)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == player)
            {
                players[i] = null;
            }
        }
    }

    internal void addPlayer(Player player)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
            {
                players[i] = player;
            }
        }
    }

    /*internal City Initialize(int v1, int[] v2, string v3, string v4)
    {
        throw new NotImplementedException();
    }*/
}
