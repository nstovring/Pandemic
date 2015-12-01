using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Stack : MonoBehaviour
{


    public enum cardType
    {
        PLAYER_STACK,
        INFECTION,
        ROLE,
        DISCARD
    };

    public Card[] cards;

    //On creation, create stack of type...
    public void Initialize(cardType type)
    {
        Sprite sprite = Instantiate(Resources.Load("Citycard_blue", typeof(Sprite))) as Sprite;
        produceCards(type);
    }

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

    //Functions you can call
    public void EmptyCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = null;
        }
    }

    public void shuffleStack()
    {
        // Knuth shuffle algorithm
        for (int i = 0; i < cards.Length; i++)
        {
            Card tmp = cards[i];
            int r = UnityEngine.Random.Range(i, cards.Length);
            cards[i] = cards[r];
            cards[r] = tmp;
        }
    }

    public void addEpidemicCards()
    {
        _epidemicCard[] epidemicCards = new _epidemicCard[5];

        for (int i = 0; i < epidemicCards.Length; i++)
        {
            epidemicCards[i] = new GameObject().AddComponent<_epidemicCard>();
        }
        for (int i = 0; i < epidemicCards.Length; i++)
        {
            epidemicCards[i].name = "Epidemic";
            epidemicCards[i].Id = 54 + i;
            //System.out.println(epidemicCards [i].name);
            //System.out.println("");
        }

        int tmpSize = cards.Length;
        Array.Resize(ref cards, cards.Length + 5);

        for (int i = 0; i < epidemicCards.Length; i++)
        {
            cards[i + tmpSize] = epidemicCards[i];
        }

    }

    public void addCard(int cardID)
    {

        Card tmpCard = null;
        for (int i = 0; i < GameManager.AllCardsStack.cards.Length; i++)
        {
            if (GameManager.AllCardsStack.cards[i].Id == cardID)
            {
                tmpCard = GameManager.AllCardsStack.cards[i];
                break;
            }
        }

        int newSize = cards.Length + 1;
        Array.Resize(ref cards, newSize);
        cards[cards.Length] = tmpCard;

    }

    public void removeCard(int cardID)
    {
        Card[] cardstmp = new Card[cards.Length - 1];

        int index = 0;

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].Id == cardID)
            {
                index = i;
                break;
            }
        }

        if (index > 0)
        {
            Array.Copy(cards, 0, cardstmp, 0, index);
        }
        if (index < cards.Length - 1)
        {
            Array.Copy(cards, index + 1, cardstmp, index, cards.Length - index - 1);
        }
        Array.Resize(ref cards, cardstmp.Length);

        for (int i = 0; i < cardstmp.Length; i++)
        {
            cards[i] = cardstmp[i];
        }
    }

    public static Stack combineStacks(Stack one, Stack two)
    {

        Stack newStack = new Stack();
        int newStackLength = one.cards.Length + two.cards.Length;
        newStack.cards = new Card[newStackLength];

        Array.Copy(one.cards, 0, newStack.cards, 0, one.cards.Length);
        Array.Copy(two.cards, 0, newStack.cards, one.cards.Length + 1, two.cards.Length);

        return newStack;
    }

    //Creates cards, remember to call addEpidemicCards function later on the playerStack.
    public void createDiscardStack()
    {
        cards = new Card[0];
    }

    public void createInfectionCards()
    {

        cards = new Card[48];
        _infectionCard[] infectionCards = new _infectionCard[48];

        //Nikolaj - another unity change
        for (int i = 0; i < infectionCards.Length; i++)
        {
            infectionCards[i] = new GameObject().AddComponent<_infectionCard>();
            infectionCards[i].transform.parent = transform;
            //infectionCards [i] = new _infectionCard();
        }
        for (int i = 0; i < infectionCards.Length; i++)
        {
            infectionCards[i].name = GameManager.cityNames[i];
            infectionCards[i].Id = i + 1;
            //System.out.println(infectionCards [i].infectionID);
            //System.out.println(infectionCards [i].name);
            //System.out.println("");
        }

        //cards = infectionCards;

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = infectionCards[i];

        }
    }

    public void createRoleCards()
    {
        _roleCard[] roleCards = new _roleCard[7];
        cards = new Card[7];

        for (int i = 0; i < roleCards.Length; i++)
        {
            //roleCards [i] = new _roleCard();
            roleCards[i] = new GameObject().AddComponent<_roleCard>();
            roleCards[i].transform.parent = transform;

        }

        roleCards[0].name = ("MEDIC");
        roleCards[1].name = ("DISPATCHER");
        roleCards[2].name = ("QURANTINE SPECIALIST");
        roleCards[3].name = ("CONTINGENCY");
        roleCards[4].name = ("RESEARCHER");
        roleCards[5].name = "SCIENTIST";
        roleCards[6].name = "OPERATIONS EXPERT";

        roleCards[0].role = _roleCard.roleType.MEDIC;
        roleCards[1].role = _roleCard.roleType.DISPATCHER;
        roleCards[2].role = _roleCard.roleType.QURANTINE_SPECIALIST;
        roleCards[3].role = _roleCard.roleType.CONTINGENCY;
        roleCards[4].role = _roleCard.roleType.RESEARCHER;
        roleCards[5].role = _roleCard.roleType.SCIENTIST;
        roleCards[6].role = _roleCard.roleType.OPERATIONS_EXPERT;


        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = roleCards[i];
        }



    }

    public void createPlayerStack()
    {
        _cityCard[] cityCards = new _cityCard[48];
        _eventCard[] eventCards = new _eventCard[5];
        cards = new Card[53];



        //Create the city cards
        for (int i = 0; i < cityCards.Length; i++)
        {
            cityCards[i] = new GameObject().AddComponent<_cityCard>();



            Debug.Log(Resources.Load("Citycard_blue"));

            if (i < 12)
            {
                cityCards[i].image = Resources.Load<Sprite>("Citycard_blue");
                Debug.Log("does it run?");
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
        for (int i = 0; i < cityCards.Length; i++)
        {
            cityCards[i].name = GameManager.cityNames[i];
            cityCards[i].Id = i + 1;
        }
        for (int i = 0; i < cityCards.Length; i++)
        {
            cards[i] = cityCards[i];
        }

        //Create the event cards
        for (int i = 0; i < eventCards.Length; i++)
        {
            eventCards[i] = new GameObject().AddComponent<_eventCard>();
            eventCards[i].transform.parent = transform;
        }
        eventCards[0].name = "RESILIENT POPULATION";
        eventCards[1].name = "ONE QUIET NIGHT";
        eventCards[2].name = "FORECAST";
        eventCards[3].name = "AIRLIFT";
        eventCards[4].name = "GOVERNMENT GRANT";

        eventCards[0].Id = 49;
        eventCards[1].Id = 50;
        eventCards[2].Id = 51;
        eventCards[3].Id = 52;
        eventCards[4].Id = 53;

        for (int i = 0; i < eventCards.Length; i++)
        {
            cards[cityCards.Length + i] = eventCards[i];
        }
    }


}

