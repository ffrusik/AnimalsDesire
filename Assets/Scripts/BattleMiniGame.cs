using System.Collections;
using UnityEngine;
using TMPro;

public class BattleMiniGame : MonoBehaviour
{
    public GameObject lineOfSuccess1;
    public GameObject lineOfSuccess2;
    public GameObject battleCursor;
    public GameObject circleOutline;
    public GameObject circleInline;
    
    //public GameObject canvas;
    public TextMeshProUGUI whoseTurnText;

    public TextMeshProUGUI circleText1;
    public TextMeshProUGUI circleText2;

    private float rotationOfBattleCursor;
    private float rotationOfLineOfSuccess1;
    private float rotationOfLineOfSuccess2;

    private bool isBattleCursorMovingRight;

    public static float speedOfBattleCursor;
    public static float lengthBetweenLines;
    public static int countOfRollsConst;
    private int countOfRolls;
 
    private int countOfSuccessfulRolls;
    public static bool isAttacked;

    private bool startBattle;
    private float timer3Secs = 3;

    public static string nameOfAnimal;
    private int countOfSuccessfulRollsOfAnimal;
    private bool isAnimalTurn;
    public static float probabilityOfSuccessfulRollOfAnimal;
    private float posOfClickOfAnimal;
    private bool onceMakeProbability;

    public TextMeshProUGUI winOrLoseText;

    void Update()
    {
        if (isAttacked)
        {
            circleOutline.SetActive(true);
            circleInline.SetActive(true);

            isBattleCursorMovingRight = true;
            timer3Secs -= Time.deltaTime;
            Debug.Log(timer3Secs);
            circleText1.text = $"{Mathf.Round(timer3Secs + 0.49f)}";
            if (timer3Secs <= 0) startBattle = true;

            if (startBattle)
            {
                battleCursor.SetActive(true);
                lineOfSuccess1.SetActive(true);
                lineOfSuccess2.SetActive(true);

                isAnimalTurn = false;
                whoseTurnText.text = "Your turn";
                rotationOfBattleCursor = Random.Range(0, 359);
                PlaceLinesOfSuccess();
                countOfSuccessfulRolls = 0;
                countOfRolls = countOfRollsConst;
                isAttacked = false;
            }
        }

        if (isAnimalTurn == false && startBattle)
        {
            MoveAndCheckForBattleCursor();
            circleText1.text = $"{countOfSuccessfulRolls}/{countOfRollsConst}";
        }

        if (countOfRolls == 0 && !isAnimalTurn && startBattle)
        {
            Debug.Log(countOfSuccessfulRolls);
            countOfRolls = countOfRollsConst;
            CircleText1.showScore = true;

            isAnimalTurn = true;
            rotationOfBattleCursor = Random.Range(0, 359);
            PlaceLinesOfSuccess();
            countOfSuccessfulRollsOfAnimal = 0;
            onceMakeProbability = true;
        }

        // Animal's Turn
        if (isAnimalTurn == true && countOfRolls > 0 && startBattle)
        {
            circleText2.text = $"{countOfSuccessfulRollsOfAnimal}/{countOfRollsConst}";
            if (onceMakeProbability)
            {
                whoseTurnText.text = $"{nameOfAnimal}'s turn";

                float lengthOfProbableClick;
                float plusMinusLength;
                lengthOfProbableClick = lengthBetweenLines / probabilityOfSuccessfulRollOfAnimal;
                plusMinusLength = (lengthOfProbableClick - lengthBetweenLines) / 2;
                posOfClickOfAnimal = Random.Range(rotationOfLineOfSuccess1 - plusMinusLength, rotationOfLineOfSuccess2 + plusMinusLength);

                onceMakeProbability = false;
            }

            AttackOfAnimal();
        }

        if (countOfRolls == 0 && isAnimalTurn && startBattle)
        {
            Debug.Log(countOfSuccessfulRollsOfAnimal);
            circleText2.text = $"{countOfSuccessfulRollsOfAnimal}/{countOfRollsConst}";
            countOfRolls = countOfRollsConst;
            CircleText2.showScore = true;

            isAnimalTurn = false;

            onceMakeProbability = false;
            startBattle = false;
            timer3Secs = 0;
            battleCursor.SetActive(false);
            lineOfSuccess1.SetActive(false);
            lineOfSuccess2.SetActive(false);
            circleOutline.SetActive(false);
            circleInline.SetActive(false);

            if (countOfSuccessfulRolls > countOfSuccessfulRollsOfAnimal)
            {
                countOfSuccessfulRolls = 0;
                countOfSuccessfulRollsOfAnimal = 0;
                Debug.Log("You win");
                winOrLoseText.text = "You win";
                AttackedBy.isOver = true;
                //canvas.SetActive(false);
            }
            else if (countOfSuccessfulRolls < countOfSuccessfulRollsOfAnimal)
            {
                countOfSuccessfulRolls = 0;
                countOfSuccessfulRollsOfAnimal = 0;
                timer3Secs = 3;
                Debug.Log("You lose");
                winOrLoseText.text = "You lose";
                StartCoroutine(PlayIfTie());
                //canvas.SetActive(false);
            }
            else
            {
                countOfSuccessfulRolls = 0;
                countOfSuccessfulRollsOfAnimal = 0;
                timer3Secs = 3;
                Debug.Log("Tie");
                winOrLoseText.text = "Tie (Restarting...)";
                StartCoroutine(PlayIfTie());
            }
        }
    }

    IEnumerator PlayIfTie()
    {
        yield return new WaitForSeconds(1);

        winOrLoseText.text = "";
        circleText1.text = "";
        circleText2.text = "";
        CircleText1.goBackToPosition = true;
        CircleText2.goBackToPosition = true;

        isAttacked = true;
        StopCoroutine(PlayIfTie());
    }

