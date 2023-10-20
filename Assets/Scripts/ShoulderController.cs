using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShoulderController : MonoBehaviour
{
    CharacterController _controller;
    Transform _camera;
    Transform _lookAtTransform;

    float _horizontal;
    float _vertical;

    public GameObject _cameraNormal;
    public GameObject _aimCamera;



    [SerializeField] float _playerSpeed = 5;

    float _turnSmoothVelocity;
    [SerializeField] float _turnSmoothTime = 0.1f;

    [SerializeField] float _jumpHeigh = 1;
    float _gravity = -9.81f;

    Vector3 _playerGravity;
    //variables sensor
    [SerializeField] Transform _sensorPosition;
    [SerializeField] float _sensorRadius = 0.2f;
    [SerializeField] LayerMask _groundLayer;

    bool _isGrounded;

    [SerializeField] AxisState xAxis;
    [SerializeField] AxisState yAxis;
    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _lookAtTransform = GameObject.Find("LookAt").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Movement(); 
        Jump(); 

        if(Input.GetButton("Fire2"))
        {
            _cameraNormal.SetActive(false);
            _aimCamera.SetActive(true);
        }
        else
        {
            _cameraNormal.SetActive(true);
            _aimCamera.SetActive(false);
        }
    }
    
    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);

        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = 0;
        }
        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerGravity.y = Mathf.Sqrt(_jumpHeigh * -2 * _gravity);
        }
        _playerGravity.y += _gravity * Time.deltaTime;
        
        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);
        
        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +  _camera.eulerAngles.y;

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection.normalized * _playerSpeed * Time.deltaTime);
        }
        
        Vector3 move = new Vector3(_horizontal, 0, _vertical).normalized;

        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, xAxis.Value, 0);
        _lookAtTransform.eulerAngles = new Vector3(yAxis.Value, transform.eulerAngles.y, 0);
    }
}
