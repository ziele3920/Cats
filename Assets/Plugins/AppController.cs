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
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
                ShowNewCat(catService.GetNextCat()); ;
        }

        private void ShowNewCat(Cat newCat)
        {
            if(currentCat != null)
                lastCat = currentCat;
            currentCat = newCat;
            imgController.SetImage(currentCat.image);
        }
    }
}
