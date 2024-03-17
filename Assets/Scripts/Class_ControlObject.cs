using UnityEngine;

public class Class_ControlObject : MonoBehaviour
{
    public GameObject thisObj;
    public Rigidbody2D rb;
    public Collider2D colli2D;
    public Vector3 velocity;
    public bool jumped;
    float jumpCountEx;
    public AudioSource audioSource;


   

    void Start()
    {
        thisObj = this.gameObject;
        rb = thisObj.GetComponent<Rigidbody2D>();
        colli2D = thisObj.GetComponent<Collider2D>();
        jumpCountEx = this.gameObject.GetComponent<Player>().jumpCount;
    }

    //Yatay Düzlemde Hareket Unity Axes-Horizantal
    public void HorizontalControls(float speed)
    {   
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += velocity * speed * Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            FindObjectOfType<AudioManager>().Play("Walk");

            transform.rotation = Quaternion.Euler(0f, 180, 0f);
        }    
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {

            FindObjectOfType<AudioManager>().Play("Walk");
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    //mobile
    public void HorizontalControlsMobile(float speed, float toWhere)
    {
        velocity = new Vector3(toWhere, 0f);
        transform.position += velocity * speed * Time.deltaTime;
        if (toWhere == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 180, 0f);
        }
        else if (toWhere == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    //Dikey Düzlemde Hareket Unity Axes-Vertical
    public void VerticalControls(float speed)
    {
        velocity = new Vector3(0f, Input.GetAxis("Vertical"));
        transform.position += velocity * speed * Time.deltaTime;
    }

    //Yatay + Dikey Düzlemde Hareket
    public void Controls2(float speed)
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");
        transform.position += velocity * speed * Time.deltaTime;
    }

    //Zıplama
    public void Jump(float jump)
    {
        if (Input.GetButtonDown("Jump") && Mathf.Approximately(rb.velocity.y, 0f))
        {

            rb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);
            jumped = true;
        }
       
        if (Mathf.Approximately(rb.velocity.y, 0f))
        {
            jumped = false;
        }
        
    }
    

    public void DoubleJump(float jump, float jumpCount)
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            FindObjectOfType<AudioManager>().Play("jump");
            rb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);
            jumped = true;
            this.gameObject.GetComponent<Player>().jumpCount--;
        }
        if (Mathf.Approximately(rb.velocity.y, 0f))
        {
            this.gameObject.GetComponent<Player>().jumpCount = jumpCountEx;
            jumped = false;
        }
    }

    public void DoubleJumpMobile(float jump, float jumpCount)
    {
        if (jumpCount > 0)
        {
            rb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);
            jumped = true;
            this.gameObject.GetComponent<Player>().jumpCount--;
        }
        if (Mathf.Approximately(rb.velocity.y, 0f))
        {
            this.gameObject.GetComponent<Player>().jumpCount = jumpCountEx;
            jumped = false;
        }
    }

    //Objeyi Hareket Edilen Yöne Çevirme
    public void GameObjectFlip()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 180, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    //Dash
    public void Dash(float dashPower){
        FindObjectOfType<AudioManager>().Play("dash");
        rb.velocity = new Vector2( Input.GetAxis("Horizontal")*dashPower,0f);
    }
}
