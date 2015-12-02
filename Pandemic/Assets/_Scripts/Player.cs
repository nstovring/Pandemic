using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public int cityID;


    public Hand hand;
    public City CurrentCity;    

    //public _roleCard role;

    public _roleCard role;
    GameManager gameManager;
    public int actionsLeft;
    public int[][] actionsTaken;
    int count;
    private int currentCard;



    //[ClientRpc]
    public void Initialize(int role, Card[] startingHand)
    {
        gameManager = GameManager.instance;

        count = 0;
        actionsTaken = new int[1000][];
        actionsLeft = 4;

        hand = new GameObject("Hand").AddComponent<Hand>();
        hand.transform.parent = transform;
        hand.Initialize(startingHand, this);

        cityID = 4;
        CurrentCity = GameManager.GetCityFromID(4);

        Debug.Log(role);
        Debug.Log(GameManager.roleCardStack.cards.Count);

        Card tempRole = GameManager.roleCardStack.cards[role];

        this.role = tempRole as _roleCard; 

        //this.role = (_roleCard) GameManager.roleCardStack.cards[role]; //GameManager.roleCardStack.roleCards.Contains(role); //Error?
        MoveToCity(cityID);
        CurrentCity.UpdatePawns();
    }

    public void shareKnowledge(Player [] allPlayers , int playerNo,  int cardIndex) {
        Card tmp = null;
        tmp = this.hand.cards[cardIndex];
        this.hand.cards[cardIndex] = allPlayers[playerNo].hand.cards[cardIndex];
        allPlayers[playerNo].hand.cards[cardIndex] = tmp;
    }

    //[ClientCallback]
    void Update()
    {
        if (isLocalPlayer)
        {
            InputMoveToCity();
        }
        else if (CurrentCity!= null && CurrentCity.cityId != cityID)
        {
            MoveToCity(cityID);
        }
    }

    public void UpdateSyncListCards()
    {
        for (int i = 0; i < hand.cards.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.SyncListPlayerCardSort.Count; j++)
            {
                if (hand.cards[i].Id == GameManager.instance.SyncListPlayerCardSort[j])
                {
                    GameManager.instance.Cmd_RemoveFromCityList(hand.cards[i].Id);
                }
            }

        }
    }


    void InputMoveToCity()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100);
        if (Input.GetMouseButtonUp(0))
        {
            if (hit.collider != null)
            {
                if (hit.transform.tag == "City" && actionsLeft > 0 && CityIsConnected(hit.transform.GetComponent<City>().cityId))
                {
                    MoveToCity(hit.transform.GetComponent<City>().cityId);
                    UpdateSyncListCards();
                }
                if (hit.transform.tag == "DiseaseCube" && actionsLeft > 0)
                {
                    Cmd_RemoveDiseaseCubes(City.GetStringFromColor(hit.transform.GetComponent<SpriteRenderer>().color));
                }
            }
        }
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
            for (int i = 0; i < hand.cards.GetLength(0); i++)
            {
                if(hand.cards[i] is _cityCard)
                {
                    GameManager.GetCityFromID(hand.cards[i].Id).active = true;
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
        for(int i = 0; i < hand.cards.GetLength(0); i++)
        {
            if (hand.cards[i] is _cityCard && hand.cards[i].Id == cityID)
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

    public void MoveToCityCard(int cityCardID)
    {
        Debug.Log("move to city: " + cityCardID);
       // int ID = cityCard.Id;
        int ID = cityCardID;
        actionsTaken[count] = new int[] { 2, cityID };
        count++;
        actionsLeft--;

        MoveToCity(ID);
        hand.discard(ID);
        GameManager.instance.Cmd_AddToCityDiscardList(ID);
    }


    [Command]
    void Cmd_ChangeCityID(int newID)
    {
        cityID = newID;
    }
    [Client]
    public void MoveToCity(int ID)
    {

        CurrentCity.removePlayer(this);
        CurrentCity.UpdatePawns();

        City newCity = GameManager.GetCityFromID(ID);
        newCity.addPlayer(this);
        newCity.UpdatePawns();
        CurrentCity = newCity;
        //Move
        cityID = ID;
        //Synchronize the cityID
        Cmd_ChangeCityID(cityID);
    }

    [Command]
    private void Cmd_RemoveDiseaseCubes(string colour)
    {
        Rpc_RemoveDiseaseCubes(colour);
    }

    [ClientRpc]
    void Rpc_RemoveDiseaseCubes(string colour)
    {
        switch (colour)
        {
            case "Blue":
                actionsTaken[count] = new int[] { 3, 1, cityID };
                count++;
                actionsLeft--;
                break;
            case "Yellow":
                actionsTaken[count] = new int[] { 3, 2, cityID };
                count++;
                actionsLeft--;
                break;
            case "Black":
                actionsTaken[count] = new int[] { 3, 3, cityID };
                count++;
                actionsLeft--;
                break;
            case "Red":
                actionsTaken[count] = new int[] { 3, 4, cityID };
                count++;
                actionsLeft--;
                break;
        }
        GameManager.GetCityFromID(cityID).ReduceDiseaseSpread(colour, role);
    }

    private void buildResearchCenter(int cityID, _cityCard city)
    {
        //gameManager.GetCityFromID(cityID).hasResearchCenter = true;
        if (cityID == city.Id)
        {
            GameManager.GetCityFromID(cityID).hasResearchCenter = true;
            hand.discard(city);
            actionsTaken[count] = new int[] { 4, cityID};
            count++;
            actionsLeft--;
        }

    }

    public void cureDisease(int [] cardIDs)
    {
       
     Debug.Log(cardIDs[1]);
        
        int[] checker = checkForCure(5, cardIDs);
        Debug.Log(checker[0]);
        if (GameManager.GetCityFromID(cityID).researchCenter && checker[0] == 1)
        {
          
            switch (checker[1])
            {
                case 0:
                    GameManager.instance.blueCure = true;

                    break;
                case 1:
                    GameManager.instance.yellowCure = true;
                    break;
                case 2:
                    GameManager.instance.blackCure = true;
                    break;

                case 3:
                    GameManager.instance.redCure = true;
                    Debug.Log("Red cure true ");
                    break;
            }
                   
         //   actionsTaken[count] = new int[] { 5, checker[1], cards[0].Id, cards[1].Id, cards[2].Id, cards[3].Id, cards[4].Id };
            count++;
            actionsLeft--;
        }
        
    }

    public void cureDisease()
    {
        Card[] cards = hand.cards;
        Debug.Log(cards[0]);
        /*
        int[] checker = checkForCure(5, cards);
        Debug.Log(checker[0]);
        if (GameManager.GetCityFromID(cityID).researchCenter && checker[0] == 1)
        {
          
            switch (checker[1])
            {
                case 0:
                    GameManager.instance.blueCure = true;

                    break;
                case 1:
                    GameManager.instance.yellowCure = true;
                    break;
                case 2:
                    GameManager.instance.blackCure = true;
                    break;

                case 3:
                    GameManager.instance.redCure = true;
                    Debug.Log("Red cure true ");
                    break;
            }
            //hand.        
            actionsTaken[count] = new int[] { 5, checker[1], cards[0].Id, cards[1].Id, cards[2].Id, cards[3].Id, cards[4].Id };
            count++;
            actionsLeft--;
        }
        */
    }
    private int[] checkForCure(int counter, int [] hand)
    {

        int[] counters = new int[4];
        for (int i = 0; i < hand.GetLength(0); i++)
        {
            Debug.Log(hand[i]);
            if (hand[i] != null)
            {
                String colour = GameManager.GetCityFromID(hand[i]).color;
                Debug.Log(colour);
                switch (GameManager.GetCityFromID(hand[i]).color)
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
            Debug.Log(counters[i]);
            if (counters[i] >= counter)
            {
                return new int[] { 1, i };
            }
        }
        return new int[] { 0, 0 };
    }

    private int[] checkForCure(int counter, Card[] hand)
    {

        int[] counters = new int[4];
        for (int i = 0; i < hand.GetLength(0); i++)
        {
            Debug.Log(hand[i]);
            if (hand[i] != null)
            {
                String colour = GameManager.GetCityFromID(hand[i].Id).color;
                Debug.Log(colour);
                switch (GameManager.GetCityFromID(hand[i].Id).color)
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
            Debug.Log(counters[i]);
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
    private bool CityIsConnected(int ID)
    {
        
        for(int i = 0; i < CurrentCity.connectedCityIDs.Length; i++)
        {
            if(CurrentCity.connectedCityIDs[i] == ID)
            {
                return true;
            }
        }
        return false;
    }
}

