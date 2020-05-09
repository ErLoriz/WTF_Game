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

        double distanciaLados = 184.665;
        double distanciaAltura = 17.873;

        float n1 = d.transform.position.x;
        float n2 = GameObject.Find("HUDPanel").transform.position.x;

        float num3 = d.transform.position.y;
        float num4 = GameObject.Find("HUDPanel").transform.position.y;

        float arriba = 0;
        float abajo = 0;

        float izq = 0;
        float der = 0;
        
        izq = n2 - (float)distanciaLados;
        der = n2 + (float)distanciaLados;

        abajo = num4 - (float)distanciaAltura;
        arriba = num4 + (float)distanciaAltura;

        if (GameObject.FindGameObjectWithTag("Mano").transform.childCount <= 7 && gameObject.name == ("Mano") && GameObject.FindGameObjectWithTag("Campo").GetComponent<DropZone>().tipoCarta == Draggable.Slot.CAMPO
            || this.tag == "CartaTienda" && GameObject.FindGameObjectWithTag("Campo").GetComponent<DropZone>().tipoCarta == Draggable.Slot.CAMPO 
            || d.tipoCarta == Draggable.Slot.CARTA_ATAQUE && gameObject.name == "Mano")

        {

            if (Jugador.GetComponent<ObjetoJugador>().getOro() >= d.GetComponent<ObjetoCarta>().getCoste() && d.tipoCarta == Draggable.Slot.MANO && d.tag == "CartaTienda") // bt.Jugador.GetComponent<ObjetoJugador>().getOro() >= d.GetComponent<ObjetoCarta>().getCoste()
            {

                Jugador.GetComponent<ObjetoJugador>().perderOro(d.GetComponent<ObjetoCarta>().getCoste());
                d.transform.gameObject.tag = "CartaMano";
                d.parentToReturnTo = this.transform;
                await ChangeTipeAsync();
                d.tipoCarta = Draggable.Slot.MANO;

            } else if(d.tipoCarta == Draggable.Slot.CARTA_ATAQUE)
            {
                d.parentToReturnTo = this.transform;
                d.transform.gameObject.tag = "CartaMano";
                await ChangeTipeAsync();

                d.tipoCarta = Draggable.Slot.MANO;
            }
        }
        else if (GameObject.FindGameObjectWithTag("Campo").transform.childCount <= 7 && gameObject.name == ("Campo") && GameObject.FindGameObjectWithTag("Campo").GetComponent<DropZone>().tipoCarta == Draggable.Slot.CAMPO
            && d.tipoCarta == Draggable.Slot.MANO && d.tag == "CartaMano")
        {

            if (d != null)
            {
                if (tipoCarta != Draggable.Slot.MANO)
                {
                    d.parentToReturnTo = this.transform;

                    await ChangeTipeAsync();

                    d.tipoCarta = Draggable.Slot.CAMPO;
                    d.transform.gameObject.tag = "CartaCampo";
                    d.tipoCarta = Draggable.Slot.CARTA_ATAQUE;


                }

            }

        } else if (gameObject.name == ("VentaCartas"))
        {
            Jugador.GetComponent<ObjetoJugador>().ganarOro(d.GetComponent<ObjetoCarta>().getCoste());
            await ChangeTipeAsync();
            Destroy(d.gameObject);
        }
        else if (gameObject.name == ("CampoEnemigo") && d.tipoCarta == Draggable.Slot.CARTA_ATAQUE)
        {
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

                Debug.Log("Carta puesta sobre:  " + gameObject.transform.GetChild(posicion).name + " con la carta " + d.name);


                Debug.Log(cartaIA.GetComponent<ObjetoCarta>().getAtaque() + " " + cartaPlayer.GetComponent<ObjetoCarta>().getAtaque());

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
