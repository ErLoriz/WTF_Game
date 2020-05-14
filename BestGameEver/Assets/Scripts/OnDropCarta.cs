using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropCarta : MonoBehaviour, IDropHandler
{

    public static GameObject Jugador;
    public BattleSystem battle;
    
    public async void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        switch (GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state)
        {

            case BattleState.ATTACKTURN:

                if (this.tag == "CartaCampoEn" && d.tipoCarta == Draggable.Slot.CARTA_ATAQUE && d.GetComponent<ObjetoCarta>().Activa == true)
                {
                    await ChangeTipeAsync();

                    d.GetComponent<ObjetoCarta>().setAtaqueActivo(true);
                    GameObject cartaPlayer = d.gameObject;

                    //combate MAMAMDISIMO

                    cartaPlayer.GetComponent<ObjetoCarta>().perderVida(this.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas
                    this.GetComponent<ObjetoCarta>().perderVida(cartaPlayer.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas



                    //Si alguna de las cartas muere, se eliminan del campo
                    if (cartaPlayer.GetComponent<ObjetoCarta>().getVida() <= 0)
                    {
                        await ChangeTipeAsync();
                        Destroy(cartaPlayer.gameObject);

                    }

                    if (this.GetComponent<ObjetoCarta>().getVida() <= 0)
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        this.GetComponent<ObjetoCarta>().setActiva(false);
                    }
                    
                }
                break;
        }


    }

    public void ChangeTipe()
    {

        Thread.Sleep(1);


    }
    async System.Threading.Tasks.Task<bool> ChangeTipeAsync()
    {
        bool bol = true;
        await System.Threading.Tasks.Task.Run(() =>
        {
            ChangeTipe();
        }
        );
        return bol;
    }

}
