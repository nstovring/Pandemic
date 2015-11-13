using UnityEngine;
using System.Collections;

public class __epidemicCards : MonoBehaviour {

	public List<String> createEpidemicCards() {
		
		List<String> EpidemicCards = new ArrayList<String>();
		
		for (int i = 0; i < 6; i++) {
			EpidemicCards.add ("GREEN_EPIDEMIC");
		}
		
		return EpidemicCards;
	}
	
	
}
