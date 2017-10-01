using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageController : MonoBehaviour {

    private Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void SetImage(Sprite sprite)
    {
        img.sprite = sprite;
        img.SetNativeSize();
    }
}
