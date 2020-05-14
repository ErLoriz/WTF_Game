using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
using UnityEngine.Animations;

public enum BattleState { START, PLAYERTURN, ATTACKTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    // Thread tt = new Thread(ThreadTiempo.ThreadTemp);



    public bool situacionCompatible;
    public int CartasManoCant;
    public GameObject[] PlayersArray;
    public GameObject Carta;
    public GameObject CartaEnemiga;
    public GameObject[] CartasArray;
    public Button botonPasar;

    //ComboPanel
    public GameObject[] ComboPanel;
    public GameObject ComboPanelPlayer;
    public GameObject ComboPanelIA;

    public Text TextoTurno;

    public Canvas manoEnemiga;

    public BattleState state;

    int time = 0;

    public ObjetoCarta.Elemento tipoElemento;

    public Draggable.Slot tipoCarta;
    public DropZone zone;

    public int CartaAleatoria;

    //Jugadores
    public GameObject Jugador;
    public GameObject IA;

    //cartas
    static GameObject Carta0;
    private GameObject Carta1;
    private GameObject Carta2;
    private GameObject Carta3;

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



    //Habilidades de combos

    //Ataque primero
    bool AtaquePrimeroIA = false;
    bool AtaquePrimeroPlayer = false;

    //Arrollar
    bool ArrollarIA4 = false;
    bool ArrollarJugador2 = false;
    bool ArrollarIA2 = false;
    bool ArrollarJugador4 = false;

    //Ira
    bool IraIA = false;
    bool IraIA2 = false;
    bool IraPlayer = false;
    bool IraPlayer2 = false;

    //Asesinos
    bool Asesino2IA = false;
    bool AsesinoIA = false;

    //Guerrero 
    bool Protector1IA = false;
    bool Protector2IA = false;

    //Luces
    GameObject imagenActiva;



