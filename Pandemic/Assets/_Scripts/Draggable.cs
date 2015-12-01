using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler {
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging");
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
