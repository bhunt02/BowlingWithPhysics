using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        if (rb == null || rb.transform.gameObject.name != "BowlingBall") return;
        
        var linearVelocityMagnitude = rb.linearVelocity.magnitude;
            
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        var xDiff = other.transform.position.x - transform.position.x;
        if (Mathf.Abs(xDiff) > 0.1f)
        {
            rb.AddForce(new Vector3(Mathf.Sign(xDiff) * -1, 0, 0) * linearVelocityMagnitude, ForceMode.Impulse);
        }
        rb.AddForce(transform.up * linearVelocityMagnitude, ForceMode.Impulse);
    }
}
