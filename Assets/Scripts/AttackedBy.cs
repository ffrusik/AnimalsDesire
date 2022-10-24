using UnityEngine;
using TMPro;

public class AttackedBy : MonoBehaviour
{
    public TextMeshProUGUI nameOfAnimalText;
    public static string nameOfAnimal;

    public bool isNamed = false;

    private GameObject player;
    private Vector3 playerVector3;
    public static bool isOver = false;

    private void Start()
    {
        if (!isNamed && nameOfAnimal != null)
        {
            nameOfAnimalText.text += nameOfAnimal;
            isNamed = true;
        }

        player = GameObject.FindWithTag("Player");
        playerVector3 = player.transform.position;
        player.transform.position = new Vector3(9999, 0, 0);
    }

    private void Update()
    {
        if (isOver && Input.GetKeyDown(KeyCode.Space))
        {
            TransferPlayerAndDestroyCanvas();
        }
    }

    public void TransferPlayerAndDestroyCanvas()
    {
        GameObject.FindWithTag("Player").transform.position = playerVector3;
        isOver = false;
        DestroyMiniGameCanvasWhenFinished.destroy = true;
    }
}