    // Start is called before the first frame update
    void Start()
    {
        zone = GameObject.FindGameObjectWithTag("Campo").GetComponent<DropZone>();

        //Luces
        //imagenActiva = GameObject.Find("TierraImagenL").gameObject;
        //imagenActiva.SetActive(false);

        //Cargar Jugador
        PlayersArray = GameObject.FindGameObjectsWithTag("Jugador");
        Jugador = PlayersArray[0];

        //Cargar ComboPanel
        ComboPanel = GameObject.FindGameObjectsWithTag("ComboPanelIA");
        ComboPanelIA = ComboPanel[0];

        //Cargar IA
        PlayersArray = GameObject.FindGameObjectsWithTag("IA");
        IA = PlayersArray[0];

        DropZone.obtenerJugador(Jugador);

        //Iniciar Partida
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        //Saldra una pantalla con 1 carta a elegir

        var number = UnityEngine.Random.Range(1, 3);

        TextoTurno.text = "Turno " + number.ToString();

        if (number == 1)
        {

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {

            state = BattleState.ENEMYTURN;
            EnemyTurn();
        }
        //Debug.Log("Hay " + manoEnemiga.FindGameObjectsWithTag("Carta0").Length);


    }

    public GameObject getJugador()
    {
        Jugador.GetComponent<ObjetoJugador>().getOro();
        return Jugador;
    }

    public void PlayerTurn()
    {
        OpenPanel_Cartas panel = GameObject.Find("ButtonCartas").GetComponent<OpenPanel_Cartas>();
        panel.forceOpen();


        CartasArray = GameObject.FindGameObjectsWithTag("CartaCampo");

        for (int i = 0; i < CartasArray.Length; i++)
        {

            CartasArray[i].GetComponent<ObjetoCarta>().setActiva(true);
            CartasArray[i].GetComponent<ObjetoCarta>().setAtaqueActivo(false);

        }

        TiendaCartas tienda = GameObject.Find("ButtonReroll").GetComponent<TiendaCartas>();

        if (GameObject.FindGameObjectWithTag("Tienda").activeInHierarchy)
            tienda.rerollTurno();

        GameObject.FindGameObjectWithTag("Mano").GetComponent<DropZone>().tipoCarta = Draggable.Slot.MANO;
        zone.tipoCarta = Draggable.Slot.CAMPO;
        botonPasar.enabled = true;
        TextoTurno.text = "Tu turno";
        Jugador.GetComponent<ObjetoJugador>().ganarOro(5);

        CartasArray = GameObject.FindGameObjectsWithTag("CartaCampoEn");

        for (int i = 0; i < CartasArray.Length; i++)
        {
            CartasArray[i].GetComponent<ObjetoCarta>().setActiva(true);
        }

    }

    public void AttackTurn()
    {

        CartasArray = GameObject.FindGameObjectsWithTag("CartaCampo");

        for (int i = 0; i < CartasArray.Length; i++)
        {
            GameObject.Find("Campo").GetComponent<DropZone>().transform.GetChild(i).GetComponent<Draggable>().tipoCarta = Draggable.Slot.CARTA_ATAQUE;

        }

        TextoTurno.text = "Ataque";
    }

    public void OnClickButtonTurn()
    {

        if (state == BattleState.PLAYERTURN)
        {

            CartasArray = GameObject.FindGameObjectsWithTag("CartaCampo");
            int cont = 0;
            for (int i = 0; i < CartasArray.Length; i++)
            {

                if (CartasArray[i].GetComponent<ObjetoCarta>().Activa == true)
                {
                    cont = 1;
                    break;
                }
                

            }

            if (cont == 1)
            {
                state = BattleState.ATTACKTURN;
                AttackTurn();
            } else
            {
                state = BattleState.ENEMYTURN;
                EnemyTurn();
            }

            
        }
        else if (state == BattleState.ATTACKTURN)
        {
            state = BattleState.ENEMYTURN;
            EnemyTurn();
        }

    }


    async void EnemyTurn()
    {
        //turnCounter++;

        CartasArray = GameObject.FindGameObjectsWithTag("CartasManoEn");
        
       
        botonPasar.enabled = false;
        TextoTurno.text = "Enemigo";

        IA.GetComponent<ObjetoJugador>().ganarOro(5);

        time = UnityEngine.Random.Range(300, 1000);
        await TimeForAIAsync(time);

     

        CartasArray = GameObject.FindGameObjectsWithTag("CartaCampoEn");

        for (int i = 0; i < CartasArray.Length; i++)
        {

            CartasArray[i].GetComponent<ObjetoCarta>().setActiva(true);

        }


        TurnBuy();
        time = UnityEngine.Random.Range(800,1000);
        await TimeForAIAsync(time);
        TurnCards();
        time = UnityEngine.Random.Range(4000, 10000);
        await TimeForAIAsync(time);
        TextoTurno.text = "Enem.F2";

        TurnBatle();
    
 
    }

    GameObject ObtenccionCartasIA()
    {
        GameObject CartaX = null;

        CartaAleatoria = UnityEngine.Random.Range(1, 18);

        switch (CartaAleatoria)
        {
            case 1:
                CartaX = Resources.Load("01_AireAsesino") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 2:
                CartaX = Resources.Load("03_AireProtector") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 3:
                CartaX = Resources.Load("04_GiganteMarino") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 4:
                CartaX = Resources.Load("11_TierraGuerrero") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 5:
                CartaX = Resources.Load("16_FuegoGuerrero") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 6:
                CartaX = Resources.Load("05_VampiroMarino") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 7:
                CartaX = Resources.Load("06_AguaGuerrero") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 8:
                CartaX = Resources.Load("10_SegadorDelBosque") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 9:
                CartaX = Resources.Load("13_TierraPirata") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 10:
                CartaX = Resources.Load("17_FuegoProtector") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 11:
                CartaX = Resources.Load("02_AireGuerrero") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 12:
                CartaX = Resources.Load("07_AguaProtector") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 13:
                CartaX = Resources.Load("09_TierraMago") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 14:
                CartaX = Resources.Load("15_AlmaPerdida") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 15:
                CartaX = Resources.Load("00_AngelSagrado") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 16:
                CartaX = Resources.Load("08_PirataGalactico") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 17:
                CartaX = Resources.Load("12_TierraProtector") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
            case 18:
                CartaX = Resources.Load("14_Incinerador") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
                break;
        }

        Debug.Log("Carta en tienda IA: " + CartaX.name);
        CartaX.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO_ENEMIGO;

        return CartaX;
    }

    void TurnBuy()//Compra
    { 

        //Aqui obtiene 4 cartas aleatorias
        Carta0 = ObtenccionCartasIA();
        Carta1 = ObtenccionCartasIA();
        Carta2 = ObtenccionCartasIA();
        Carta3 = ObtenccionCartasIA();

        //Por ahora cojera solo la primera de forma predeterminada
        if(IA.GetComponent<ObjetoJugador>().getOro() >= Carta0.GetComponent<ObjetoCarta>().getCoste())
        {
            IA.GetComponent<ObjetoJugador>().perderOro(Carta0.GetComponent<ObjetoCarta>().getCoste());
            Carta0.transform.gameObject.tag = "CartasManoEn";
            Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("ManoEnemigo").transform);
        }
      


    }

