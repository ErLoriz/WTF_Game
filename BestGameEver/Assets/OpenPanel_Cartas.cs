using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel_Cartas : MonoBehaviour
{

    public GameObject Panel_Cartas;
    public bool Open = false;
    public void OpenPanel()
    
    {
        if(Open == false)
        {
            Panel_Cartas.SetActive(true);
            Open = true;
        }
        else if(Open == true)
        {
            Panel_Cartas.SetActive(false);
            Open = false;
        }
   

       
     
    }
  
}
