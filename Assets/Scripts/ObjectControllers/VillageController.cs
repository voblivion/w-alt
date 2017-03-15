using UnityEngine;
using System.Collections;

/// <summary>
/// A DecorController for noot's villages.
/// </summary>
public class VillageController : DecorController {

    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.Village, JsonUtility.ToJson(this));
    }
}
