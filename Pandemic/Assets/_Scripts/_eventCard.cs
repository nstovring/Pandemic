using UnityEngine;
using System.Collections;



public class _eventCard : Card{
	private int ID;
	Player player;
	GameManager gm;

	public void initialize(){
		gm = GameManager.instance;
		player = new Player ();
	}


	public void Airlift (int city_id, int player_id){

		gm.players[player_id].MoveToCity (city_id);
		player.hand.discard (this);

	}

	public void OneQuietNight (){
		
		
		
	}

	public void Forecast (){
		gm.infectCardStack;
		Cards[] cards = new Cards[6];
		int a = 0;

		for(int i = gm.infectCardStack.infectionCards.length -1; i > 0; i--){

		if(gm.infectCardStack.infectionCards[i]!= null){
			if(a<6){
					cards[a]= gm.infectCardStack.infectionCards[i];
					a++;
					gm.infectCardStack.infectionCards[i]= null;	
			}
		}
	
	}
	
	public cards[] swap(Cards[] cards, int index1, int index2){
			cards temp = Cards[index1];
			cards[index1]=cards[index2];
			cards[index2]=temp;

		}



	public void GovermentGrant (int CityId){
			Gm.GetCityFromID(CityId).researchCenter = true;
		
	}

	public void ResilientPopulation(int index){
			gm.infectDiscardStack.infectionCards[index]= null;
		
	}
}


}
