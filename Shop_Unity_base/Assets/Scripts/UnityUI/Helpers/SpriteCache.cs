using System.Collections.Generic;
using UnityEngine;

// This class offers a facade for the Resources.Load method. It is used to load the icons from the Resources/icons/ subfolder
// It also caches the Sprites it creates. Please mind that the cache is never cleared. So any Sprite you loaded will remain in memory
// until the application closes.
public static class SpriteCache
{
    static private Dictionary<string, Sprite> filenameToSprite = new Dictionary<string, Sprite>();

    /// <summary>
    /// Get a sprite, specified by the name of the file 
    /// </summary>
    public static Sprite Get(string filename)
    {
        if (!filenameToSprite.ContainsKey(filename))
        {
            Texture2D texture = Resources.Load<Texture2D>("icons/" + filename);
            if (texture == null)
                return null;

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0f));
            filenameToSprite.Add(filename, sprite);
        }

        return filenameToSprite[filename];
    }
}
