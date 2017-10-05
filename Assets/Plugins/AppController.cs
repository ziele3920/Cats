using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ziele3920.Cats
{
    [RequireComponent(typeof(WebCatService))]
    public class AppController : MonoBehaviour
    {
        public Text upperText, votes;
        public Button lubeButton, neLubeButton;
        private WebCatService catService;
        private ImageController imgController;
        private Cat currentCat, lastCat;
        public static readonly int upperTextFrac = 10, lowerButtinsFrac = 10;


        void Start()
        {
            imgController = FindObjectOfType<ImageController>();
            float d = (100 - upperTextFrac - lowerButtinsFrac) / (float)100;
            imgController.maxImageHeight =(int) (Screen.height * d);
            Debug.Log(Screen.width + " " +Screen.height + " " + imgController.maxImageHeight);
            catService = GetComponent<WebCatService>();
            catService.FirstCatReceived += ShowNewCat;
            lubeButton.onClick.AddListener(LubeClicked);
            neLubeButton.onClick.AddListener(NeLubeClicked);
        }

        private void NeLubeClicked()
        {
            catService.VoteCatUp(currentCat.id);
            ShowNewCat(catService.GetNextCat());
        }

        private void LubeClicked()
        {
            catService.VoteCatDown(currentCat.id);
            ShowNewCat(catService.GetNextCat());
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
                ShowNewCat(catService.GetNextCat());
        }

        private void ShowNewCat(Cat newCat)
        {
            if(currentCat != null)
                lastCat = currentCat;
            currentCat = newCat;
            imgController.SetImage(currentCat.image);
            upperText.text = newCat.name;
            votes.text = "ma " + newCat.vote_count.ToString() + " punktóf";
        }
    }
}
