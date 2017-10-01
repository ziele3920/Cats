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
            FillDefaultImg();
            catService = GetComponent<WebCatService>();
            catService.NewCatReceived += OnCatReceived;
        }

        private void FillDefaultImg()
        {
            
        }

        private void OnCatReceived(Cat receivedCat)
        {
            if(currentCat != null)
                lastCat = currentCat;
            currentCat = receivedCat;
        }

        private void OnDestroy()
        {
            catService.NewCatReceived -= OnCatReceived;
        }

    }
}
