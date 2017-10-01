using System;
using UnityEngine;

namespace ziele3920.Cats
{
    [RequireComponent(typeof(WebCatService))]
    public class AppController : MonoBehaviour
    {
        private WebCatService catService;
        private ImageController imgController;
        private Cat currentCat, lastCat;

        void Start()
        {
            imgController = FindObjectOfType<ImageController>();
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
