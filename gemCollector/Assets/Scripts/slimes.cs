using UnityEngine;

public class slimes : MonoBehaviour
{

    private enum STATE
    {
        MOVE,
        IDLE
    }

    public float speed = 2.5f;

    private STATE state = STATE.MOVE;
    private GameStatus globals;
    private GameObject CurrentWaypoint;
    private GridScript Grid;
    private Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {

        globals = GameObject.Find("EventSystem").GetComponent<GameStatus>();

        anim = GetComponent<Animator>();
        anim.SetBool("isMoving", true);
        setState(STATE.MOVE);

        Grid = GameObject.Find("Grid").GetComponent<GridScript>();

        // this is used to find a random start position for the slime
        int x, y;
        do
        {
            x = Random.Range(0, Grid.column);
            y = Random.Range(0, Grid.row);
        } 
        while (!Grid.getExists(y, x) && !Grid.onPlayer(y, x));

        CurrentWaypoint = Grid.getWaypoint(new int[] { y, x });

        transform.position = CurrentWaypoint.gameObject.transform.position;
    }

    void Update()
    {
        if (globals.getState() == GameStatus.GameState.GAMEOVER)
        {
            anim.SetBool("isMoving", false);
            setState(STATE.IDLE);
        }

        if ((CurrentWaypoint != null) && (state == STATE.MOVE))
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentWaypoint.gameObject.transform.position, speed * Time.deltaTime);

            if (transform.position == CurrentWaypoint.gameObject.transform.position)
                GetNeighbor();

            // flipping animation
            if ((transform.position.x < CurrentWaypoint.gameObject.transform.position.x) && !facingRight) // heading right
                Flip();
            else if ((transform.position.x > CurrentWaypoint.gameObject.transform.position.x) && facingRight) //heading left
                Flip();

        }
    }

    private void GetNeighbor()
    {
        if (CurrentWaypoint != null)
        {
            while (!CurrentWaypoint.GetComponent<dummy>().getState()) ; // empty statement to wait for neighbors to finish setting up

            int index;
            do
            {
                index = Random.Range(0, CurrentWaypoint.GetComponent<dummy>().getNum());
            }
            while (Grid.getWaypoint(CurrentWaypoint.GetComponent<dummy>().getNeighbor(index)) == null);


            CurrentWaypoint = Grid.getWaypoint(CurrentWaypoint.GetComponent<dummy>().getNeighbor(index));
        }
    }

    private void setState(STATE state)
    {
        this.state = state;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /*void Move()
    {
       
        if (CurrentWaypoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentWaypoint.gameObject.transform.position, speed * Time.deltaTime);
            if (transform.position == CurrentWaypoint.gameObject.transform.position)
                GetNeighbor();
        }
        
    }*/
}
