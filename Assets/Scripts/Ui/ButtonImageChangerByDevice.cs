using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
public class ButtonImageChangerByDevice : MonoBehaviour
{
    [SerializeField] private Image Image;

    [SerializeField] private Sprite XBoxTexture;
    [SerializeField] private Sprite KeyboardTexture;

    public void Awake()
    {
        InputSystem.onActionChange += OnActionChange;
        //InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputDevice currentDevice = ((InputAction)obj).activeControl.device;

            switch (currentDevice)
            {
                case Gamepad: //can be broken down further... and making functions out of that
                    Image.overrideSprite = XBoxTexture;
                    break;
                case Keyboard:
                case Mouse:
                    Image.overrideSprite = KeyboardTexture;
                    break;
            }
            Image.enabled = true;
        }
    }
}
