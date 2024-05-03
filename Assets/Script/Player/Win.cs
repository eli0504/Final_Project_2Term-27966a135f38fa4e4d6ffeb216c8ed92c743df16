using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Win : MonoBehaviour
{
 //   public EnemyHealth enemyHealth;

    public GameObject goldKey;

    public ParticleSystem winParticles;
    public ParticleSystem winParticles2;

    public GameObject winPanel;

    private void Start()
    {
       // enemyHealth = GetComponent<EnemyHealth>();

        winPanel.SetActive(false);
    }

    public void PlayerWin()
    {
        Instantiate(goldKey, new Vector3(-1f, 2f, 0), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "goldKey")
        {
            Destroy(other.gameObject);
            audioLibrary.PlaySound("live");
            winParticles.Play();
            winParticles2.Play();
            StartCoroutine(waitForWinPanel());
        }
    }

    IEnumerator waitForWinPanel()
    {
        yield return new WaitForSeconds(5);

        winPanel.SetActive(true);
    }
}
