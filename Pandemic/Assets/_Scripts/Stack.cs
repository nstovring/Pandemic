using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
	

public class Stack : MonoBehaviour {

<<<<<<< HEAD
	public enum cardType {PLAYER_STACK, INFECTION, ROLE};
=======
	public _eventCard [] eventCards = new _eventCard [5];
	public _cityCard [] cityCards = new _cityCard [48];
	public _infectionCard [] infectionCards = new _infectionCard [48];
	public _epidemicCard [] epidemicCards = new _epidemicCard [5];
	public _roleCard [] roleCards = new _roleCard [7];	
	public enum cardType {CITY, INFECTION, EVENT, EPIDEMIC, ROLE};

>>>>>>> origin/master

    public Card [] cards;

    // Nikolaj - Purely changes for unity
    public void Initialize(cardType type)
    {
        produceCards(type);
    }


    public void produceCards (cardType type){
		switch (type){
		case cardType.PLAYER_STACK:	createPlayerStack();
		        break;
		case cardType.INFECTION: createInfectionCards ();
		        break;
		case cardType.ROLE:	createRoleCards();
		        break;
		default:
			break;
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

    public Stack combineStacks (Stack one, Stack two){

		Stack newStack = new Stack ();
		int newStackLength = one.cards.Length + two.cards.Length;
		newStack.cards = new Card[newStackLength];

		Array.Copy (one.cards, 0, newStack.cards, 0, one.cards.Length); 
		Array.Copy (two.cards, 0, newStack.cards, one.cards.Length+1, two.cards.Length);

		return newStack;
	}

	public Stack removeCard (Stack input, String cardName){

		Stack output = new Stack ();
		output.cards = new Card[input.cards.Length - 1];

		int index = 0;

		for (int i = 0; i < input.cards.Length; i++) {
			if (input.cards[i].name == cardName){
				index = i;
				break;
			}
		}

		if (index > 0) {
			Array.Copy (input.cards, 0, output.cards, 0, index);
		}
		if (index < input.cards.Length - 1) {
			Array.Copy (input.cards, index+1, output.cards, index, input.cards.Length - index - 1);

		}
		return output;
	}

	public Stack addCard (Stack source, Stack target, String cardName){

		int index = 0;

		for (int i = 0; i < source.cards.Length; i++) {
			if (source.cards[i].name == cardName){
				index = i;
				break;
			}
		}

		int newSize = target.cards.Length + 1;

		Array.Resize (ref target.cards, newSize);
		target.cards [target.cards.Length] = source.cards [index];

		return target;
	}

	public void createInfectionCards (){

        cards = new Card[48];

        _infectionCard[] infectionCards = new _infectionCard[48];
        
        //Nikolaj - another unity change
		for (int i = 0; i < infectionCards.Length; i++){
            infectionCards[i] = new GameObject().AddComponent<_infectionCard>();
            //infectionCards [i] = new _infectionCard();
        }
		for (int i = 0; i < infectionCards.Length; i++){
			infectionCards [i].name = GameManager.cityNames [i];
			infectionCards [i].infectionID = i + 1;
			//System.out.println(infectionCards [i].infectionID);
			//System.out.println(infectionCards [i].name);
			//System.out.println("");
		}
		for (int i = 0; i < infectionCards.Length; i++){
			cards[i] = infectionCards [i];
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
        }

<<<<<<< HEAD
        roleCards[0].name = ("MEDIC");
        roleCards[1].name = ("DISPATCHER");
        roleCards[2].name = ("QURANTINE SPECIALIST");
        roleCards[3].name = ("CONTINGENCY");
        roleCards[4].name = ("RESEARCHER");
        roleCards[5].name = "SCIENTIST";
        roleCards[6].name = "OPERATIONS EXPERT";
=======
        roleCards [0].name  = ("MEDIC");
		roleCards [1].name  = ("DISPATCHER");
		roleCards [2].name  = ("QURANTINE SPECIALIST");
		roleCards [3].name  = ("CONTINGENCY");
		roleCards [4].name  = ("RESEARCHER");
		roleCards [5].name  = "SCIENTIST";
		roleCards [6].name  = "OPERATIONS EXPERT";

        roleCards[0].role = _roleCard.roleType.MEDIC;
        roleCards[1].role = _roleCard.roleType.DISPATCHER;
        roleCards[2].role = _roleCard.roleType.QURANTINE_SPECIALIST;
        roleCards[3].role = _roleCard.roleType.CONTINGENCY;
        roleCards[4].role = _roleCard.roleType.RESEARCHER;
        roleCards[5].role = _roleCard.roleType.SCIENTIST;
        roleCards[6].role = _roleCard.roleType.OPERATIONS_EXPERT;


        for (int i = 0; i < roleCards.Length; i++){
			cards[i] = roleCards [i];
		}
		
		for (int i = 0; i < roleCards.Length; i++){
			//System.out.println(roleCards [i].name);
			//System.out.println("");
		}
		
	}
>>>>>>> origin/master

        for (int i = 0; i < roleCards.Length; i++)
        {
            cards[i] = roleCards[i];
        }

        for (int i = 0; i < roleCards.Length; i++)
        {
            //System.out.println(roleCards [i].name);
            //System.out.println("");
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
        }
        for (int i = 0; i < cityCards.Length; i++)
        {
            cityCards[i].name = GameManager.cityNames[i];
            cityCards[i].cityID = i + 1;
        }
        for (int i = 0; i < cityCards.Length; i++)
        {
            cards[i] = cityCards[i];
        }

        //Create the event cards
        for (int i = 0; i < eventCards.Length; i++)
        {
            eventCards[i] = new _eventCard();
        }
        eventCards[0].name = "RESILIENT POPULATION";
        eventCards[1].name = "ONE QUIET NIGHT";
        eventCards[2].name = "FORECAST";
        eventCards[3].name = "AIRLIFT";
        eventCards[4].name = "GOVERNMENT GRANT";

        for (int i = 0; i < eventCards.Length; i++)
        {
            cards[cityCards.Length + i] = eventCards[i];
        }
    }

    public void addEpidemicCards()
    {
        _epidemicCard[] epidemicCards = new _epidemicCard[5];

        for (int i = 0; i < epidemicCards.Length; i++)
        {
            epidemicCards[i] = new _epidemicCard();
        }
        for (int i = 0; i < epidemicCards.Length; i++)
        {
            epidemicCards[i].name = "Epidemic";
            //System.out.println(epidemicCards [i].name);
            //System.out.println("");
        }

        int tmpSize = cards.Length;
        Array.Resize(ref cards, cards.Length + 5);

        for (int i = 0; i < epidemicCards.Length; i++) {
            cards[i+ tmpSize] = epidemicCards[i];
        }

    }


}
