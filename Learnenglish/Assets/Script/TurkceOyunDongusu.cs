using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public static class rastegeleSirala2 //liste boyunca soruları random olarak atar.
{
    public static void Shuffle2<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}

public class TurkceOyunDongusu : MonoBehaviour
{
   
    float zaman;
    public Slider zamanSlider;
    public Image fill;
    public Gradient gradient;
    [Header("SORULAR")]
    public SorularList sorular;
    [Header("Soru Degişkenleri")]
    public TextMeshProUGUI soru_TMP;
    public Text A_T, B_T, C_T, D_T;
    public int soruNo;
    BirimSoruModel birimSoruModel;
    public Color dogruRenk, yanlisRenk;
    public Image[] butonImages;
    public List<int> sorularSirasi;

    AudioSource sesKaynagi;
    public AudioClip dogruSesi, yanlisSesi;
    public GameObject gameOver_P;
    public bool oyunBasladimi;

    void Start()
    {
        sesKaynagi = GetComponent<AudioSource>();

        
        StartCoroutine(turkceSorulariGetir());
        zaman = 15; //ilk zaman atama
        soruNo = 0;
    }

    void sorulariSormaSirasi()
    {
        for (int i = 0; i < sorular.butunSorular.Count; i++)
        {
            sorularSirasi.Add(i);
        }
        sorularSirasi.Shuffle2();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunBasladimi)
        {
            if (zaman > 0)
            {
                zaman -= Time.deltaTime;
                zamanSlider.value = zaman;
                fill.color = gradient.Evaluate(zaman / birimSoruModel.saniye);
            }
            else
            {
                gameOver();
            }
        }
        
    }

    private void gameOver()
    {
        gameOver_P.SetActive(true);
        Time.timeScale = 0;
    }

    public void geri_B()
    {
        SahneDegistirici.sahneDegis(2);
    }

    
    IEnumerator turkceSorulariGetir()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "turkceSorulari");


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/LearnWordsDepo/veritabani_islemleri.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                sorular = JsonUtility.FromJson<SorularList>(www.downloadHandler.text);
                oyunBasladimi = true;
                sorulariSormaSirasi(); //soruları sorulma sırası
                soruSor(soruNo);       // ilk soru sorma
            }
        }
    }

    void soruSor(int soruNo)  //soruları soru ekranında gösterebilmek için
    {
        
        birimSoruModel = sorular.butunSorular[sorularSirasi[soruNo]];

        soru_TMP.text = birimSoruModel.soru;
        A_T.text = birimSoruModel.a_Cevab;
        B_T.text = birimSoruModel.b_Cevab;
        C_T.text = birimSoruModel.c_Cevab;
        D_T.text = birimSoruModel.d_Cevab;

        zaman = birimSoruModel.saniye;
        zamanSlider.maxValue = birimSoruModel.saniye;
    }

    public void kontrolEt(int basilanCevap) //cevap doğrumu yanlışmı kontrol ediyor.
    {
        birimSoruModel = sorular.butunSorular[sorularSirasi[soruNo]];
        if (birimSoruModel.dogruCevab == basilanCevap)
        {
            Debug.Log("Doğru");
            sesKaynagi.PlayOneShot(dogruSesi); //doğru cevap verildiğinde doğru sesini 1 defa çal
            soruNo++;
            soruSor(soruNo);
        }
        else
        {
            Debug.Log("Yanlış");
            sesKaynagi.PlayOneShot(yanlisSesi); //yanlış cevap verildiğinde yanlış sesini 1 defa çal
            butonImages[basilanCevap - 1].color = yanlisRenk;
            butonImages[birimSoruModel.dogruCevab - 1].color = dogruRenk;
            gameOver();
        }
    }
}




