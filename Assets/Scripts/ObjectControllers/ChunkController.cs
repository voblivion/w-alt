using UnityEngine;
using System.Collections;

/// <summary>
/// An ObjectController for grouping objects into chunks.
/// </summary>
public class ChunkController : ObjectController {
    // Attributes / Parameters
    public int id = 0;
    [SerializeField]
    private string[] w_objectControllers;

    // Static members
    public static Vector2 size = new Vector2(32, 32);
    public static string nameFromId(int id) {
        return id < 0 ? "n" + (-id) : "p" + id; ;
    }
    
    // Serialization
    public override void onBeforeWrap() {
        base.onBeforeWrap();
        RelativeObjectController[] ctrls = GetComponentsInChildren<RelativeObjectController>();
        w_objectControllers = new string[ctrls.Length];
        for(int k = 0; k < ctrls.Length; ++k) {
            w_objectControllers[k] = WrappedObjectController.wrap(ctrls[k]);
        }
    }
    public override void onAfterUnwrap() {
        base.onAfterUnwrap();
        foreach(string wrappedObjectController in w_objectControllers) {
            WrappedObjectController.unwrap(wrappedObjectController, transform);
        }
        transform.position = new Vector2(size.x * id, 0);
    }
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        
        return new WrappedObjectController(WrappedObjectController.Type.Chunk, JsonUtility.ToJson(this));
    }
}
