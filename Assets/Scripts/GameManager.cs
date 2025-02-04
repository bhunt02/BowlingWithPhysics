
using UnityEngine;
using UnityEngine.Events;

public class GameManager: MonoBehaviour
{
    [SerializeField] private int score = 0;

    private InputManager _inputManager;
    public UnityEvent<int> onScoreChanged = new();
    private Vector3 _pinsPosition = new (0, 0.5f, 19);
    private GameObject _currentPins = null;
    private FallTrigger[] _fallTriggers;
    
    private void Start()
    {
        ResetGame();
        _inputManager = transform.GetComponent<InputManager>();
        if (_inputManager == null) 
            return;
        _inputManager.onReset.AddListener(ResetGame);
    }

    private void ResetGame()
    {
        if (_currentPins != null)
        {
            Destroy(_currentPins);
        }
        
        _currentPins = Instantiate(Resources.Load<GameObject>("10Pin"));
        _currentPins.transform.position = _pinsPosition;
        
        _fallTriggers = FindObjectsOfType<FallTrigger>(true);
        foreach (var fallTrigger in _fallTriggers)
        {
            fallTrigger.onPinFall.AddListener(() =>
            {
                score++;
                onScoreChanged.Invoke(score);
            });
        }
    }
}