using UnityEngine;

public class RabbitCutScene : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    public static bool jumpAndHideLeft = false;
    public static bool jumpAndHideRight = false;
    public static bool jump = false;
    public static bool animate = false;
    public static bool localScaleReverse = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (jumpAndHideLeft && !jumpAndHideRight && animate)
        {
            animator.Play("Rabbit_Jump&HideLeft");
            animate = false;
        }

        if (jumpAndHideRight && !jumpAndHideLeft && animate)
        {
            animator.Play("Rabbit_Jump&HideRight");
            animate = false;
        }
        
        if (jump == true)
        {
            animator.SetBool("isRunning", true);
            
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            Run();
        }
    }

    private void LateUpdate()
    {
        if (transform.position == new Vector3(-0.9f, 0.65f, 0) || transform.position == new Vector3(-1.85f, 0.6f, 0))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Run()
    {
        transform.Translate(0, -0.03f, 0); 
    }
}
