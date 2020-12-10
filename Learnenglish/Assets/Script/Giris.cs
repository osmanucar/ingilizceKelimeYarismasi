using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class Giris : MonoBehaviour
{
    public TMP_InputField kullaniciAdi;
    public TMP_InputField sifre;
    public Button kaydol;
    public Button kontrol;
    public GameObject girisYap_B;
    public TextMeshProUGUI mesajYazisi;
    void Start()
    {
      //  girisYap_B.SetActive(false); //Giriş yap butonu ilk başta kapalıdır.
    }

    public void register_Buton()
    {
        SceneManager.LoadScene("RegisterScreen");   //Kaydolma sahnesine geçmeye yarar.
    }

    void Update()
    {
        
    }

    public void girisButonu()
    {
        if (kullaniciAdi.text.Equals("") || sifre.text.Equals(""))  //Kullanıcı Adı veya şifre alanı boşsa
        {
            textYazdir("Alanları boş bırakmayınız");
        }
        else
        {
            StartCoroutine(girisYap()); // Bilgiler doğru girilirse girisYap methodu çagrılır.
           // textYazdir("Başarılı");
        }
    }

    IEnumerator girisYap()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "girisYapma");    //php deki girisYapma fonksiyonu   
        form.AddField("kullaniciAdi", kullaniciAdi.text);
        form.AddField("sifre", sifre.text);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/LearnWordsDepo/veritabani_islemleri.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                textYazdir(www.downloadHandler.text);
                if (www.downloadHandler.text.Equals("Giriş Başarılı"))
                {
                    girisYap_B.SetActive(true); // Kullanıcı adı ve şifre doğru girilirse giriş yap butonu aktif hale gelir. 
                }
               // kullaniciAdi.interactable = false;
               // sifre.interactable = false;
            }
        }
    }
    void textYazdir(string mesaj)
    {
        mesajYazisi.text = mesaj;
        Invoke("sifirla", 1.5f); //1,5 saniye sonra mesajı sıfırla.
    }
    void sifirla()
    {
        mesajYazisi.text = "";
    }
}
