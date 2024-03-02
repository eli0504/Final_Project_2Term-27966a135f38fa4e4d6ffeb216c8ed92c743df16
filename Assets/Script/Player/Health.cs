using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    private GameOver gameOver;
    private Animator anim;

    public static int lives = 3;
    public int numberOfHearts;

    //visual lives
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        gameOver = GetComponent<GameOver>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(lives > numberOfHearts)
            {
                lives = numberOfHearts;
            }

            if(i < lives)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public int GetLives()
    {
        return lives;
    }

    //When the player takes damage, their lives are subtracted
    public void GetDamage()
    {
        lives--;
        audioLibrary.PlaySound("hurt");
        if (lives <= 0)
        {
            gameOver.IsGameOver();
            gameObject.SetActive(true);
            anim.SetBool("isDead", true);   
        }
    }
}
