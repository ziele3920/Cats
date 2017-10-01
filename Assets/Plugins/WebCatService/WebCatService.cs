using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ziele3920.Cats
{
    public class WebCatService : MonoBehaviour
    {
        public event Action<Cat> NewCatReceived;
        public event Action<int> CatDownloadError;
        private readonly string defaultCatUrl = "http://smieszne-koty.herokuapp.com/api/kittens/";

        public void GetCat(int catID)
        {
            StartCoroutine(StartGetCatRequest(catID));
        }

        private IEnumerator StartGetCatRequest(int catID)
        {
            string url = defaultCatUrl + catID;
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Cat downloadedCat;
                try
                {
                    downloadedCat = Cat.CreateFromJson(www.downloadHandler.text);
                }
                catch(Exception ex)
                {
                    if (CatDownloadError != null)
                        CatDownloadError(catID);
                    yield break;
                }
                StartCoroutine(AppendImage(downloadedCat));
            }
        }

        private IEnumerator AppendImage(Cat downloadedCat)
        {
            WWW www = new WWW(downloadedCat.url);
            yield return www;

            if (www.texture == null)
            {
                if (CatDownloadError != null)
                    CatDownloadError(downloadedCat.id);
                yield break;
            }

            downloadedCat.AppendImage(www.texture);
            if (NewCatReceived != null)
                NewCatReceived(downloadedCat);
        }
    }
}
