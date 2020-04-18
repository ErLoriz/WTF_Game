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

    public Transform Panel_Cartas;

    void Start()
    {

        nuevasCartas();

    }

    void nuevasCartas()
    {

        Carta0 = Resources.Load("Carta0") as GameObject;
        Carta1 = Resources.Load("Carta1") as GameObject;
        Carta2 = Resources.Load("Carta2") as GameObject;
        Carta3 = Resources.Load("Carta3") as GameObject;

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
        } else if(CartaPanel1 == null)
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
    }

    void cartasAMano(PointerEventData eventData)
    {



    }
     
    void reroll()
    { 
        nuevasCartas();
    }
    
}



