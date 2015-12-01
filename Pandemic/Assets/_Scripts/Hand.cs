using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

internal class Hand : MonoBehaviour
{
    public Card[] cards = new Card[7];
    public GameObject[] CardButtons = new GameObject[7];
    public GameObject cardPrefab;
    GameManager gm;

    public Player player;
    public void Initialize(Card[] cards, Player owner)
    {
        gm = GameManager.instance;
        GameObject CardsOnHand = GameObject.Find("CardsOnHand");
        player = owner;

        if (player.isLocalPlayer)
        {
            for (int i = 0; i < CardButtons.Length; i++)
            {
                CardButtons[i] = CardsOnHand.transform.GetChild(i).gameObject;

                if (i < cards.Length)
                {
                    CardButtons[i].GetComponentInChildren<Text>().text = cards[i].name;
                    addToHand(cards[i]);
                    if (cards[i].GetType() == typeof (_cityCard))
                    {
                        int i1 = i;
                        CardButtons[i].GetComponent<Button>().onClick.AddListener(delegate { DelegateMove(i1); });
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

    void DelegateMove(int inputCard)
    {
        if (player.isLocalPlayer)
        {
            player.MoveToCityCard(cards[inputCard]);
        }
    }

    public void addToHand(Card inputCard)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null)
            {
                cards[i] = inputCard;
                GameManager.playerCardStack.RemoveCard(inputCard.Id);
                CardButtons[i].SetActive(true);
                CardButtons[i].GetComponentInChildren<Text>().text = inputCard.name;
                break;
            }
        }

    }

    internal void discard(_cityCard city)
    {
        throw new NotImplementedException();
    }
    public void discard(Card card)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (card == cards[i])
            {
                GameManager.playerDiscardStack.AddCard(card.Id, 0);
                CardButtons[i].SetActive(false);
                cards[i] = null;
                break;
            }
        }
    }
}



