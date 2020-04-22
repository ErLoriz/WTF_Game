using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoCarta : MonoBehaviour
{
 
    public int Ataque;
    public int Vida;

    public Text T_ataque;
    public Text T_vida;

    void Start()
    {
        T_ataque.text = Ataque.ToString();
        T_vida.text = Vida.ToString();
    }

    public int getAtaque()
    {
        return Ataque;
    }

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
        T_ataque.text = Ataque.ToString();
        T_vida.text = Vida.ToString();
    }


  


}
