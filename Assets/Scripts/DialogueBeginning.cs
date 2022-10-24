using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueBeginning : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject rabbit;

    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI whoIsTalkingTextComponent;

    public bool beginImmediately;
    public string[] lines;
    public string[] whoIsTalking;
    public float[] pausesForLines;
    public float textSpeed;
    
    private bool pause;
    private int index;
    private bool isStarted;

    void Start()
    {
        if (beginImmediately)
        {
            dialogueBox.SetActive(true);
            isStarted = true;
            pause = false;
            textComponent.text = "";
            StartDialogue();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pause && isStarted)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        whoIsTalkingTextComponent.text = whoIsTalking[index];

        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = "";
            
            StartCoroutine(PauseLine(pausesForLines[index])); // After that coroutine TypeLine will be proceeded
        }
        else
        {
            RabbitCutScene.jump = true;
            MainCameraAnimation.ZoomOut = true;
            MainCameraAnimation.ZoomIn = false;
            isStarted = false;
            dialogueBox.SetActive(false);
        }
    }

    IEnumerator PauseLine(float time)
    {
        pause = true;

        if (time != 0)
        {
            dialogueBox.SetActive(false);

            if (index == 5)
            {
                Debug.Log("ZoomIn");
                MainCameraAnimation.ZoomIn = true;
                MainCameraAnimation.ZoomOut = false;
                RabbitCutScene.animate = true;
                RabbitCutScene.jumpAndHideLeft = true;
                RabbitCutScene.jumpAndHideRight = false;
            }

            if (index == 9)
            {
                Debug.Log("Rabbits");
                MainCameraAnimation.ZoomIn = true;
                MainCameraAnimation.ZoomOut = false;
                RabbitCutScene.animate = true;
                RabbitCutScene.jumpAndHideLeft = false;
                RabbitCutScene.jumpAndHideRight = true;

                StartCoroutine(CreateRabbits(2));
            }

            yield return new WaitForSeconds(time);
            dialogueBox.SetActive(true);
        }

        if (index == 6)
        {
            Debug.Log("ZoomOut");
            MainCameraAnimation.ZoomOut = true;
            MainCameraAnimation.ZoomIn = false;
        }

        pause = false;
        StartCoroutine(TypeLine());
    }

    IEnumerator CreateRabbits(float time)
    {
        GameObject rab1;
        GameObject rab2;
        GameObject rab3;
        GameObject rab4;

        yield return new WaitForSeconds(time);
        rab1 = Instantiate(
            rabbit,
            new Vector3(0.8f, 0.65f, 0),
            Quaternion.identity);

        rab2 = Instantiate(
            rabbit,
            new Vector3(1.85f, 0.6f, 0),
            Quaternion.identity);

        rab3 = Instantiate(
            rabbit,
            new Vector3(-0.9f, 0.65f, 0),
            Quaternion.identity);

        rab4 = Instantiate(
            rabbit,
            new Vector3(-1.85f, 0.6f, 0),
            Quaternion.identity);

        StopCoroutine(CreateRabbits(2));
    }
}