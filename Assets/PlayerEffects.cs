using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public int particlePerSecond = 50;
    public List<ParticleSystem> particleSystems;


    // Update is called once per frame
    void Update()
    {
        var gameManager = FindObjectOfType<GameManager>();
        var currentLevelCells = gameManager.levels[FindObjectOfType<GameManager>().currentLevel].levelCell;
        //currentLevelCells.ForEach(cell => {
        //    var cell = game
        //});

        var cell0 = gameManager.actions[currentLevelCells[0]];
        var directionValue0 = cell0.GetComponentInChildren<MoveAction>().directionValue;
        PlayParticleByKeyPress(new KeyCode[] { KeyCode.W, KeyCode.UpArrow }, (directionValue0 + 2) % 4, "jet1");

        var cell1 = gameManager.actions[currentLevelCells[1]];
        var directionValue1 = cell1.GetComponentInChildren<MoveAction>().directionValue;
        PlayParticleByKeyPress(new KeyCode[] { KeyCode.A, KeyCode.LeftArrow }, (directionValue1 + 2) % 4, "jet2");

        var cell2 = gameManager.actions[currentLevelCells[2]];
        var directionValue2 = cell2.GetComponentInChildren<MoveAction>().directionValue;
        PlayParticleByKeyPress(new KeyCode[] { KeyCode.S, KeyCode.DownArrow }, (directionValue2 + 2) % 4, "jet3");


        var cell3 = gameManager.actions[currentLevelCells[3]];
        var directionValue3 = cell3.GetComponentInChildren<MoveAction>().directionValue;
        PlayParticleByKeyPress(new KeyCode[] { KeyCode.D, KeyCode.RightArrow }, (directionValue3 + 2) % 4, "jet4");




    }

    private void OnDestroy()
    {
        FindObjectOfType<AudioManager>().Stop("jet1");
        FindObjectOfType<AudioManager>().Stop("jet2");
        FindObjectOfType<AudioManager>().Stop("jet3");
        FindObjectOfType<AudioManager>().Stop("jet4");

    }

    public void PlayParticleByKeyPress(KeyCode[] keys, int position, string sound)
    {
        if (Input.GetKeyDown(keys[0]) || Input.GetKeyDown(keys[1]))
        {
            Debug.Log("position" + position);
            var pSystem = particleSystems[position];
            pSystem.Play();


            FindObjectOfType<AudioManager>().Play(sound);
        }
        if (Input.GetKeyUp(keys[0]) || Input.GetKeyUp(keys[1]))
        {
            var pSystem = particleSystems[position];
            pSystem.Stop();

            FindObjectOfType<AudioManager>().Stop(sound);

        }
    }
}
