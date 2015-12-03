using UnityEngine;
using System.Collections;

public abstract class Card : MonoBehaviour {

    
    //Each card needs a name
	string name = null;

    //Each card has an id
    public int Id;

    //Each card has an associated sprite
    public Sprite image;

}
