using System;
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

    //ComboPanel
    public GameObject[] ComboPanel;
    public GameObject ComboPanelPlayer;
    public GameObject ComboPanelIA;

    //Contador combos
    public int Aire;
    public int Agua;
    public int Tierra;
    public int Fuego;
    public int Luz;
    public int Oscuridad;

    public int Protector;
    public int Guerrero;
    public int Asesino;
    public int Mago;
    public int Pirata;
    public int Deidad;

    public GameObject[] PlayersArray;
    public GameObject IA;

    //Habilidades de combos

    //Ataque primero
    bool AtaquePrimeroPlayer = false;

    //Arrollar
    bool ArrollarPlayer2 = false;
    bool ArrollarPlayer4 = false;

    //Ira
    bool IraPlayer = false;
    bool IraPlayer2 = false;

    //Curacion
    bool Curacion1 = false;
    bool Curacion2 = false;

    //Asesinos
    bool Asesino2IA = false;
    bool AsesinoIA = false;

    //Guerrero 
    bool Protector1IA = false;
    bool Protector2IA = false;

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
                        Debug.Log("Aqui vuelven");
                        combos(GameObject.FindGameObjectsWithTag("CartaCampo"));
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

                            Debug.Log("hola buenas tarde aqui se actualiza al tierar las cartas al campo");
                            combos(GameObject.FindGameObjectsWithTag("CartaCampo"));


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
               /* else if (gameObject.name == ("CampoEnemigo") && d.tipoCarta == Draggable.Slot.CARTA_ATAQUE && d.GetComponent<ObjetoCarta>().Activa == true)
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

                    if (posicion != -1) //Aqui inicia el combate
                    {
                        GameObject cartaIA = gameObject.transform.GetChild(posicion).gameObject;
                        GameObject cartaPlayer = d.gameObject;

                        //combate MAMAMDISIMO

                        //Cargar ComboPanel
                        ComboPanel = GameObject.FindGameObjectsWithTag("ComboPanel");
                        ComboPanelPlayer = ComboPanel[0];

                        //Cargar IA
                        PlayersArray = GameObject.FindGameObjectsWithTag("IA");
                        IA = PlayersArray[0];

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
                */else if (gameObject.name == ("HUDEnemigo") && d.tipoCarta == Draggable.Slot.CARTA_ATAQUE && d.GetComponent<ObjetoCarta>().Activa == true && d.GetComponent<ObjetoCarta>().AtaqueActivo == false)
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

    void combos(GameObject[] cartasArray)
    {
        Aire = 0; Agua = 0; Tierra = 0; Fuego = 0; Luz = 0; Oscuridad = 0;

        Protector = 0; Guerrero = 0; Asesino = 0; Mago = 0; Pirata = 0; Deidad = 0;

        String Elemento = "";
        String Clase = "";


        for (int i = 0; i < cartasArray.Length; i++)
        {
            Elemento = cartasArray[i].GetComponent<ObjetoCarta>().getElemento().ToString();
            Clase = cartasArray[i].GetComponent<ObjetoCarta>().getClase().ToString();

            if (Elemento.Equals("Aire"))
            {
                ++Aire;
            }
            else if (Elemento.Equals("Agua"))
            {
                ++Agua;
            }
            else if (Elemento.Equals("Tierra"))
            {
                ++Tierra;
            }
            else if (Elemento.Equals("Fuego"))
            {
                ++Fuego;
            }
            else if (Elemento.Equals("Luz"))
            {
                ++Luz;
            }
            else if (Elemento.Equals("Oscuridad"))
            {
                ++Oscuridad;
            }

            if (Clase.Equals("Protector"))
            {
                ++Protector;
            }
            else if (Clase.Equals("Asesino"))
            {
                ++Asesino;
            }
            else if (Clase.Equals("Guerrero"))
            {
                ++Guerrero;
            }
            else if (Clase.Equals("Mago"))
            {
                ++Mago;
            }
            else if (Clase.Equals("Pirata"))
            {
                ++Pirata;
            }
            else if (Clase.Equals("Deidad"))
            {
                ++Deidad;
            }
        }
        //Contadores listos


        //Aire
        if (Aire >= 6)
        {
            AtaquePrimeroPlayer = true;
        }
        else if (Aire >= 3)
        {
            AtaquePrimeroPlayer = true;

        }


        //Agua
        if (Agua >= 6) //6- Cura 3 vida a todas las cartas del campo y jugador
        {
            IA.GetComponent<ObjetoJugador>().ganarVida(3);
            for (int i = 0; i < cartasArray.Length; i++)
            {

                cartasArray[i].GetComponent<ObjetoCarta>().ganarVida(3);
                if (cartasArray[i].GetComponent<ObjetoCarta>().getVida() > cartasArray[i].GetComponent<ObjetoCarta>().getVidaDef())
                {
                    cartasArray[i].GetComponent<ObjetoCarta>().setVida(cartasArray[i].GetComponent<ObjetoCarta>().getVidaDef());
                    cartasArray[i].GetComponent<ObjetoCarta>().updateStatis();

                }

            }

        }
        else if (Agua >= 3)
        {

            for (int i = 0; i < cartasArray.Length; i++)
            {

                cartasArray[i].GetComponent<ObjetoCarta>().ganarVida(1);

                if (cartasArray[i].GetComponent<ObjetoCarta>().getVida() > cartasArray[i].GetComponent<ObjetoCarta>().getVidaDef())
                {
                    cartasArray[i].GetComponent<ObjetoCarta>().setVida(cartasArray[i].GetComponent<ObjetoCarta>().getVidaDef());
                    cartasArray[i].GetComponent<ObjetoCarta>().updateStatis();

                }

            }
        }

        //Tierra
        if (Tierra >= 4)
        {
            ArrollarPlayer4 = true;
            //Activar Luz Imagen
          //  imagenActiva.SetActive(true);
        }
        else if (Tierra >= 2)
        {
            ArrollarPlayer2 = true;
            //Activar Luz Imagen
            //  imagenActiva.SetActive(true);
        }
        else
        {
            // imagenActiva.SetActive(false);
        }

        //Fuego
        if (Fuego >= 6)
        {
            IraPlayer = true;
        }
        else if (Fuego >= 3)
        {
            IraPlayer = true;

        }

        //Clases


        //Asesinos (Ataca siempre al jugador)
        if (Asesino <= 4)
        {
            Asesino2IA = true;
        }
        else if (Asesino <= 2)
        {
            AsesinoIA = true;

        }

        //Asesinos (Ataca siempre al jugador)
        if (Guerrero == 4)
        {
            Protector2IA = true;
        }
        else if (Asesino == 1)
        {
            Protector1IA = true;

        }

        Debug.Log("Asesino1:" + AsesinoIA);
        Debug.Log("Asesino2:" + Asesino2IA);

        ComboPanel = GameObject.FindGameObjectsWithTag("ComboPanel");
        ComboPanelPlayer = ComboPanel[0];

        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setAire(Aire);
        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setAgua(Agua);
        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setTierra(Tierra);
        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setFuego(Fuego);
        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setProtector(Protector);
        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setGuerrero(Guerrero);
        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setAsesino(Asesino);
        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setMago(Mago);
        ComboPanelPlayer.GetComponent<ComboPanelObjeto>().setPirata(Pirata);
    }

    void Ira(GameObject cartaIASelect, GameObject cartaJugadorSelect)
    {
        if (IraPlayer2 == true)
        {
            cartaIASelect.GetComponent<ObjetoCarta>().setAtaque(cartaIASelect.GetComponent<ObjetoCarta>().getAtaque() + cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque() * 3);

        }
        else
        {
            cartaIASelect.GetComponent<ObjetoCarta>().setAtaque(cartaIASelect.GetComponent<ObjetoCarta>().getAtaque() + cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque());

        }
    }

    void AtaquePrimero(GameObject cartaIASelect, GameObject cartaJugadorSelect, int vidaPrevciaAlCombate)
    {
        cartaJugadorSelect.GetComponent<ObjetoCarta>().setVida(cartaJugadorSelect.GetComponent<ObjetoCarta>().getVida() - cartaIASelect.GetComponent<ObjetoCarta>().getAtaque());

        if (cartaIASelect.GetComponent<ObjetoCarta>().getAtaque() < vidaPrevciaAlCombate)
        {
            cartaIASelect.GetComponent<ObjetoCarta>().perderVida(cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas

        }

    }

    void Proteccion(GameObject cartaIASelect, GameObject cartaJugadorSelect) //Aqui entran las cartas de tipo protector
    {
        if (Protector2IA == true)
        {
            double vidaDecimal = cartaIASelect.GetComponent<ObjetoCarta>().getVida() - (cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque() * 0.6);
            cartaIASelect.GetComponent<ObjetoCarta>().setVida((int)vidaDecimal);
            cartaJugadorSelect.GetComponent<ObjetoCarta>().setVida(cartaJugadorSelect.GetComponent<ObjetoCarta>().getVida() - cartaIASelect.GetComponent<ObjetoCarta>().getAtaque());

        }
        else if (Protector1IA == true)
        {
            double vidaDecimal = cartaIASelect.GetComponent<ObjetoCarta>().getVida() - (cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque() * 0.3);
            cartaIASelect.GetComponent<ObjetoCarta>().setVida((int)vidaDecimal);
            cartaJugadorSelect.GetComponent<ObjetoCarta>().setVida(cartaJugadorSelect.GetComponent<ObjetoCarta>().getVida() - cartaIASelect.GetComponent<ObjetoCarta>().getAtaque());
        }
        else
        {

            cartaIASelect.GetComponent<ObjetoCarta>().setVida(cartaIASelect.GetComponent<ObjetoCarta>().getVida() - cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque());
            cartaJugadorSelect.GetComponent<ObjetoCarta>().setVida(cartaJugadorSelect.GetComponent<ObjetoCarta>().getVida() - cartaIASelect.GetComponent<ObjetoCarta>().getAtaque());
        }
    }

}
