
using System;
using UnityEngine;

public class City : MonoBehaviour
{

    public int cityId;
    public int[] connectedCityIDs;
    public string color;
    public Color cityColor;
    public string name;
    public bool researchCenter;

    public bool locked;
    public int diseaseSpread;

    public void Initialize(int cityId, int[] connectedCityIDs, string color, string name)
    {
        this.cityId = cityId;
        this.connectedCityIDs = connectedCityIDs;
        this.color = color;
        this.name = name;
        transform.name = name;
        researchCenter = false;

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
         switch (diseaseSpread)
        {
            case 1:
                GetComponent<Renderer>().material.color = new Color(1, 0.8f, 0.8f);
                //GetComponent<Renderer>().material.color *= 0.9f;
                break;
            case 2:
                GetComponent<Renderer>().material.color = new Color(1, 0.6f, 0.6f);
                break;
            case 3:
                GetComponent<Renderer>().material.color = new Color(1, 0.4f, 0.4f);
                break;
        }
    }

    internal void removePlayer(Player player)
    {
        throw new NotImplementedException();
    }

    internal void addPlayer(Player player)
    {
        throw new NotImplementedException();
    }

    /*internal City Initialize(int v1, int[] v2, string v3, string v4)
    {
        throw new NotImplementedException();
    }*/
}
