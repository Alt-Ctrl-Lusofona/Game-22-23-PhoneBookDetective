using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteCodeGame : MonoBehaviour
{
    [SerializeField] private AudioSource    _startCall;

    [SerializeField] private PhoneNumber    _company;
    [SerializeField] private PhoneNumber    _writter;
    [SerializeField] private PhoneNumber    _wife;
    [SerializeField] private PhoneNumber    _coolegue;
    [SerializeField] private PhoneNumber    _subManager;

    [SerializeField] private PhoneNumber    _companyOneTwo;
    [SerializeField] private PhoneNumber    _companyThree;
    [SerializeField] private AudioSource    _wrongNumber;



    private bool    _started;
    private bool    _hanging;
    private bool    _disking;
    private float   _currentTime;
    private float   _waitTime = 4.0f;
    private string  _code;
    private bool    _onCallWithCompany;

    private void Start()
    {
        _started = false;
        _hanging = true;
        _currentTime = 0.0f;
        _code = "";
        _companyOneTwo.ChangeState(PhoneState.Right);
        _companyThree.ChangeState(PhoneState.Right);
        _company.ChangeState(PhoneState.Right);
    }

    private void Update()
    {
        //CheckPlayerInputPC();
        if (_disking)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _waitTime)
            {
                Call();
                _disking = false;
            }
        }
    }

    // private void CheckPlayerInputPC()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha0))
    //         Message("0");
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //         Message("1");
    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //         Message("2");
    //     if (Input.GetKeyDown(KeyCode.Alpha3))
    //         Message("3");
    //     if (Input.GetKeyDown(KeyCode.Alpha4))
    //         Message("4");
    //     if (Input.GetKeyDown(KeyCode.Alpha5))
    //         Message("5");
    //     if (Input.GetKeyDown(KeyCode.Alpha6))
    //         Message("6");
    //     if (Input.GetKeyDown(KeyCode.Alpha7))
    //         Message("7");
    //     if (Input.GetKeyDown(KeyCode.Alpha8))
    //         Message("8");
    //     if (Input.GetKeyDown(KeyCode.Alpha9))
    //         Message("9");
    //     if (Input.GetKeyDown(KeyCode.I))
    //         Message("i");
    //     if (Input.GetKeyDown(KeyCode.O))
    //         Message("o");
    //     if (Input.GetKeyDown(KeyCode.S))
    //         Message("s");
    // }

    private void Call()
    {
        _hanging = true;
        if (_onCallWithCompany)
        {
            if (_companyOneTwo.Number == _code || _code == "2")
            {
                _companyOneTwo.Play();
                _hanging = false;
            }
            else if (_companyThree.Number == _code)
            {
                if (_companyThree.State == PhoneState.Right)
                {
                    Debug.Log("Change writter state");
                    _writter.ChangeState(PhoneState.Right);
                }
                _companyThree.Play();
                _onCallWithCompany = false;
                _hanging = true;
            }
            _code = "";
            return;
        }

        if (_company.Number == _code)
        {
            _company.Play();
            _onCallWithCompany = true;
            _hanging = false;
        }
        else if (_writter.Number == _code)
        {
            if (_writter.State == PhoneState.Right)
            {
                _wife.ChangeState(PhoneState.Right);
            }
            _writter.Play();
        }
        else if (_wife.Number == _code)
        {
            if (_wife.State == PhoneState.Right)
            {
                _coolegue.ChangeState(PhoneState.Right);
            }
            _wife.Play();
        }
        else if (_coolegue.Number == _code)
        {
            if (_coolegue.State == PhoneState.Right)
            {
                _subManager.ChangeState(PhoneState.Right);
            }
            _coolegue.Play();
        }
        else if (_subManager.Number == _code)
        {
            if (_subManager.State == PhoneState.Right)
            {
                Debug.Log("End game");
            }
            _subManager.Play();
        }
        else
        {
            _wrongNumber.Play();
        }

        _code = "";
    }

    // void OnMessageArrived(string msg)
    // {
    //     if (!_started)
    //     {
    //         if (msg == "s")
    //         {
    //             _startCall.Play();
    //             _started = true;
    //         }
    //         return;
    //     }

    //     if (msg == "o")
    //         _hanging = true;
    //     if (msg == "i")
    //         _hanging = false;
        
    //     if (!_hanging)
    //         ChangeCode(msg);
    // }

    void OnMessageArrived(string msg)
    {
        Debug.Log("#" + msg + "#");
        if (!_started)
        {
            Debug.Log("Has not stated");
            if (msg.Contains('s'))
            {
                Debug.Log("StartCall");
                _startCall.Play();
                _started = true;
            }
            return;
        }
        else if (msg.Contains('i'))
            _hanging = true;
        else if (msg.Contains('o'))
            _hanging = false;  
        else if (!_hanging)
        {
            char[] charArray = msg.ToCharArray();
            ChangeCode(charArray[0].ToString());
        }
            
    }

    private void ChangeCode(string code)
    {
        _disking = true;
        _code += code;
        _currentTime = 0.0f;
    }

}
