using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour
{
    float pushRotation;
    public float damage;
    public float pushForce;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Enemy attack function
        if (
            other.transform.tag == "Player"
            && other.gameObject.TryGetComponent(out Player healthPlayer)
        )
        {
            if (other.GetComponent<Player>().canDamage == true)
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
            }

            //Player Damage
            healthPlayer.TakeDamge(damage);
        }
    }
}
