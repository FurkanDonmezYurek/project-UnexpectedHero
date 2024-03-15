using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRun : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Vector2 newPos;
    public float speed;
    public float attackRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        //Enemy rotasyonu
        float enemyRotation = player.transform.position.x - rb.position.x;
        if (enemyRotation < 0)
        {
            rb.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            newPos = new Vector2(-1f, 0f);
        }
        else
        {
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            newPos = new Vector2(1f, 0f);
        }

        rb.position += newPos * speed * Time.fixedDeltaTime;
        //////////////////////////////////////////
        // Yeni vector playerin x değeri ve enemynin rigidbodysinin y değeri(y değerinin sabit kalması için)
        // Vector2 target = new Vector2(player.position.x, rb.position.y);
        //enemyi target a götürmesini sağlar
        // newPos = Vector2.MoveTowards(rb.position, player.position, speed * Time.fixedDeltaTime);
        //yavaşça olmasını sağlar
        // rb.MovePosition(newPos);

        //player attack rangesine girdiyse attack enemy triggetını çalıştırır
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("AttackEnemy");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //stabil çalışması için
        animator.ResetTrigger("AttackEnemy");
    }
}
