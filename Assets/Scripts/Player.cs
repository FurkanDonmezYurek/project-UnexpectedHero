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

    public float damageCD;
    public bool canDamage;
    float damageTime;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        ctrlObj = this.gameObject.GetComponent<Class_ControlObject>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        //LifeBar start full
        currentHealth = maxHealth;
        // UpdateLifeBar();
        canDamage = true;
    }

    void Update()
    {
        if (canDamage == false)
        {
            damageTime = Time.time;
            if (damageCD < damageTime)
            {
                canDamage = true;
                damageTime = 0;
            }
        }
        // UpdateLifeBar();
        //Dead
        if(currentHealth <= 0){
            animator.SetBool("IsDead",true);
            Invoke("ReloadScene",2f);
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
            // UpdateLifeBar();
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
    // private void UpdateLifeBar()
    // {
    //     healthSlider.value = currentHealth / maxHealth;
    // }

  
}
