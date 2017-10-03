using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageController : MonoBehaviour
{

    private Image img;
    public int maxImageHeight;

    private void Start()
    {
        float scale = transform.root.localScale.x;
        transform.localScale *= 1 / scale;
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
        newWidth = (int)((float)maxImageHeight / img.sprite.texture.height * img.sprite.texture.width);
        newHeight = (int)((float)Screen.width / img.sprite.texture.width * img.sprite.texture.height);
        if (newHeight <= maxImageHeight)
            AdjustImageHorizontally(newHeight);
        else
            AdjustImageVerically(newWidth);
    }

    private void AdjustImageVerically(int newWidth)
    {
        TextureScale.Bilinear(img.sprite.texture, newWidth, maxImageHeight);
        img.sprite = Sprite.Create(img.sprite.texture, new Rect(0.0f, 0.0f, newWidth, maxImageHeight), new Vector2(0.5f, 0.5f));
        img.SetNativeSize();
    }

    private void AdjustImageHorizontally(int newHeight)
    {
        TextureScale.Bilinear(img.sprite.texture, Screen.width, newHeight);
        img.sprite = Sprite.Create(img.sprite.texture, new Rect(0.0f, 0.0f, Screen.width, newHeight), new Vector2(0.5f, 0.5f));
        img.SetNativeSize();
    }
}
