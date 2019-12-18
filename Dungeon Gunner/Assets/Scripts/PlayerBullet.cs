using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRb;
    public GameObject impactEffect;
    public int damageToGive = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet according to its own coordinates, so movement is always to the right, and movement in the world coords are based on it's rotation.
        theRb.velocity = transform.right * speed;
    }

    // The arguement is the object we're colliding with
    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.instance.PlaySFX(4);
        if (other.tag == "Enemy")
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        else
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        // Destroy self
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
