using UnityEngine;

namespace ziele3920.Cats
{
    [System.Serializable]
    public class Cat
    {
        public int id;
        public string name;
        public string url;
        public int vote_count;
        public Sprite image;

        public static Cat CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<Cat>(jsonString);
        }

        public void AppendImage(Texture2D tex)
        {
            image = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        } 

    }
}
