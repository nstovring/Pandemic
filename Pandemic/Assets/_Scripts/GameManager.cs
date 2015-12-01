using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[Serializable]
public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    //Stack Variables
    public static Stack infectCardStack;
    public static Stack playerCardStack;
    public static Stack infectDiscardStack;
    public static Stack playerDiscardStack;
    public static Stack roleCardStack;


    public static List<NetworkConnection> Connections;

    //City Variables
    public GameObject cityPrefab;

    public static City[] researchCenterCities;
    /// <summary>
    /// Returns the city coressponding to the ID provided
    /// </summary>
    /// <param name="iD"></param>
    /// <returns></returns>
    public static City GetCityFromID(int iD)
    {
        return cities[iD - 1];
    }
    public static City[] cities = new City[48];

    /// <summary>
    /// Returns the city corresponding to the string provided
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static City GetCityFromName(string name)
    {
        for (int i = 0; i < cityNames.GetLength(0); i++)
        {
            if (cityNames[i].Equals(name))
            {
                return cities[i];
            }
        }
        return null;
    }

    private Vector2[] cityPositions = new Vector2[48];

    static readonly string[] cityColors = { "Blue", "Yellow", "Black", "Red" };
    public static readonly string[] cityNames = { "San Fransisco", "Chicago", "Montreal", "Atlanta", "Washington", "New York", "London",
            "Madrid", "Paris", "Essen", "Milan", "St. Petersbug", "Los Angeles", "Mexico City",
            "Miami", "Bogota", "Lima", "Santiago", "Buenos Aires", "Sao Paulo", "Lagos",
            "Kinshasa", "Khartoum", "Johannesburg", "Algiers", "Istanbul", "Moscow", "Tehran",
            "Baghdad", "Cairo", "Riyadh", "Karrachi", "Delhi", "Mumbai", "Chennai", "Kolkata",
            "Bangkok", "Jakarta", "Ho Chi minh City", "Hong Kong", "Shanghai", "Beijing", "Seoul",
            "Tokyo", "Osaka", "Taipei", "Manila", "Sydney" };
    //The city connections correspond in order to a city in the cities array 
    public readonly int[][] connectedCities = {
            new int[] { 13, 2, 44, 47 },
            new int[] { 1, 3, 13, 14, 4 },
            new int[] { 2, 6, 5 },
            new int[] { 2, 5, 15 },
            new int[] { 4, 6, 3, 15 },
            new int[] { 5, 3, 7, 8 },
            new int[] { 6, 8, 9, 10 },
            new int[] { 6, 7, 9, 20, 25 },
            new int[] { 8, 7, 10, 11, 25 },
            new int[] { 7, 9, 11, 12, },
            new int[] { 9, 10, 26 },
            new int[] { 10, 26, 27 },
            new int[] { 1, 2, 14, 48 },
            new int[] { 2, 13, 15, 16, 17 },
            new int[] { 4, 5, 14, 16 },
            new int[] { 14, 15, 17, 19, 20 },
            new int[] { 14, 16, 18 },
            new int[] { 17 },
            new int[] { 16, 20 },
            new int[] { 8, 16, 19, 21 },
            new int[] { 20, 22, 23 },
            new int[] { 21, 23, 24 },
            new int[] { 21, 22, 23 },
            new int[] { 22, 23 },
            new int[] { 8, 9, 26, 30 },
            new int[] { 11, 12, 25, 27, 29, 30 },
            new int[] { 12, 26, 28 },
            new int[] { 27, 29, 32, 33 },
            new int[] { 26, 28, 30, 31, 32 },
            new int[] { 23, 25, 26, 29, 31 },
            new int[] { 29, 30, 32 },
            new int[] { 28, 29, 31, 33, 34 },
            new int[] { 28, 32, 34, 35, 36 },
            new int[] { 32, 33, 35 },
            new int[] { 33, 34, 36, 37, 38 },
            new int[] { 33, 35, 37, 40 },
            new int[] { 35, 36, 38, 39, 40 },
            new int[] { 35, 37, 39, 48 },
            new int[] { 37, 38, 40, 47 },
            new int[] { 36, 37, 39, 41, 46, 47 },
            new int[] { 40, 42, 43, 44, 46 },
            new int[] { 41, 43 },
            new int[] { 41, 42, 44 },
            new int[] { 1, 41, 43, 45 },
            new int[] { 44, 45 },
            new int[] { 40, 41, 45, 47 },
            new int[] { 1, 39, 40, 46 },
            new int[] { 13, 38, 47 }
            };

    //General Game Variables
    int infectionRate = 2;
    int epidemicCount = 0;
    int outbreakCounter = 0;
    int maxSingleDisease = 24;


    /// <summary>
    /// Returns the current disease with the highest amount
    /// </summary>
    /// <returns></returns>
    int currentDiseaseSpread()
    {
        if (redDiseaseSpread >= maxSingleDisease)
        {
            return redDiseaseSpread;
        }
        if (blueDiseaseSpread >= maxSingleDisease)
        {
            return blueDiseaseSpread;
        }
        if (yellowDiseaseSpread >= maxSingleDisease)
        {
            return yellowDiseaseSpread;
        }
        if (blackDiseaseSpread >= maxSingleDisease)
        {
            return blackDiseaseSpread;
        }
        return 0;
    }
    int maxDiseaseSpread = 96;

    public SyncListInt SyncListPlayerCardSort = new SyncListInt();
    public SyncListInt SyncListinfectionSort = new SyncListInt();
    public SyncListInt SyncListPlayerDiscardSort = new SyncListInt();
    public SyncListInt SyncListinfectionDiscardSort = new SyncListInt();

    [SyncVar]
    public int redDiseaseSpread = 0;
    [SyncVar]
    public int blueDiseaseSpread = 0;
    [SyncVar]
    public int yellowDiseaseSpread = 0;
    [SyncVar]
    public int blackDiseaseSpread = 0;

    [SyncVar]public int turnOrder = 0;

    [SyncVar]
    public bool redCure;
    [SyncVar]
    public bool blueCure;
    [SyncVar]
    public bool yellowCure;
    [SyncVar]
    public bool blackCure;
    public bool GetCureFromString(string color)
    {
        switch (color)
        {
            case "Blue":
                return blueCure;
            case "Yellow":
                return yellowCure;
            case "Black":
                return blackCure;
            case "Red":
                return redCure;
        }
        return false;
    }

    public static List<Player> players = new List<Player>();
    private NetworkIdentity netIdentity;
    [SyncVar] public bool initialize = true;

    private void Start()
    {
        instance = this;
        netIdentity = GetComponent<NetworkIdentity>();
    }

    public int testingPlayers = 0;
    private float timer = 5f;
    private void Update()
    {
        if (isServer)
        {
            if (netIdentity.observers.Count > testingPlayers && initialize && timer<= 5f)
            {
                timer -= Time.deltaTime;
                if (timer <= 5f)
                {
                    Rpc_InitializeBoard();

                    int[] roles = new[]
                    {
                        UnityEngine.Random.Range(0, 7), UnityEngine.Random.Range(0, 7), UnityEngine.Random.Range(0, 7),
                        UnityEngine.Random.Range(0, 7)
                    };

                    Rpc_InitializePlayers(roles);
                    initialize = false;
                }
            }
        }
    }

    [ClientRpc]
    private void Rpc_InitializePlayers(int[] roles)
    {
        int[] startingHands = { 47, 46, 45, 44, 43, 42 };

        GameObject[] playersGameObjects = GameObject.FindGameObjectsWithTag("Player");
        int count = 0;

        for (int i = 0; i < playersGameObjects.Length; i++)
        {
            Card[] startingHand = new Card[3];
            for (int j = 0; j < startingHand.Length; j++)
            {
                startingHand[j] = playerCardStack.cards[startingHands[count]];
                count ++;
            }

            playersGameObjects[i].GetComponent<Player>().Initialize(roles[i], startingHand);
            players.Add(playersGameObjects[i].GetComponent<Player>());
        }
    }

    [Command]
    public void CmdSwitchTurn()
    {
        turnOrder = turnOrder == netIdentity.observers.Count+1 ? 1: turnOrder++;
    }

    [ClientRpc]
    public void Rpc_InitializeBoard()
    {
        CreateCities();
        CreateStacks();
    }

    [ClientRpc]
    private void Rpc_Testing(int i, int j , int k , int l)
    {
        //Outbreak Testing code forthwith
        City HongKong = cities[i];
        City Shanghai = cities[j];
        City Kolkata = cities[k];
        City Bangkok = cities[l];

        InfectCity(Bangkok.cityId, Bangkok, 2);
        InfectCity(HongKong.cityId, HongKong, 2);
        InfectCity(Shanghai.cityId, Shanghai, 3);
        InfectCity(Kolkata.cityId, Kolkata, 4);
        CheckForOutbreak();
    }

    /// <summary>
    /// Instantiates four different stacks and initializes them
    /// </summary>
    //[Command]
    public static Stack AllCardsStack;
    
    private void CreateStacks()
    {
        AllCardsStack = new GameObject("AllCards").AddComponent<Stack>();
        AllCardsStack.Initialize(Stack.cardType.PLAYER_STACK);
        AllCardsStack.addEpidemicCards();

        infectCardStack = new GameObject("infectCardStack").AddComponent<Stack>();
        infectCardStack.Initialize(Stack.cardType.INFECTION);
        infectCardStack.shuffleStack();
        int[] shuffledInfectInts = new int[infectCardStack.cards.Length];
        for (int j = 0; j < infectCardStack.cards.Length; j++)
        {
            shuffledInfectInts[j] = infectCardStack.cards[j].Id-1;
            if (isServer)
            {
                Debug.Log("Add");
                SyncListinfectionSort.Add(infectCardStack.cards[j].Id - 1); //new stuff
            }
        }
        Destroy(infectCardStack.gameObject);

        playerCardStack = new GameObject("playerCardStack").AddComponent<Stack>();
        playerCardStack.Initialize(Stack.cardType.PLAYER_STACK);
        playerCardStack.shuffleStack();
        int[] shuffledCityInts = new int[playerCardStack.cards.Length];
        for (int j = 0; j < playerCardStack.cards.Length; j++)
        {
            shuffledCityInts[j] = playerCardStack.cards[j].Id - 1;
            if (isServer)
            {
                Debug.Log("Add");
                SyncListPlayerCardSort.Add(playerCardStack.cards[j].Id - 1); //new stuff
            }
        }
        Destroy(playerCardStack.gameObject);

        roleCardStack = new GameObject("roleCardStack").AddComponent<Stack>();
        roleCardStack.Initialize(Stack.cardType.ROLE);

        if (isServer)
        {
            Rpc_CreateStacks();
            Rpc_InitialInfection();
        }
    }

    /// <summary>
    /// Copy the values from the server to the clients
    /// </summary>
    /// <param name="shuffledInfectInts"></param>
    /// <param name="shuffledCityInts"></param>
    [ClientRpc]
    private void Rpc_CreateStacks()
    {
        infectCardStack = new GameObject("infectCardStack").AddComponent<Stack>();
        infectCardStack.Initialize(Stack.cardType.INFECTION);
        infectCardStack.cards = SortCardsToList(infectCardStack.cards, SyncListinfectionSort);

       
        playerCardStack = new GameObject("playerCardStack").AddComponent<Stack>();
        playerCardStack.Initialize(Stack.cardType.PLAYER_STACK);
        playerCardStack.cards = SortCardsToList(playerCardStack.cards, SyncListPlayerCardSort);

       
        infectDiscardStack = new GameObject("infectCardStack").AddComponent<Stack>();
        infectDiscardStack.Initialize(Stack.cardType.INFECTION);
        infectDiscardStack.EmptyCards();

        playerDiscardStack = new GameObject("playerDiscardStack").AddComponent<Stack>();
        playerDiscardStack.Initialize(Stack.cardType.PLAYER_STACK);
        playerDiscardStack.EmptyCards();

    }

    /// <summary>
    /// Method responsible for creating and initializing the cities
    /// </summary>
    private void CreateCities()
    {
        cityPositions = transform.GetChild(0).GetComponent<SaveCityPositions>().positions; // Get the position of the city from the SaveCityPositions class array positions
        int colorIncrement = 1;
        GameObject cityParent = new GameObject("CityParent"); // Instantiate an empty GameObject to serve as the parent to all cities
        for (int iD = 0, colorGroup = 0; iD < cities.Length; iD++, colorIncrement++)
        {
            GameObject cityGameObject = Instantiate(cityPrefab, cityPositions[iD], Quaternion.Euler(-90, 0, 0)) as GameObject; // Instantiate the city
            cityGameObject.transform.parent = cityParent.transform; // Set the parent
            colorIncrement = colorIncrement % 13 == 0 ? colorIncrement = 1 : colorIncrement % 13; // Color increment loops from 1->12
            cities[iD] = cityGameObject.GetComponent<City>(); // Assign the City class of the City GameObject to the cities array
            cities[iD].Initialize(iD + 1, connectedCities[iD], cityColors[colorGroup], cityNames[iD]); // Initialize the city

            colorGroup = colorIncrement >= 12 ? colorGroup + 1 : colorGroup; // If color increment oversteps or is equal to 12 increment colorGroup
        }
    }

    /// <summary>
    /// Epidemic Method
    /// </summary>
    public void Epidemic()
    {
        Card bottomCard = infectCardStack.cards[0]; // Pick the bottom card of the stack
        InfectCity((_infectionCard) bottomCard, 3); // Infect the city coressponding to that card
        //Add bottom card to discards
        //infectCardStack = Stack.removeCard(infectCardStack, bottomCard.name);//infectCardStack
        //infectDiscardStack = Stack.addCard(infectCardStack,infectDiscardStack, bottomCard.name);//infectCardStack
        //Shuffle discards here
        //infectDiscardStack.Shuffle();
        //And then combine stacks
        //infectCardStack = CombineStacks(infectCardStack, infectDiscardStack);
        //Set infectionRate & increment epidemicCount
        epidemicCount++;
        infectionRate = epidemicCount > 3 ? 3 : epidemicCount > 5 ? 4 : infectionRate;
        //Then infect cities
        InfectCities();
    }
    //The cities are infected from the top of the stack up during initialization

    public Card[] SortCardsToList(Card[] cards, SyncListInt sortListInt)
    {
        Debug.Log(AllCardsStack.cards.Length);
        Debug.Log(cards.Length);

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = AllCardsStack.cards[(sortListInt[i])];
        }
        return cards;
    }
    [ClientRpc]
    private void Rpc_InitialInfection()
    {
        int increment = 0;
        //infectCardStack.cards = SortCardsToList(infectCardStack.cards, SyncListinfectionSort);
        //Loop counts from the top nine cards down
        for (int i = infectCardStack.cards.Length - 1, infectRate = 3; i > infectCardStack.cards.Length - 10; i--, increment++)
        {
            increment = increment % 4 == 0 ? increment = 1 : increment % 4; //Infection progresses as such: 3 first cities get 3 diseaseMarker, 3 next get 2, 3 last gets 1
            Card infectionCard =  infectCardStack.cards[i];
            InfectCity(infectionCard, infectRate);
            infectRate = increment >= 3 ? infectRate - 1 : infectRate;
        }
        UpdateInfectionStacks();
    }

    /// <summary>
    /// General Infection method meant for end of turn infection
    /// </summary>
    public void InfectCities()
    {
        for (int i = 1; i < infectionRate; i++)
        {
            _infectionCard infectionCard = (_infectionCard)infectCardStack.cards[infectCardStack.cards.Length - i];
            InfectCity(infectionCard, 1);
        }
        CheckForOutbreak();
    }

    /// <summary>
    /// Infection method for directly infecting a specific city
    /// </summary>
    /// <param name="infectionCard"></param>
    /// <param name="infectRate"></param>
    private void InfectCity(Card infectionCard, int infectRate)
    {
        City infectedCity = GetCityFromID(infectionCard.Id);
        infectedCity.IncrementDiseaseSpread(infectedCity.color, infectRate);
        //infectDiscardStack.AddCard(infectionCard.Id, 1);
        //infectCardStack.RemoveCard(infectionCard.Id);
        Cmd_ReduceInfectionSyncListInt(infectedCity.cityId);
        SetDiseaseSpread(infectedCity.color, infectRate);
    }

    
    [Command]
    void Cmd_ReduceInfectionSyncListInt(int removal)
    {
        SyncListinfectionDiscardSort.Add(removal);
        SyncListinfectionSort.Remove(removal);
    }

    [Command]
    void Cmd_ReduceCitySyncListInt(int removal)
    {
        SyncListPlayerDiscardSort.Add(removal);
        SyncListPlayerCardSort.Remove(removal);
    }
    [Command]
    public void Cmd_AddToCityDiscardList(int removal)
    {
        SyncListPlayerDiscardSort.Add(removal);
        Rpc_UpdateCityStacks();
    }

    //[ClientRpc]
    void UpdateInfectionStacks()
    {
       // infectCardStack.cards = SortCardsToList(infectCardStack.cards, SyncListinfectionSort);
       // infectDiscardStack.cards = SortCardsToList(infectDiscardStack.cards, SyncListinfectionDiscardSort);
    }

    [ClientRpc]
    void Rpc_UpdateCityStacks()
    {
        //playerCardStack.cards = SortCardsToList(playerCardStack.cards, SyncListPlayerCardSort);
       // playerDiscardStack.cards = SortCardsToList(playerDiscardStack.cards, SyncListPlayerDiscardSort);
    }

    /// <summary>
    /// Infection method for directly infecting a specific city
    /// </summary>
    /// <param name="cityID"></param>
    /// <param name="infectRate"></param>
    private void InfectCity(int cityID, City infectionSource, int infectRate)
    {
        City infectedCity = GetCityFromID(cityID);
        infectedCity.IncrementDiseaseSpread(infectionSource.color, infectRate);
        //infectedCity.diseaseSpread += infectRate;
        SetDiseaseSpread(infectedCity.color, infectRate);
    }

    //[Command]
    public void SetDiseaseSpread(string color, int infectRate)
    {
        switch (color)
        {
            case "Blue":
                blueDiseaseSpread += infectRate;
                break;
            case "Black":
                blackDiseaseSpread += infectRate;
                break;
            case "Yellow":
                yellowDiseaseSpread += infectRate;
                break;
            case "Red":
                redDiseaseSpread += infectRate;
                break;
        }
    }

    public void CheckForOutbreak()
    {
        for (int i = 0; i < cities.Length; i++)
        {
            if (cities[i].DiseaseSpread > 3 && !cities[i].locked)
            {
                cities[i].locked = true;
                Debug.Log("Outbreak happened in " + (cityNames[i]));
                SpreadOutbreak(i);
            }
        }
        //Unlock cities and set the disease counter to normal
        ResetCities();
    }
    public void SpreadOutbreak(int cityIndex)
    {
        City outbreakSource = cities[cityIndex];
        for (int j = 0; j < outbreakSource.connectedCityIDs.Length; j++)
        {
            City infectedCity = GetCityFromID(outbreakSource.connectedCityIDs[j]);
            InfectCity(outbreakSource.connectedCityIDs[j], outbreakSource, 1);
            Debug.Log("Disease has spread to " + infectedCity.name + " " + infectedCity.DiseaseSpread);
        }
        CheckForOutbreak();
        outbreakCounter++;
    }

    public void ResetCities()
    {
        foreach (City t in cities)
        {
            if (t.DiseaseSpread > 3 && t.locked)
            {
                SetDiseaseSpread(t.color, -(t.DiseaseSpread - 3));
                t.ResetValues();
            }
        }
    }
    [ClientRpc]
    public void Rpc_CheckForGameOver()
    {
        if (redCure && blackCure && blueCure && yellowCure)
        {
            WinGame();
        }
        if (outbreakCounter >= 8 || playerCardStack.cards.Length < 2 || currentDiseaseSpread() >= maxSingleDisease)
        {
            LoseGame();
        }
    }
    public void WinGame()
    {
        Debug.Log("Game Won!");
    }
    public void LoseGame()
    {
        Debug.Log("Game lost!");
    }
}

