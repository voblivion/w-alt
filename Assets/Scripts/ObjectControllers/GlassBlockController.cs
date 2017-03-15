using UnityEngine;
using System.Collections;

/// <summary>
/// A BlockController for glass blocks.
/// </summary>
[ExecuteInEditMode]
public class GlassBlockController : BlockController {
    public override float hardness {
        get { return 0.6f; }
    }
    public override BlockItem loot {
        get { return new GlassBlockItem(1); }
    }

    // Wrapping
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.GlassBlock, JsonUtility.ToJson(this));
    }
}
