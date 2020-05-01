using System.Collections.Generic;
using UnityEngine;

// This class offers a facade for the Resources.Load method. It is used to load the icons from the Resources/icons/ subfolder
// It also caches the Sprites it creates. Please mind that the cache is never cleared. So any Sprite you loaded will remain in memory
// until the application closes.
public static class SpriteCache
{
    static private Dictionary<string, Sprite> cache = new Dictionary<string, Sprite>();

    /// <summary>
    /// Get a sprite, specified by identifier
    /// </summary>
    public static Sprite Get(string identifier)
    {
        if (!cache.ContainsKey(identifier))
        {
            Texture2D texture = Resources.Load<Texture2D>("icons/" + identifier);
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0f));
                cache[identifier] = sprite;
            }
            else
            {
                return null;
            }
        }
        return cache[identifier];
    }
}
