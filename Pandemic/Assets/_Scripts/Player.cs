using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Hand hand;
    int cityID;
    _roleCards role;
    GameManager gameManager;
	Player(GameManager gameManager)
    {
		
        hand = new Hand();
        cityID = 1;
        this.gameManager = gameManager;
        MoveToCity(cityID);

        MoveToConnectedCity(cityID);

    }

    public void MoveToConnectedCity(int newID)
    {
        MoveToCity(gameManager.connectedCities[cityID][newID]);
    }
    public void MoveToResearchCity(int ID)
    {
        if (gameManager.GetCityFromID(ID).researchCenter)
        {
            int choose = 0;
            try
            {
                choose = Integer.getInteger(in.readLine());
            }
            catch (IOException e)
            {

                e.printStackTrace();
            }
            MoveToCity(gameManager.researchCenterCities[choose].cityId);
        }
    }
    private void MoveToCityCard(Cards cityCard)
    {
        int ID = cityCard.cityID;
        MoveToCity(ID);
        //hand.discard(cityCard);
    }

    private void MoveToCity(int ID)
    {
        gameManager.GetCityFromID(ID).removePlayer(this);
        gameManager.GetCityFromID(ID).addPlayer(this);
        cityID = ID;
    }

    private void RemoveDiseaseCubes(int cityID)
    {
        if (gameManager.GetCityFromID(cityID).diseaseSpread > 0)
        {
            gameManager.GetCityFromID(cityID).diseaseSpread--;
        }

        //gameManager.GetCityFromID(cityID).RemoveDiseaseCubes(1);

    }
    private void buildResearchCenter(int cityID, cityCard city)
    {
        gameManager.GetCityFromID(cityID).researchCenter = true;
        if (cityID == city.cityID)
        {
            gameManager.GetCityFromID(cityID).researchCenter = true;
            hand.discard(city);
        }

    }
    private void cureDisease()
    {

        if (gameManager.GetCityFromID(cityID).researchCenter)
        {
            //hand.
        }
    }
    private void checkForCure(int counter, cityCard[] hand)
    {
        int[] counters = new int[4];
        for (int i = 0; i < hand.length; i++)
        {
            if (hand[i] != null)
            {
                String colour = gameManager.GetCityFromID(hand[i].cityID).color;
                switch (gameManager.GetCityFromID(hand[i].cityID).color)
                {
                    case "Blue":
                        counters[0]++;
                        break;
                    case "Yellow":
                        counters[1]++;
                        break;
                    case "Black":
                        counters[2]++;
                        break;
                    case "Red":
                        counters[3]++;
                        break;
                }
            }
        }

    }

}
}
