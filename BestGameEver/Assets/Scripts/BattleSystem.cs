using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    ObjetoCarta oc = new ObjetoCarta();

    // Thread tt = new Thread(ThreadTiempo.ThreadTemp);


    public int CartasManoCant;
    public GameObject Carta;
    public GameObject CartaEnemiga;
    public GameObject[] CartasMano;
    public Button botonPasar;

    public Text TextoTurno;

    public Canvas manoEnemiga;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        //Saldra una pantalla con 1 carta a elegir

        var number = Random.Range(1, 3);

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

    public void PlayerTurn()
    {
        botonPasar.enabled = true;
        TextoTurno.text = "Tu turno";

    }

    public void OnClickButtonTurn()
    {

        state = BattleState.ENEMYTURN;
        EnemyTurn();

    }


    async void EnemyTurn()
    {
        botonPasar.enabled = false;
        //   tt.Start();
        TextoTurno.text = "Enemigo";
        Debug.Log("Inicia el turno del enemigo");
        await TimeForAIAsync();
        TurnBuy();
        await TimeForAIAsync();
        TurnCards();
        await TimeForAIAsync();
        TurnBatle();
        Debug.Log("Fin del turno del enemigo");

        TextoTurno.text = "Enemigo";

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }


    void TurnBuy()//Compra
    {

    }

    async void TurnCards()//Mueve cartas de la mano al campo
    {
        CartasManoCant = GameObject.FindGameObjectsWithTag("CartasManoEn").Length;
        Debug.Log("El enemigo tiene: " + CartasManoCant + " cartas en la mano");


        var ramd = Random.Range(0, GameObject.FindGameObjectsWithTag("CartasManoEn").Length);
        var n = 0;
        do // Elije el numero de carta que va a sacar de forma aleatoria
        {
            if (GameObject.FindGameObjectsWithTag("CartasManoEn").Length > 0) // Selecciona una carta aleatoria de la mano
            {

               
                CartasMano = GameObject.FindGameObjectsWithTag("CartasManoEn");
                var selecciondeCarta = Random.Range(0, GameObject.FindGameObjectsWithTag("CartasManoEn").Length);
                Carta = CartasMano[selecciondeCarta];
                Carta.transform.gameObject.tag = "CartaCampoEn";
                Instantiate(Carta, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("CampoEnemigo").transform);
                DestroyImmediate(Carta);

            }
            n++;
            await TimeForAIAsync();
        } while (n < ramd);



    }

    void TurnBatle()//Lucha
    {
        //seleccionar la primera carta del otro campo

        bool cartaEnemigaSelect = false;
    
        CartasMano = GameObject.FindGameObjectsWithTag("CartaCampo");
        if (CartasMano.Length != 0)
        {
            CartaEnemiga = CartasMano[0];
            cartaEnemigaSelect = true;
      
        }
       
        //Selecciona la primera carta de su campo
        CartasMano = GameObject.FindGameObjectsWithTag("CartaCampoEn");
        Carta = CartasMano[0];
        //combate
        if (cartaEnemigaSelect)
        {
            Debug.Log("Inicia el combate:");
            Debug.Log("La carta de la IA tiene " + Carta.GetComponent<ObjetoCarta>().getAtaque() + " de daño");
            Debug.Log("Tu carta tiene " + CartaEnemiga.GetComponent<ObjetoCarta>().getVida() + " de vida");
            CartaEnemiga.GetComponent<ObjetoCarta>().perderVida(Carta.GetComponent<ObjetoCarta>().getAtaque());
            Debug.Log("Despues del combate tu carta tiene  " + CartaEnemiga.GetComponent<ObjetoCarta>().getVida() + " de vida");

            Carta.GetComponent<ObjetoCarta>().perderVida(CartaEnemiga.GetComponent<ObjetoCarta>().getAtaque());
        }
      





    }

    void TimeForAI()
    {
        //  int time = Random.Range(500, 1500);
        Thread.Sleep(1000);
    }


    async System.Threading.Tasks.Task<bool> TimeForAIAsync()
    {
        bool bol = true;
        await System.Threading.Tasks.Task.Run(() =>
        {
            TimeForAI();
        }
        );
        return bol;
    }

}
