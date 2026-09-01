using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed);
    }
}
