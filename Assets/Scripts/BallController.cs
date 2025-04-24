using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    int scoreP1;
    int scoreP2;

    Text scoreUIP1;
    Text scoreUIP2;

    AudioSource audioPlay;
    public AudioClip hitSound;
    public AudioClip peluitClip;

    GameObject panelSelesai;
    Text txtPemenang;

    GameObject panelGoal;

    public int force;
    Rigidbody2D rb;

    Vector2 startPos;
    Vector2 paddle1StartPos;
    Vector2 paddle2StartPos;

    GameObject paddle1;
    GameObject paddle2;

    void Start()
    {
        scoreP1 = 0;
        scoreP2 = 0;

        rb = GetComponent<Rigidbody2D>();
        audioPlay = GetComponent<AudioSource>();

        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();

        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);

        panelGoal = GameObject.Find("PanelGoal");
        panelGoal.SetActive(false);

        startPos = transform.localPosition;

        paddle1 = GameObject.Find("Paddle1");
        paddle2 = GameObject.Find("Paddle2");
        paddle1StartPos = paddle1.transform.localPosition;
        paddle2StartPos = paddle2.transform.localPosition;

        StartCoroutine(JedaDanReset(1));
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "GawangKanan")
        {
            scoreP1 += 1;
            TampilkanScore();

            if (scoreP1 == 5)
            {
                panelSelesai.SetActive(true);
                txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txtPemenang.text = "Player 1 Menang!";
                Destroy(gameObject);
                return;
            }

            StartCoroutine(TampilkanGoalDanReset("Player 1", 1));
        }
        else if (coll.gameObject.name == "GawangKiri")
        {
            scoreP2 += 1;
            TampilkanScore();

            if (scoreP2 == 5)
            {
                panelSelesai.SetActive(true);
                txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txtPemenang.text = "Player 2 Menang!";
                Destroy(gameObject);
                return;
            }

            StartCoroutine(TampilkanGoalDanReset("Player 2", -1));
        }
        else if (coll.gameObject.name == "Paddle1" || coll.gameObject.name == "Paddle2")
        {
            audioPlay.PlayOneShot(hitSound);

            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rb.velocity.x, sudut);

            if (Mathf.Abs(arah.y) < 0.5f)
            {
                arah.y = 0.5f * Mathf.Sign(arah.y != 0 ? arah.y : Random.Range(-1f, 1f));
            }

            arah = arah.normalized;
            rb.velocity = Vector2.zero;
            rb.AddForce(arah * force * 2);
        }
        else
        {
            audioPlay.PlayOneShot(hitSound);
        }
    }

    void TampilkanScore()
    {
        scoreUIP1.text = scoreP1.ToString();
        scoreUIP2.text = scoreP2.ToString();
    }

    IEnumerator TampilkanGoalDanReset(string player, int arahX)
    {
        panelGoal.SetActive(true);

        yield return new WaitForSeconds(1f);
        panelGoal.SetActive(false);

        StartCoroutine(JedaDanReset(arahX));
    }

    IEnumerator JedaDanReset(int arahX)
    {
        rb.velocity = Vector2.zero;
        transform.localPosition = startPos;

        paddle1.transform.localPosition = paddle1StartPos;
        paddle2.transform.localPosition = paddle2StartPos;

        // Nonaktifkan kontrol paddle (ganti PaddleController dengan script kamu)
        paddle1.GetComponent<PaddleController>().enabled = false;
        paddle2.GetComponent<PaddleController>().enabled = false;

        yield return new WaitForSeconds(1f);
        audioPlay.PlayOneShot(peluitClip);

        // Aktifkan kembali kontrol paddle
        paddle1.GetComponent<PaddleController>().enabled = true;
        paddle2.GetComponent<PaddleController>().enabled = true;

        MulaiPermainan(arahX);
    }

    void MulaiPermainan(int arahX)
    {
        Vector2 arah = new Vector2(arahX, 0).normalized;
        rb.AddForce(arah * force);
    }
}
