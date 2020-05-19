using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboPanelObjeto : MonoBehaviour
{
    public int Aire;
    public int Agua;
    public int Tierra;
    public int Fuego;
    public int Veneno;
    public int Luz;
    public int Oscuridad;
    public int Protector;
    public int Guerrero;
    public int Asesino;
    public int Mago;
    public int Pirata;
    public int Deidad;

    public int AireMax = 6;
    public int AguaMax = 6;
    public int TierraMax = 4;
    public int FuegoMax = 6;
    public int VenenoMax = 4;
    public int LuzMax = 1;
    public int OscuridadMax = 1;
    public int ProtectorMax = 4;
    public int GuerreroMax = 4;
    public int AsesinoMax = 4;
    public int MagoMax = 2;
    public int PirataMax = 3;
    public int DeidadMax = 2;

    public Text T_Aire;
    public Text T_Agua;
    public Text T_Tierra;
    public Text T_Fuego;
    public Text T_Veneno;
    public Text T_Luz;
    public Text T_Oscuridad;
    public Text T_Protector;
    public Text T_Guerrero;
    public Text T_Asesino;
    public Text T_Mago;
    public Text T_Pirata;
    public Text T_Deidad;

    public Text T_AireMax;
    public Text T_AguaMax;
    public Text T_TierraMax;
    public Text T_FuegoMax;
    public Text T_VenenoMax;
    public Text T_LuzMax;
    public Text T_OscuridadMax;
    public Text T_ProtectorMax;
    public Text T_GuerreroMax;
    public Text T_AsesinoMax;
    public Text T_MagoMax;
    public Text T_PirataMax;
    public Text T_DeidadMax;

    public void Start()
    {
    //Se estable el numero maximo de cartas de un tipo para una combo dentro del texto del juego
    T_AireMax.text = AireMax.ToString();
    T_AguaMax.text = AguaMax.ToString();
    T_TierraMax.text = TierraMax.ToString();
    T_FuegoMax.text = FuegoMax.ToString();
    //T_VenenoMax.text = VenenoMax.ToString();
    //T_LuzMax.text = LuzMax.ToString();
    //T_OscuridadMax.text = OscuridadMax.ToString();
    T_ProtectorMax.text = ProtectorMax.ToString();
    T_GuerreroMax.text = GuerreroMax.ToString();
    T_AsesinoMax.text = AsesinoMax.ToString();
    T_MagoMax.text = MagoMax.ToString();
    T_PirataMax.text = PirataMax.ToString();
        //T_DeidadMax.text = DeidadMax.ToString();
    }

    public void Update()
    {
        //Aqui se actualiza el numero de combos
        T_Aire.text = Aire.ToString();
        T_Agua.text = Agua.ToString();
        T_Tierra.text = Tierra.ToString();
        T_Fuego.text = Fuego.ToString();
       // T_Veneno.text = Veneno.ToString();
       // T_Luz.text = Luz.ToString();
       // T_Oscuridad.text = Oscuridad.ToString();
        T_Protector.text = Protector.ToString();
        T_Guerrero.text = Guerrero.ToString();
        T_Asesino.text = Asesino.ToString();
        T_Mago.text = Mago.ToString();
       // T_Deidad.text = Deidad.ToString();
    }

    public void setAire(int aire)
    {
        Aire = aire;
        Update();
    }
    public void setAgua(int agua)
    {
        Agua = agua;
        Update();
    }
    public void setTierra(int tierra)
    {
        Tierra = tierra;
        Update();
    }
    public void setFuego(int fuego)
    {
        Fuego = fuego;
        Update();
    }

    public void setProtector(int protector)
    {
        Protector = protector;
        Update();
    }
    public void setGuerrero(int guerrero)
    {
        Guerrero = guerrero;
        Update();
    }
    public void setAsesino(int asesino)
    {
        Asesino = asesino;
        Update();
    }
    public void setMago(int mago)
    {
        Mago = mago;
        Update();
    }
    public void setPirata(int pirata)
    {
        Pirata = pirata;
        Update();
    }

    public int getAire()
    {
        return Aire;
    }
    public int getAgua()
    {
        return Agua;
    }
    public int getTierra()
    {
        return Tierra;
    }
    public int getFuego()
    {
        return Fuego;
    }

    public int getProtector()
    {
        return Protector;
    }
    public int getGuerrero()
    {
        return Guerrero;
    }
    public int getAsesino()
    {
        return Asesino;
    }
    public int getMago()
    {
        return Mago;
    }
    public int getPirata()
    {
        return Pirata;
    }


}
