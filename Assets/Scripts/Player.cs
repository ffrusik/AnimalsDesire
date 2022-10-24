using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float horizontal;
    private float vertical;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Flip
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Animation
        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Equal speed when move diagonal
        float horizontalTemp = horizontal;
        float verticalTemp = vertical;
        if (Mathf.Abs(verticalTemp) == 1 && horizontal == 1) horizontal = 0.71f;
        if (Mathf.Abs(horizontalTemp) == 1 && vertical == 1) vertical = 0.71f;
        if (Mathf.Abs(verticalTemp) == 1 && horizontal == -1) horizontal = -0.71f;
        if (Mathf.Abs(horizontalTemp) == 1 && vertical == -1) vertical = -0.71f;

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
