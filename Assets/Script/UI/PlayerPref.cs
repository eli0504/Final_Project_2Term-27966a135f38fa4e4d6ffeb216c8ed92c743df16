using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPref : MonoBehaviour
{
    public TMP_InputField _InputField;

    public void SaveData()
    {
        PlayerPrefs.SetString("Input", _InputField.text);
    }

    public void LoadData()
    {
        _InputField.text = PlayerPrefs.GetString("Input");
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("Input");
    }
}

