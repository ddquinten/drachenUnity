using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    public float speed;

    private bool playerDead;
    private bool die;
    private bool facingRight;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        setDeath(false);
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (die)
            anim.SetBool("monsterHit", true);
        // flip direction
        if (!die)
        {
            float h = Input.GetAxis("Horizontal");
            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();
            // movement animation
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
               Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
    }
    void FixedUpdate()
    {
        if (!playerDead)
            transform.Translate((transform.up * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized * speed * Time.deltaTime);

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void setDeath(bool status)
    {
        playerDead = status;
        die = status;
    }

    public bool isPlayerDead()
    {
        return playerDead;
    }
}
