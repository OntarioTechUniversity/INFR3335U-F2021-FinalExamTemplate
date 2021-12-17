using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    public Transform _ControlConnect; // Connector

    public GameObject _player;
    
    private float rotAng;
    private RaycastHit rayCout;
    private float veloY;
    

    private CharacterController _characterController;

    private FixedJoystick _joystickLeft;
    

    private Camera _camera;

    private Transform _cmfl;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _joystickLeft = _ControlConnect.GetChild(0).GetChild(0).GetComponent<FixedJoystick>();
        //_joystickRight = _ControlConnect.GetChild(0).GetChild(1).GetComponent<FixedJoystick>();
        _camera = _ControlConnect.GetChild(1).GetComponent<Camera>();
        _cmfl = _ControlConnect.GetChild(2);
        _cmfl.GetComponent<Cinemachine.CinemachineFreeLook>().Follow = _player.transform;
        _cmfl.GetComponent<Cinemachine.CinemachineFreeLook>().LookAt = _player.transform.GetChild(2);
    }

    void Update()
    {
        _player.transform.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y + rotAng, 0);


        countDown();
 

        
        if (isCollidingVertically(false))
        {
            
            
            if(jsMove(false))
            {
                
                _characterController.Move(_player.transform.forward * Time.deltaTime);

            }
            
        }

        //Debug.Log(veloY);
        _characterController.Move(new Vector3(0f, veloY, 0f) * Time.deltaTime);
    }

    private void countDown()
    {
        if (!isCollidingVertically(false))
            veloY += -9.8f * Time.deltaTime;
    }


    private bool isCollidingVertically(bool isDebugging)
    {
        Physics.Raycast(_player.transform.position, Vector3.down, out rayCout);

        if (isDebugging)
            Debug.Log("rayCout.distance: " + rayCout.distance);

        if (rayCout.distance <= 0.1 || _characterController.isGrounded)
        {
            return true;
        }

        return false;
    }


    private bool jsMove(bool isDebugging)
    {
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            Vector3 inputVect = new Vector3(_joystickLeft.Horizontal, 0.0f, _joystickLeft.Vertical);
            if (_joystickLeft.Horizontal == 0 && _joystickLeft.Vertical == 0)
            {
                rotAng = 0;
                return false;
            }
            
            rotAng = Vector3.Angle(Vector3.forward, inputVect.normalized)
                * Mathf.Sign(Vector3.Dot(
                    Vector3.Cross(Vector3.forward, inputVect.normalized),Vector3.up));

            if (isDebugging)
                Debug.Log(rotAng);
            return true;
        }
        return false;
    }
}
