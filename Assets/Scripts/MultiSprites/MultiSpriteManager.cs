using UnityEngine;
using System.Collections.Generic;

public class MultiSpriteManager {
    private Dictionary<string, MultiSprite> multiSprites;
    Texture2D[] blocks;
    Texture2D[] villages;
    Texture2D[] noots;
    Texture2D[] backgs;
    Texture2D[] walters;

    // Singleton
    private static MultiSpriteManager instance;
    public static void DeleteInstance() {
        instance = null;
    }
    public static MultiSpriteManager Instance {
        get {
            if(instance == null) {
                instance = new MultiSpriteManager();
            }
            return instance;
        }
    }
    private MultiSpriteManager() {
        multiSprites = new Dictionary<string, MultiSprite>();
        blocks = new Texture2D[4];
        blocks[0] = Resources.Load("Sprites/Blocks/uv") as Texture2D;
        blocks[1] = Resources.Load("Sprites/Blocks/rgb") as Texture2D;
        blocks[2] = Resources.Load("Sprites/Blocks/ir") as Texture2D;
        blocks[3] = Resources.Load("Sprites/Blocks/special") as Texture2D;
        villages = new Texture2D[4];
        villages[0] = Resources.Load("Sprites/Village/uv") as Texture2D;
        villages[1] = Resources.Load("Sprites/Village/rgb") as Texture2D;
        villages[2] = Resources.Load("Sprites/Village/ir") as Texture2D;
        villages[3] = Resources.Load("Sprites/Village/special") as Texture2D;
        noots = new Texture2D[4];
        noots[0] = Resources.Load("Sprites/Noot/uv") as Texture2D;
        noots[1] = Resources.Load("Sprites/Noot/rgb") as Texture2D;
        noots[2] = Resources.Load("Sprites/Noot/ir") as Texture2D;
        noots[3] = Resources.Load("Sprites/Noot/special") as Texture2D;
        backgs = new Texture2D[4];
        backgs[0] = Resources.Load("Sprites/Backgrounds/uv") as Texture2D;
        backgs[1] = Resources.Load("Sprites/Backgrounds/rgb") as Texture2D;
        backgs[2] = Resources.Load("Sprites/Backgrounds/ir") as Texture2D;
        backgs[3] = Resources.Load("Sprites/Backgrounds/special") as Texture2D;
        walters = new Texture2D[4];
        walters[0] = Resources.Load("Sprites/Walter/uv") as Texture2D;
        walters[1] = Resources.Load("Sprites/Walter/rgb") as Texture2D;
        walters[2] = Resources.Load("Sprites/Walter/ir") as Texture2D;
        walters[3] = Resources.Load("Sprites/Walter/special") as Texture2D;

        // Register all multi sprites
        MultiSprite dirt = new MultiSprite(blocks, 0, 16, 16, 16);
        multiSprites.Add("dirt", dirt);
        MultiSprite water = new MultiSprite(blocks, 16, 16, 16, 16);
        multiSprites.Add("water", water);
        MultiSprite rock = new MultiSprite(blocks, 0, 0, 16, 16);
        multiSprites.Add("rock", rock);
        MultiSprite brick = new MultiSprite(blocks, 16, 0, 16, 16);
        multiSprites.Add("brick", brick);
        MultiSprite glass = new MultiSprite(blocks, 32, 16, 16, 16);
        multiSprites.Add("glass", glass);
        MultiSprite bedrock = new MultiSprite(blocks, 32, 0, 16, 16);
        multiSprites.Add("bedrock", bedrock);
        MultiSprite village = new MultiSprite(villages, 0, 0, 16, 16);
        multiSprites.Add("village", village);
        MultiSprite noot = new MultiSprite(noots, 0, 0, 8, 8);
        multiSprites.Add("noot", noot);
        MultiSprite space = new MultiSprite(backgs, 0, 0, 256, 256, 32);
        multiSprites.Add("space", space);
        MultiSprite clouds = new MultiSprite(backgs, 0, 256, 256, 256, 32);
        multiSprites.Add("clouds", clouds);
        MultiSprite walter = new MultiSprite(walters, 0, 0, 32, 32, 32);
        multiSprites.Add("walter", walter);
    }

    public Sprite GetSprite(string name) {
        return multiSprites[name].Sprite;
    }

    public MultiSprite GetMultiSprite(string name) {
        return multiSprites[name];
    }
    
    public void UpdateSprites(float r, float g, float b) {
        foreach(KeyValuePair<string, MultiSprite> pair in multiSprites) {
            pair.Value.Update(r, g, b);
        }
    }
}
