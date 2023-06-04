using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Texture
{
    public static class TextureLoader
    {
        private static Dictionary<string, Texture2D> textures = new();

        public static Texture2D LoadTexture(string path)
        {
            if (!textures.ContainsKey(path))
            {
                textures.Add(path, RoguelikeGame.instance.LoadTexture(path));
            }
            return textures[path];
        }
    }
}
