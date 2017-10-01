using System;
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
        AdjustImageSize();
    }

    private void AdjustImageSize()
    {
        if (img.sprite.texture.width / img.sprite.texture.height >= Screen.width / Screen.height)
            AdjustImageHorizontally();
        else
            AdjustImageVerically();
    }

    private void AdjustImageVerically()
    {
        TextureScale.Bilinear(img.sprite.texture, img.sprite.texture.width / (img.sprite.texture.height / Screen.height), Screen.height);
        img.sprite = Sprite.Create(img.sprite.texture, new Rect(0.0f, 0.0f, img.sprite.texture.width, img.sprite.texture.height), new Vector2(0.5f, 0.5f));
        img.SetNativeSize();
    }

    private void AdjustImageHorizontally()
    {
        TextureScale.Bilinear(img.sprite.texture, Screen.width, img.sprite.texture.height / (img.sprite.texture.width / Screen.width));
        img.sprite = Sprite.Create(img.sprite.texture, new Rect(0.0f, 0.0f, img.sprite.texture.width, img.sprite.texture.height), new Vector2(0.5f, 0.5f));
        img.SetNativeSize();
    }
}
