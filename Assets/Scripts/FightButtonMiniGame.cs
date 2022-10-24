using UnityEngine;
using UnityEngine.UI;

public class FightButtonMiniGame : MonoBehaviour
{
    public Button fightButton;

    private void Start()
    {
        Button btn = fightButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        BattleMiniGame.isAttacked = true;
        Debug.Log("FIGHT");
    }
}
