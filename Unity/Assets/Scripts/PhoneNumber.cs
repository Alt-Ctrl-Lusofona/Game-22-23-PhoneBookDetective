using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneNumber : MonoBehaviour
{
    [SerializeField] private string         _phoneNumber;
    [SerializeField] private AudioSource    _beforeState;
    [SerializeField] private AudioSource    _rightState;
    [SerializeField] private AudioSource    _afterState;

    private PhoneState  _state;

    public string Number 
    {
        get => _phoneNumber;
        private set => _phoneNumber = value;
    }

    public PhoneState   State
    {
        get => _state;
        set => _state = value;
    }

    private void Start()
    {
        _state = PhoneState.Before;
    }

    public void Play()
    {
        if (_state == PhoneState.Before)
        {
            _beforeState.PlayOneShot(_beforeState.clip);
        }
        else if (_state == PhoneState.Right)
        {
            _rightState.PlayOneShot(_rightState.clip);
            _state = PhoneState.After;
        }
        else
        {
            _afterState.PlayOneShot(_afterState.clip);
        }
    }

    public void ChangeState(PhoneState state)
    {
        _state = state;
    }

}
