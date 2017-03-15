using UnityEngine;
using System.Collections;

/// <summary>
/// A BlockController for rock blocks.
/// </summary>
public class RockBlockController : BlockController {
    public override float hardness {
        get { return 2.1f; }
    }
    public override BlockItem loot {
        get { return new RockBlockItem(1); }
    }
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.RockBlock, JsonUtility.ToJson(this));
    }
}
