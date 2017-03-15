using UnityEngine;
using System.Collections;

/// <summary>
/// An ObjectController for object placed inside chunks.
/// </summary>
public abstract class RelativeObjectController : ObjectController {
    [SerializeField]
    private Vector3 w_relativePosition;

    public override void onBeforeWrap() {
        base.onBeforeWrap();
        w_relativePosition = transform.localPosition;
    }
    public override void onAfterUnwrap() {
        base.onAfterUnwrap();
        transform.localPosition = w_relativePosition;
    }
}
