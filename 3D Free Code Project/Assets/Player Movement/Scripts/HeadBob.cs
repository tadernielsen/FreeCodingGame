using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float _amp = 0.015f;
    [SerializeField, Range(0, 30)] private float _freq = 10.0f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;
    
    private Vector3 _startPos;
    private PlayerMovement _controller;
    
    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<PlayerMovement>();
        _startPos = _camera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enable) return;
        
        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _freq) * _amp;
        pos.x += Mathf.Cos(Time.time * _freq / 2) * _amp;
        return pos;
    }

    private void CheckMotion()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) return;
        if (!_controller.grounded) return;
        
        PlayMotion(FootStepMotion());
    }
    
    private void PlayMotion(Vector3 motion){
        _camera.localPosition += motion; 
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
