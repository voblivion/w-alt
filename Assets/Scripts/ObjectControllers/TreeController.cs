using UnityEngine;
using System.Collections;

/// <summary>
/// A DecorController for trees.
/// </summary>
public class TreeController : DecorController {

    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.Tree, JsonUtility.ToJson(this));
    }
}
