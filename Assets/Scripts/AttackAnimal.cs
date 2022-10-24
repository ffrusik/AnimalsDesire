using UnityEngine;

public class AttackAnimal : MonoBehaviour
{
    public GameObject canvas;
    public string nameOfAnimal;
    public float speedOfCursor;
    public float lengthBetweenLines;
    public int countOfRolls;
    public float probabilityOfSuccessfulRollOfAnimal;

    private void Start()
    {
        // Attack();
    }

    void Attack()
    {
        Debug.Log("Attack");
        Debug.Log(canvas);

        Instantiate(canvas);

        AttackedBy.nameOfAnimal = nameOfAnimal;
        BattleMiniGame.nameOfAnimal = nameOfAnimal;
        BattleMiniGame.speedOfBattleCursor = speedOfCursor;
        BattleMiniGame.lengthBetweenLines = lengthBetweenLines;
        BattleMiniGame.countOfRollsConst = countOfRolls;
        BattleMiniGame.probabilityOfSuccessfulRollOfAnimal = probabilityOfSuccessfulRollOfAnimal;

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            MainCameraAnimation.shake = true;
            Attack();
            // StartCoroutine(Fight());
        }
    }

    /*
    IEnumerator Fight()
    {
        MainCameraAnimation.shake = true;
        yield return new WaitForSeconds(0f);
        Attack();
        StopCoroutine(Fight());
    }*/
}
