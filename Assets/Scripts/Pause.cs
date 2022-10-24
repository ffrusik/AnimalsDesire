using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPause = false;

    private void Update()
    {
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
