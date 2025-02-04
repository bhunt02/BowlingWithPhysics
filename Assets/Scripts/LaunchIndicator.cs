using Unity.VisualScripting;
using UnityEngine;

public class LaunchIndicator : MonoBehaviour
{
    private PlayerController _player;
    private BallController _ball;
    private MeshFilter _cylinderMesh;
    
    private Vector3 _startPosition;
    private Vector3 _startScale;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _startPosition = transform.localPosition;
        _startScale = transform.localScale;
        _player = transform.parent.GameObject().GetComponent<PlayerController>();
        _ball = GameObject.Find("BowlingBall").GetComponent<BallController>();
        _cylinderMesh = this.GetComponentInChildren<MeshFilter>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_player || !_ball) return;
        
        if (_ball.IsLaunched())
        {   
            _cylinderMesh.gameObject.SetActive(false);
            return;
        }
            
        _cylinderMesh.gameObject.SetActive(true);

        if (_player.IsCharging())
        {
            var chargeScale = Mathf.Min(_player.MaxChargeTime(),_player.TimeCharging());
            transform.localPosition = new Vector3(_startPosition.x, _startPosition.y, 3 + chargeScale);
            transform.localScale = new Vector3(_startScale.x, _startScale.y, chargeScale);
        }
        else
        {
            transform.localPosition = new Vector3(_startPosition.x, _startPosition.y, 4);
            transform.localScale = new Vector3(_startScale.x, _startScale.y, 1);
        }
    }
}
