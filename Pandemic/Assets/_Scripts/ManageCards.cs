using System;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageCards : MonoBehaviour, IPointerEnterHandler
{

    private Button button;
    private Vector3 lerpDown;
    private Vector3 lerpUp;



    public void OnPointerEnter(PointerEventData eventData)
    {

        StartCoroutine("lerpCard");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exits card");
        StartCoroutine("lerpCardDown");
    }



    private IEnumerator lerpCard()
    {
        while (transform.position.y <= lerpUp.y - 1)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, transform.position.y + 25, transform.position.z), 0.1f);
            Debug.Log("lerpUp is: "+ lerpUp.y);
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine("lerpCard");
        
    }

    /*
    IEnumerator lerpCardDown()
    {
        while (transform.position.y >= lerpUp.y + 1)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, transform.position.y - 25, transform.position.z), 0.1f);
            yield return new WaitForEndOfFrame();

            StopCoroutine("lerpCardDown");

            Debug.Log("coroutine not stopped");
        }

    }
    */

    // Use this for initialization
    void Start()
    {
        button = GetComponentInParent<Button>();
        lerpUp = new Vector3(transform.position.x, transform.position.y + 25, transform.position.z);
        lerpDown =  new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Debug.Log(lerpUp.y);

    }

    // Update is called once per frame
    void Update()
    {



    }
}
