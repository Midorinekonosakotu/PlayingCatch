using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public bool ThrowPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}