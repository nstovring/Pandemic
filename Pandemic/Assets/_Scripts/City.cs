
using System;
using UnityEngine;

public class City : MonoBehaviour
{

    public int cityId;
    public int[] connectedCityIDs;
    public string color;
    public string name;
    public bool researchCenter;

    public bool locked;
    public int diseaseSpread;

    public City(int cityId, int[] connectedCityIDs, string color, string name)
    {
        this.cityId = cityId;
        this.connectedCityIDs = connectedCityIDs;
        this.color = color;
        this.name = name;
        researchCenter = false;
    }

    internal void removePlayer(Player player)
    {
        throw new NotImplementedException();
    }

    internal void addPlayer(Player player)
    {
        throw new NotImplementedException();
    }
}
