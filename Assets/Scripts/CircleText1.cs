using UnityEngine;

public class CircleText1 : MonoBehaviour
{
    public Animator animator;

    public static bool showScore = false;
    public static bool goBackToPosition = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (showScore)
        {
            animator.SetBool("ShowScore", showScore);
            showScore = false;
        }

        if (goBackToPosition)
        {
            gameObject.transform.position = new Vector3(0, -126.9999f, 0);
            goBackToPosition = false;
        }
    }
}
