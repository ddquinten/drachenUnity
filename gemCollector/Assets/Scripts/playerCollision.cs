using UnityEngine;

public class playerCollision: MonoBehaviour
{
    public Transform GlobalEvents;
    public GridScript Grid;

    private GameStatus globals;

    void Start()
    {
        globals = GlobalEvents.GetComponent<GameStatus>();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "diamond")
        {
            globals.gameScore += 10;
            globals.collected++;
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "slime")
        {
            GetComponent<CharacterAnim>().setDeath(true);
        }

        /* Suppose to be used to update player position on grid, but i turned off collision 
         * between those two layers so it doesn't work right now
        if (c.gameObject.tag == "dummy")
        {
            Grid.updatePlayerPos(c.gameObject.GetComponent<dummy>().getY(), c.gameObject.GetComponent<dummy>().getX());
            Debug.Log(c.gameObject.GetComponent<dummy>().getY() + c.gameObject.GetComponent<dummy>().getY());
        }*/
    }
}
