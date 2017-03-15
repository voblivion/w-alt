using UnityEngine;
using System.Collections;

public class WOC {
    public int type;
    public string data;
}

public class CC {
    public int id;
    public string[] w_objectControllers;
}

public class NC {
    public Vector3 w_relativePosition;
}

public class GameEditorController : MonoBehaviour {
    public string json = "";
    public float r = 3;
    public float g = 4;
    public float b = 5;

    private ChunkController chunk;
    void Start() {
        if(json == "") {
            var v = new Vector3(1, 2, 3);
            var nc = new NC();
            nc.w_relativePosition = v;
            var wnc = new WrappedObjectController(WrappedObjectController.Type.Noot, JsonUtility.ToJson(nc));
            var cc = new CC();
            cc.id = 1;
            cc.w_objectControllers = new string[1];
            cc.w_objectControllers[0] = JsonUtility.ToJson(wnc);
            var wcc = new WOC();
            wcc.type = 1;
            wcc.data = JsonUtility.ToJson(cc);
            json = JsonUtility.ToJson(wcc);
        }
        WrappedObjectController w = JsonUtility.FromJson<WrappedObjectController>(json);
        var o = w.unwrap(this.transform);
        chunk = (ChunkController)o;
    }

    void Update() {
        var tmp = WrappedObjectController.wrap(chunk);
        if(json != tmp) {
            json = tmp;
        }

        r = Mathf.Round(Mathf.Max(0, Mathf.Min(r, 11)) * 10) / 10;
        g = Mathf.Round(Mathf.Max(0, Mathf.Min(g, 11)) * 10) / 10;
        b = Mathf.Round(Mathf.Max(0, Mathf.Min(b, 11)) * 10) / 10;
        MultiSpriteManager.Instance.UpdateSprites(r, g, b);
    }
}
