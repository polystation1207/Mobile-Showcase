using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 6;
    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] float loadDelay = 1;
    int maxHealth = 6;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Bullet")
        {
            health--;
            myText.text = "Health: " + health;
            if (health <= 0)
            {
                Invoke("ReloadScene", loadDelay);
            }
        }

        if (collision.gameObject.tag == "Med Kit" && health < maxHealth)
        {
            health += 3;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            myText.text = "Health: " + health;
            Destroy(collision.gameObject);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        myText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
