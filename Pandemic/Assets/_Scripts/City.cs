
using System;
using System.Linq;
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

    //make rpc 
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
            /*if (diseaseSpread < diseaseCubes.Length)
            {
                diseaseCubes[diseaseSpread].enabled = true;
                diseaseCubes[diseaseSpread].color = GetColorFromString(color);
            }*/
            diseaseSpread++;
        }
    }

    public void ReduceDiseaseSpread(string color, _roleCard role)
    {
        //Add a check which Player role is removing diseases

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
        for (int i = 0; i < diseaseCubes.Length; i++)
        {
            //diseaseCubes[i].enabled = i + 1 <= diseaseSpread();
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
