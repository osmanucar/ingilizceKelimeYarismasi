using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yonetim : MonoBehaviour
{
    //public Button girisButton;
    public GameObject giris_P, anamenu_P;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("girisDurumu")) // Eğer hiç giriş yapılmamışsa giriş panelinden başla
        {
            giris_P.SetActive(true);
            anamenu_P.SetActive(false);           
        }
        else
        {
            giris_P.SetActive(false);
            anamenu_P.SetActive(true);
        }
    }

   public void girisButton()
    {
        PlayerPrefs.SetInt("girisDurumu", 1); //Daha önce giriş yapılmışşsa anamenu panelinden başla.
        giris_P.SetActive(false);
        anamenu_P.SetActive(true);

    }
}
