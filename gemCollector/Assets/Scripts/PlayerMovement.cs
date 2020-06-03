using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    static public bool playerDead = false;
    //private Rigidbody2D rb2d;

    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>(); for old movement
    }
    void FixedUpdate()
    {
        /* float moveHoriz = Input.GetAxis("Horizontal");
           float moveVerti = Input.GetAxis("Vertical");
           Vector2 movement = new Vector2(moveHoriz, moveVerti);
           rb2d.AddForce(movement * 10);*/ //UFO like movement - slow deceleration
        if (!playerDead)
            transform.Translate((transform.up * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized * speed * Time.deltaTime);
        //adding raw removes smooth output (slow down)
    }
}
