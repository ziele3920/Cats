using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ziele3920.Cats
{
    public class WebCatService : MonoBehaviour
    {
        public event Action<Cat> FirstCatReceived;
        //private event Action<Cat> NewCatReceived;
        private event Action CatDownloadError;
        private readonly string defaultCatUrl = "http://smieszne-koty.herokuapp.com/api/kittens";
        private Queue<Cat> imagelessCats, cats;
        int currentPage = 1, minCatLength = 6;
        private bool waitingForFirstCat = true;

        public Cat GetNextCat()
        {
            if (cats.Count < minCatLength)
                DownloadNextCat();
            return cats.Dequeue();
        }

        public void VoteCatUp(int catID) {
            StartCoroutine(StartVoteCatUpRequest(catID));
        }

        public void VoteCatDown(int catID) {
            StartCoroutine(StartVoteDownRequest(catID));
        }

        private void DownloadNextCat()
        {
            if(imagelessCats.Count < minCatLength)
                GetCatList(currentPage++);
            if(imagelessCats.Count > 0)
                StartCoroutine(AppendImage(imagelessCats.Dequeue()));
        }

        private void Start()
        {
            imagelessCats = new Queue<Cat>();
            cats = new Queue<Cat>();
            CatDownloadError += DownloadNextCat;
            GetCatList(currentPage++);
        }

        private void AppendCat(Cat cat)
        {
            cats.Enqueue(cat);
            if (cats.Count < minCatLength)
                DownloadNextCat();
            if (waitingForFirstCat)
            {
                waitingForFirstCat = false;
                if (FirstCatReceived != null)
                    FirstCatReceived(cats.Dequeue());
            }
        }

        private void GetCatList(int page)
        {
            StartCoroutine(StartGetCatListRequest(page));
        }

        private IEnumerator StartVoteDownRequest(int catID)
        {
            string putS = defaultCatUrl + "/" + catID + "/votes";
            Debug.Log("Sending Delete request url: " + putS + " body: " + "dupa");
            UnityWebRequest www = UnityWebRequest.Delete(putS);
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Voted for cat " + catID);
            }
        }

        private IEnumerator StartVoteCatUpRequest(int catID) {
            string putS = defaultCatUrl + "/" + catID + "/votes";
            Debug.Log("Sending PUT request url: " + putS + " body: " + "dupa");
            UnityWebRequest www = UnityWebRequest.Put(putS, "dupa");
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) {
                Debug.LogError(www.error);
            }
            else {
                Debug.Log("Voted for cat " + catID);
            }
        }

        private IEnumerator StartGetCatListRequest(int page)
        {
            UnityWebRequest www = UnityWebRequest.Get(defaultCatUrl + "?page=" + page);
            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
            imagelessCats = GetCatQueue(www.downloadHandler.text);
            if(waitingForFirstCat)
                StartCoroutine(AppendImage(imagelessCats.Dequeue()));

        }

        private Queue<Cat> GetCatQueue(string text)
        {
            Queue<Cat> cats = new Queue<Cat>();
            if (text.Length < 5)
            {
                currentPage = 1;
                DownloadNextCat();
                return cats;
            }
            text = text.Remove(1, 1);
            text = text.Remove(text.Length - 2);
            string[] jsonCats = text.Split(new string[] { "}," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < jsonCats.Length; ++i)
            {
                try
                {
                    cats.Enqueue(Cat.CreateFromJson(jsonCats[i] + "}"));
                }
                catch
                {
                    continue;
                }
            }
            return cats;
        }

        private IEnumerator AppendImage(Cat downloadedCat)
        {
            WWW www = new WWW(downloadedCat.url);
            yield return www;

            if (www.texture == null)
            {
                if (CatDownloadError != null)
                    CatDownloadError();
                yield break;
            }

            downloadedCat.AppendImage(www.texture);
            AppendCat(downloadedCat);
        }

        private void OnDestroy()
        {
            CatDownloadError -= DownloadNextCat;
        }
    }
}
