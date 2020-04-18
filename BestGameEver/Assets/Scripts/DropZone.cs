﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler , IPointerEnterHandler , IPointerExitHandler
{

    public Draggable.Slot tipoCarta = Draggable.Slot.MANO;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");

        if (eventData.pointerDrag == null)
            return;

        if (gameObject.name == ("Campo"))
        {

            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null)
            {
                if (tipoCarta != Draggable.Slot.MANO)
                {
                    d.placeHolderParent = this.transform;
                }

            }

        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");

        if (eventData.pointerDrag == null)
            return;
            


        if (gameObject.name == ("Campo"))
        {
            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null && d.placeHolderParent == this.transform)
            {
                if (tipoCarta != Draggable.Slot.MANO)
                {
                    d.placeHolderParent = this.transform;
                }

            }
        }


    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was droppd on " + gameObject.name);

        if (gameObject.name == ("Campo"))
        {
            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            if(d != null)
            {
                if(tipoCarta != Draggable.Slot.MANO)
                {
                    d.parentToReturnTo = this.transform;
                }

            }
        }



    }

}