using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{
    private int x, y;
    private List<int[]> neighbors = new List<int[]>();
    private bool ready = false; // used by slimes to let them know the neighbors are finished setting up

    void Start()
    {
        setNeighbors();
    }
    public void setIndex(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }
    public int getNum()
    {
        return neighbors.Count;
    }

    public bool getState()
    {
        return ready;
    }
    public int[] getNeighbor(int i)
    {
        return neighbors[i];
    }

    public void setNeighbors()
    {
        GridScript gridScript = GameObject.Find("Grid").GetComponent<GridScript>();
        int c = gridScript.column;
        int r = gridScript.row;

        // the following checks neighbors in a cross pattern
        for (int i = x-1; i <= x+1; i += 2) // skip x itself dont need to check own position
            if ((i >= 0 && i < c))
                neighbors.Add(new int[] { y, i });

        for (int j = y-1; j <= y+1; j += 2) // same reason here
                if ((j >= 0 && j < r))
                    neighbors.Add(new int[] { j, x });

        ready = true;
    }
}
