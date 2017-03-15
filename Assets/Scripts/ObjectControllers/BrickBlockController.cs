using UnityEngine;
using System.Collections;

/// <summary>
/// A BlockController for dirt blocks.
/// </summary>
[ExecuteInEditMode]
public class BrickBlockController : BlockController {
    public override float hardness {
        get { return 0.6f; }
    }
    public override BlockItem loot {
        get { return new BrickBlockItem(1); }
    }

    // Wrapping
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.BrickBlock, JsonUtility.ToJson(this));
    }
}
