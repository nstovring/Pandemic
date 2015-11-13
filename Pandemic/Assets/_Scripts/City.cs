
public class City
{

    public int cityId;
    public int[] connectedCityIDs;
    public string color;
    public string name;

    public bool locked;
    public int diseaseSpread;

    public City(int cityId, int[] connectedCityIDs, string color, string name)
    {
        this.cityId = cityId;
        this.connectedCityIDs = connectedCityIDs;
        this.color = color;
        this.name = name;
    }



}
