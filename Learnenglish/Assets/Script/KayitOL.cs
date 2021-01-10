using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;


public class KayitOL : MonoBehaviour
{
    public TMP_InputField ad;
    public TMP_InputField soyad;
    public TMP_InputField kullaniciAdi;
    public TMP_InputField sifre;
    public TMP_InputField sifreTekrar;
    public Button devam;
    public Button kaydol;
    public TextMeshProUGUI mesajYazisi;

    public GameObject giris_P, anamenu_P;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void devamButonu()
    {
       // giris_P.SetActive(true);
       // anamenu_P.SetActive(false);
        // SahneDegistirici.sahneDegis(0);
        SceneManager.LoadScene("Anamenu"); //Tekrar giriş alanına dönmek için.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void kaydolButonu()
    {
        if (NetKontrol.internet)
        {
            if (ad.text.Equals("") || soyad.text.Equals("") || kullaniciAdi.text.Equals("") || sifre.text.Equals("") || sifreTekrar.text.Equals(""))
            {
                //Alanlardan herhangi birisi boş ise
                textYazdir("Alanları boş bırakmayınız");
            }
            else if (sifre.text != (sifreTekrar.text)) // Şifreler eşleşmiyorsa
            {
                mesajYazisi.text = "Şifreler Eşleşmiyor";
                textYazdir("Şifreler Eşleşmiyor");
            }
            else
            {
                StartCoroutine(kayitOl()); // Herşey uygunsa kayıt ol methodu çağrılıyor.
            }
        }
        else
        {
            textYazdir("Lütfen internetinizi açınız");
        }
       
    }

    IEnumerator kayitOl()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "kayitOl");
        form.AddField("ad", ad.text);
        form.AddField("soyad", soyad.text);
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
                Debug.Log("Kaydınız başarıyla gerçekleştirildi");
                ad.interactable = false;
                soyad.interactable = false;
                kullaniciAdi.interactable = false; // kayıt başarılı olursa inputfield alanları kapanır. 
                sifre.interactable = false;
                sifreTekrar.interactable = false;
            }
        }
    }

    void textYazdir(string mesaj) //mesaj yazdırmak için
    {
        mesajYazisi.text = mesaj;
        Invoke("sifirla", 1.5f); //1,2 saniye sonra mesajı sıfırla.
    }
    void sifirla()
    {
        mesajYazisi.text = "";
    }
}
