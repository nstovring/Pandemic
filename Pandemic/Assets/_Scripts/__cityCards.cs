using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class __cityCards : MonoBehaviour {
	
	public List<String> createCityCards () throws IOException{
		
		String Path = "/src/resources/cityCards.txt";
		String filePath = new File("").getAbsolutePath();
		String line = null;  
		List<String> CityCards = new ArrayList<String>();
		
		BufferedReader reader = new BufferedReader(new FileReader(filePath + Path));
		while ((line = reader.readLine()) != null) {
			CityCards.add(line);
			//System.out.println(line); 
		}
		reader.close();
		return CityCards;
	}
	
}

