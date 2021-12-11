using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public FixedJoystick _joystickRight;

    private CinemachineFreeLook _cmFL;


    void Start()
    {
        _cmFL = transform.GetComponent<CinemachineFreeLook>();
    }

    private void FixedUpdate()
    {
        jsMove();
    }

    private void jsMove()
    {
        
        Debug.Log(_joystickRight.Vertical);
        _cmFL.m_YAxis.Value += _joystickRight.Vertical * Time.deltaTime * 0.2f;
        _cmFL.m_XAxis.Value += _joystickRight.Horizontal;
    }
}
