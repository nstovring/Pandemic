using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class Player : NetworkBehaviour
{
    Hand hand;
    public City CurrentCity;
    [SyncVar] public int cityID;
    public _roleCard role;
    GameManager gameManager;
    int actionsLeft;
    int[][] actionsTaken;
    int count;

    //[ClientRpc]
    public void Initialize(int role)
    {
        count = 0;
        actionsTaken = new int[4][];
        actionsLeft = 4;

        hand = new GameObject("Hand").AddComponent<Hand>();
        hand.transform.parent = this.transform;
        cityID = 4;
        this.role = GameManager.roleCardStack.roleCards[role]; //GameManager.roleCardStack.roleCards.Contains(role);

        gameManager = GameManager.instance;
        MoveToCity(cityID);
        GameManager.GetCityFromID(cityID).UpdatePawns();
    }

    public void exchangeCards() {

            
    }

    public void deactivateCities()
    {
        for (int i = 0; i < GameManager.cities.GetLength(0); i++)
        {
            GameManager.GetCityFromID(i).active = false;
        }
    }

    public void activateCities()
    {
        if (cardEqualToCity())
        {
            for(int i = 0; i < GameManager.cities.GetLength(0); i++)
            {
                GameManager.GetCityFromID(i).active = true;
            }
        }
        else
        {
            int[] cityIDs = GameManager.GetCityFromID(cityID).connectedCityIDs;
            
            for (int i = 0; i < cityIDs.GetLength(0); i++)
            {
                GameManager.GetCityFromID(cityIDs[i]).active = true;
            }
            for (int i = 0; i < hand.hand.GetLength(0); i++)
            {
                if(hand.hand[i] is _cityCard)
                {
                    GameManager.GetCityFromID(hand.hand[i].cityID).active = true;
                }
            }
            for (int i = 0; i < GameManager.researchCenterCities.GetLength(0); i++)
            {
                GameManager.researchCenterCities[i].active = true;
            }
        }
    }
    public bool cardEqualToCity()
    {
        for(int i = 0; i < hand.hand.GetLength(0); i++)
        {
            if (hand.hand[i] is _cityCard && hand.hand[i].cityID == cityID)
            {
                return true;
            }
        }
        return false;
    }

    public void MoveToConnectedCity(int newID)
    {
        actionsTaken[count] = new int[] {0,cityID};
        count++;
        actionsLeft--;
        MoveToCity(gameManager.connectedCities[cityID][newID]);
    }
    public void MoveToResearchCity(int ID)
    {
        if (GameManager.GetCityFromID(cityID).researchCenter)
        {
            actionsTaken[count] = new int[] { 1, cityID };
            count++;
            actionsLeft--;
            int choose = 0;
            MoveToCity(GameManager.researchCenterCities[choose].cityId);
        }
    }

    private void MoveToCityCard(_cityCard cityCard)
    {
        int ID = cityCard.cityID;
        actionsTaken[count] = new int[] { 2, cityID };
        count++;
        actionsLeft--;

        MoveToCity(ID);
        hand.discard(cityCard);
    }

    //[ClientRpc]
    private void MoveToCity(int ID)
    {
        //GameManager.GetCityFromID(ID).removePlayer(this);
        GameManager.GetCityFromID(ID).addPlayer(this);
        GameManager.GetCityFromID(ID).UpdatePawns();
        CurrentCity = GameManager.GetCityFromID(ID);
        cityID = ID;
    }

    private void RemoveDiseaseCubes(string colour)
    {
        switch (colour) {
            case "Blue":
                actionsTaken[count] = new int[] { 3,1, cityID};
                count++;
                actionsLeft--;
                break;
            case "Yellow":
                actionsTaken[count] = new int[] { 3, 2, cityID};
                count++;
                actionsLeft--;
                break;
            case "Black":
                actionsTaken[count] = new int[] { 3, 3, cityID};
                count++;
                actionsLeft--;
                break;
            case "Red":
                actionsTaken[count] = new int[] { 3, 4, cityID};
                count++;
                actionsLeft--;
                break;
        }
        GameManager.GetCityFromID(cityID).ReduceDiseaseSpread(colour, role);

    }
    private void buildResearchCenter(int cityID, _cityCard city)
    {
        //gameManager.GetCityFromID(cityID).hasResearchCenter = true;
        if (cityID == city.cityID)
        {
            GameManager.GetCityFromID(cityID).hasResearchCenter = true;
            hand.discard(city);
            actionsTaken[count] = new int[] { 4, cityID};
            count++;
            actionsLeft--;
        }

    }
    private void cureDisease()
    {
        _cityCard[] cards = new _cityCard[5];
        int[] checker = checkForCure(5, cards);
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
            actionsTaken[count] = new int[] { 5, checker[1], cards[0].cityID, cards[1].cityID, cards[2].cityID, cards[3].cityID, cards[4].cityID };
            count++;
            actionsLeft--;
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
            if (counters[i] >= counter)
            {
                return new int[] { 1, i };
            }
        }
        return new int[] { 0, 0 };
    }
    public IEnumerator waitForCity()
    {
        bool done = false;
        while (!done)
        {
            if (true)
            {

            }
        }
        yield return 0;
    }
}

