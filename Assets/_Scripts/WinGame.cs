using UnityEngine;

public class WinGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MenuController.Instance.WinGame(ScoreSystem.instance.GetScorePercentage());
    }
}
