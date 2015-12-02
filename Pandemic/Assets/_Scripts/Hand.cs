using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Hand : NetworkBehaviour
{
    //public Card[] cards = new Card[3];
    public Card[] cards = new Card[5];
    public GameObject[] CardButtons = new GameObject[7];
    public GameObject cardPrefab;
    GameManager gm;

    public int currentCardValue = -1;

    public Player player;


    public void Initialize(Card[] cards, Player owner)
    {
        gm = GameManager.instance;



        GameObject actionButtons = GameObject.Find("ActionButtons");
        GameObject[] actionButtonChildren = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            actionButtonChildren[i] = actionButtons.transform.GetChild(i).gameObject;
        }
        //move button
        actionButtonChildren[0].GetComponent<Button>().onClick.AddListener(delegate { DelegateMove(currentCardValue); });
        //cure button
        actionButtonChildren[2].GetComponent<Button>().onClick.AddListener(delegateCure);


        //    GameObject CardsOnHand = GameObject.Find("CardsOnHand");
        GameObject CardsOnHand = GameObject.Find("HandArea");

        player = owner;

        if (player.isLocalPlayer)
        {
            //  this.cards = cards;
            for (int i = 0; i < CardButtons.Length; i++)
            {
                CardButtons[i] = CardsOnHand.transform.GetChild(i).gameObject;


                if (i < cards.Length)
                {
                    CardButtons[i].GetComponentInChildren<Text>().text = cards[i].name;
                    CardButtons[i].GetComponent<Image>().sprite = cards[i].image;

                    addToHand(cards[i]);
                    if (cards[i].GetType() == typeof(_cityCard))
                    {
                        int i1 = i;
                        CardButtons[i].GetComponent<Button>().onClick.AddListener(delegate { ChooseCard(i1); });


                    }
                }
                else
                {
                    CardButtons[i].SetActive(false);
                }
            }
        }
        else
        {
            this.cards = cards;
        }
    }

    private void delegateCure()
    {
        if (player.isLocalPlayer)
        {
            int[] currentCardValues = new int[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                currentCardValues[i] = cards[i].Id;
            }
            player.cureDisease(currentCardValues);
            Debug.Log("calls delegate method");
        }

    }

    void DelegateMove(int inputCard)
    {
        if (player.isLocalPlayer)
        {
            if (player.isServer)
            {
                player.Rpc_MoveToCityCard(currentCardValue);
                Debug.Log("currentCardValue " + currentCardValue);
            }
            else
            {
                player.Cmd_MoveToCityCard(currentCardValue);
                Debug.Log("currentCardValue " + currentCardValue);
            }

        }
    }

    void ChooseCard(int inputCard)
    {

        if (player.isLocalPlayer) currentCardValue = cards[inputCard].Id;
    }

    public void addToHand(Card inputCard)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null)
            {
                cards[i] = inputCard;
                Debug.Log("cards added to hand " + cards[i].Id);
                CardButtons[i].SetActive(true);
                CardButtons[i].GetComponentInChildren<Text>().text = inputCard.name;
                break;
            }
        }

    }


    //overloaded method for actionButtons
    //[ClientRpc]
    //[Command]
    public void discard(int cardID)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] is Card)
            {
                if (cardID == cards[i].Id)
                {
                    if (CardButtons[i] != null)
                    {
                        CardButtons[i].SetActive(false);
                    }
                    cards[i] = null;
                    break;
                }
            }
        }
    }

    [Command]
    public void Cmd_discard(int cardID)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] is Card)
            {
                if (cardID == cards[i].Id)
                {
                    if (CardButtons[i] != null)
                    {
                        CardButtons[i].SetActive(false);
                    }
                    cards[i] = null;
                    break;
                }
            }
        }
    }


    public void discardArray(int[] discards)
    {
        //Debug.Log("start discard");
        for (int i = 0; i < discards.Length; i++)
        {
          //Debug.Log("checking " + i + " of discards");
           
            for (int j = 0; j < cards.Length; j++)
            {
               /* Debug.Log("checking " + j + " of cards");
                Debug.Log(cards[j].Id);
                Debug.Log(discards[i]);*/
                if (cards[j] is Card)
                {
                    Debug.Log("YAY");
                    if (cards[j].Id == discards[i])
                    {
                        Debug.Log("WHY WON'T YOU WORK");
                        CardButtons[i].SetActive(false);
                        cards[j] = null;
                    }
                }
            }
        }
    }

    public void discard(Card card)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (card == cards[i])
            {
                GameManager.instance.Cmd_AddToCityDiscardList(card.Id);
                GameManager.infectDiscardStack.SortCardsToList(GameManager.instance.SyncListinfectionDiscardSort);
                CardButtons[i].SetActive(false);
                cards[i] = null;
                break;
            }
        }
    }



}



