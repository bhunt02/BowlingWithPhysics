using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum MoveDirection
{
    Forward,
    Back,
    Left,
    Right
}

public class InputManager : MonoBehaviour
{
    public UnityEvent onReset = new();
    public UnityEvent onSpaceDown = new();
    public UnityEvent onSpaceUp = new();
    public UnityEvent<List<MoveDirection>> onMovement = new();
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            onReset.Invoke();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) 
            onSpaceDown?.Invoke();
        if (Input.GetKeyUp(KeyCode.Space))
            onSpaceUp?.Invoke();
        
        var moveDirections = new List<MoveDirection>();
        if (Input.GetKey(KeyCode.W))
            moveDirections.Add(MoveDirection.Forward);
        if (Input.GetKey(KeyCode.S))
            moveDirections.Add(MoveDirection.Back);
        if (Input.GetKey(KeyCode.A))
            moveDirections.Add(MoveDirection.Left);
        if (Input.GetKey(KeyCode.D)) 
            moveDirections.Add(MoveDirection.Right);
        
        if (moveDirections.Count > 0)
        {
            onMovement.Invoke(moveDirections);
        }
    }
}
