using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjetoCarta : MonoBehaviour
{
 
    public int Ataque;
    public int Vida;
    public int AtaqueDefault;
    public int VidaDefault;
    public int Escudo;
    public int Coste;
    public bool Activa;
    public bool AtaqueActivo;



    public enum Elemento {Aire, Agua, Tierra, Fuego};
    public Elemento tipoElemento;
    public enum Clase {Asesino, Protector, Guerrero, Mago, Pirata};
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
    public int getCoste()
    {
      
        return Coste;
    }
    public int getEscudo()
    {

        return Escudo;
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

    public void setEscudo(int escudo)
    {
        Escudo = escudo;
    }

    public void setActiva(bool activa)
    {
        Activa = activa;
    }

    public void setAtaque(int ataque)
    {
        Ataque = ataque;
    }
    public bool getAtaqueActivo()
    {
        return AtaqueActivo;
    }

    public void setAtaqueActivo(bool activa)
    {
        AtaqueActivo = activa;
    }

    public void perderVida(int cantidad)
    {
        Vida -= cantidad;
        T_ataque.text = Ataque.ToString();
        T_vida.text = Vida.ToString();


    }
    public void ganarVida(int cantidad)
    {
        Vida += cantidad;
        //  T_ataque.text = Ataque.ToString();
        T_vida.text = Vida.ToString();
    }

    public void updateStatis()
    {
        T_ataque.text = Ataque.ToString();
        T_vida.text = Vida.ToString();
    }

    public int getAtaqueDef()
    {
        return AtaqueDefault;
    }

    public int getVidaDef()
    {
        return VidaDefault;
    }



}
