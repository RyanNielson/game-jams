using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSwitch : MonoBehaviour
{
    public bool isPressed = false;

    public LevelComplete attachedLevelComplete;

    public void Pressed(Piece piece)
    {
        isPressed = true;
        attachedLevelComplete.Activate();
    }

    public void Released(Piece piece)
    {
        isPressed = false;
        attachedLevelComplete.Deactivate();
    }
}
