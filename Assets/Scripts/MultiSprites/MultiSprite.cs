using UnityEngine;
using System.Collections;

public class MultiSprite {
    // Attributes
    Sprite sprite;
    int size;
    int depth;

    float[,] colors;
    float[,] alphas;

    /** Creates a new 16 channel's MultiSprite.
     */
    public MultiSprite(Texture2D[] textures, int x, int y, int width, int height, int ppu = 16) {
        // Collect all colors and alphas
        size = width * height;
        depth = 3 * textures.Length;
        colors = new float[size, depth];
        alphas = new float[size, textures.Length];
        for(int k = 0; k < textures.Length; ++k) {
            Color[] textureColors = textures[k].GetPixels(x, y, width, height);

            for(int p = 0; p < size; ++p) {
                colors[p, k * 3 + 0] = textureColors[p].r;
                colors[p, k * 3 + 1] = textureColors[p].g;
                colors[p, k * 3 + 2] = textureColors[p].b;
                alphas[p, k] = textureColors[p].a;
            }
        }

        // Initialize Sprite
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;
        sprite = Sprite.Create(texture, new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f), ppu);
    }

    /**  The underlying sprite
     */
    public Sprite Sprite {
        get {
            return sprite;
        }
    }

    /** Update sprite according to given expected pixels.
     */
    public void Update(float r, float g, float b) {
        // Compute factors
        int r1 = Mathf.FloorToInt(r);
        int r2 = Mathf.CeilToInt(r);
        float tr = r - r1;
        int ar1 = Mathf.FloorToInt(r1 / 3);
        int ar2 = Mathf.FloorToInt(r2 / 3);
        int g1 = Mathf.FloorToInt(g);
        int g2 = Mathf.CeilToInt(g);
        float tg = g - g1;
        int ag1 = Mathf.FloorToInt(g1 / 3);
        int ag2 = Mathf.FloorToInt(g2 / 3);
        int b1 = Mathf.FloorToInt(b);
        int b2 = Mathf.CeilToInt(b);
        float tb = b - b1;
        int ab1 = Mathf.FloorToInt(b1 / 3);
        int ab2 = Mathf.FloorToInt(b2 / 3);
        // Compute new colors
        Color[] pixels = new Color[size];
        for(int i = 0; i < size; ++i) {
            r = r1 < 0 ? 0 : colors[i, r1] * (1 - tr) + colors[i, r2] * tr;
            g = g1 < 0 ? 0 : colors[i, g1] * (1 - tg) + colors[i, g2] * tg;
            b = b1 < 0 ? 0 : colors[i, b1] * (1 - tb) + colors[i, b2] * tb;
            float ttr = r1 < 0 ? 0 : alphas[i, ar1] * (1 - tr) + alphas[i, ar2] * tr;
            float ttg = g1 < 0 ? 0 : alphas[i, ag1] * (1 - tg) + alphas[i, ag2] * tg;
            float ttb = b1 < 0 ? 0 : alphas[i, ab1] * (1 - tb) + alphas[i, ab2] * tb;
            pixels[i] = new Color(r, g, b, (ttr + ttg + ttb) / 3);
        }
        // Update sprite's colors
        sprite.texture.SetPixels(pixels);
        sprite.texture.Apply();
    }

    /** Split Color's array into float's matrix.
     */
    float[,] SplitColors(Color[] colors) {
        float[,] splitedColors = new float[colors.Length, 4];
        for(int i = 0; i < colors.Length; ++i) {
            splitedColors[i, 0] = colors[i].r;
            splitedColors[i, 1] = colors[i].g;
            splitedColors[i, 2] = colors[i].b;
            splitedColors[i, 3] = colors[i].a;
        }
        return splitedColors;
    }

    /** Merge float's matrix into Color's array.
     */
    Color[] MergeColors(float[,] colors) {
        Color[] mergedColors = new Color[colors.GetLength(0)];
        for(int i = 0; i < colors.GetLength(0); ++i) {
            mergedColors[i] = new Color(
                colors[i, 0],
                colors[i, 1],
                colors[i, 2],
                colors[i, 3]);
        }
        return mergedColors;
    }
}
