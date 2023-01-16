using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeController : MonoBehaviour
{
    [SerializeField] private List<GameObject>   _possiblePhoneNumbers;

    private List<PhoneNumber>       _phoneNumbers;
    private PhoneNumber             _currentAutoTalk;
    private bool                    _isPlayingAuto;

    private void Start()
    {
        _isPlayingAuto = false;
        _phoneNumbers = new List<PhoneNumber>();
        foreach (GameObject phoneNumber in _possiblePhoneNumbers)
        {
            GameObject phoneInst = Instantiate(phoneNumber, this.transform.position, Quaternion.identity, this.transform);
            PhoneNumber phoneScript = phoneInst.GetComponent<PhoneNumber>();
            _phoneNumbers.Add(phoneScript);
        }
    }

    public void CallNumber(string number)
    {
        if (_isPlayingAuto)
        {
            _isPlayingAuto = !_currentAutoTalk.CheckAutoPlay(number);
            return;
        }

        foreach (PhoneNumber phoneNumber in _phoneNumbers)
        {
            if (phoneNumber.Number == number)
            {
                phoneNumber.Play();
                if (phoneNumber.IsAutoTalk && phoneNumber.State == PhoneState.Right)
                {
                    _currentAutoTalk = phoneNumber;
                    _isPlayingAuto = true;
                }
            }
        }
    }
}
