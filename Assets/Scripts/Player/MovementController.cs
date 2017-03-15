using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class MovementController : MonoBehaviour {
    // Parameters
    public float maxSpeed = 3;
    public float jumpForce = 200;
    //public float jumpSpeed = 8;
    public float maxJumpDelay = 0.1f;
    public float flightInertia = 0.975f;

    public GroundChecker groundChecker;
    public GroundChecker leftChecker;
    public GroundChecker rightChecker;
    public GroundChecker topChecker;


    private PlayerController player;
    private Rigidbody2D rb;
    public bool wantToJump = false;
    private float timeJumpAsked;
    public bool isGrounded = true;
    private BlockController diggingTarget = null;
    private float diggingTime = 0f;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        // Jumping
        UpdateJumping();

        // Digging
        UpdateDigging();
    }

    void FixedUpdate() {
        isGrounded = groundChecker.isTriggered;

        Vector2 velocity = rb.velocity;

        /* Jump
        if(isGrounded && wantToJump) {
            velocity.y = jumpSpeed;
            wantToJump = false;
        }*/

        // Walk
        float move = Input.GetAxis("Horizontal");
        if(isGrounded) {
            velocity.x = move * maxSpeed;
        }

        // Horizontal thrusts
        if(!isGrounded) {
            velocity.x = (velocity.x * flightInertia + move * maxSpeed * (1 - flightInertia));
        }


        if(velocity.x > 0 && rightChecker.isTriggered) {
            velocity.x = 0;
        }
        else if(velocity.x < 0 && leftChecker.isTriggered) {
            velocity.x = 0;
        }
        rb.velocity = velocity;

        // Building
        /*
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.DrawLine(transform.position, mousePosition);
        */
    }

    private void UpdateJumping() {
        // Jump request
        if(Input.GetButtonDown("Jump")) {
            wantToJump = true;
            timeJumpAsked = Time.time;
        }
        if(wantToJump && timeJumpAsked + maxJumpDelay < Time.time) {
            wantToJump = false;
        }

        // Jump
        if(isGrounded && wantToJump) {
            Vector2 velocity = rb.velocity;
            velocity.y = 0;
            rb.velocity = velocity;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            wantToJump = false;
        }
        /* Walljump
        else if(leftChecker.isTriggered && wantToJump) {
            rb.velocity = Vector2.zero;
            Vector2 force = new Vector2(0.8f, 0.6f);
            force *= jumpForce;
            rb.AddForce(force, ForceMode2D.Impulse);
            wantToJump = false;
        }
        else if(rightChecker.isTriggered && wantToJump) {
            rb.velocity = Vector2.zero;
            Vector2 force = new Vector2(-0.8f, 0.6f);
            force *= jumpForce;
            rb.AddForce(force, ForceMode2D.Impulse);
            wantToJump = false;
        }
        */
    }

    private void UpdateDigging() {
        // Dig only if drill attached
        if(player.drill == null) {
            return;
        }

        // Dig only if grounded and not moving
        Vector2 velocity = rb.velocity;
        if(!isGrounded || velocity.sqrMagnitude > 0) {
            diggingTarget = null;
            diggingTime = 0f;
            return;
        }

        // Select target
        BlockController target = null;
        if(Input.GetAxis("Vertical") < 0) {
            target = FindDiggingTarget(Vector2.down, groundChecker);
        }
        else if(Input.GetAxis("Horizontal") < 0) {
            target = FindDiggingTarget(Vector2.left, leftChecker);
        }
        else if(Input.GetAxis("Horizontal") > 0) {
            target = FindDiggingTarget(Vector2.right, rightChecker);
        }
        if(target != diggingTarget) {
            diggingTime = 0f;
            diggingTarget = target;
        }

        // Dig target
        if(diggingTarget != null) {
            diggingTime += Time.deltaTime * player.drill.efficiency;
            if(diggingTime >= diggingTarget.hardness) {
                Destroy(diggingTarget.gameObject);
                diggingTarget = null;
            }
        }
    }
    private BlockController FindDiggingTarget(Vector2 direction, GroundChecker checker) {
        Debug.DrawRay(transform.position, direction);
        string[] maskNames = new string[player.drill.layers.Length];
        for(int k = 0; k < player.drill.layers.Length; ++k) {
            maskNames[k] = player.drill.layers[k].ToString();
        }
        LayerMask blocksMask = LayerMask.GetMask(maskNames);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, blocksMask);
        BlockController target = null;
        if(hit.collider != null) {
            target = hit.collider.GetComponentInParent<BlockController>();
        }
        else {
            ICollection<Collider2D> triggers = checker.Triggers;
            foreach(Collider2D trigger in triggers) {
                if(trigger == null) continue;
                BlockController ctrl = trigger.GetComponentInParent<BlockController>();
                if(ctrl != null) {
                    target = ctrl;
                    if(ctrl == diggingTarget) {
                        break;
                    }
                }
            }
        }
        return target;
    }
}
