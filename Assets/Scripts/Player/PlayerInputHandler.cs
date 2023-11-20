using UnityEngine;

public class PlayerInputHandler
{

    public InputData GetInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isShootPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        return new InputData()
        {
            horizontalInput = horizontalInput,
            verticalInput = verticalInput,
            isShootPressed = isShootPressed
        };
    }
}