    async void TurnCards()//Mueve cartas de la mano al campo
    {
        GameObject[] CartasArrayEnCampo;
        GameObject[] CartasArrayEnMano;

        int maximaCartasCampo = 7;



        var modoIA = UnityEngine.Random.Range(0, 2); //Dos modos, uno conservador y otro agresivo

        var tipoRamdonSelect = UnityEngine.Random.Range(0, 3); //Elije su tipoMain

       // tipoRamdonSelect = 0;

        if (tipoRamdonSelect == 0)
        {
            tipoElemento = ObjetoCarta.Elemento.Aire;
        }
        else if (tipoRamdonSelect == 1)
        {
            tipoElemento = ObjetoCarta.Elemento.Agua;
        }
        else if (tipoRamdonSelect == 2)
        {
            tipoElemento = ObjetoCarta.Elemento.Tierra;
        }
        else if (tipoRamdonSelect == 3)
        {
            tipoElemento = ObjetoCarta.Elemento.Fuego;
        }

        Debug.Log("La IA esta llendo a " + tipoElemento);

        modoIA = 0;
  


        if (modoIA == 0)  //En este modo no sacara al campo carta de su color main y las que este en el campo las pasara a la mano
        {
            Debug.Log("Pruebas a tope " + modoIA);
            CartasArrayEnCampo = GameObject.FindGameObjectsWithTag("CartaCampoEn");

            for (int i = 0; i < CartasArrayEnCampo.Length; i++)
            {
                Carta = CartasArrayEnCampo[i];
                Debug.Log("Pruebas a tope1 " + Carta.name);
                
                if (Carta.GetComponent<ObjetoCarta>().tipoElemento == tipoElemento)
                {
                    Carta.transform.gameObject.tag = "CartasManoEn";
                    Carta.transform.SetParent(GameObject.FindGameObjectWithTag("ManoEnemigo").transform);
                    // Instantiate(Carta, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("ManoEnemigo").transform);
                    // DestroyImmediate(Carta);
                }
                time = UnityEngine.Random.Range(300, 1300);
                await TimeForAIAsync(time);
            }

            CartasArrayEnMano = GameObject.FindGameObjectsWithTag("CartasManoEn");

            for (int i = 0; i < CartasArrayEnMano.Length; i++)
            {
                CartasArrayEnCampo = GameObject.FindGameObjectsWithTag("CartaCampoEn");
                Carta = CartasArrayEnMano[i];
                Debug.Log("Pruebas a tope3: " + Carta.name + " de tipo " + Carta.GetComponent<ObjetoCarta>().getElemento() + " sale al campo por que es diferente a " + tipoElemento);

                if (Carta.GetComponent<ObjetoCarta>().tipoElemento != tipoElemento && CartasArrayEnCampo.Length < maximaCartasCampo)
                {

                    Carta.transform.gameObject.tag = "CartaCampoEn";
                    Carta.transform.SetParent(GameObject.FindGameObjectWithTag("CampoEnemigo").transform);
                    //Instantiate(Carta, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("CampoEnemigo").transform);
                    //DestroyImmediate(Carta);
                }
                time = UnityEngine.Random.Range(300, 1300);
                await TimeForAIAsync(time);
            }

        }
        else
        {
            Debug.Log("Pruebas a tope " + modoIA);
            CartasArrayEnCampo = GameObject.FindGameObjectsWithTag("CartaCampoEn");

            for (int i = 0; i < CartasArrayEnCampo.Length; i++)
            {
                Carta = CartasArrayEnCampo[i];
                Debug.Log("Pruebas a tope1 " + Carta.name);

                if (Carta.GetComponent<ObjetoCarta>().tipoElemento != tipoElemento)
                {
                    Carta.transform.gameObject.tag = "CartasManoEn";
                    Carta.transform.SetParent(GameObject.FindGameObjectWithTag("ManoEnemigo").transform);
                    //   Instantiate(Carta, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("ManoEnemigo").transform);
                    // DestroyImmediate(Carta);
                }
                time = UnityEngine.Random.Range(300, 1300);
                await TimeForAIAsync(time);
            }

            CartasArrayEnMano = GameObject.FindGameObjectsWithTag("CartasManoEn");

            for (int i = 0; i < CartasArrayEnMano.Length; i++)
            {
                CartasArrayEnCampo = GameObject.FindGameObjectsWithTag("CartaCampoEn");
                Carta = CartasArrayEnMano[i];
                Debug.Log("Pruebas a tope3: " + Carta.name + " de tipo " + Carta.GetComponent<ObjetoCarta>().getElemento() + " sale al campo por que es diferente a " + tipoElemento);

                if (Carta.GetComponent<ObjetoCarta>().tipoElemento == tipoElemento && CartasArrayEnCampo.Length < 3)
                {
                    Debug.Log("COMETAMEPLIS: " + CartasArrayEnCampo.Length + " < 3");
                    Carta.transform.gameObject.tag = "CartaCampoEn";
                    Carta.transform.SetParent(GameObject.FindGameObjectWithTag("CampoEnemigo").transform);
                    //Instantiate(Carta, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("CampoEnemigo").transform);
                    //DestroyImmediate(Carta);
                }
                time = UnityEngine.Random.Range(300, 1300);
                await TimeForAIAsync(time);
            }

        }


  

        //La IA elije un color al principio del combate, por ahora aleatorio y se centra en el
        // Para esto necesito tener la capacidad de detectar todas las cartas de la mano y del campo con sus respectivos 
        //colores para decidir cuales sacar y cuales guardar

        // En el modo conservador, saca carta que no sean del color elejido, para ahorar las cartas de su color

        // En el modo agresivo, saca todas las cartas que puede de su color elejido para hacer un ataque fuerte



    }

