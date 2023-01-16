using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneNumber : MonoBehaviour
{
    [SerializeField]private string     _phoneNumber;

    [Header("Before State")]
    [SerializeField]private bool                _hasBeforeState;
    [SerializeField]private string         _audioBeforeState;

    [Header("Right State")]
    [SerializeField]private bool                _hasRightState;
    [SerializeField]private string         _audioRightState;

    [Header("After State")]
    [SerializeField]private bool                _hasAfterState;
    [SerializeField]private string         _audioAfterState;

    [Header("Auto Talk")]
    [SerializeField]private bool                _isAutoTalk;
    [SerializeField]private List<GameObject>    _possibleAutoTalkNumbers;
    [SerializeField]private GameObject          _leaveNumber;

    [Header("Unlock new Audio")]
    [SerializeField]private bool                _unlockNewAudios;
    [SerializeField]private List<GameObject>    _audiosToUnlock;

    private PhoneState          _state;
    private List<PhoneNumber>   _autoTalkNumbers;
    private PhoneNumber         _leaveNumberScript;

    public string Number 
    {
        get => _phoneNumber;
        private set => _phoneNumber = value;
    }

    public bool IsAutoTalk 
    {
        get => _isAutoTalk;
        private set => _isAutoTalk = value;
    }

    public PhoneState State
    {
        get => _state;
        private set => _state = value;
    }

    private void Start()
    {
        if (!_hasBeforeState)
        {
            State = PhoneState.Right;
        }

        if (!_hasRightState)
        {
            State = PhoneState.After;
        }

        if (_isAutoTalk)
        {
            GameObject leaveNumberInst = Instantiate(_leaveNumber, this.transform.position, Quaternion.identity, this.transform);
            _leaveNumberScript = leaveNumberInst.GetComponent<PhoneNumber>();

            _autoTalkNumbers = new List<PhoneNumber>();

            foreach (GameObject autoTalkNumber in _possibleAutoTalkNumbers)
            {
                GameObject phoneInst = Instantiate(autoTalkNumber, this.transform.position, Quaternion.identity, this.transform);
                PhoneNumber phoneScript = phoneInst.GetComponent<PhoneNumber>();
                _autoTalkNumbers.Add(phoneScript);
            }
        }
    }

    public void Play()
    {
        switch(_state)
        {
            case PhoneState.Before:
                if (_hasBeforeState)
                    //_audioBeforeState.Play();
                    Debug.Log(_audioBeforeState);
            break;

            case PhoneState.Right:
                if (_hasRightState)
                    //_audioRightState.Play();
                    Debug.Log(_audioRightState);
                if (_unlockNewAudios)
                {
                    Debug.Log("Unloooock");
                    foreach(GameObject audioGO in _audiosToUnlock)
                    {
                        PhoneNumber phone = audioGO.GetComponent<PhoneNumber>();
                        phone.Unlock();
                    }
                }
                _state = PhoneState.After;
            break;

            case PhoneState.After:
                if (_hasAfterState)
                    //_audioAfterState.Play();
                    Debug.Log(_audioAfterState);
            break;

            default:
            break;
        }
    }

    public void Unlock()
    {
        State = PhoneState.Right;
    }

    public bool CheckAutoPlay(string number)
    {
        if (_leaveNumberScript.Number == number)
        {
            _leaveNumberScript.Play();
            return true;
        }

        foreach (PhoneNumber phoneNumber in _autoTalkNumbers)
        {
            if (phoneNumber.Number == number)
                phoneNumber.Play();
        }
        return false;
    }
}
