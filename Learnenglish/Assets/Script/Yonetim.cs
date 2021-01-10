using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

   /* public void devamButonu()
    {
        
        giris_P.SetActive(true);
       // anamenu_P.SetActive(false);
        SahneDegistirici.sahneDegis(0);
        // SceneManager.LoadScene("Anamenu"); //Tekrar giriş alanına dönmek için.
    }
    */

    public void girisButton()
    {
        PlayerPrefs.SetInt("girisDurumu", 1); //Daha önce giriş yapılmışşsa anamenu panelinden başla.
        giris_P.SetActive(false);
        anamenu_P.SetActive(true);

    }

    public void basla_B()
    {
        SahneDegistirici.sahneDegis(2);
    }

    public void cikis_B()
    {
        anamenu_P.SetActive(false);
        giris_P.SetActive(true);
    }
}
