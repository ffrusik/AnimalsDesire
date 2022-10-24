using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyMiniGameCanvasWhenFinished : MonoBehaviour
{
    public static bool destroy = false;

    void Update()
    {
        if (destroy)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        destroy = false;
        SceneManager.LoadScene(1);
        // Destroy(gameObject);
    }
}
