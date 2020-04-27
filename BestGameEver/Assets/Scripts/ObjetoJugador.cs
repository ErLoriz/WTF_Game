using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoJugador : MonoBehaviour
{
    public int Vida = 100;
    public Text T_vida;

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
}
