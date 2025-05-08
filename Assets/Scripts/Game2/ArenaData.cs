using UnityEngine;

[CreateAssetMenu(fileName = "ArenaData", menuName = "Game/ArenaData")]

public class ArenaData : ScriptableObject
{
    public string namaArena;
    public Sprite previewImage;
    public GameObject bolaPrefab;
    public GameObject paddle1Prefab;
    public GameObject paddle2Prefab;
    public Sprite fieldBackground;
    public AudioClip bgm;

    public Difficulty difficulty; // 🎯 Tambahan di sini
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}
