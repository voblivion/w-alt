using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// A RelativeObjectController
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class NootController : RelativeObjectController {
    [SerializeField]
    private Vector2 w_villageRelativePosition;
    private Vector2 villagePosition;
    public bool arrived = false;
    public int side = 1;
    public float speed = 3;
    public float jumpForce = 50;
    public float minDistToVillage = 0.5f;
    
    private GroundChecker groundChecker;
    private Rigidbody2D rb;
    private Vector2 lastPos = Vector2.zero;
    private int roundsBlocked = 0;

    // Lifecycle
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        groundChecker = GetComponentInChildren<GroundChecker>();
        villagePosition = w_villageRelativePosition;

        // No gravity if noot arrived
        if(arrived) {
            rb.gravityScale = 0;
        }
    }
    void FixedUpdate() {
        if(!arrived) {
            // Check if arrived
            Vector2 diff = transform.position;
            diff -= villagePosition;
            if(diff.magnitude < minDistToVillage) {
                arrived = true;
                // FIXME tmp
                GameController game = FindObjectOfType<GameController>();
                --game.remaining;
                // FIXME end
                return;
            }

            // Check if block by wall
            LayerMask mask = Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Noot"));
            RaycastHit2D mid = Physics2D.Raycast(transform.position, new Vector2(side, 0), 0.5f, mask);
            RaycastHit2D up = Physics2D.Raycast(transform.position + new Vector3(0, 0.9f, 0), new Vector2(side, 0.9f), 1f, mask);
            if(mid.collider != null && groundChecker.isTriggered) {
                if(up.collider != null) {
                    side *= -1;
                }
                else {
                    jump();
                }
            }
            // FIXME tmp
            Color c1 = Color.white;
            if(mid.collider != null) c1 = Color.red;
            Color c2 = Color.white;
            if(up.collider != null) c2 = Color.red;
            Debug.DrawLine(transform.position, transform.position + new Vector3(0.5f*side, 0, 0), c1);
            Debug.DrawLine(transform.position + new Vector3(0, 0.9f, 0), transform.position + new Vector3(0.5f*side, 0.9f, 0), c2);
            // FIXME end

            // Check if immobile
            diff = transform.position;
            diff -= lastPos;
            if(diff.magnitude == 0) {
                ++roundsBlocked;
            }
            else {
                roundsBlocked = 0;
            }
            lastPos = transform.position;
            if(roundsBlocked > 10) {
                side *= -1;
                roundsBlocked = 0;
            }

            // Move
            Vector2 velocity = rb.velocity;
            velocity.x = side * speed;
            rb.velocity = velocity;
        }
    }

    // Methods
    public void jump() {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    // Wrapping
    public override void onBeforeWrap() {
        base.onBeforeWrap();
        villagePosition = transform.TransformPoint(w_villageRelativePosition);
    }
    public override void onAfterUnwrap() {
        base.onAfterUnwrap();
        w_villageRelativePosition = transform.InverseTransformPoint(villagePosition);
    }
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.Noot, JsonUtility.ToJson(this));
    }
}
