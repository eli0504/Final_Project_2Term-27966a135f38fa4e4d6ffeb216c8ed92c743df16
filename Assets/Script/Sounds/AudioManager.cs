using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    public TextMeshProUGUI volumeTextUI = null;

    private void Start()
    {
        //We check if there is already a volume value saved in the player preferences
        if (!PlayerPrefs.HasKey("VolumeValue"))
        {
            //If there is no saved value, we set the default volume to 1
            PlayerPrefs.SetFloat("VolumeValue", 1);
        }
        else
        {
            LoadValues();
        }
    }

    public void ChangeSoundVolume()
    {
        //We adjust the Audio volume to the value of the slider
        AudioListener.volume = volumeSlider.value;
        SaveVolumeButton();
    }

    public void SaveVolumeButton()
    {
        //We save the slider value in the player preferences.
        PlayerPrefs.SetFloat("VolumeValue", volumeSlider.value);
        LoadValues();
    }

    public void LoadValues()
    {
        //We assign the value of the saved volume to the slider
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeValue");
    }
}
