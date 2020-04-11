using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN , ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
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
            TextoTurno.text = "Tu turno";
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        } else
        {
            TextoTurno.text = "Enemigo";
            state = BattleState.ENEMYTURN;
            EnemyTurn();
        }
        //Debug.Log("Hay " + manoEnemiga.FindGameObjectsWithTag("Carta0").Length);
        

    }

    void PlayerTurn()
    {

    }

    public void OnClickButtonTurn()
    {
        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }


    void EnemyTurn()
    {



    }

}
