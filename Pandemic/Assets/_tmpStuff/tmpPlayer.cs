using UnityEngine;
using System.Collections;

public class tmpPlayer : MonoBehaviour {

    public Hand hand;
    public Card role;
    //public City CurrentCity;
    //public int cityID; 
    
    


    public void Start () {
        //role = new GameObject("role").AddComponent<_roleCard>();
        print("Start of Player");
 
        hand = new GameObject("hand").AddComponent<Hand>();
    }

    void Update () {
	    
	}
}