    void PlaceLinesOfSuccess()
    {
        isBattleCursorMovingRight = !isBattleCursorMovingRight;
        if (isBattleCursorMovingRight)
        {
            rotationOfLineOfSuccess2 = Random.Range(rotationOfBattleCursor - 60, rotationOfBattleCursor - 225);
            rotationOfLineOfSuccess1 = rotationOfLineOfSuccess2 - lengthBetweenLines;
        }
        else
        {
            rotationOfLineOfSuccess1 = Random.Range(rotationOfBattleCursor + 60, rotationOfBattleCursor + 225);
            rotationOfLineOfSuccess2 = rotationOfLineOfSuccess1 + lengthBetweenLines;
        }

        lineOfSuccess1.transform.rotation = Quaternion.Euler(0, 0, rotationOfLineOfSuccess1);
        lineOfSuccess2.transform.rotation = Quaternion.Euler(0, 0, rotationOfLineOfSuccess2);
    }

    void MoveAndCheckForBattleCursor()
    {
        // Move
        if (isBattleCursorMovingRight)
        {
            battleCursor.transform.rotation = Quaternion.Euler(0, 0, rotationOfBattleCursor -= speedOfBattleCursor * Time.deltaTime);
        }
        else
        {
            battleCursor.transform.rotation = Quaternion.Euler(0, 0, rotationOfBattleCursor += speedOfBattleCursor * Time.deltaTime);
        }

        // Check if the battle cursor is behind the lines of success, if so, you lose and start new roll if it's not over
        if (((rotationOfBattleCursor > rotationOfLineOfSuccess1)
                && (rotationOfBattleCursor > rotationOfLineOfSuccess2) && !isBattleCursorMovingRight) 
                || ((rotationOfBattleCursor < rotationOfLineOfSuccess1)
                && (rotationOfBattleCursor < rotationOfLineOfSuccess2) && isBattleCursorMovingRight))
        {
            Debug.Log("Game Over");
            // isBattleCursorMovingRight = !isBattleCursorMovingRight;

            countOfRolls -= 1;

            if (countOfRolls > 0)
            {
                PlaceLinesOfSuccess();
            }
        }

        // Check if the battle cursor is in the right spot when clicked, also starting new roll if it's not over
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(rotationOfBattleCursor);
            Debug.Log(rotationOfLineOfSuccess1);
            Debug.Log(rotationOfLineOfSuccess2);

            // isBattleCursorMovingRight = !isBattleCursorMovingRight;

            if ((rotationOfBattleCursor >= rotationOfLineOfSuccess1)
                && (rotationOfBattleCursor <= rotationOfLineOfSuccess2))
            {
                Debug.Log("Nice");
                countOfRolls -= 1;
                countOfSuccessfulRolls += 1;
            }
            else
            {
                Debug.Log("Oops");
                countOfRolls -= 1;
            }

            while (rotationOfBattleCursor >= 360) rotationOfBattleCursor -= 360;
            while (rotationOfBattleCursor < 0) rotationOfBattleCursor += 360;

            if (countOfRolls > 0)
            {
                PlaceLinesOfSuccess();
            }
        }
    }

    void AttackOfAnimal()
    {
        // Move
        if (isBattleCursorMovingRight)
        {
            battleCursor.transform.rotation = Quaternion.Euler(0, 0, rotationOfBattleCursor -= speedOfBattleCursor * Time.deltaTime);
        }
        else
        {
            battleCursor.transform.rotation = Quaternion.Euler(0, 0, rotationOfBattleCursor += speedOfBattleCursor * Time.deltaTime);
        }

        // Check if the battle cursor is behind the lines of success, if so, you lose and start new roll if it's not over
        if (((rotationOfBattleCursor > rotationOfLineOfSuccess1)
                && (rotationOfBattleCursor > rotationOfLineOfSuccess2) && !isBattleCursorMovingRight)
                || ((rotationOfBattleCursor < rotationOfLineOfSuccess1)
                && (rotationOfBattleCursor < rotationOfLineOfSuccess2) && isBattleCursorMovingRight))
        {
            Debug.Log("Game Over");
            // isBattleCursorMovingRight = !isBattleCursorMovingRight;

            countOfRolls -= 1;

            if (countOfRolls > 0)
            {
                PlaceLinesOfSuccess();
            }

            onceMakeProbability = true;
        }

        // Check if the battle cursor is in the right spot when clicked, also starting new roll if it's not over
        if (Mathf.Abs(posOfClickOfAnimal - rotationOfBattleCursor) <= 0.5f)
        {
            Debug.Log(rotationOfBattleCursor);
            Debug.Log(rotationOfLineOfSuccess1);
            Debug.Log(rotationOfLineOfSuccess2);

            // isBattleCursorMovingRight = !isBattleCursorMovingRight;

            if ((rotationOfBattleCursor >= rotationOfLineOfSuccess1)
                && (rotationOfBattleCursor <= rotationOfLineOfSuccess2))
            {
                Debug.Log("Nice");
                countOfRolls -= 1;
                countOfSuccessfulRollsOfAnimal += 1;
            }
            else
            {
                Debug.Log("Oops");
                countOfRolls -= 1;
            }

            while (rotationOfBattleCursor >= 360) rotationOfBattleCursor -= 360;
            while (rotationOfBattleCursor < 0) rotationOfBattleCursor += 360;

            if (countOfRolls > 0)
            {
                PlaceLinesOfSuccess();
            }

            onceMakeProbability = true;
        }
    }
}
