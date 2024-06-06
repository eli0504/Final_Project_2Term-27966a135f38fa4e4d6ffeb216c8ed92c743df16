using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionMenu : MonoBehaviour
{
    private int index; //character index
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI name;

    private CharacterManager characterManager;

    private void Start()
    {
        characterManager = CharacterManager.Instance;

        index = PlayerPrefs.GetInt("IndexPlayer");

        if(index > characterManager.characters.Count - 1) //avoid errors
        {
            index = 0;
        }

        ChangeValuesScreen();
    }

    private void ChangeValuesScreen()
    {
        PlayerPrefs.SetInt("IndexPlayer", index);
        image.sprite = characterManager.characters[index].image;
        name.text = characterManager.characters[index].name;
    }

    //buttons

    public void NextCharacterButton()
    {
        if(index == characterManager.characters.Count - 1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }

        ChangeValuesScreen();
    }

     public void PreviousCharacterButton()
    {
        if(index == 0)
        {
            index = characterManager.characters.Count - 1;
        }
        else
        {
            index -= 1;
        }

        ChangeValuesScreen();
    }
}
