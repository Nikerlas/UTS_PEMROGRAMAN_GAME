using UnityEngine;

public class ArenaLoader : MonoBehaviour
{
    public SpriteRenderer bgImage; // ⬅️ ubah dari Image ke SpriteRenderer
    public Transform spawnPaddle1, spawnPaddle2, spawnBall;
    public AudioSource bgmPlayer;

    void Start()
    {
        ArenaData data = ArenaInfo.selectedArena;
        if (data == null) return;

        bgImage.sprite = data.fieldBackground;

        GameObject p1 = Instantiate(data.paddle1Prefab, spawnPaddle1.position, Quaternion.identity);
        GameObject p2 = Instantiate(data.paddle2Prefab, spawnPaddle2.position, Quaternion.identity);
        GameObject bola = Instantiate(data.bolaPrefab, spawnBall.position, Quaternion.identity);

        bgmPlayer.clip = data.bgm;
        bgmPlayer.Play();

        // 🔁 Assign bola ke paddle AI
        PaddleController ai1 = p1.GetComponent<PaddleController>();
        PaddleController ai2 = p2.GetComponent<PaddleController>();

        if (ai1 != null && ai1.isAI)
            ai1.bola = bola.transform;

        if (ai2 != null && ai2.isAI)
            ai2.bola = bola.transform;
    }

}
