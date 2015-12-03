using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class Stack : MonoBehaviour
{
    //Each stack has an list of cards. For obvious reasons of course
    public List<Card> cards;

    //This enum is used to denote which type of stack you want to create when calling the intitialize function during stack creation
    public enum cardType
    {
        PLAYER_STACK,
        INFECTION,
        ROLE,
        DISCARD
    };

    //On creation, create stack of type...
    public void Initialize(cardType type)
    {
        Sprite sprite = Instantiate(Resources.Load("Citycard_blue", typeof(Sprite))) as Sprite;
        produceCards(type);
    }

    //When called, depending on the enum typed, it will call the appropiate method that creates cards
    public void produceCards(cardType type)
    {
        switch (type)
        {
            case cardType.PLAYER_STACK:
                createPlayerStack();
                break;
            case cardType.INFECTION:
                createInfectionCards();
                break;
            case cardType.ROLE:
                createRoleCards();
                break;
            case cardType.DISCARD:
                createDiscardStack();
                break;
            default:
                break;
        }
    }

    //Empties the card list by equalling it to an empty list
    public void EmptyCards()
    {
       cards = new List<Card>();
    }

    //Shuffles the card list when called
    public void shuffleStack()
    {
        // Knuth shuffle algorithm
        for (int i = 0; i < cards.Count; i++)
        {
            Card tmp = cards[i];
            int r = UnityEngine.Random.Range(i, cards.Count);
            cards[i] = cards[r];
            cards[r] = tmp;
        }
    }

    //As the name suggests, it simply adds the epidemic cards to the cards list
    public void addEpidemicCards()
    {
        //Creates an array of epidemic cards
        _epidemicCard[] epidemicCards = new _epidemicCard[5];

        //instantiates said epidemic cards and puts them under a single parent - or else the hierarchy will be crowded big time
        for (int i = 0; i < epidemicCards.Length; i++)
        {
            epidemicCards[i] = new GameObject().AddComponent<_epidemicCard>();
            epidemicCards[i].transform.parent = transform;
        }

        //Denotes both a name and an ID to the epidemic cards
        for (int i = 0; i < epidemicCards.Length; i++)
        {
            epidemicCards[i].name = "Epidemic";
            epidemicCards[i].Id = 54 + i;
        }

        //Adds the epidemic cards to the cards list
        for (int i = 0; i < epidemicCards.Length; i++)
        {
            cards.Add(epidemicCards[i]);
        }

    }

    //Creates an empty cards list, to make it function as a discard pile
    public void createDiscardStack()
    {
        cards = new List<Card>();
    }

    //Creates the infection stack when called
    public void createInfectionCards()
    {
        //Initializes the card list array
        cards = new List<Card>();

        //Creates the infection cards
        _infectionCard[] infectionCards = new _infectionCard[48];

        //Instantiates the infection cards
        for (int i = 0; i < infectionCards.Length; i++)
        {
            infectionCards[i] = new GameObject().AddComponent<_infectionCard>();
            infectionCards[i].transform.parent = transform;
        }

        //Gives a name to each infection card + ID based on the cityNames array in the GameManager
        for (int i = 0; i < infectionCards.Length; i++)
        {
            infectionCards[i].name = GameManager.cityNames[i];
            infectionCards[i].Id = i + 1;
        }

        //Adds all the infection cards to the cards list
        for (int i = 0; i < infectionCards.Length; i++)
        {
            cards.Add(infectionCards[i]);

        }
    }

    //Creates the role cards
    public void createRoleCards()
    {

        //Creates the role cards
        _roleCard[] roleCards = new _roleCard[7];

        //Initializes the card list array
        cards = new List<Card>();

        //Intantiates the role cards
        for (int i = 0; i < roleCards.Length; i++)
        {
            roleCards[i] = new GameObject().AddComponent<_roleCard>();
            roleCards[i].transform.parent = transform;

        }

        //Manually adds the names to each of the role cards...
        Debug.Log("Creating Role Cards");
        roleCards[0].name = ("MEDIC");
        roleCards[1].name = ("DISPATCHER");
        roleCards[2].name = ("QURANTINE SPECIALIST");
        roleCards[3].name = ("CONTINGENCY");
        roleCards[4].name = ("RESEARCHER");
        roleCards[5].name = "SCIENTIST";
        roleCards[6].name = "OPERATIONS EXPERT";

        //Then sets the roles accordingly with the naming above. It uses an enum found within the rolecards for this.
        roleCards[0].role = _roleCard.roleType.MEDIC;
        roleCards[1].role = _roleCard.roleType.DISPATCHER;
        roleCards[2].role = _roleCard.roleType.QURANTINE_SPECIALIST;
        roleCards[3].role = _roleCard.roleType.CONTINGENCY;
        roleCards[4].role = _roleCard.roleType.RESEARCHER;
        roleCards[5].role = _roleCard.roleType.SCIENTIST;
        roleCards[6].role = _roleCard.roleType.OPERATIONS_EXPERT;

        //Adds the rolecards to the cards list array
        for (int i = 0; i < roleCards.Length; i++)
        {
            cards.Add(roleCards[i]);
        }
    }

    //Creates the playerstack, which hold the city cards and the event cards
    public void createPlayerStack()
    {
        //Creates both the city cards and the event cards
        _cityCard[] cityCards = new _cityCard[48];
        _eventCard[] eventCards = new _eventCard[5];

        //Initializes the cards list
        cards = new List<Card>();

        //Instantiates the city cards
        for (int i = 0; i < cityCards.Length; i++)
        {
            cityCards[i] = new GameObject().AddComponent<_cityCard>();
            //Debug.Log(Resources.Load("Citycard_blue"));

            //Depending on which value i has, fetch a different sprite. There are four spirte colours, therefore there are four stages to this.
            if (i < 12)
            {
                cityCards[i].image = Resources.Load<Sprite>("Citycard_blue");
                //Debug.Log("does it run?");
            }
            else if (i >= 12 && i < 24)
            {
                cityCards[i].image = Resources.Load<Sprite>("Citycard_yellow");
            }
            else if (i >= 24 && i < 36)
            {
                cityCards[i].image = Resources.Load<Sprite>("Citycard_black");
            }
            else if (i >= 36 && i < 48)
            {
                cityCards[i].image = Resources.Load<Sprite>("Citycard_red");
            }
            cityCards[i].transform.parent = transform;
        }

        //Fetches names and ID's to the city cards, from the GameManager.cityNames array
        for (int i = 0; i < cityCards.Length; i++)
        {
            cityCards[i].name = GameManager.cityNames[i];
            cityCards[i].Id = i + 1;
        }

        //Adds the city cards to the card list
        for (int i = 0; i < cityCards.Length; i++)
        {
            //cards[i] = cityCards[i];
            cards.Add(cityCards[i]);

        }

        //Create the event cards and instantiates them
        for (int i = 0; i < eventCards.Length; i++)
        {
            eventCards[i] = new GameObject().AddComponent<_eventCard>();
            eventCards[i].transform.parent = transform;
        }

        //Manually adds names to the events cards...
        eventCards[0].name = "RESILIENT POPULATION";
        eventCards[1].name = "ONE QUIET NIGHT";
        eventCards[2].name = "FORECAST";
        eventCards[3].name = "AIRLIFT";
        eventCards[4].name = "GOVERNMENT GRANT";

        //...and manually adds ID's to the event cards in line with the names
        eventCards[0].Id = 49;
        eventCards[1].Id = 50;
        eventCards[2].Id = 51;
        eventCards[3].Id = 52;
        eventCards[4].Id = 53;

        //Adds the event cards to the card list as well
        for (int i = 0; i < eventCards.Length; i++)
        {
            cards.Add(eventCards[i]);
        }
    }

    //Sorts the list in accordance with the master stack found in the game manaager.
    public void SortCardsToList(SyncListInt sortListInt)
    {
        List<Card> tempCityList = new List<Card>();
        for (int i = 0; i < sortListInt.Count; i++)
        {
            tempCityList.Add(GameManager.AllCardsStack.cards[sortListInt[(i)]-1]);
        }
        cards = tempCityList;
    }

    //Code beyond this point is not used anymore.
    /*
    public static Stack combineStacks(Stack one, Stack two)
    {

        Stack newStack = new Stack();
        for (int i = 0; i < one.cards.Count; i++)
        {
            newStack.cards.Add(one.cards[i]);
        }
        for (int i = 0; i < one.cards.Count; i++)
        {
            newStack.cards.Add(two.cards[i]);
        }
        return newStack;
    }
    */


}

