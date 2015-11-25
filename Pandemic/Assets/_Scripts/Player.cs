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
    public void initialize()
    {
        
        actionsTaken = new int[4][];
        actionsLeft = 4;
		
        hand = new Hand();
        cityID = 1;
        gameManager = GameManager.instance;
        MoveToCity(cityID);

        MoveToConnectedCity(cityID);

    }

    public void MoveToConnectedCity(int newID)
    {
        MoveToCity(gameManager.connectedCities[cityID][newID]);
    }
    public void MoveToResearchCity(int ID)
    {
        if (GameManager.GetCityFromID(ID).researchCenter)
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
        hand.discard(cityCard);
    }

    private void MoveToCity(int ID)
    {
        GameManager.GetCityFromID(ID).removePlayer(this);
        GameManager.GetCityFromID(ID).addPlayer(this);
        cityID = ID;
    }

    private void RemoveDiseaseCubes(String colour)
    {
        GameManager.GetCityFromID(cityID).ReduceDiseaseSpread(colour, role);

    }
    private void buildResearchCenter(int cityID, _cityCard city)
    {
        //gameManager.GetCityFromID(cityID).hasResearchCenter = true;
        if (cityID == city.cityID)
        {
            GameManager.GetCityFromID(cityID).hasResearchCenter = true;
            hand.discard(city);
        }

    }
    private void cureDisease()
    {
        int[] checker = checkForCure(5, hand.hand);
        if (GameManager.GetCityFromID(cityID).researchCenter && checker[0] == 1)
        {
            switch (checker[1])
            {
                case 0:
                    GameManager.blueCure = true;
                    break;
                case 1:
                    GameManager.yellowCure = true;
                    break;
                case 2:
                    GameManager.blackCure = true;
                    break;

                case 3:
                    GameManager.redCure = true;
                    break;
            }
            //hand.
        }
    }
    private int[] checkForCure(int counter, _cityCard[] hand)
    {
        
        int[] counters = new int[4];
        for (int i = 0; i < hand.GetLength(0); i++)
        {
            if (hand[i] != null && hand[i] is _cityCard)
            {
                String colour = GameManager.GetCityFromID(hand[i].cityID).color;
                switch (GameManager.GetCityFromID(hand[i].cityID).color)
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
        for (int i = 0; i < counters.GetLength(0); i++)
        {
            if(counters[i] >= counter)
            {
                return new int[] { 1, i };
            }
        }
        return new int[] { 0, 0 };
    }

}

