using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    Hand hand;
    int cityID;
    _roleCard role;
    GameManager gameManager;
    int actionsLeft;
    int[][] actionsTaken;
    public delegate void cityNum(int ID);
    public delegate void cardSel(int ID, _cityCard cityID);
    public delegate void cureCheck(int ID, _cityCard hand);

    Player(GameManager gameManager)
    {
        
        actionsTaken = new int[4][];
        actionsLeft = 4;
		
        hand = new Hand();
        cityID = 1;
        this.gameManager = gameManager;
        MoveToCity(cityID);

        MoveToConnectedCity(cityID);

    }

    public void MoveToConnectedCity(int newID)
    {
        MoveToCity(gameManager.connectedCities[cityID][newID]);
    }
    public void MoveToResearchCity(int ID)
    {
        if (gameManager.GetCityFromID(ID).researchCenter)
        {
            int choose = 0;
            MoveToCity(gameManager.researchCenterCities[choose].cityId);
        }
    }

    private void MoveToCity(object cityId)
    {
        throw new NotImplementedException();
    }

    private void MoveToCityCard(_cityCard cityCard)
    {
        int ID = cityCard.cityID;
        MoveToCity(ID);
        //hand.discard(cityCard);
    }

    private void MoveToCity(int ID)
    {
        gameManager.GetCityFromID(ID).removePlayer(this);
        gameManager.GetCityFromID(ID).addPlayer(this);
        cityID = ID;
    }

    private void RemoveDiseaseCubes(int cityID)
    {
        if (gameManager.GetCityFromID(cityID).diseaseSpread > 0)
        {
            gameManager.GetCityFromID(cityID).diseaseSpread--;
        }

        //gameManager.GetCityFromID(cityID).RemoveDiseaseCubes(1);

    }
    private void buildResearchCenter(int cityID, _cityCard city)
    {
        gameManager.GetCityFromID(cityID).hasResearchCenter = true;
        if (cityID == city.cityID)
        {
            gameManager.GetCityFromID(cityID).hasResearchCenter = true;
            hand.discard(city);
        }

    }
    private void cureDisease()
    {

        if (gameManager.GetCityFromID(cityID).researchCenter)
        {
            //hand.
        }
    }
    private void checkForCure(int counter, _cityCard[] hand)
    {
        
        int[] counters = new int[4];
        for (int i = 0; i < hand.GetLength(0); i++)
        {
            if (hand[i] != null)
            {
                String colour = gameManager.GetCityFromID(hand[i].cityID).color;
                switch (gameManager.GetCityFromID(hand[i].cityID).color)
                {
                    case "Blue":
                        counters[0]++;
                        break;
                    case "Yellow":
                        counters[1]++;
                        break;
                    case "Black":
                        counters[2]++;
                        break;
                    case "Red":
                        counters[3]++;
                        break;
                }
            }
        }

    }
    public void takeAction(cityNum method)
    {

    }

}

