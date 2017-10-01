using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Dupa : MonoBehaviour
{

    public Image catImage;
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://smieszne-koty.herokuapp.com/api/kittens/4");
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}
 