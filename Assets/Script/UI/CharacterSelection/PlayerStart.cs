using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    
    void Start()
    {
        int playerIndex = PlayerPrefs.GetInt("IndexPlayer");
        Instantiate(CharacterManager.Instance.characters[playerIndex].playableCharacter, transform.position, Quaternion.identity);
    }
}
