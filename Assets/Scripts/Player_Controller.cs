using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float moveSpeed = 3f;
    private Vector2 movement;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(GameManager.Instance != null && GameManager.Instance.returningFromBattle){
        transform.position = GameManager.Instance.playerPosition;
        GameManager.Instance.returningFromBattle = false;
        }

        if (rb == null) Debug.LogError("No Rigidbody2D found on player!");
        if (animator == null) Debug.LogError("No Animator found on player!");
    }

    void Update()
    {
        // Store input each frame
        movement = Vector2.zero;

        if (Input.GetKey("w")) movement.y = 1f;
        if (Input.GetKey("s")) movement.y = -1f;
        if (Input.GetKey("a")) movement.x = -1f;
        if (Input.GetKey("d")) movement.x = 1f;

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        // Debug to confirm input is being read
        if (movement != Vector2.zero)
            Debug.Log("Moving: " + movement);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
    //encounter enemies (on trigger only)
    void OnTriggerStay2D(Collider2D other)
{
    if (other.CompareTag("EncounterZone"))
    {
        //Debug.Log(Scene_Manager.Instance);
        GameManager.Instance.playerPosition = transform.position;

        GameManager.Instance.TryEncounter();
    }
}

}