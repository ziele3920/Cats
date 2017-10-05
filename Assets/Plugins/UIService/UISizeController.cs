using System;
using UnityEngine;
using ziele3920.Cats;

public class UISizeController : MonoBehaviour {

    public RectTransform lubeButton;
    public RectTransform neLubeButton;
    public RectTransform titleText;
    public RectTransform votesText;
    private int freeButtonSpace;
    private float titleFrac = 0.65f;

	void Start () {
        freeButtonSpace = 10;
        ResizeButtons();
        ResizeText();
	}

    private void ResizeText()
    {
        titleText.sizeDelta = new Vector2(Screen.width, Screen.height / AppController.upperTextFrac * titleFrac);
        votesText.sizeDelta = new Vector2(Screen.width, Screen.height / AppController.upperTextFrac * (1 - titleFrac));
        votesText.anchoredPosition = new Vector2(0, -titleText.sizeDelta.y);
    }

    private void ResizeButtons()
    {
        lubeButton.sizeDelta = new Vector2(Screen.width / 2 - freeButtonSpace / 2, Screen.height / AppController.lowerButtinsFrac);
        neLubeButton.sizeDelta = new Vector2(Screen.width / 2 - freeButtonSpace / 2, Screen.height / AppController.lowerButtinsFrac);
    }
}
