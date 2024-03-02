using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OnTrigger : MonoBehaviour
{
    private Health healthScript;

    public TextMeshProUGUI coinsCounterText;
    public TextMeshProUGUI keysCounterText;

    public GameObject winPanel;
    public GameObject player;
    public GameObject bigPotion;
    public GameObject smallPotion;
    public GameObject heart;
    public GameObject goldKey;

    public Volume volume;
    private Vignette vignette;

    public ParticleSystem boxParticles;
    public ParticleSystem boxParticles2;
    public ParticleSystem winParticles;
    public ParticleSystem winParticles2;

    public float speed = 25f;
    public float stairsSpeed = 5f;
    private float smallPowerUp = 0.3f;
    private float bigPowerUp = 1f;
    private int coinsCounter;
    private int keysCounter;
   
    private void Awake()
    {
        volume = GetComponent<Volume>();
    }
    private void Start()
    {
        volume.profile.TryGet(out vignette); //find and plug the vignette

        vignette.intensity.value = 0.5f;
        vignette.color.value = Color.red;
        
        healthScript = GetComponent<Health>();

        winPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //coins
        if (other.gameObject.tag == "coins")
        {
            audioLibrary.PlaySound("coin");
            Destroy(other.gameObject); //the collectable dissapear 
            coinsCounter++;
            coinsCounterText.text = $"{coinsCounter}";
        }

        //keys
        if (other.gameObject.tag == "keys")
        {
            audioLibrary.PlaySound("live");
            Destroy(other.gameObject);
            keysCounter++;
            keysCounterText.text = $"{keysCounter}";

        } else if ((other.gameObject.tag == "goldKey"))
        {
            audioLibrary.PlaySound("live");
            Destroy(other.gameObject); 
            keysCounter++;
            keysCounterText.text = $"{keysCounter}";
            //Time.timeScale = 0f;
            winParticles.Play();
            winParticles2.Play();
            StartCoroutine(waitForWinPanel());
        }

        //PowerUp
        if (other.gameObject.tag == "smallPowerUp")
        {
            Destroy(other.gameObject);
            audioLibrary.PlaySound("poison");
            transform.localScale = new Vector3(smallPowerUp, smallPowerUp, 0);
        }
        else if (other.gameObject.tag == "bigPowerUp")
        {
            Destroy(other.gameObject);
            audioLibrary.PlaySound("poison");
            transform.localScale = new Vector3(bigPowerUp, bigPowerUp, 0);
        }

        //enemies and traps
        if (other.gameObject.tag == "traps" || other.gameObject.tag == "enemy")
        {
            healthScript = GetComponent<Health>();

            if (healthScript != null)
            {
                StartCoroutine(Desactive()); //The red vignette appears when the player takes damage.
                healthScript.GetDamage();
            }
        }

        //box
        if (other.CompareTag("box"))
        {
            Destroy(other.gameObject);
            Instantiate(smallPotion, new Vector3(-6, -2, 0), Quaternion.identity);
            boxParticles.Stop();
        }
        else if (other.CompareTag("box2"))
        {
            Destroy(other.gameObject);
            Instantiate(bigPotion, new Vector3(44.85f, -1.55f, 0), Quaternion.identity);
            boxParticles2.Stop();
        }

        //live
        if (other.CompareTag("live"))
        {
            audioLibrary.PlaySound("live");
            Health.lives++;
            Destroy(other.gameObject);
        }

        //edges
        if (other.CompareTag("Edge"))
        {
            transform.position = new Vector3(-15f, -2.78f, 1f);
        } else if (other.CompareTag("Edge2"))
        {
            transform.position = new Vector3(152.7f, 5.23f, 1f);
        } else if (other.CompareTag("Edge3"))
        {
            transform.position = new Vector3(-15f, -2.78f, 1f);
            healthScript.GetDamage();
        }

        //finalLevel
        if (other.CompareTag("finishLevel"))
        {
            Instantiate(goldKey, new Vector3(152.55f, 10f, 0), Quaternion.identity);
        }

    }

    //coroutine to change color and disable vignette
    public IEnumerator Desactive() 
    {
        yield return new WaitForSeconds(0.1f);
        vignette.intensity.value = 1f;
        vignette.color.value = Color.red;

        yield return new WaitForSeconds(0.5f);
        vignette.active = false;
    }

    IEnumerator waitForWinPanel()
    {
        yield return new WaitForSeconds(2);

        winPanel.SetActive(true);
    }
}
       
