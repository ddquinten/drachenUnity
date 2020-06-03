using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public int gameScore = 0;
    public int slimeInc = 2;
    public int collected = 0; // gems collected per spawn of group

    public GameObject Grid;
    public GameObject player;
    public Canvas UI;

    public enum GameState
    {
        PLAYING,
        GAMEOVER
    }

    private GameState state = GameState.PLAYING;
    private GridScript gridScript;


    void Start()
    {
        gridScript = Grid.GetComponent<GridScript>();
    }
    void Update()
    {
        // max points for this level 1220 (122 diamonds)
        if (collected > 122) //greater than because of diamond that spawns on player
        {
            collected = 0;
            gridScript.SpawnDiamonds();
            gridScript.numOfSlimes += slimeInc;
        }

        if (player.GetComponent<CharacterAnim>().isPlayerDead())
        {
            setState(GameState.GAMEOVER);
            UI.transform.GetChild(0).gameObject.SetActive(false); // score
            UI.transform.GetChild(1).gameObject.SetActive(true); // game over screen
        }
            
    }

    public GameState getState()
    {
        return state;
    }
    public void setState(GameState state)
    {
        this.state = state;
    }
}
