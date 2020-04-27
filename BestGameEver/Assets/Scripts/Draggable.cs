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

    public enum Slot { MANO, CAMPO, CAMPO_OFF, MANO_ENEMIGO, CAMPO_ENEMIGO };
    public Slot tipoCarta;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (tipoCarta == Slot.MANO)
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

        }
        
        //DropZone[] zones = GameObject.FindObjectOfType<DropZone>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (tipoCarta == Slot.MANO)
        {
            this.transform.position = eventData.position;

           // if (placeholder.transform.parent != placeHolderParent)
             //   placeholder.transform.SetParent(placeHolderParent);

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

        if (tipoCarta == Slot.MANO)
        {

            //Debug.Log("OnEndDrag");
           
            this.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;
           // EventSystem.current.RaycastAll(eventData);
            Destroy(placeholder);

           

          //  DropZone d = eventData.pointerDrag.GetComponent<DropZone>();

            Debug.Log("Nombre de gameobject........... " + gameObject.name);

          //  if (gameObject.name == ("Campo"))
           // {

               
           // }
           
            // d.tipoCarta = Draggable.Slot.CAMPO;

        }
    }

}
