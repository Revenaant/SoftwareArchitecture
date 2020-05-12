namespace View
{
    using GXPEngine.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TextureCache
    {
        private Dictionary<string, Texture2D> iconCache;

        public TextureCache()
        {
            iconCache = new Dictionary<string, Texture2D>();
        }

        public void ClearCache()
        {
            iconCache.Clear();
        }

        public Texture2D GetCachedTexture(string filename)
        {
            if (!iconCache.ContainsKey(filename))
            {
                iconCache.Add(filename, new Texture2D("media/" + filename + ".png"));
            }
            return iconCache[filename];
        }

    }
}
