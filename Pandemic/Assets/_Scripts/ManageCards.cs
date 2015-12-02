using System;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Button button;
    private Vector3 lerpDown;
    private Vector3 lerpUp;

    public bool selected = false;



    public void OnPointerEnter(PointerEventData eventData)
    {

        StartCoroutine("lerpCard");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      
        StartCoroutine("lerpCardDown");
    }



    private IEnumerator lerpCard()
    {
        while (transform.position.y <= lerpUp.y)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, transform.position.y + 25, transform.position.z), 0.1f);


            yield return new WaitForEndOfFrame();
        }
        StopCoroutine("lerpCard");
        
    }

    
    IEnumerator lerpCardDown()
    {
        while (transform.position.y >= lerpDown.y)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, transform.position.y - 25, transform.position.z), 0.1f);

            yield return new WaitForEndOfFrame();

        }
        StopCoroutine("lerpCardDown");
    }
    

    // Use this for initialization
    void Start()
    {
        button = GetComponentInParent<Button>();
        lerpDown = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        lerpUp = new Vector3(transform.position.x, transform.position.y + 25, transform.position.z);
        
      

    }

    // Update is called once per frame
    void Update()
    {



    }
}
