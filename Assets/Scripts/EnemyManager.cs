using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject Player;
    float range;
    public float triggerRange;
    Animator animator;
    Rigidbody2D rb;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //player ile arasındaki mesafenin float değeri
        // range = this.gameObject.transform.position.x - Player.transform.position.x;
        if(Player.GetComponent<Animator>().GetBool("IsDead")==false)
        {
        range = Vector3.Distance(this.gameObject.transform.position, Player.transform.position);
        if (Mathf.Approximately(rb.velocity.y, 0f))
        {
            //rangenin mutlak değeri gelirlenen mesafeden küçük olursa
            if (range < triggerRange)
            {
                animator.SetBool("TriggerPlayer", true);
            }
            else
            {
                animator.SetBool("TriggerPlayer", false);
            }
        }
            
        }else
            {
                animator.SetBool("TriggerPlayer",false);
            }
    }

    //Reset Forces
    public void ResetAllForces()
    {
        rb.velocity = Vector3.zero; // Hızı sıfırla
    }
}
