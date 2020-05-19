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


        if (tipoCarta == Slot.MANO && this.tag != "CartaCampo" || tipoCarta == Slot.MANO && this.tag == "CartaCampo" && GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state == BattleState.PLAYERTURN || this.tipoCarta == Slot.CARTA_ATAQUE && this.GetComponent<ObjetoCarta>().Activa == true && this.GetComponent<ObjetoCarta>().AtaqueActivo == false)
        {

            placeholder = new GameObject();
            placeholder.transform.SetParent(this.transform.parent);
            float sph_x = (float)0.4;
            this.gameObject.transform.localScale += new Vector3(sph_x, sph_x, 0);
          
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
            
            
            if(this.tag == "CartaMano" || this.tag == "CartaCampo" && GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state == BattleState.PLAYERTURN)
            {
                GameObject.Find("VentaCartas").transform.position = new Vector3(GameObject.Find("HUDPanel").transform.position.x, GameObject.Find("HUDPanel").transform.position.y, GameObject.Find("HUDPanel").transform.position.z);
                Text valor = GameObject.Find("OroVenta").GetComponent<Text>();
                valor.text = Convert.ToString(this.GetComponent<ObjetoCarta>().getCoste());
            }

        }
      
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (tipoCarta == Slot.MANO && this.tag != "CartaCampo" || tipoCarta == Slot.MANO && this.tag == "CartaCampo" && GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state == BattleState.PLAYERTURN || this.tipoCarta == Slot.CARTA_ATAQUE && this.GetComponent<ObjetoCarta>().Activa == true && this.GetComponent<ObjetoCarta>().AtaqueActivo == false)
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

        if (tipoCarta == Slot.MANO && this.tag != "CartaCampo" || tipoCarta == Slot.MANO && this.tag == "CartaCampo" && GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state == BattleState.PLAYERTURN || this.tipoCarta == Slot.CARTA_ATAQUE && this.GetComponent<ObjetoCarta>().Activa == true && this.GetComponent<ObjetoCarta>().AtaqueActivo == false)
        {
          
            float sph_x = (float)0.4;
            this.gameObject.transform.localScale -= new Vector3(sph_x, sph_x, 0);
            this.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            Destroy(placeholder);
            if (this.tag == "CartaMano" || this.tag == "CartaCampo" && GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state == BattleState.PLAYERTURN)
            {
                GameObject.Find("VentaCartas").transform.position = new Vector3(-200, -200, 0);
            }
            
        }
    }

}
