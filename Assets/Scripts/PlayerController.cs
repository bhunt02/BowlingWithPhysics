using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float maxChargeTime = 3;
    
    public UnityEvent<BallLaunchParameters> launchBall = new();
    
    private InputManager _inputManager;
    private Camera _mainCamera;
    private Rigidbody _rb;

    private bool _isCharging;
    private float _chargeTimeDelta;

    private Vector3 _initialPosition;
    
    private void Start()
    {
        _initialPosition = transform.position;
        
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        _inputManager = GameObject.Find("GameManager")?.GetComponent<InputManager>();
        if (_inputManager == null) return;
        _inputManager.onReset.AddListener(() =>
        {
            transform.position = _initialPosition;
            _isCharging = false;
            _chargeTimeDelta = 0;
        });
        _inputManager.onMovement.AddListener(OnMove);
        _inputManager.onSpaceDown.AddListener(() => _isCharging = true);
        _inputManager.onSpaceUp.AddListener(() =>
        {
            launchBall.Invoke(new BallLaunchParameters(_chargeTimeDelta, transform.forward));
            _chargeTimeDelta = 0;
            _isCharging = false;
        });
    }

    private void Update()
    {
        if (_isCharging) _chargeTimeDelta += Time.deltaTime;

        if (_mainCamera != null)
        {
            transform.forward = _mainCamera.transform.forward;
            transform.rotation = Quaternion.Euler(0,_mainCamera.transform.rotation.eulerAngles.y,0);
        }
    }
    
    
    
    void OnMove(List<MoveDirection> directions)
    {
        if (_rb == null) return;
        var moveVector = Vector3.zero;
        foreach (var direction in directions)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    moveVector += Vector3.left; //transform.right;
                    break;
                case MoveDirection.Right:
                    moveVector += Vector3.right; //transform.right;
                    break;
                /*
                case MoveDirection.Forward:
                    moveVector += transform.forward;
                    break;
                case MoveDirection.Back:
                    moveVector -= transform.forward;
                    break;
                */
            }
        }
        _rb.AddForce(new Vector3(moveVector.x,0,moveVector.z) * moveSpeed);
    }
    
    public bool IsCharging() => _isCharging;

    public float TimeCharging() => _chargeTimeDelta;
    
    public float MaxChargeTime() => maxChargeTime;
}