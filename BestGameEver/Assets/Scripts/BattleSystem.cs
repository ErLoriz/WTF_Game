using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    // Thread tt = new Thread(ThreadTiempo.ThreadTemp);

    public bool situacionCompatible;
    public int CartasManoCant;
    public GameObject[] PlayersArray; 
    public GameObject Jugador;
    public GameObject IA;
    public GameObject Carta;
    public GameObject CartaEnemiga;
    public GameObject[] CartasArray;
    public Button botonPasar;

    public Text TextoTurno;

    public Canvas manoEnemiga;

    public BattleState state;

    int time = 0;
    
    public ObjetoCarta.Elemento tipoElemento;

    public Draggable.Slot tipoCarta;
    public DropZone zone;

    public int CartaAleatoria;

    //cartas
    static GameObject Carta0;
    private GameObject Carta1;
    private GameObject Carta2;
    private GameObject Carta3;


    // Start is called before the first frame update
    void Start()
    {
        zone = GameObject.FindGameObjectWithTag("Campo").GetComponent<DropZone>();
        PlayersArray = GameObject.FindGameObjectsWithTag("Jugador");
        Jugador = PlayersArray[0];
        Debug.LogWarning("Aqui deberia pasar el jugador a la clase dropZone");
        DropZone.obtenerJugador(Jugador);

        PlayersArray = GameObject.FindGameObjectsWithTag("IA");
        IA = PlayersArray[0];

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
        zone.tipoCarta = Draggable.Slot.CAMPO;
        botonPasar.enabled = true;
        TextoTurno.text = "Tu turno";
        Jugador.GetComponent<ObjetoJugador>().ganarOro(5);

    }

    public void OnClickButtonTurn()
    {

        state = BattleState.ENEMYTURN;
        EnemyTurn();

    }


    async void EnemyTurn()
    {
        zone.tipoCarta = Draggable.Slot.CAMPO_OFF;
        botonPasar.enabled = false;
        TextoTurno.text = "Enem.F1";
        Debug.Log("Inicia el turno del enemigo");

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

        CartaAleatoria = UnityEngine.Random.Range(1, 4);

        switch (CartaAleatoria)
        {
            case 1:
                CartaX = Resources.Load("Angel") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                break;
            case 2:
                CartaX = Resources.Load("DemonioRojo") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                break;
            case 3:
                CartaX = Resources.Load("Segador") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                break;
            case 4:
                CartaX = Resources.Load("VampiroAgua") as GameObject;
                CartaX.transform.gameObject.tag = "CartaTienda";
                break;
        }

        Debug.Log("Carta en tienda IA: " + CartaX.name);


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
                           // Debug.Log("HOLAAAAA AQUI SE LIMPIAN LOS LIST 5// " + CartasJugador[j].name);
                          combate(CartasIA[i], CartasJugador[j]);
                            await TimeForAIAsync(2000);

                        }
                        else
                        {
                            Debug.Log("La Vida del jugador es: " + Jugador.GetComponent<ObjetoJugador>().getVida());
                            Jugador.GetComponent<ObjetoJugador>().perderVida(CartasIA[i].GetComponent<ObjetoCarta>().getAtaque());
                            CartasIA[i].GetComponent<ObjetoCarta>().setActiva(false);
                            Debug.Log("La Vida del jugador es ahora: " + Jugador.GetComponent<ObjetoJugador>().getVida());
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
        CartaJugadorSelect.GetComponent<ObjetoCarta>().perderVida(CartaIASelect.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas
        CartaIASelect.GetComponent<ObjetoCarta>().perderVida(CartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas

        time = 1000;
            await TimeForAIAsync(time); //Tiempo de observacion y animacion;

        //Si alguna de las cartas muere, se eliminan del campo
            if (CartaJugadorSelect.GetComponent<ObjetoCarta>().getVida() <= 0) 
            {

                DestroyImmediate(CartaJugadorSelect);
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
