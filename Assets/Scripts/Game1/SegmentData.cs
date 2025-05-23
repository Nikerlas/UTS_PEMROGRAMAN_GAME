using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SegmentData", menuName = "Game/SegmentNegara")]
public class SegmentData : ScriptableObject

{
    public AudioClip greetingAudio;
    public string correctCountry;
    public Sprite[] flagChoices;
    public string correctFlagName;
    public string funFact;
}