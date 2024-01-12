using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] GameObject prefab;
    [SerializeField] float shootSpeed = 10;
    [SerializeField] float bulletLifetime = 2;
    [SerializeField] float timer = 1;
    [SerializeField] bool mouseShoot = true;
    [SerializeField] int bulletCount = 200;
    [SerializeField] bool playerShoot = true;
    [SerializeField] AudioClip shootingSound;
    int maxBulletCount = 200;
    float x = 2;
    float y = 0;

    Animator myAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Max Ammo" && bulletCount < maxBulletCount)
        {
            bulletCount = 200;
            if (bulletCount > maxBulletCount)
            {
                bulletCount = maxBulletCount;
            }
            myText.text = "Bullet Count: " + bulletCount;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Ammo Crate" && bulletCount < maxBulletCount)
        {
            bulletCount += 10;
            if (bulletCount > maxBulletCount)
            {
                bulletCount = maxBulletCount;
            }
            myText.text = "Bullet Count: " + bulletCount;
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float tempX = Input.GetAxisRaw("Horizontal");
        float tempY = Input.GetAxisRaw("Vertical");

        if (bulletCount <= 0)
        {
            playerShoot = false;
        }

        else if (bulletCount > 0)
        {
            playerShoot = true;
        }

        if (mouseShoot)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 shootDir = mousePosition - transform.position;
            shootDir.z = 0;
            shootDir.Normalize();
            x = shootDir.x;
            y = shootDir.y;
        }
        else if (tempX != 0 || tempY != 0)
        {
            x = tempX;
            y = tempX;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // I have pressed the fire button
            if (playerShoot)
            {
                bulletCount--;
                myText.text = "Bullet Count: " + bulletCount;
                timer = 1;
                myAnimator.SetTrigger("isShooting");
                Camera.main.GetComponent<AudioSource>().PlayOneShot(shootingSound);
                GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * shootSpeed;
                Destroy(bullet, bulletLifetime);
            }
        }
    }
}
