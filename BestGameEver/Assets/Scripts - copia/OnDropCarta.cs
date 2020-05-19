using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropCarta : MonoBehaviour, IDropHandler
{

    public static GameObject Jugador;
    public BattleSystem battle;
    public GameObject combos;
    public int aireComb;
    public int fuegoComb;

    public async void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

       

        switch (GameObject.Find("BattleSystem").GetComponent<BattleSystem>().state)
        {

            case BattleState.ATTACKTURN:

                if (this.tag == "CartaCampoEn" && d.tipoCarta == Draggable.Slot.CARTA_ATAQUE && d.GetComponent<ObjetoCarta>().Activa == true && d.GetComponent<ObjetoCarta>().AtaqueActivo == false)
                {
                    
                    await ChangeTipeAsync();

                    d.GetComponent<ObjetoCarta>().setAtaqueActivo(true);
                    GameObject cartaPlayer = d.gameObject;
                    GameObject cartaIA = this.gameObject;

                    //combate MAMAMDISIMO
                    combos = GameObject.Find("ComboPanelElemento");

                    aireComb = combos.GetComponent<ComboPanelObjeto>().getAire();
                    fuegoComb = combos.GetComponent<ComboPanelObjeto>().getFuego();

                    if (aireComb >= 3 && cartaPlayer.GetComponent<ObjetoCarta>().getElemento().ToString().Equals("Aire")) //habilidad del aire
                    {
                       
                        AtaquePrimero(cartaIA, cartaPlayer, cartaIA.GetComponent<ObjetoCarta>().getVida());

                    }
                    else
                    {
                        cartaPlayer.GetComponent<ObjetoCarta>().perderVida(this.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas
                        this.GetComponent<ObjetoCarta>().perderVida(cartaPlayer.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas

                        Debug.Log("Si fuego esta activo y es una carta de tupo fuego...");
                        if (fuegoComb >= 2 && cartaPlayer.GetComponent<ObjetoCarta>().getElemento().ToString().Equals("Fuego"))
                        {
                            Ira(cartaPlayer, cartaIA);
                        }
                       
                    }


                  



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
    void AtaquePrimero(GameObject cartaIASelect, GameObject cartaJugadorSelect, int vidaPrevciaAlCombate)
    {
        cartaIASelect.GetComponent<ObjetoCarta>().perderVida(cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque());
   
        if (cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque() < vidaPrevciaAlCombate)
        {
            cartaJugadorSelect.GetComponent<ObjetoCarta>().perderVida(cartaIASelect.GetComponent<ObjetoCarta>().getAtaque());//Se actualiza la vida de las cartas

        }

    }

    void Ira(GameObject cartaJugadorSelect, GameObject  cartaIASelect)
    {
        Debug.Log("Entro en IraJugador");
        if (fuegoComb >= 4)
        {
            Debug.Log("Su ataque es "+ cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque());
            cartaJugadorSelect.GetComponent<ObjetoCarta>().setAtaque(cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque() + cartaIASelect.GetComponent<ObjetoCarta>().getAtaque() * 2);
            Debug.Log("Y ahora es " + cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque());
        }
        else
        {
            cartaJugadorSelect.GetComponent<ObjetoCarta>().setAtaque(cartaJugadorSelect.GetComponent<ObjetoCarta>().getAtaque() + cartaIASelect.GetComponent<ObjetoCarta>().getAtaque());

        }
        cartaJugadorSelect.GetComponent<ObjetoCarta>().updateStatis();
    }
}
