using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoCarta : MonoBehaviour
{
 
    public int Ataque;
    public int Vida;
    public bool Activa;

    public enum Elemento {Aire, Agua, Tierra, Fuego};
    public Elemento tipoElemento;
    public enum Clase {Asesino, Apoyo, Guerrero, Tirador};
    public Clase tipoClase;

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

    public Elemento getElemento()
    {
        return tipoElemento;
    }

    public Clase getClase()
    {
        return tipoClase;
    }

    public void setVida(int vida)
    {
        Vida = vida;
    }

    public bool getActiva()
    {
        return Activa;
    }

    public void setActiva(bool activa)
    {
        Activa = activa;
    }



    public void perderVida(int cantidad)
    {
        Vida -= cantidad;
        T_ataque.text = Ataque.ToString();
        T_vida.text = Vida.ToString();



    }


  


}
