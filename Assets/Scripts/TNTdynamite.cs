using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TNTdynamite : MonoBehaviour
{
    public float pushRotation;
    public float pushForce;
    Animator animator;
    public float damage;

    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //animasyon trigger - force reset - destroy ball
        if (LayerMask.LayerToName(other.gameObject.layer) != "Enemy")
        {
            animator.SetTrigger("Explosion");
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Destroy(this.gameObject, 0.24f);
        }
        //Enemy attack function
        if (
            other.gameObject.layer == 8
            && other.gameObject.TryGetComponent(out Player healthPlayer)
        )
        {
            // Player push rotation value
            pushRotation =
                this.gameObject.transform.position.x - other.gameObject.transform.position.x;
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

            //Player Push
            if (pushRotation < 0)
            {
                rb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
                other.gameObject.transform.rotation = Quaternion.Euler(0f, 180, 0f);
            }
            else
            {
                rb.AddForce(Vector2.left * pushForce, ForceMode2D.Impulse);
                other.gameObject.transform.rotation = Quaternion.Euler(0f, 0, 0f);
            }
            //Player Damage
            healthPlayer.TakeDamge(damage);
        }
    }
}
