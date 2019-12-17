using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private float activeMovesSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 5f, dashInvicibility = .5f;
    private float dashCounter, dashCoolCounter;

    private Vector2 moveInput;

    public Rigidbody2D theRb;
    public Transform gunArm;
    private Camera theCam;
    public Animator anim;

    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    public SpriteRenderer bodySR;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // Optimization - never call the camera locally each time. Instantiate it once!
        theCam = Camera.main;
        activeMovesSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        // transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);
        theRb.velocity = moveInput * activeMovesSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);
        Vector2 offSet = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        // Rotate gun arm
        float angle = Mathf.Atan2(offSet.y, offSet.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

                shotCounter = timeBetweenShots;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMovesSpeed = dashSpeed;
                dashCounter = dashLength;

                anim.SetTrigger("dash");

                PlayerHealthController.instance.MakeInvicible(dashInvicibility);
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMovesSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        } 
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
