using UnityEngine;

public class GridScript : MonoBehaviour
{
    static private GameObject[,] Waypoints = new GameObject[11, 16];

    //prefabs
    public GameObject dummyPrefab;
    public GameObject diamondPrefab;
    public GameObject slimePrefab;
    public GameObject playerObject;
    public Transform GlobalEvents;

    public int column, row;
    public int numOfSlimes;
    public float x_space, y_space;
    public float x_start, y_start;


    private bool[,] exists = new bool[11, 16];
    private GameStatus globals;
    private int[] playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        globals = GlobalEvents.GetComponent<GameStatus>();
        SpawnDummy();

        // move player into valid position here
        playerPosition = new int[2];
        int x, y;
        do
        {
            x = Random.Range(0, column);
            y = Random.Range(0, row);
        } 
        while (!exists[y, x]);

        updatePlayerPos(y, x);
        playerObject.gameObject.transform.position = getWaypoint(new int[] { y, x }).gameObject.transform.position;

        SpawnDiamonds();
    }
    void Update()
    {
        while (numOfSlimes > 0)
        {
            SpawnSlime();
            numOfSlimes--;
        }

        // Was used to see contents of exists 2d array
        /*int x = 0, y = 0;
        StringBuilder buffer = new StringBuilder();
        for (x = 0; x < row; x++)
        {
            for (y = 0; y < column; y++)
            {
                buffer.Append(exists[0, y]);
            }
            Debug.Log(buffer.ToString());
            buffer.Length = 0;
        }*/

    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "diamond")
        {
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "dummy")
        {
            exists[c.gameObject.GetComponent<dummy>().getY(), c.gameObject.GetComponent<dummy>().getX()] = false;
            Destroy(c.gameObject);
        }
    }

    public bool getExists(int y, int x)
    {
        return exists[y, x];
    }

    public GameObject getWaypoint(int[] position)
    {
        return Waypoints[position[0], position[1]];
    }

    public void SpawnSlime()
    {
        Instantiate(slimePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void SpawnDummy()
    {
        // create dummy variables for building enemy waypoints
        for (int i = 0; i < column * row; i++)
        {
            exists[i / column, i % column] = true;
            GameObject temp = Instantiate(dummyPrefab, new Vector3(x_start + (x_space * (i % column)), y_start + (-y_space * (i / column)), 0), Quaternion.identity);
            temp.GetComponent<dummy>().setIndex(i % column, i / column);
            Waypoints[i / column, i % column] = temp;
        }
    }

    public void SpawnDiamonds()
    {
        globals.gameScore -= 10; // because of diamond that spawns on player
        for (int i = 0; i < column * row; i++)
        {
            Instantiate(diamondPrefab, new Vector3(x_start + (x_space * (i % column)), y_start + (-y_space * (i / column)), 0), Quaternion.identity);
        }
    }

    public void updatePlayerPos(int y, int x)
    {
        playerPosition[0] = y;
        playerPosition[1] = x;
    }

    public bool onPlayer(int y, int x)
    {
        return ((playerPosition[0] == y) && (playerPosition[1] == x));
    }
}