    async void TurnBatle()//Lucha
    {

        List<GameObject> CartasIA = new List<GameObject>();
        List<GameObject> CartasJugador = new List<GameObject>();

        GameObject CartaIASelect = null;
        GameObject CartaJugadorSelect = null;

        int ataquesCompletados = 0;
        string[] tipoDeSituacion = {"FreeKill","Ablandar","Intercambio","Sacrificio","Cabeza"};
        int x = 0;
        int y = 0;

        CartasArray = GameObject.FindGameObjectsWithTag("CartaCampoEn"); // Encuentra todas las cartas en mano de la IA, por primera vez para las combos
        combos(CartasArray);
        do
        {
         
            CartasIA.Clear();
            CartasJugador.Clear();
            situacionCompatible = false;
            CartasArray = GameObject.FindGameObjectsWithTag("CartaCampoEn"); // Encuentra todas las cartas en mano de la IA

            for (int i = 0; i < CartasArray.Length; i++) //Se mete todas las cartas activas en el ArrayList CartasIA
            {

                if (CartasArray[i].GetComponent<ObjetoCarta>().getActiva())//Si la carta esta activa, se mete en el array 
                {
                    CartasIA.Add(CartasArray[i]);
                }
            }


            CartasArray = GameObject.FindGameObjectsWithTag("CartaCampo"); // Encuentra todas las cartas en mano del Jugador
            for (int i = 0; i < CartasArray.Length; i++) //Se mete todas las cartas activas en el ArrayList CartasIA
            {
                CartasJugador.Add(CartasArray[i]);
            }

            for (int i = 0; i < CartasIA.Count; i++)//recorre todas las cartas del jugador
            {
              //  Debug.Log("HOLAAAAA AQUI SE LIMPIAN LOS LIST 3// CartaIASelect: " + i + " // " + CartasIA.Count);
                for (int j = 0; j < CartasJugador.Count; j++)//recorre todas las cartas IA
                {
                    //Aqui se cojen dos cartas y se comprueba si la tipoDeSituacion se da
                    Debug.Log("HOLAAAAA AQUI SE LIMPIAN LOS LIST 4// CartaJugadorSelect: " + j + " // " + CartasJugador.Count);
                    situacionCompatible = situacionDeCombate(CartasIA, CartasJugador,i ,j, tipoDeSituacion[x]); //true si atacara false si no


                    if (situacionCompatible == true) //si es true ataca y reinicia el ciclo
                    {
                        if (x != 4)
                        {

                            if (CartasIA[i].GetComponent<ObjetoCarta>().getClase().ToString().Equals("Asesino") && AsesinoIA == true) //Habilidad asesino, ataca a cabeza
                            {
                                Debug.Log("Asesino activo:" + Asesino);
                                Jugador.GetComponent<ObjetoJugador>().perderVida(CartasIA[i].GetComponent<ObjetoCarta>().getAtaque());
                                CartasIA[i].GetComponent<ObjetoCarta>().setActiva(false);
                            }
                            else if (CartasIA[i].GetComponent<ObjetoCarta>().getClase().ToString().Equals("Asesino") && Asesino2IA == true)//Habilidad asesino, ataca a cabeza x2
                            {
                                Debug.Log("Asesino activo:" + Asesino);
                                Jugador.GetComponent<ObjetoJugador>().perderVida(CartasIA[i].GetComponent<ObjetoCarta>().getAtaque() * 2);
                                CartasIA[i].GetComponent<ObjetoCarta>().setActiva(false);
                            }
                            else
                            {
                                Debug.Log("Asesino no activo:" + Asesino);
                                combate(CartasIA[i], CartasJugador[j]);
                                await TimeForAIAsync(2000);
                            }

                        }
                        else
                        {
                            Debug.Log("La Vida del jugador es: " + Jugador.GetComponent<ObjetoJugador>().getVida());
                            Jugador.GetComponent<ObjetoJugador>().perderVida(CartasIA[i].GetComponent<ObjetoCarta>().getAtaque());
                            CartasIA[i].GetComponent<ObjetoCarta>().setActiva(false);
                            Debug.Log("La Vida del jugador es ahora: " + Jugador.GetComponent<ObjetoJugador>().getVida());

                            if (GameObject.Find("Jugador").GetComponent<ObjetoJugador>().getVida() <= 0)
                            {

                                GameObject.Find("MarcoDerrota").transform.position = new Vector3(GameObject.Find("Canvas").transform.position.x, GameObject.Find("Canvas").transform.position.y, 0);
                                
                            }

                        }


                        break;
                    }
                    else
                    {
                        
                    }


                }
                if (situacionCompatible == true)
                {
                    if (x < 2)
                    {
                        x = 0;
                    }

                    break;

                }

                //aqui atacan a cabeza
                if (x == 4)
                {
                    Debug.Log("La Vida del jugador es: " + Jugador.GetComponent<ObjetoJugador>().getVida());
                    Jugador.GetComponent<ObjetoJugador>().perderVida(CartasIA[i].GetComponent<ObjetoCarta>().getAtaque());
                    CartasIA[i].GetComponent<ObjetoCarta>().setActiva(false);
                    Debug.Log("La Vida del jugador es ahora: " + Jugador.GetComponent<ObjetoJugador>().getVida());

                }
            }

            if (situacionCompatible == false)
            {
                x++;
            }
         //   Debug.Log("CartasCompletadas: " + ataquesCompletados);
         //   Debug.Log("CartasContadas: " + CartasIA.Count);
            y++;
        } while (y != 10);

        AtaquePrimeroIA = false;
        ArrollarIA2 = false;
        ArrollarIA4 = false;
        IraIA = false;
        IraIA2 = false;
        Asesino2IA = false;
        AsesinoIA = false;

        OpenPanel_Cartas panel = GameObject.Find("ButtonCartas").GetComponent<OpenPanel_Cartas>();
        panel.forceClose();

        state = BattleState.PLAYERTURN;
        PlayerTurn();
        Debug.Log("Fin del turno del enemigo");

    }

