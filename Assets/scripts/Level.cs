using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string name;
    public Sprite puzzleSprite;
    public int time;
    public List<int> levelDirections;

    public List<int> levelCell;
    public List<int> levelStartCell;
    public List<int> finishStartCell;

    public GameObject levelLayout;

    public List<Sprite> symbols;
}
 