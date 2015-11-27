using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
	

public class Stack : MonoBehaviour {

	public _eventCard [] eventCards = new _eventCard [5];
	public _cityCard [] cityCards = new _cityCard [48];
	public _infectionCard [] infectionCards = new _infectionCard [48];
	public _epidemicCard [] epidemicCards = new _epidemicCard [5];
	public _roleCard [] roleCards = new _roleCard [7];	
	public enum cardType {CITY, INFECTION, EVENT, EPIDEMIC, ROLE};
    public enum roleType { MEDIC, DISPATCHER, QURANTINE_SPECIALIST, CONTINGENCY, RESEARCHER, SCIENTIST, OPERATIONS_EXPERT};


    public Card [] cards;


    // Nikolaj - Purely changes for unity
    public void Initialize(int stackSize, cardType type)
    {
        cards = new Card[stackSize];
        produceCards(type);
    }

    /*public Stack (int stackSize, cardType type){
		cards = new Card [stackSize];
		produceCards (type);
	}*/

    public void produceCards (cardType type){
		switch (type){
		case cardType.CITY:	createCityCards();
		        break;
		case cardType.INFECTION:	 ();
		        break;
		case cardType.ROLE:	createRoleCards();
		        break;
		case cardType.EVENT:	createEventCards();
		        break;
		case cardType.EPIDEMIC:	createEpidemicCards();
		        break;
		default:
			break;
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

    public void ShuffleInfectCards()
    {
        // Knuth shuffle algorithm
        for (int i = 0; i < infectionCards.Length; i++)
        {
            _infectionCard tmp = infectionCards[i];
            int r = UnityEngine.Random.Range(i, infectionCards.Length);
            infectionCards[i] = infectionCards[r];
            infectionCards[r] = tmp;
        }
    }

    public Stack shuffleStack (ref Stack input){

		// Knuth shuffle algorithm
		for (int i = 0; i < input.cards.Length; i++ )
		{
			Card tmp = input.cards[i];
			int r = UnityEngine.Random.Range(i, input.cards.Length);
			input.cards[i] = input.cards[r];
			input.cards[r] = tmp;
		}
		return input;
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

	public void createCityCards (){
		for (int i = 0; i < cityCards.Length; i++){
			//cityCards [i] = new _cityCard();
            cityCards[i] = new GameObject().AddComponent<_cityCard>();

        }
        for (int i = 0; i < cityCards.Length; i++){
			cityCards [i].name = GameManager.cityNames [i];
			cityCards [i].cityID = i + 1;
			//System.out.println(cityCards [i].cityID);
			//System.out.println(cityCards [i].name);
			//System.out.println("");
		}
		for (int i = 0; i < cityCards.Length; i++){
			cards[i] = cityCards [i];
		}
	}

	public void createInfectionCards (){
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

	public void createRoleCards () {
		for (int i = 0; i < roleCards.Length; i++){
			//roleCards [i] = new _roleCard();
            roleCards[i] = new GameObject().AddComponent<_roleCard>();
        }

        roleCards [0].name  = ("MEDIC");
		roleCards [1].name  = ("DISPATCHER");
		roleCards [2].name  = ("QURANTINE SPECIALIST");
		roleCards [3].name  = ("CONTINGENCY");
		roleCards [4].name  = ("RESEARCHER");
		roleCards [5].name  = "SCIENTIST";
		roleCards [6].name  = "OPERATIONS EXPERT";
		
		for (int i = 0; i < roleCards.Length; i++){
			cards[i] = roleCards [i];
		}
		
		for (int i = 0; i < roleCards.Length; i++){
			//System.out.println(roleCards [i].name);
			//System.out.println("");
		}
		
	}

	public void createEventCards (){
		for (int i = 0; i < eventCards.Length; i++){
			eventCards [i] = new _eventCard();
		}
		
		eventCards [0].name  = "RESILIENT POPULATION";
		eventCards [1].name  = "ONE QUIET NIGHT";
		eventCards [2].name  = "FORECAST";
		eventCards [3].name  = "AIRLIFT";
		eventCards [4].name  = "GOVERNMENT GRANT";
		
		for (int i = 0; i < eventCards.Length; i++){
			cards[i] = eventCards [i];
		}

		for (int i = 0; i < eventCards.Length; i++){
			//System.out.println(eventCards[i].name);
			//System.out.println("");
		}
	}

	public void createEpidemicCards (){
		for (int i = 0; i < epidemicCards.Length; i++){
			epidemicCards [i] = new _epidemicCard();
		}
		for (int i = 0; i <  epidemicCards.Length; i++){
			epidemicCards [i].name = "Epidemic";
			//System.out.println(epidemicCards [i].name);
			//System.out.println("");
		}
		for (int i = 0; i < epidemicCards.Length; i++){
			cards[i] = epidemicCards [i];
		}
	}
}
