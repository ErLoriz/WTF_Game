using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Draggable : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler
{

    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;
    GameObject placeholder = null;

    public bool Open = false;

    public enum Slot { MANO, CAMPO, CAMPO_OFF, MANO_ENEMIGO, CAMPO_ENEMIGO, CARTA_ATAQUE };
    public Slot tipoCarta;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
       

        if (tipoCarta == Slot.MANO || this.tipoCarta == Slot.CARTA_ATAQUE)
        {

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

            float num1 = (float) 332.5831;

            
            if(this.tag == "CartaMano" || this.tag == "CartaCampo")
            {
                GameObject.Find("VentaCartas").transform.position = new Vector3(GameObject.Find("HUDPanel").transform.position.x, GameObject.Find("HUDPanel").transform.position.y, GameObject.Find("HUDPanel").transform.position.z);
                Text valor = GameObject.Find("OroVenta").GetComponent<Text>();
                valor.text = Convert.ToString(this.GetComponent<ObjetoCarta>().getCoste());
            }

        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (tipoCarta == Slot.MANO || this.tipoCarta == Slot.CARTA_ATAQUE)
        {
            this.transform.position = eventData.position;
            
            int newSiblingIndex = placeHolderParent.childCount;

            for (int i = 0; i < placeHolderParent.childCount; i++)
            {
                if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (tipoCarta == Slot.MANO || this.tipoCarta == Slot.CARTA_ATAQUE)
        {
            
               
            this.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            Destroy(placeholder);
            if (this.tag == "CartaMano" || this.tag == "CartaCampo")
            {
                GameObject.Find("VentaCartas").transform.position = new Vector3(-200, -200, 0);
            }
            
        }
    }

}