    private bool situacionDeCombate(List<GameObject> CartasIA, List<GameObject> CartasJugador,int IA, int Jugador, string tipoDeSituacion)
    {
        bool situacionCompatible = false;

        if (tipoDeSituacion.Equals("FreeKill")) //Se busca que la carta de la IA mate a la carta del Jugador sin que la suya muera
        {
            if (CartasIA[IA].GetComponent<ObjetoCarta>().getAtaque() >= CartasJugador[Jugador].GetComponent<ObjetoCarta>().getVida() && CartasIA[IA].GetComponent<ObjetoCarta>().getVida() > CartasJugador[Jugador].GetComponent<ObjetoCarta>().getAtaque())
            {
                situacionCompatible = true;
            }

        }

        if (tipoDeSituacion.Equals("Ablandar")) //Se busca que la carta de la IA dañe la carta del Jugador sin que la suya muera
        {
            if (CartasIA[IA].GetComponent<ObjetoCarta>().getAtaque() < CartasJugador[Jugador].GetComponent<ObjetoCarta>().getVida() && CartasIA[IA].GetComponent<ObjetoCarta>().getVida() > CartasJugador[Jugador].GetComponent<ObjetoCarta>().getAtaque())
            {
                situacionCompatible = true;
            }

        }

        if (tipoDeSituacion.Equals("Intercambio")) //Se busca que la carta de la IA mate la carta del Jugador muriendo las dos
        {
            if (CartasIA[IA].GetComponent<ObjetoCarta>().getAtaque() >= CartasJugador[Jugador].GetComponent<ObjetoCarta>().getVida() && CartasIA[IA].GetComponent<ObjetoCarta>().getVida() <= CartasJugador[Jugador].GetComponent<ObjetoCarta>().getAtaque())
            {
                int decisionAleatoria = UnityEngine.Random.Range(1, 3); //Por ahora esta decision es aleatoria en un 50%
                Debug.Log("La IA decid:  " + decisionAleatoria);

                if (decisionAleatoria == 1)
                {
                    situacionCompatible = true;
                }
                
            }

        }

        //sacrificio
        if (tipoDeSituacion.Equals("Sacrificio")) // La carta que no tienen otras opciones atacan a cabeza
        {

           // situacionCompatible = true;
        }

        if (tipoDeSituacion.Equals("Cabeza")) // La carta que no tienen otras opciones atacan a cabeza
        {
            situacionCompatible = true;
        }

        return situacionCompatible; 
    }

