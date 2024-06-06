using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public List<CharacterSelection> characters;

    private void Awake()
    {
        if(CharacterManager.Instance == null)
        {
            CharacterManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
