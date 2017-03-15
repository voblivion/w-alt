using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
    public GameObject target;
    public float speed = 0.5f;
    public int width = 2;
    public int height = 1;
    public GameObject tile;
    public Vector2 tileSize = new Vector2(8, 8);

    private GameObject[,] tiles;
    private Vector2 offset = new Vector2(0, 0);

    void Start() {
        tiles = new GameObject[ 2 * width + 1,  2 * height + 1];
        for(int i = - width; i < width + 1; ++i) {
            for(int j = - height;  j < height + 1; ++j) {
                tiles[i + width, j + height] = Instantiate(tile, transform.position, Quaternion.identity, transform) as GameObject;
            }
        }
    }

    void Update() {
        UpdateTilesPositions();

        Vector2 mid = tiles[width, height].transform.position;
        Vector2 pos = target.transform.position;
        if(pos.x < mid.x - tileSize.x / 2) {
            offset.x -= 1;
        }
        else if(mid.x + tileSize.x / 2 < pos.x) {
            offset.x += 1;
        }
        if(pos.y < mid.y - tileSize.y / 2) {
            offset.y -= 1;
        }
        else if(mid.y + tileSize.y / 2 < pos.y) {
            offset.y += 1;
        }
    }

    void UpdateTilesPositions() {
        Vector3 pos = target.transform.position;
        for(int i = - width; i < width + 1; ++i) {
            for(int j = - height; j < height + 1; ++j) {
                Vector3 tmp = pos * speed + new Vector3((i + offset.x) * tileSize.x, (j + offset.y) * tileSize.y, 0);
                tmp.z = transform.position.z;
                tiles[i + width, j + height].transform.position = tmp;
            }
        }
    }
}
