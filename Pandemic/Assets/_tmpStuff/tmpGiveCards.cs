using UnityEngine;
using System.Collections;

public class tmpGiveCards : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    Stack roleCards;
    Stack playerStack;


    void Start () {

        roleCards = new GameObject("roleCards").AddComponent<Stack>();
        playerStack = new GameObject("playerStack").AddComponent<Stack>();

        roleCards.Initialize(Stack.cardType.ROLE);
        playerStack.Initialize(Stack.cardType.PLAYER_STACK);

        //give roles to the damn players
        player1.GetComponent<Player>().role = (_roleCard) roleCards.cards[0];
        player2.GetComponent<Player>().role = (_roleCard) roleCards.cards[1];


        //give cards to the playerhand
        player1.GetComponent<Player>().hand.cards[0] = playerStack.cards[10];
        player1.GetComponent<Player>().hand.cards[1] = playerStack.cards[20];

        player2.GetComponent<Player>().hand.cards[0] = playerStack.cards[30];
        player2.GetComponent<Player>().hand.cards[1] = playerStack.cards[40];


    }

    // Update is called once per frame
    void Update () {
	
	}
}
