using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{ 
    public static GameManager instance;

    //Stack Variables
    public static Stack infectCardStack;
    public static Stack playerCardStack;
    public static Stack infectDiscardStack;
    public static Stack playerDiscardStack;
    //Method for combining stacks
    private Stack CombineStacks(Stack infectionStack, Stack infectionDiscardStack)
    {
        return infectionStack;
    }

    //City Variables
    public GameObject cityPrefab;

    public City[] researchCenterCities;
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

    static readonly string[] cityColors = {"Blue", "Yellow", "Black", "Red"};
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

    int redDiseaseSpread = 0;
    int blueDiseaseSpread = 0;
    int yellowDiseaseSpread = 0;
    int blackDiseaseSpread = 0;

    public static bool redCure;
    public static bool blueCure;
    public static bool yellowCure;
    public static bool blackCure;
    public static bool GetCureFromString(string color)
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

    Player[] players;

    private void Start()
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        instance = this;
        CreateCities();
        CreateStacks();
        //InitialInfection();

        Testing();

        //CheckForOutbreak();
        Debug.Log("currentDiseaseSpread: " + currentDiseaseSpread());

        Debug.Log("Outbreak Amount: " + outbreakCounter);
        CheckForGameOver();
    }

    private void Testing()
    {
        //Outbreak Testing code forthwith
        City HongKong = GetCityFromName("Hong Kong");
        City Shanghai = GetCityFromName("Shanghai");
        City Kolkata = GetCityFromName("Kolkata");
        City Bangkok = GetCityFromName("Bangkok");

        InfectCity(Bangkok.cityId, Bangkok, 2);
        InfectCity(HongKong.cityId, HongKong, 2);
        InfectCity(Shanghai.cityId, Shanghai, 3);
        InfectCity(Kolkata.cityId, Kolkata, 4);
        CheckForOutbreak();
    }

    /// <summary>
    /// Instantiates four different stacks and initializes them
    /// </summary>
    private void CreateStacks()
    {
        infectCardStack = new GameObject("infectCardStack").AddComponent<Stack>();
        infectCardStack.Initialize(48, Stack.cardType.INFECTION);

        playerCardStack = new GameObject("playerCardStack").AddComponent<Stack>();
        playerCardStack.Initialize(48, Stack.cardType.CITY);

        infectDiscardStack = new GameObject("infectCardStack").AddComponent<Stack>();
        infectDiscardStack.Initialize(48, Stack.cardType.INFECTION);

        playerDiscardStack = new GameObject("playerDiscardStack").AddComponent<Stack>();
        playerDiscardStack.Initialize(48, Stack.cardType.CITY);
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
            GameObject cityGameObject = Instantiate(cityPrefab, cityPositions[iD],Quaternion.Euler(-90,0,0)) as GameObject; // Instantiate the city
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
        _infectionCard bottomCard = infectCardStack.infectionCards[0]; // Pick the bottom card of the stack
        InfectCity(bottomCard, 3); // Infect the city coressponding to that card
        //Add bottom card to discards
        // infectCardStack.RemoveCard(bottomCard);
        // infectDiscardStack.AddCard(bottomCard);
        //Shuffle discards here
        //infectDiscardStack.Shuffle();
        //And then combine stacks
        infectCardStack = CombineStacks(infectCardStack, infectDiscardStack);
        //Set infectionRate & increment epidemicCount
        epidemicCount++;
        infectionRate = epidemicCount > 3 ? 3 : epidemicCount > 5 ? 4 : infectionRate;
        //Then infect cities
        //InfectCities();
    }
    //The cities are infected from the top of the stack up during initialization
    private void InitialInfection()
    {
        int increment = 0;
        //Loop counts from the top nine cards down
        for (int i = infectCardStack.cards.Length - 1, infectRate = 3; i > infectCardStack.cards.Length - 10; i--, increment++)
        {
            increment = increment % 4 == 0 ? increment = 1 : increment % 4; //Infection progresses as such: 3 first cities get 3 diseaseMarker, 3 next get 2, 3 last gets 1
            _infectionCard infectionCard = infectCardStack.infectionCards[i]; ;
            InfectCity(infectionCard, infectRate);
            infectRate = increment >= 3 ? infectRate - 1 : infectRate;
        }
    }


    /// <summary>
    /// General Infection method meant for end of turn infection
    /// </summary>
    private void InfectCities()
    {
        for (int i = 1; i < infectionRate; i++)
        {
            _infectionCard infectionCard = infectCardStack.infectionCards[infectCardStack.infectionCards.Length - i];//(infectCardStack.cardStack.size() - i);
            InfectCity(infectionCard, 1);
        }
        CheckForOutbreak();
    }


    /// <summary>
    /// Infection method for directly infecting a specific city
    /// </summary>
    /// <param name="infectionCard"></param>
    /// <param name="infectRate"></param>
    private void InfectCity(_infectionCard infectionCard, int infectRate)
    {
        City infectedCity = GetCityFromID(infectionCard.infectionID);
        infectedCity.IncrementDiseaseSpread(infectedCity.color, infectRate);
        //infectedCity.diseaseSpread += infectRate;
        // infectCardStack.RemoveCard(infectionCard);
        // infectDiscardStack.AddCard(infectionCard);
        SetDiseaseSpread(infectedCity.color, infectRate);
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

    void SetDiseaseSpread(string color, int infectRate)
    {
        switch (color)
        {
            case "Blue":
                blueDiseaseSpread += infectRate;
                break;
            case "Yellow":
                blackDiseaseSpread += infectRate;
                break;
            case "Black":
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

    public void CheckForGameOver()
    {
        if (redCure && blackCure && blueCure && yellowCure)
        {
            WinGame();
        }
        if (outbreakCounter >= 8 || playerCardStack.cityCards.Length < 2 || currentDiseaseSpread() >= maxSingleDisease)
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

