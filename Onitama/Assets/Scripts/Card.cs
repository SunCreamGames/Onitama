using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Card : ScriptableObject
{
    public List<string> moves;
    public Sprite sprite;
    public bool isActive;
}
