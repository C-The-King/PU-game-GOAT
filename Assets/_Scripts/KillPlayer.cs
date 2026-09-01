using Unity.VisualScripting;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] int damage = 500;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IHealth>()?.TakeDamage(damage);

    }
}
