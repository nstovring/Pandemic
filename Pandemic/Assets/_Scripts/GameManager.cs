using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    Player[] players;
    public City[] researchCenterCities;
    public static GameManager instance;

    static Stack infectCardStack;
    static Stack playerCardStack;
    static Stack infectDiscardStack;
    static Stack playerDiscardStack;
    private Stack CombineStacks(Stack infectionStack, Stack infectionDiscardStack)
    {
        return infectionStack;
    }

    public GameObject cityPrefab;
    public static City[] cities = new City[48];
    public City GetCityFromID(int iD)
    {
        return cities[iD - 1];
    }
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
	
	public int[][] connectedCities = {
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
    int infectionRate = 2;
    int epidemicCount = 0;
    int outbreakCounter = 0;
    int maxSingleDisease = 24;

    int currentDiseaseSpread()
    {
        if (redDiseaseSpread >= maxSingleDisease)
        {
            return redDiseaseSpread;
        }
        else if (blueDiseaseSpread >= maxSingleDisease)
        {
            return blueDiseaseSpread;
        }
        else if (yellowDiseaseSpread >= maxSingleDisease)
        {
            return yellowDiseaseSpread;
        }
        else if (blackDiseaseSpread >= maxSingleDisease)
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

    bool redCure;
    bool blueCure;
    bool yellowCure;
    bool blackCure;


    public void InitializeGame()
    {
        instance = this;
        CreateCities();
        CreateStacks();
        InitialInfection();

        //Testing();

        CheckForOutbreak();
        Debug.Log("currentDiseaseSpread: " + currentDiseaseSpread());

        Debug.Log("Outbreak Amount: " + outbreakCounter);
        CheckForGameOver();
    }

    void Testing()
    {
        //Outbreak Testing code forthwith
        City HongKong = GetCityFromName("Hong Kong");
        City Shanghai = GetCityFromName("Shanghai");
        City Taipei = GetCityFromName("Taipei");

        //InfectCity(HongKong.cityId, 4);
        //InfectCity(Shanghai.cityId, 3);
        //InfectCity(Taipei.cityId, 3);

        //InfectCity(infectCardStack.cardStack.get(0), 4);

        //System.out.println(infectCardStack.cardStack.get(0).cardName);

        //System.out.println(GetCityFromName(infectCardStack.cardStack.get(0).cardName).diseaseSpread);
         //Epidemic testing code forthwith 
		Epidemic();
		Epidemic();
		Epidemic();
		Epidemic();
		City SanFran = GetCityFromName("San Fransisco");
		Debug.Log("Epidemic Count: "+ epidemicCount);
        Debug.Log(SanFran.diseaseSpread);
        Debug.Log("Infection Rate: " + infectionRate);
		//Testing code end
    }

    public void CreateStacks()
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

    private void Start()
    {
        InitializeGame();
    }

    //Method responsible for creating the cities
    public void CreateCities()
    {
        cityPositions = transform.GetChild(0).GetComponent<WriteCityPositions>().positions;
        int colorIncrement = 1;
        GameObject cityParent = new GameObject("CityParent");
        for (int i = 0, colorGroup = 0; i < cities.Length; i++, colorIncrement++)
        {
            GameObject cityGameObject = Instantiate(cityPrefab, cityPositions[i],Quaternion.Euler(-90,0,0)) as GameObject;
            cityGameObject.transform.parent = cityParent.transform;
            colorIncrement = colorIncrement % 13 == 0 ? colorIncrement = 1 : (colorIncrement % 13);

            cities[i] = cityGameObject.GetComponent<City>();
            cities[i].Initialize(i + 1, connectedCities[i], cityColors[colorGroup], cityNames[i]);

            colorGroup = colorIncrement >= 12 ? colorGroup + 1 : colorGroup;
        }
    }
    public void Epidemic()
    {
        _infectionCard bottomCard = infectCardStack.infectionCards[0];
        InfectCity(bottomCard, 3);
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
    //The cities are infected from the bottom of the stack up during initialization
    public void InitialInfection()
    {
        int increment = -1;
        for (int i = infectCardStack.cards.Length - 1, infectRate = 3; i > infectCardStack.cards.Length - 10; i--, increment++)
        {
            increment = increment % 4 == 0 ? increment = 1 : increment % 4;
            _infectionCard infectionCard = infectCardStack.infectionCards[i]; ;
            InfectCity(infectionCard, infectRate);
            infectRate = increment >= 3 ? infectRate - 1 : infectRate;
        }
    }
    public void InfectCities()
    {
        for (int i = 1; i < infectionRate; i++)
        {
            _infectionCard infectionCard = infectCardStack.infectionCards[infectCardStack.infectionCards.Length - i];//(infectCardStack.cardStack.size() - i);
            InfectCity(infectionCard, 1);
        }
        CheckForOutbreak();
    }

    public void InfectCity(_infectionCard infectionCard, int infectRate)
    {
        City infectedCity = GetCityFromID(infectionCard.infectionID);
        infectedCity.diseaseSpread += infectRate;
        // infectCardStack.RemoveCard(infectionCard);
        // infectDiscardStack.AddCard(infectionCard);
        SetDiseaseSpread(infectedCity.color, infectRate);
    }
    public void InfectCity(int cityID, int infectRate)
    {
        City infectedCity = GetCityFromID(cityID);
        infectedCity.diseaseSpread += infectRate;
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
            if (cities[i].diseaseSpread > 3 && !cities[i].locked)
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
            InfectCity(outbreakSource.connectedCityIDs[j], 1);
            Debug.Log("Disease has spread to " + infectedCity.name + " " + infectedCity.diseaseSpread);
        }
        CheckForOutbreak();
        outbreakCounter++;
    }

    public void ResetCities()
    {
        for (int i = 0; i < cities.Length; i++)
        {
            if (cities[i].diseaseSpread > 3 && cities[i].locked)
            {
                SetDiseaseSpread(cities[i].color, -(cities[i].diseaseSpread - 3));
                cities[i].locked = false;
                cities[i].diseaseSpread = 3;
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

