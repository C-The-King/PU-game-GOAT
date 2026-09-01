using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private int points = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            ScoreSystem.instance.AddScore(points);
            Destroy(gameObject);
        }
    }
}
