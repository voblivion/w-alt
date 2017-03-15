using UnityEngine;
using System.Collections;

[ExecuteInEditMode, RequireComponent(typeof(SpriteRenderer))]
public class MultiSpriteController : MonoBehaviour {
    private SpriteRenderer sr;

    public string spriteName;
    public SpriteRenderer SpriteRenderer {
        get { return sr; }
    }

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = MultiSpriteManager.Instance.GetSprite(spriteName);
    }
}
