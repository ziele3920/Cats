using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ziele3920.Cats
{
    public class WebCatService : MonoBehaviour
    {
        private event Action<Cat> NewCatReceived;
        private event Action CatDownloadError;
        private readonly string defaultCatUrl = "http://smieszne-koty.herokuapp.com/api/kittens";
        private Queue<Cat> imagelessCats, cats;
        int currentPage = 1, minCatLength = 6;

        public Cat GetNextCat()
        {
            if (cats.Count < minCatLength)
                DownloadNextCat();
            return cats.Dequeue();
        }

        private void DownloadNextCat()
        {
            if(imagelessCats.Count < minCatLength)
                GetCatList(currentPage++);
            StartCoroutine(AppendImage(imagelessCats.Dequeue()));
        }

        private void Start()
        {
            imagelessCats = new Queue<Cat>();
            cats = new Queue<Cat>();
            NewCatReceived += AppendCat;
            CatDownloadError += DownloadNextCat;
            GetCatList(currentPage++);
        }

        private void AppendCat(Cat cat)
        {
            cats.Enqueue(cat);
            if (cats.Count < minCatLength)
                DownloadNextCat();
        }

        private void GetCatList(int page)
        {
            StartCoroutine(StartGetCatListRequest(page));
        }

        private IEnumerator StartGetCatListRequest(int page)
        {
            string url = defaultCatUrl;
            UnityWebRequest www = UnityWebRequest.Get(url + "?page=" + page);
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
            if(currentPage == 2)
                StartCoroutine(AppendImage(imagelessCats.Dequeue()));

        }

        private Queue<Cat> GetCatQueue(string text)
        {
            Queue<Cat> cats = new Queue<Cat>();
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
            if (NewCatReceived != null)
                NewCatReceived(downloadedCat);
        }

        private void OnDestroy()
        {
            NewCatReceived -= AppendCat;
            CatDownloadError -= DownloadNextCat;
        }
    }
}
