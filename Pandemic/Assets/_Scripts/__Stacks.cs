using UnityEngine;
using System.Collections;

public class __Stacks : MonoBehaviour {

	public static List<__Cards> cardStack = new ArrayList <__Cards>();
	
	__Stacks () throws IOException{
		cardStack = cards.giveMeCards(__Cards.typeOfCard.INFECTION);
		
	}	
	
	
	public static __Cards cards = new __Cards();
	private static List <__Cards> playerStack = new ArrayList<__Cards>(); //Without the epidemic cards!
	private static List <__Cards> infectionStack = new ArrayList<__Cards>();
	private static List <__Cards> roleStack = new ArrayList<__Cards>();
	
	
	
	public void playerStackCreation () throws IOException{
		playerStack.addAll(cards.giveMeCards(__Cards.typeOfCard.CITY));
		playerStack.addAll(cards.giveMeCards(__Cards.typeOfCard.EVENT));
		Collections.shuffle(playerStack);	
	}
	
	public void infectionStackCreation () throws IOException{
		infectionStack.addAll(cards.giveMeCards(__Cards.typeOfCard.INFECTION));
		Collections.shuffle(infectionStack);	
	}
	
	private void roleStackCreation () throws IOException{
		roleStack.addAll(cards.giveMeCards(__Cards.typeOfCard.ROLE));
		Collections.shuffle(roleStack);	
	}
	
	
	
	
	public static void main(String[] args) throws IOException {
		
		/*Stacks stacks = new Stacks();
		
		stacks.playerStackCreation ();
		stacks.infectionStackCreation ();
		stacks.roleStackCreation(); */
		
		
		/*for (int i = 0; i < roleStack.size(); i++) {
			System.out.println(i+1);
			System.out.println(roleStack.get(i).cardName);
			System.out.println(roleStack.get(i).cardColor);
			System.out.println(roleStack.get(i).cardType);
			System.out.println(" ");
		}*/
		
		
		for (int i = 0; i < infectionStack.size(); i++) {
			System.out.println(i+1);
			System.out.println(infectionStack.get(i).cardName);
			System.out.println(infectionStack.get(i).cardColor);
			System.out.println(infectionStack.get(i).cardType);
			System.out.println(infectionStack.get(i).cityID);
			
			System.out.println(" ");
		}
		
		
		/*for (int i = 0; i < playerStack.size(); i++) {
			System.out.println(i+1);
			System.out.println(playerStack.get(i).cardName);
			System.out.println(playerStack.get(i).cardColor);
			System.out.println(playerStack.get(i).cardType);
			System.out.println(" ");
		}*/
		
		
		
	}
	
}