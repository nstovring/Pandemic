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

        player1.GetComponent<tmpPlayer>().role = (_roleCard) roleCards.cards[0];
        player1.GetComponent<tmpPlayer>().Start();

        player1.GetComponent<tmpPlayer>().hand
        
            
        print("FINAL");



        //player2.GetComponent<tmpPlayer>().role = (_roleCard) roleCards.cards[1];







        //print(player1.GetComponent<tmpPlayer>().hand.cards[0].name);

        //player1.GetComponent<tmpPlayer>().hand.cards[0] = playerStack.cards[10];
        //player1.GetComponent<tmpPlayer>().hand.cards[1] = playerStack.cards[20];

        // player2.GetComponent<Player>().hand.cards[0] = playerStack.cards[30];
        //player2.GetComponent<Player>().hand.cards[1] = playerStack.cards[40];


    }

    // Update is called once per frame
    


}
