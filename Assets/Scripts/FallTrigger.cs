using UnityEngine;
using UnityEngine.Events;

public class FallTrigger : MonoBehaviour
{
    public UnityEvent onPinFall = new ();

    private bool _isPinFallen = false;
    
    public bool IsPinFallen() => _isPinFallen;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground") || _isPinFallen) return;
        _isPinFallen = true;
        onPinFall.Invoke();
    }
}
