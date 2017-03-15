using UnityEngine;
using System.Collections;

/// <summary>
/// A RelativeObjectController for any blocks.
/// </summary>
[ExecuteInEditMode]
public abstract class BlockController : RelativeObjectController {
    public bool breakable = true;
    public abstract float hardness { get; }
    public abstract BlockItem loot { get; }

    // Lifecycle
    protected virtual void Update() {
        SnapOnGrid();
    }
    
    // Methods
    public void SnapOnGrid() {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        transform.position = pos;
    }
}
