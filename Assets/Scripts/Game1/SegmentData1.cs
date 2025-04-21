using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SegmentData1", menuName = "Game/SegmentMakanan")]
public class SegmentData1 : ScriptableObject
{
    
    public Sprite[] imagesNew;
    public string correctFoodName;
    public Sprite[] foodChoices;
    public string funFact;
}