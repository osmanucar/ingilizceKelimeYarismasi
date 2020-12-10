using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetKontrol : MonoBehaviour
{
    public static bool internet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(internetDurum());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator internetDurum()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/LearnWordsDepo/netkontrol.txt", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error); // internet yoksa hatayı bastır
                internet = false;
            }
            else
            {
                internet = true;
                Debug.Log("İnternet Var"); 
            }
            
                      
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(internetDurum());
    }
}
