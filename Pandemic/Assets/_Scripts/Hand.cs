using System;

internal class Hand
{
    public Card[] hand;
    GameManager gm;
    void start()
    {
        hand = new Card[7];
        gm = GameManager.instance;
    }
    public void addToHand(Card card)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] = null)
            {
                hand[i] = card;
            }
        }

    }

    internal void discard(_cityCard city)
    {
        throw new NotImplementedException();
    }
    public void discard(Card card)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            if (card = hand[i])
            {
                //gm.playerDiscardStack.add(card);
                hand[i] = null;
                break;

            }
        }

    }
}



