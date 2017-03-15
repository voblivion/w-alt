using UnityEngine;
using System.Collections;

/// <summary>
/// A BlockController for dirt blocks.
/// </summary>
[ExecuteInEditMode]
public class DirtBlockController : BlockController {
    public override float hardness {
        get { return 0.6f; }
    }
    public override BlockItem loot {
        get { return new DirtBlockItem(1); }
    }

    // Wrapping
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.DirtBlock, JsonUtility.ToJson(this));
    }
}
