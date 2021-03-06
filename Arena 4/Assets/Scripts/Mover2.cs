﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


public class Mover2 : MonoBehaviour
{

    [SerializeField]
    private float speed = 10f;


    public Rigidbody2D rb;


    private PlayerInputActions playerInput;
    private Vector2 moveInput = Vector2.zero;

    [Space]

    //Bullet spawn variables
    public Transform firePoint;
    public GameObject bulletPrefabs;
    public GameObject bulletPrefabs2;
    public float bulletForce = 20f;

    [Space]

    //Rotate gun (AimShoot)variables
    public GameObject Gun;
    Vector2 stickInput;
    private float stickInputf;
    private float stickInput_r;
    bool FacingRight = true;




    [Space]

    //Shield spawn variables

    public GameObject ShieldPrefabs;
    public GameObject ShieldPrefabs2;
    
    public float destroyTime = 1f;

    [Space]

    //Ammo variables

    public int count = 3;

    [Space]

    //dash variables

    IEnumerator dashCoroutine;
    public bool isDashing;
    public bool canDash = true;
    public float dashForce;
    public float dashDur;
    public float dashCd;




  
    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }


    void FixedUpdate()
    {
      
        rb.velocity = moveInput * speed;
        if (isDashing)
        {
            rb.AddForce(moveInput.normalized * dashForce, ForceMode2D.Impulse);
        }
        AimShoot();
       
        
    }

   
    private void OnEnable()
    {
        playerInput.Enable();
    }


    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void SetInputVector(Vector2 direction)
    {
        moveInput = direction;
    }


    //Set vector from the handler callbackcontext ReadValue, Aimshoot

    public void SetInputVectorRot(Vector2 rotVector)
    {
        stickInput = rotVector;
    }

    //Aimshoot method

    private void AimShoot()
    {
        stickInput_r = stickInput.x;

        if (FacingRight)
        {
            stickInputf = stickInput.x + stickInput.y * 90;
            Gun.transform.rotation = Quaternion.Euler(0f, 0f, stickInputf);
        }

        else

        {
            stickInputf = stickInput.x + stickInput.y * -90;
            Gun.transform.rotation = Quaternion.Euler(0f, 180f, -stickInputf);
        }

        if (stickInput_r < 0 && FacingRight)
        {
            // Executes the void: Flip()
            Flip();
        }
        else if (stickInput_r > 0 && !FacingRight)
        {
            // Executes the void: Flip()
            Flip();
        }
    }

    //Flip for Aimshoot
    private void Flip()
    {
        // Flips the player.
        FacingRight = !FacingRight;

        transform.Rotate(0, 180, 0);

    }



    /// Shoot and ammo


    public void Shoot()
    {

        
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
   
       
    }


    public void Shoot2()
    {


        GameObject bullet = Instantiate(bulletPrefabs2, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);


    }

    public void CancelBullet()
    {
        if (count != 0)
        {
            Shoot();
            count--;
        }


    }

    public void CancelBullet2()
    {
        if (count != 0)
        {
            Shoot2();
            count--;
        }


    }


    //Shields

    public void Shield1black()
    {

        GameObject Shield = Instantiate(ShieldPrefabs, transform.position, transform.rotation);
       Shield.transform.parent = transform;
        Destroy(Shield, destroyTime);
        
    }

    public void Shield1white()
    {

        GameObject Shield = Instantiate(ShieldPrefabs2, transform.position, transform.rotation);
        Shield.transform.parent = transform;
        Destroy(Shield, destroyTime);

    }

    //Dash

    public void Dasher()
    {
        
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }

        dashCoroutine = Dash(dashDur, dashCd);
        StartCoroutine(dashCoroutine);
    }

    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        isDashing = true;
        canDash = false;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }


}