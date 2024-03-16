using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : MonoBehaviour
{
    GameObject player;
    Player playerSc;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSc = player.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            playerSc.currentHealth++;
            Destroy(this.gameObject);
        }
    }
}
