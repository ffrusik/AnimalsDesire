using UnityEngine;

public class MainCameraAnimation : MonoBehaviour
{
    public Animator animator;

    public static bool ZoomIn = false;
    public static bool ZoomOut = false;
    public static bool shake = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("ZoomIn", ZoomIn);
        animator.SetBool("ZoomOut", ZoomOut);
        animator.SetBool("Shake", shake);
        shake = false;
    }
}
