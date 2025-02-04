using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour
{
    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager != null)
        {
            _gameManager.onScoreChanged.AddListener((score) =>
            {
                GetComponent<TextMeshProUGUI>().text = $"Score: {score.ToString()}";
            });
        }
    }
}
