using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoJugador : MonoBehaviour
{
    public int Vida = 100;
    public int Oro = 0;
    public Text T_vida;
    public Text T_Oro;

    public int getVida()
    {
        return Vida;
    }

    public void setVida(int vida)
    {
        Vida = vida;
    }

    public void perderVida(int cantidad)
    {
        Vida -= cantidad;
        T_vida.text = Vida.ToString();
    }

    public void ganarOro(int cantidad)
    {
        Oro += cantidad;
       T_Oro.text = Oro.ToString();
    }

    public void perderOro(int cantidad)
    {
        Oro -= cantidad;
        T_Oro.text = Oro.ToString();
    }

    public int getOro()
    {
        return Oro;
    }
}