    int calcularAtaqueTotalJugador(List<GameObject> CartasIA)
    {
        int ataqueTotal = 0;

        for (int i = 0; i < CartasIA.Count; i++)//recorre todas las cartas de la IA para sumar el ataque total
        {
            ataqueTotal =  CartasIA[i].GetComponent<ObjetoCarta>().getAtaque();
        }

            return ataqueTotal;
    }
    int calcularAtaqueTotalIA(List<GameObject> CArtasJugador)
    {
        int ataqueTotal = 0;

        for (int i = 0; i < CArtasJugador.Count; i++)//recorre todas las cartas de la IA para sumar el ataque total
        {
            ataqueTotal = CArtasJugador[i].GetComponent<ObjetoCarta>().getAtaque();
        }

        return ataqueTotal;
    }

    public async void combate(GameObject CartaIASelect, GameObject CartaJugadorSelect) //Se le pasan dos cartas 
    {
        int vidaPrevciaAlCombate = CartaJugadorSelect.GetComponent<ObjetoCarta>().getVida();
        //AireIA (AtaquePrimero)
        if (CartaIASelect.GetComponent<ObjetoCarta>().getElemento().ToString().Equals("Aire") && AtaquePrimeroIA == true)
        {
            AtaquePrimero(CartaIASelect, CartaJugadorSelect, vidaPrevciaAlCombate);
        }
        //Al final, si no encuentra ningun efecto de cartas, aplica el ataque normal
        else
        {
            CartaIASelect.GetComponent<ObjetoCarta>().perderVida(CartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque());
            CartaJugadorSelect.GetComponent<ObjetoCarta>().perderVida(CartaIASelect.GetComponent<ObjetoCarta>().getAtaque());
        }


        if (IraIA == true)
        {
            Ira(CartaIASelect, CartaJugadorSelect);
        }




        time = 1000;
        await TimeForAIAsync(time); //Tiempo de observacion y animacion;

        //Si alguna de las cartas muere, se eliminan del campo
        if (CartaJugadorSelect.GetComponent<ObjetoCarta>().getVida() <= 0)
        {
            DestroyImmediate(CartaJugadorSelect);
            if (ArrollarIA2 == true && CartaIASelect.GetComponent<ObjetoCarta>().getElemento().ToString().Equals("Tierra"))
            {
                Jugador.GetComponent<ObjetoJugador>().perderVida(CartaIASelect.GetComponent<ObjetoCarta>().getAtaque() - vidaPrevciaAlCombate);
            }
            else if (ArrollarIA4 == true && CartaIASelect.GetComponent<ObjetoCarta>().getElemento().ToString().Equals("Tierra"))
            {
                Jugador.GetComponent<ObjetoJugador>().perderVida((CartaIASelect.GetComponent<ObjetoCarta>().getAtaque() - vidaPrevciaAlCombate) * 2);

            }
        }

        if (CartaIASelect.GetComponent<ObjetoCarta>().getVida() <= 0)
        {
            DestroyImmediate(CartaIASelect);
        }
        else
        {
            CartaIASelect.GetComponent<ObjetoCarta>().setActiva(false);
        }

    }

