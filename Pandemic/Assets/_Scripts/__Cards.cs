using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class __Cards : MonoBehaviour {

	public int cityID;
	public String cardName;
	public String cardColor;
	public String cardType;
	
	private static List<String> citycards = new ArrayList<>();
	private static List<String> rolecards = new ArrayList<>();
	private static List<String> eventcards = new ArrayList<>();
	private static List<String> epidemiccards = new ArrayList<>();
	
	//Fetches the info from the composite card classes and adds them to the arrays above
	private static void setCardInfo () throws IOException{
		__cityCards city = new __cityCards();
		__roleCards role = new __roleCards();
		__eventCards event = new __eventCards();
		__epidemicCards epidemic = new __epidemicCards();
		
		citycards = city.createCityCards();
		rolecards = role.createRoleCards();	
		eventcards = event.createEventCards();
		epidemiccards = epidemic.createEpidemicCards();
	}
	
	//When called, generates card objects based on their parameters
	private static List<__Cards> produceCards(List<String> card, String type){	
		List <__Cards> cards = new ArrayList<__Cards>();
		
		for (int i = 0; i < card.size(); i++){
			cards.add (new __Cards());
			String string = card.get(i);
			String[] parts = string.split("_");
			String Color = parts[0];
			String Name = parts[1];
			
			cards.get(i).cityID = i+1;
			cards.get(i).cardName = Name;
			cards.get(i).cardColor = "Color: " + Color;
			cards.get(i).cardType = "Type: " + type;
		}
		return cards;
	}
	
	//Enum for type of card. You put in the type and it gives you the cards you want. Easy pie.
	public enum typeOfCard {CITY, INFECTION, ROLE, EVENT, EPIDEMIC};	
	public List<__Cards> giveMeCards (typeOfCard type) throws IOException{
		setCardInfo();
		switch (type)
		{
		case CITY:     	return produceCards(citycards, "City");
		case INFECTION:	return produceCards(citycards, "Infection");
		case ROLE:		return produceCards(rolecards, "Role");
		case EVENT:		return produceCards(eventcards, "Event");
		case EPIDEMIC:	return produceCards(epidemiccards, "Epidemic");
		default:      	return null;
		}
	}
	
	
	
	//Delete Later
	public static void main(String[] args) throws IOException {
		
		__Cards temp = new __Cards();
		
		setCardInfo();
		@SuppressWarnings("unused")
			List<__Cards> tmp = temp.giveMeCards (typeOfCard.INFECTION);
		
		/*for (int i = 0; i < tmp.size(); i++) {
			System.out.println(i+1);
			System.out.println(tmp.get(i).cardName);
			System.out.println(tmp.get(i).cardColor);
			System.out.println(tmp.get(i).cardType);
			System.out.println(" ");
		}	*/
		
		
	}
	
}
