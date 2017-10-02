using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageController : MonoBehaviour {

    private Image img;
    public int imageHeight;

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
        int newWidth, newHeight;
        newWidth = (int)((float)imageHeight / img.sprite.texture.height * img.sprite.texture.width);
        newHeight = (int)((float)Screen.width / img.sprite.texture.width * img.sprite.texture.height);
        if (newHeight <= imageHeight)
            AdjustImageHorizontally();
        else
            AdjustImageVerically();
    }

    private void AdjustImageVerically()
    {
        TextureScale.Bilinear(img.sprite.texture, img.sprite.texture.width / (img.sprite.texture.height / imageHeight), imageHeight);
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
