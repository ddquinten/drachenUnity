using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scoreDisplay : MonoBehaviour
{
    public TMP_Text score;
    public Transform GlobalEvents;

    private GameStatus globals;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMP_Text>();
        globals = GlobalEvents.GetComponent<GameStatus>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(name == "finalScore")
            score.text = "Final Score: " + globals.gameScore;
        else
            score.text = "Score: " + globals.gameScore;
    }
}
