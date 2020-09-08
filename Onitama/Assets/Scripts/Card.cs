using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Card : ScriptableObject
{
    public List<int[]> moves { private set; get; }
    public Sprite sprite;
    public bool isActive;
    public int team;
}