    void Ira(GameObject cartaIASelect, GameObject cartaJugadorSelect)
    {
        if (IraIA2 == true)
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
            AtaquePrimeroIA = true;
        }
        else if (Aire >= 3)
        {
            AtaquePrimeroIA = true;

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
            ArrollarIA4 = true;
            //Activar Luz Imagen
            imagenActiva.SetActive(true);
        }
        else if (Tierra >= 2)
        {
            ArrollarIA2 = true;
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
            IraIA = true;
        }
        else if (Fuego >= 3)
        {
            IraIA = true;

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

        ComboPanelIA.GetComponent<ComboPanelObjeto>().setAire(Aire);
        ComboPanelIA.GetComponent<ComboPanelObjeto>().setAgua(Agua);
        ComboPanelIA.GetComponent<ComboPanelObjeto>().setTierra(Tierra);
        ComboPanelIA.GetComponent<ComboPanelObjeto>().setFuego(Fuego);
    }

    void TimeForAI(int tiempo)
    {
        Thread.Sleep(tiempo);
    }


     async System.Threading.Tasks.Task<bool> TimeForAIAsync(int tiempo)
    {
        bool bol = true;
        await System.Threading.Tasks.Task.Run(() =>
        {
            TimeForAI(tiempo);
        }
        );
        return bol;
    }

}
