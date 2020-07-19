using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> actions;
    public List<Level> levels;
    public List<GameObject> actionContent;


    public int currentLevel = 0;

    // timer
    float currCountdownValue;
    public Text timerText;


    public GameObject puzzleCanvas;
    public GameObject startGameCanvas;
    public GameObject gameOverCanvas;
    public GameObject nextLevelCanvas;
    public GameObject finish;

    public GameObject playerPrefab;


    public GameObject legendCanvas;
    public List<GameObject> legendChips;
    public List<GameObject> legendBulbs;


    public List<Sprite> symbols;
    public List<GameObject> initialPosition;


    public GameObject playerInstance;



    public List<GameObject> rows;

    public Image puzzleStarterImage;

    public bool hasStarted;

    public void StartPuzzle(Level level)
    {
        hasStarted = true;



        puzzleCanvas.SetActive(true);
        level.levelLayout.SetActive(true);
        StartCoroutine(StartCountdown(level.time));
    }

    public IEnumerator StartCountdown(float countdownValue = 10)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue >= 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            timerText.text = currCountdownValue.ToString();
            if (currCountdownValue > 0)
            {
                GetComponent<AudioManager>().Play("Tick");
            }
            else if (currCountdownValue == 0)
            {
                GetComponent<AudioManager>().Play("TickTickTick");
            }
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        StartGamePlay();

    }

    public void StartGamePlay()
    {
        puzzleCanvas.SetActive(false);

        var player = Instantiate(playerPrefab);

    }

    public void StartFirstGame()
    {
        currentLevel = 0;
        startGameCanvas.SetActive(false);
        StartPuzzle(levels[0]);
    }

    public void GameOver()
    {
        hasStarted = false;
        gameOverCanvas.SetActive(true);
    }


    public void Restart()
    {
        startGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        nextLevelCanvas.SetActive(false);

        RemovePlayer();
        RemoveOldLevels();




        legendCanvas.SetActive(true);

        //StartPuzzle(levels[currentLevel]);
    }


    public void RemoveOldLevels()
    {
        levels.ForEach(level => level.levelLayout.SetActive(false));
    }


    public void RemovePlayer()
    {
        var pieces = GameObject.FindGameObjectsWithTag("Piece");

        foreach (var piece in pieces)
        {
            Destroy(piece);
        }


        var lastPlayer = FindObjectOfType<PlayerMovement>();
        Debug.Log(lastPlayer);
        if (lastPlayer != null)
        {
            Destroy(lastPlayer.gameObject);
        }
    }

    public void Finish()
    {
        currentLevel++;
        hasStarted = false;
        if (currentLevel == 4)
        {
            finish.SetActive(true);
        }
        else
            nextLevelCanvas.SetActive(true);
    }



    public void LegendPress()
    {
        if (!hasStarted)
        {
            StartPuzzle(levels[currentLevel]);
        }
        legendCanvas.SetActive(false);

    }

    public void ActivateLegendCanvas()
    {
        legendCanvas.SetActive(true);
        startGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        nextLevelCanvas.SetActive(false);

        for (int i = 0; i < initialPosition.Count; i++)
        {
            var transformParent = actions[i].transform;
            var transformChild = initialPosition[i].transform;
            transformChild.SetParent(transformParent, false);
        }

        //for (int i = 0; i < levels[currentLevel].levelDirections.Count; i++)
        //{
        //    var transformParent = actions[i].transform;
        //    var transformChild = actionContent[levels[currentLevel].levelCell[i]].transform;
        //    transformChild.SetParent(transformParent, false);
        //}


        for (int i = 0; i < levels[currentLevel].symbols.Count; i++)
        {
            actions[i].GetComponentsInChildren<Image>()[1].sprite = levels[currentLevel].symbols[levels[currentLevel].levelStartCell[i]];
        }


        for (int i = 0; i < levels[currentLevel].levelCell.Count; i++)
        {
            legendBulbs[i].GetComponent<Image>().sprite = levels[currentLevel].symbols[levels[currentLevel].finishStartCell[levels[currentLevel].levelCell[i]]];
        }





        RemoveOldLevels();
        RemovePlayer();


        puzzleStarterImage.sprite = levels[currentLevel].puzzleSprite;

        for (int i = 0; i < rows.Count; i++)
        {
            Vector3 temp = rows[i].transform.position;
            int randomIndex = Random.Range(0, rows.Count);
            rows[i].transform.position = rows[randomIndex].transform.position;
            rows[randomIndex].transform.position = temp;
        }

    }

}
