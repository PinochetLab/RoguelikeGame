using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Texture;

/// <summary>
/// Класс, предназначенный для загрузки текстур. Текстура загружается только один раз.
/// </summary>
public static class TextureMaster
{
    private static readonly Dictionary<string, Texture2D> Textures = new();

    /// <summary>
    /// Метод для получения текстуры по названию.
    /// </summary>
    public static Texture2D GetTexture(string path)
    {
        if (!Textures.ContainsKey(path))
        {
            Textures.Add(path, RoguelikeGame.Instance.LoadTexture(path));
        }
        return Textures[path];
    }
}