using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WriteCityPositions : MonoBehaviour
{
    public Vector2[] positions = new Vector2[48];
	void Update () {
	    for (int i = 0; i < positions.Length; i++)
	    {
	        positions[i] = transform.GetChild(i).transform.position;
	    }
	}
}
