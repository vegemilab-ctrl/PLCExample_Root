using UnityEngine;

public class TriggerMove : MonoBehaviour
{
    public float moveSpeed = 1f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            //rb.linearVelocity = transform.right * moveSpeed;
            rb.MovePosition(rb.position + transform.right * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
