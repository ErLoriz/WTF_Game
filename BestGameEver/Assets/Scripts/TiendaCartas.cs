using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TiendaCartas : MonoBehaviour
{

    private GameObject Carta0;
    private GameObject Carta1;
    private GameObject Carta2;
    private GameObject Carta3;

    static GameObject CartaPanel1 = null;
    static GameObject CartaPanel2 = null;
    static GameObject CartaPanel3 = null;
    static GameObject CartaPanel4 = null;

    public int CartaAleatoria;

    public static GameObject Jugador;

    BattleSystem battleSystem;

    private static int turn;

    public Text coste4;

    public Transform Panel_Cartas;

    public Draggable.Slot tipoCarta;

    void Start()
    {
        nuevasCartas();

    }

    public void nuevasCartas()
    {
        if (CartaPanel1 != null)
        {
            DestroyImmediate(CartaPanel1);
            DestroyImmediate(CartaPanel2);
            DestroyImmediate(CartaPanel3);
            DestroyImmediate(CartaPanel4);

            CartaPanel1 = Instantiate(ObtenccionCartasIA(), new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
            CartaPanel2 = Instantiate(ObtenccionCartasIA(), new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
            CartaPanel3 = Instantiate(ObtenccionCartasIA(), new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
            CartaPanel4 = Instantiate(ObtenccionCartasIA(), new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);

        }
        else if (CartaPanel1 == null) { 

        CartaPanel1 = Instantiate(ObtenccionCartasIA(), new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
        CartaPanel2 = Instantiate(ObtenccionCartasIA(), new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
        CartaPanel3 = Instantiate(ObtenccionCartasIA(), new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
        CartaPanel4 = Instantiate(ObtenccionCartasIA(), new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);

    }
                /*

        Carta0 = Resources.Load("00_AngelSagrado") as GameObject;
        Carta0.transform.gameObject.tag = "CartaTienda";
        Carta0.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
        Carta1 = Resources.Load("15_AlmaPerdida") as GameObject;
        Carta1.transform.gameObject.tag = "CartaTienda";
        Carta1.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
        Carta2 = Resources.Load("10_SegadorDelBosque") as GameObject;
        Carta2.transform.gameObject.tag = "CartaTienda";
        Carta2.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;
        Carta3 = Resources.Load("05_VampiroMarino") as GameObject;
        Carta3.transform.gameObject.tag = "CartaTienda";
        Carta3.GetComponent<Draggable>().tipoCarta = Draggable.Slot.MANO;

        var primera = Random.Range(1, 5);
        var segunda = Random.Range(1, 5);
        var tercero = Random.Range(1, 5);
        var cuarto = Random.Range(1, 5);
        
        if (CartaPanel1 != null)
        {
            DestroyImmediate(CartaPanel1);
            DestroyImmediate(CartaPanel2);
            DestroyImmediate(CartaPanel3);
            DestroyImmediate(CartaPanel4);

          

            switch (primera)
            {
                case 1:
                    CartaPanel1 = Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
                    break;
                case 2:
                    CartaPanel1 = Instantiate(Carta1, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
                    break;
                case 3:
                    CartaPanel1 = Instantiate(Carta2, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
                    break;
                case 4:
                    CartaPanel1 = Instantiate(Carta3, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
                    break;
            }

            switch (segunda)
            {
                case 1:
                    CartaPanel2 = Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
                    break;
                case 2:
                    CartaPanel2 = Instantiate(Carta1, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
                    break;
                case 3:
                    CartaPanel2 = Instantiate(Carta2, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
                    break;
                case 4:
                    CartaPanel2 = Instantiate(Carta3, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
                    break;
            }

            switch (tercero)
            {
                case 1:
                    CartaPanel3 = Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
                    break;
                case 2:
                    CartaPanel3 = Instantiate(Carta1, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
                    break;
                case 3:
                    CartaPanel3 = Instantiate(Carta2, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
                    break;
                case 4:
                    CartaPanel3 = Instantiate(Carta3, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
                    break;
            }

            switch (cuarto)
            {
                case 1:
                    CartaPanel4 = Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);
                    break;
                case 2:
                    CartaPanel4 = Instantiate(Carta1, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);
                    break;
                case 3:
                    CartaPanel4 = Instantiate(Carta2, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);
                    break;
                case 4:
                    CartaPanel4 = Instantiate(Carta3, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);
                    break;
            }
        }
        else if (CartaPanel1 == null)
        {
            switch (primera)
            {
                case 1:
                    CartaPanel1 = Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
                    break;
                case 2:
                    CartaPanel1 = Instantiate(Carta1, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
                    break;
                case 3:
                    CartaPanel1 = Instantiate(Carta2, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
                    break;
                case 4:
                    CartaPanel1 = Instantiate(Carta3, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas1").transform);
                    break;
            }

            switch (segunda)
            {
                case 1:
                    CartaPanel2 = Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
                    break;
                case 2:
                    CartaPanel2 = Instantiate(Carta1, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
                    break;
                case 3:
                    CartaPanel2 = Instantiate(Carta2, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
                    break;
                case 4:
                    CartaPanel2 = Instantiate(Carta3, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas2").transform);
                    break;
            }

            switch (tercero)
            {
                case 1:
                    CartaPanel3 = Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
                    break;
                case 2:
                    CartaPanel3 = Instantiate(Carta1, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
                    break;
                case 3:
                    CartaPanel3 = Instantiate(Carta2, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
                    break;
                case 4:
                    CartaPanel3 = Instantiate(Carta3, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas3").transform);
                    break;
            }

            switch (cuarto)
            {//esta wea se debe cambiar

                case 1:
                    CartaPanel4 = Instantiate(Carta0, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);
                     break;
                case 2:
                    CartaPanel4 = Instantiate(Carta1, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);
                    break;
                case 3:
                    CartaPanel4 = Instantiate(Carta2, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);
                    break;
                case 4:
                    CartaPanel4 = Instantiate(Carta3, new Vector3(185, 165, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("PanelCartas4").transform);
                    break;
            }

        }
        */
    }

    public void reroll()
    {

        if (GameObject.Find("Jugador").GetComponent<ObjetoJugador>().getOro() >= 2)
        {
            GameObject.Find("Jugador").GetComponent<ObjetoJugador>().perderOro(2);

            if (GameObject.FindGameObjectWithTag("PanelCartas1").transform.childCount == 0) { CartaPanel1 = new GameObject(); }
           
            if (GameObject.FindGameObjectWithTag("PanelCartas2").transform.childCount == 0) { CartaPanel2 = new GameObject(); }

            if (GameObject.FindGameObjectWithTag("PanelCartas3").transform.childCount == 0) { CartaPanel3 = new GameObject(); }

            if (GameObject.FindGameObjectWithTag("PanelCartas4").transform.childCount == 0) { CartaPanel4 = new GameObject(); }
            

            nuevasCartas();
        }
        
    }

    public void rerollTurno()
    {

            if (GameObject.FindGameObjectWithTag("PanelCartas1").transform.childCount == 0) { CartaPanel1 = new GameObject(); }

            if (GameObject.FindGameObjectWithTag("PanelCartas2").transform.childCount == 0) { CartaPanel2 = new GameObject(); }

            if (GameObject.FindGameObjectWithTag("PanelCartas3").transform.childCount == 0) { CartaPanel3 = new GameObject(); }

            if (GameObject.FindGameObjectWithTag("PanelCartas4").transform.childCount == 0) { CartaPanel4 = new GameObject(); }


        nuevasCartas();
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

        return CartaX;
    }



}



