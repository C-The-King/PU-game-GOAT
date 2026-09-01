using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    Vector3 spawnPoint = Vector3.zero;
    private void Awake()
    {
        if (startPoint != null)
        {
            spawnPoint = startPoint.position;
        }
    }
    public void SetPlayerRespawn(Vector3 point)
    {
        spawnPoint = point;
    }

    public void RewpawnPlayer()
    {
        transform.position = spawnPoint;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
