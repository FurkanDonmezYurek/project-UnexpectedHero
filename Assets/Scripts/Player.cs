using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Class_ControlObject ctrlObj;
    Rigidbody2D rb;
    public float speed;
    public float jump;
    public float jumpCount;
    Animator animator;
    private float moveInput;
    public int comboCount;

    public Slider healthSlider;
    public float maxHealth;
    public float currentHealth;
    public GameObject gameOverScreen;

    public float damageCD;
    public bool canDamage;
    float damageTime;

    //Dash
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    private TrailRenderer tr;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        ctrlObj = this.gameObject.GetComponent<Class_ControlObject>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        tr = this.gameObject.GetComponent<TrailRenderer>();

        //LifeBar start full
        currentHealth = maxHealth;
        UpdateLifeBar();
        canDamage = true;
    }

    void Update()
    {
        //life bugfix
        if(currentHealth >maxHealth){
            currentHealth = maxHealth;
        }
        if(isDashing){
            return;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)&&canDash&&canDamage){
            StartCoroutine(Dash());
        }
        if (canDamage == false)
        {
            damageTime = Time.time;
            if (damageCD < damageTime)
            {
                canDamage = true;
                damageTime = 0;
            }
        }
        UpdateLifeBar();
        //Dead
        if(currentHealth <= 0){
            animator.SetBool("IsDead",true);
            Invoke("GameOver",2f);
        }

        ctrlObj.DoubleJump(jump, jumpCount);

        if (ctrlObj.jumped)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    private void FixedUpdate()
    {
        if(isDashing){
            return;
        }
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
       if(animator.GetBool("IsDead")==false)
       {
            moveInput = Input.GetAxisRaw("Horizontal");
            ctrlObj.HorizontalControls(speed);
        }
        
    }

    //hasar alınca canı azalması ve animasyon aktivasyonu
    public void TakeDamge(float damage)
    {
        if (canDamage == true)
        {
            currentHealth -= damage;
            animator.SetTrigger("TakeDamage");
            UpdateLifeBar();
            damageCD = Time.time + 0.5f;
            canDamage = false;
        }
    }
    void ReloadScene(){
        Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
    }
    //Reset Forces
    public void ResetAllForces()
    {
        rb.velocity = Vector3.zero; // Hızı sıfırla
    }

    //Update LifeBar
    private void UpdateLifeBar()
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    private IEnumerator Dash(){
        int originalLayer = this.gameObject.layer;
        this.gameObject.layer = 8;
        float originalSpeed = speed;
        speed =0;
        canDamage =false;
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        ctrlObj.Dash(dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        this.gameObject.layer =originalLayer;
        ResetAllForces();
        speed = originalSpeed;
        canDamage = true;
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    void GameOver(){
        gameOverScreen.SetActive(true);
    }
}
