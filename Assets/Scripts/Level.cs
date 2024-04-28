using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Task
{
    public ItemType ItemType;
    public int Number;
    public int LevelNumber;
}


public class Level : MonoBehaviour
{
    public int NumberOfBalls = 50;
    public int MaxCreatedBallLevel = 1;
    public Task[] Tasks;

}
