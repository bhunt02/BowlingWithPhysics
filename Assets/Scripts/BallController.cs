using Unity.VisualScripting;
using UnityEngine;

public record BallLaunchParameters
{
    public float DeltaTime = 0.0f;
    public Vector3 LaunchDirection = Vector3.zero;

    public BallLaunchParameters(float deltaTime, Vector3 launchDirection)
    {
        DeltaTime = deltaTime;
        LaunchDirection = launchDirection;
    }
}

public class BallController : MonoBehaviour
{ 
    [SerializeField] private float baseForce = 1f;
    [SerializeField] private Transform ballAnchor;
    
    private InputManager _inputManager;
    private PlayerController _player;
    private bool _foundPlayer;
    private Rigidbody _rb;
    private bool _isFired;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _rb = this.AddComponent<Rigidbody>();
        ResetBall();
        
        _player = GameObject.Find("Player")?.GetComponent<PlayerController>();
        if (_player!= null)
        {
            _player.launchBall.AddListener(LaunchBall);
        }
        
        _inputManager = GameObject.Find("GameManager")?.GetComponent<InputManager>();
        if (_inputManager == null) return;
        _inputManager.onReset.AddListener(ResetBall);
    }

    private void LaunchBall(BallLaunchParameters parameters)
    {
        if (_isFired) return;

        transform.parent = null;
        _rb.isKinematic = false;
        _isFired = true;
        _rb.AddForce(transform.forward * baseForce * parameters.DeltaTime, ForceMode.Impulse);
    }

    private void ResetBall()
    {
        _isFired = false;
        transform.parent = ballAnchor;
        transform.localPosition = Vector3.zero;
        transform.forward = ballAnchor.forward;
        _rb.isKinematic = true;
    }
    
    public bool IsLaunched() => _isFired;
}
