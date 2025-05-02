using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    int livesP1;
    int livesP2;
    int maxLives = 5;

    Slider hpBarP1;
    Slider hpBarP2;

    AudioSource audioPlay;
    public AudioClip hitSound;
    public AudioClip goalSound;
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
        livesP1 = maxLives;
        livesP2 = maxLives;

        rb = GetComponent<Rigidbody2D>();
        audioPlay = GetComponent<AudioSource>();

        hpBarP1 = GameObject.Find("HPBarP1").GetComponent<Slider>();
        hpBarP2 = GameObject.Find("HPBarP2").GetComponent<Slider>();

        hpBarP1.maxValue = maxLives;
        hpBarP2.maxValue = maxLives;

        hpBarP1.value = livesP1;
        hpBarP2.value = livesP2;

        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);

        panelGoal = GameObject.Find("PanelGoal");
        panelGoal.SetActive(false);

        startPos = transform.localPosition;

        paddle1 = GameObject.Find("Paddle1(Clone)");
        paddle2 = GameObject.Find("Paddle2(Clone)");
        paddle1StartPos = paddle1.transform.localPosition;
        paddle2StartPos = paddle2.transform.localPosition;

        audioPlay.PlayOneShot(peluitClip);
        MulaiPermainan(1);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "GawangKanan")
        {
            audioPlay.PlayOneShot(goalSound);
            livesP2 -= 1;
            TampilkanLives();

            if (livesP2 <= 0)
            {
                panelSelesai.SetActive(true);
                txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txtPemenang.text = "Player 1 Menang!";
                Destroy(gameObject);
                return;
            }

            StartCoroutine(TampilkanGoalDanReset(1));
        }
        else if (coll.gameObject.name == "GawangKiri")
        {
            audioPlay.PlayOneShot(goalSound);
            livesP1 -= 1;
            TampilkanLives();

            if (livesP1 <= 0)
            {
                panelSelesai.SetActive(true);
                txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txtPemenang.text = "Player 2 Menang!";
                Destroy(gameObject);
                return;
            }

            StartCoroutine(TampilkanGoalDanReset(-1));
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

    void TampilkanLives()
    {
        hpBarP1.value = livesP1;
        hpBarP2.value = livesP2;
    }

    IEnumerator TampilkanGoalDanReset(int arahX)
    {
        StartCoroutine(JedaDanReset(arahX));
        panelGoal.SetActive(true);

        yield return new WaitForSeconds(3f);
        panelGoal.SetActive(false);
    }

    IEnumerator JedaDanReset(int arahX)
    {
        rb.velocity = Vector2.zero;
        transform.localPosition = startPos;

        paddle1.transform.localPosition = paddle1StartPos;
        paddle2.transform.localPosition = paddle2StartPos;

        paddle1.GetComponent<PaddleController>().enabled = false;
        paddle2.GetComponent<PaddleController>().enabled = false;

        yield return new WaitForSeconds(4f);
        audioPlay.PlayOneShot(peluitClip);

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
