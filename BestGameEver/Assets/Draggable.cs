﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Draggable : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler
{

    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;
    GameObject placeholder = null;

    public enum Slot { MANO, CAMPO };
    public Slot tipoCarta = Slot.MANO;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = placeholder.AddComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = placeholder.AddComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //DropZone[] zones = GameObject.FindObjectOfType<DropZone>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeHolderParent)
            placeholder.transform.SetParent(placeHolderParent);

        int newSiblingIndex = placeHolderParent.childCount;

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if(this.transform.position.x < placeHolderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                    break;
                }
            }
            placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        tipoCarta = Slot.CAMPO;

        //EventSystem.current.RaycastAll(eventData);
        Destroy(placeholder);
    }

}
