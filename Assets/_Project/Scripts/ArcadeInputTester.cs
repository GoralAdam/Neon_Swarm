using UnityEngine;
using UnityEngine.InputSystem;

public class ArcadeInputTester : MonoBehaviour
{
    public ArcadePlayerController targetShip; 

    void Update()
    {Vector2 moveInput = Vector2.zero;
        
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveInput.y = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveInput.y = -1f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveInput.x = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveInput.x = 1f;
        
        targetShip.SetMoveInput(moveInput.normalized);
    }
}
