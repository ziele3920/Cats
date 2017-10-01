using System;
using UnityEngine;
using UnityEngine.UI;

namespace ziele3920.Cats
{
    [RequireComponent(typeof(WebCatService))]
    public class AppController : MonoBehaviour
    {
        public Text upperText;
        private WebCatService catService;
        private ImageController imgController;
        private Cat currentCat, lastCat;
        private int upperTextFrac = 10, lowerButtinsFrac = 10;

        void Start()
        {
            imgController = FindObjectOfType<ImageController>();
            float d = (100 - upperTextFrac - lowerButtinsFrac) / (float)100;
            imgController.imageHeight =(int) (Screen.height * d);
            Debug.Log(Screen.width + " " +Screen.height + " " + imgController.imageHeight);
            catService = GetComponent<WebCatService>();
            catService.NewCatReceived += OnCatReceived;
            catService.GetCat(4);
        }

        private void OnCatReceived(Cat receivedCat)
        {
            if(currentCat != null)
                lastCat = currentCat;
            currentCat = receivedCat;
            imgController.SetImage(currentCat.image);
        }

        private void OnDestroy()
        {
            catService.NewCatReceived -= OnCatReceived;
        }

    }
}
