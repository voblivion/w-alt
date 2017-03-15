using UnityEngine;
using System.Collections;

/// <summary>
/// A BlockController for dirt blocks.
/// </summary>
[ExecuteInEditMode]
public class BedrockBlockController : BlockController {
    public override float hardness {
        get { return 0.6f; }
    }
    public override BlockItem loot {
        get { return new BedrockBlockItem(1); }
    }

    // Lifecycle
    void Start() {
        breakable = false;
    }

    // Wrapping
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.BedrockBlock, JsonUtility.ToJson(this));
    }
}
