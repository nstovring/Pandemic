
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class City : NetworkBehaviour
{

    public Player[] players = new Player[4];
    public int cityId;
    public int[] connectedCityIDs;
    public string color;
    public Color cityColor;
    public string name;
    public bool hasResearchCenter = false;
    public bool active = false;
    public SpriteRenderer[] diseaseCubes;
    public SpriteRenderer[] pawnSpriteRenderers;
    public SpriteRenderer researchCenter;

    public bool locked;

    private int diseaseSpread;

    public int DiseaseSpread
    {
        get { return diseaseSpread; }
        set { diseaseSpread = value; }
    }


    public void ResetValues()
    {
        if (DiseaseSpread > 3 && locked)
        {
            locked = false;
            DiseaseSpread = 3;
        }
    }

    
    public void UpdatePawns()
    {
        for (int i = 0; i < pawnSpriteRenderers.Length; i++)
        {
            for (int j = 0; j < players.Length; j++)
            {
                if (players[j])
                {
                    if (players[j].role.transform.name == pawnSpriteRenderers[j].name)
                    {
                        pawnSpriteRenderers[j].enabled = true;
                    }
                    else
                    {
                        pawnSpriteRenderers[j].enabled = false;
                    }
                }
                else
                {
                    pawnSpriteRenderers[i].enabled = false;

                }
            }
        }
    }


    public void IncrementDiseaseSpread(string color, int infectRate)
    {
       
       for (int i = 0; i < infectRate; i++)
        {
            foreach (SpriteRenderer t in diseaseCubes)
            {
                if (!t.enabled)
                {
                    t.enabled = true;
                    t.color = GetColorFromString(color);
                    break;
                }
            }
            diseaseSpread++;
        }
    }

    public void ReduceDiseaseSpread(string color, _roleCard role)
    {
        //Add a check which Player role is removing diseases
        //if(role.name == "MEDIC")
        // add [command]
        if (GameManager.GetCureFromString(color))
        {
            foreach (SpriteRenderer t in diseaseCubes.Where(t => t.color == GetColorFromString(color)))
            {
                t.enabled = false;
                diseaseSpread--;
            }
        }
        else
        {
            foreach (SpriteRenderer t in diseaseCubes.Where(t => t.color == GetColorFromString(color)))
            {
                t.enabled = false;
                diseaseSpread--;
                return;
            }
        }
    }

    public void Initialize(int cityId, int[] connectedCityIDs, string color, string name)
    {
        this.cityId = cityId;
        this.connectedCityIDs = connectedCityIDs;
        this.color = color;
        this.name = name;
        transform.name = name;
        hasResearchCenter = name == "Atlanta";
        UpdatePawns();
        GetComponent<Renderer>().material.color = GetColorFromString(color);
    }

    private Color GetColorFromString(string color)
    {
        switch(color)
        {
            case "Blue":
                return Color.blue;
            case "Yellow":
                return Color.yellow;
            case "Black":
                return Color.black;
            case "Red":
                return Color.red;
        }
        return Color.white;
    }

    

    private void Update()
    {
        researchCenter.enabled = hasResearchCenter;
        if (hasResearchCenter)
        {
            //GameManager.researchCenterCities[cityId] = this;
        }
    }


    internal void removePlayer(Player player)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == player)
            {
                players[i] = null;
                return;
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
                return;
            }
        }
    }
}
