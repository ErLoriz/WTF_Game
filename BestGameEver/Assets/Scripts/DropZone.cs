using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Draggable.Slot tipoCarta;

    public BattleSystem bt = new BattleSystem();
    public static GameObject Jugador;
    public BattleSystem battle;

    public void OnPointerEnter(PointerEventData eventData)
    {

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

    public async void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        switch (GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state)
        {

        case BattleState.PLAYERTURN:

            if (GameObject.FindGameObjectWithTag("Mano").transform.childCount <= 7 && gameObject.name == ("Mano"))
                {
                    if (Jugador.GetComponent<ObjetoJugador>().getOro() >= d.GetComponent<ObjetoCarta>().getCoste() && d.tag == "CartaTienda") // bt.Jugador.GetComponent<ObjetoJugador>().getOro() >= d.GetComponent<ObjetoCarta>().getCoste()
                    {

                        Jugador.GetComponent<ObjetoJugador>().perderOro(d.GetComponent<ObjetoCarta>().getCoste());
                        d.transform.gameObject.tag = "CartaMano";
                        d.parentToReturnTo = this.transform;
                        await ChangeTipeAsync();
                        d.tipoCarta = Draggable.Slot.MANO;

                    }
                    else if (d.tag == "CartaCampo")
                    {
                        d.parentToReturnTo = this.transform;
                        d.transform.gameObject.tag = "CartaMano";
                        await ChangeTipeAsync();

                        d.tipoCarta = Draggable.Slot.MANO;
                    }
                }
                else if (GameObject.FindGameObjectWithTag("Campo").transform.childCount <= 7 && gameObject.name == ("Campo") && d.tag == "CartaMano")
                {
                    if (d != null)
                    {
                        if (tipoCarta != Draggable.Slot.MANO)
                        {
                            d.parentToReturnTo = this.transform;

                            await ChangeTipeAsync();

                            d.transform.gameObject.tag = "CartaCampo";
                            
                        }

                    }
                }
                else if (gameObject.name == ("VentaCartas"))
                {
                    Jugador.GetComponent<ObjetoJugador>().ganarOro(d.GetComponent<ObjetoCarta>().getCoste());
                    await ChangeTipeAsync();
                    Destroy(d.gameObject);
                }

                break;
        case BattleState.ATTACKTURN:
                
                if (gameObject.name == ("VentaCartas") && d.tag == "CartaMano")
                {
                    Jugador.GetComponent<ObjetoJugador>().ganarOro(d.GetComponent<ObjetoCarta>().getCoste());
                    await ChangeTipeAsync();
                    Destroy(d.gameObject);
                }
                else if (gameObject.name == ("CampoEnemigo") && d.tipoCarta == Draggable.Slot.CARTA_ATAQUE && d.GetComponent<ObjetoCarta>().Activa == true)
                {
                    await ChangeTipeAsync();
                    d.GetComponent<ObjetoCarta>().setAtaqueActivo(true);

                    double distancia = 39.3;

                    var posicion = -1;

                    float num1 = d.transform.position.x;
                    float num2 = 0;

                    float left = 0;
                    float right = 0;

                    for (int i = 0; i < gameObject.transform.childCount; i++)
                    {

                        num2 = gameObject.transform.GetChild(i).position.x;
                        left = num2 - (float)distancia;
                        right = num2 + (float)distancia;

                        if (num1 >= left && num1 <= right)
                        {
                            posicion = i;
                            break;
                        }

                    }

                    if (posicion != -1)
                    {
                        GameObject cartaIA = gameObject.transform.GetChild(posicion).gameObject;
                        GameObject cartaPlayer = d.gameObject;

                        //combate MAMAMDISIMO

                        cartaPlayer.GetComponent<ObjetoCarta>().perderVida(cartaIA.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas
                        cartaIA.GetComponent<ObjetoCarta>().perderVida(cartaPlayer.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas



                        //Si alguna de las cartas muere, se eliminan del campo
                        if (cartaPlayer.GetComponent<ObjetoCarta>().getVida() <= 0)
                        {
                            await ChangeTipeAsync();
                            Destroy(cartaPlayer.gameObject);

                        }

                        if (cartaIA.GetComponent<ObjetoCarta>().getVida() <= 0)
                        {
                            Destroy(cartaIA);
                        }
                        else
                        {
                            cartaIA.GetComponent<ObjetoCarta>().setActiva(false);
                        }
                    }
                    else
                    {
                        Debug.Log("No se ha puesto sobre ninguna carta.");
                    }

                }
                else if (gameObject.name == ("HUDEnemigo") && d.tipoCarta == Draggable.Slot.CARTA_ATAQUE && d.GetComponent<ObjetoCarta>().Activa == true && d.GetComponent<ObjetoCarta>().AtaqueActivo == false)
                {
                    await ChangeTipeAsync();
                    GameObject.Find("IA").GetComponent<ObjetoJugador>().perderVida(d.GetComponent<ObjetoCarta>().getAtaque());
                    d.GetComponent<ObjetoCarta>().setAtaqueActivo(true);

                    if (GameObject.Find("IA").GetComponent<ObjetoJugador>().getVida() <= 0)
                    {
                        GameObject.Find("MarcoVictoria").transform.position = new Vector3(GameObject.Find("Canvas").transform.position.x, GameObject.Find("Canvas").transform.position.y, 0);
                       
                    }

                }

                if (GameObject.FindGameObjectWithTag("Mano").transform.childCount <= 7 && gameObject.name == ("Mano") && d.tag == "CartaTienda")
                {
                    if (Jugador.GetComponent<ObjetoJugador>().getOro() >= d.GetComponent<ObjetoCarta>().getCoste() && d.tag == "CartaTienda") // bt.Jugador.GetComponent<ObjetoJugador>().getOro() >= d.GetComponent<ObjetoCarta>().getCoste()
                    {

                        Jugador.GetComponent<ObjetoJugador>().perderOro(d.GetComponent<ObjetoCarta>().getCoste());
                        d.transform.gameObject.tag = "CartaMano";
                        d.parentToReturnTo = this.transform;
                        await ChangeTipeAsync();
                        d.tipoCarta = Draggable.Slot.MANO;

                    }

                }

                break;
        case BattleState.ENEMYTURN:

                if (gameObject.name == ("VentaCartas") && d.tag == "CartaMano")
                {
                    Jugador.GetComponent<ObjetoJugador>().ganarOro(d.GetComponent<ObjetoCarta>().getCoste());
                    await ChangeTipeAsync();
                    Destroy(d.gameObject);
                }
                else if (GameObject.FindGameObjectWithTag("Mano").transform.childCount <= 7 && gameObject.name == ("Mano") && d.tag == "CartaTienda")
                {
                    if (Jugador.GetComponent<ObjetoJugador>().getOro() >= d.GetComponent<ObjetoCarta>().getCoste() && d.tag == "CartaTienda") // bt.Jugador.GetComponent<ObjetoJugador>().getOro() >= d.GetComponent<ObjetoCarta>().getCoste()
                    {

                        Jugador.GetComponent<ObjetoJugador>().perderOro(d.GetComponent<ObjetoCarta>().getCoste());
                        d.transform.gameObject.tag = "CartaMano";
                        d.parentToReturnTo = this.transform;
                        await ChangeTipeAsync();
                        d.tipoCarta = Draggable.Slot.MANO;

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

    public static void obtenerJugador(GameObject jugador)
    {
        Jugador = jugador;
        Debug.LogWarning("Aqui llega: " + Jugador.name);

    }

}
