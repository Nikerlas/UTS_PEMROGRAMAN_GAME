using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float batasAtas = 4.5f;
    public float batasBawah = -4.5f;
    public float batasKiri = -7f;
    public float batasKanan = 7f;

    public float kecepatan = 5f;
    public string axis = "Vertical";
    public string axisX = "Horizontal";

    public bool isAI = false;         // Aktifkan jika paddle ini dikendalikan AI
    public Transform bola;           // Drag objek bola ke sini dari inspector
    public float kecepatanAI = 4f;   // AI bisa beda kecepatannya

    void Update()
    {
        if (isAI)
        {
            JalankanAI();
        }
        else
        {
            JalankanManual();
        }
    }

    void JalankanManual()
    {
        float gerakY = Input.GetAxis(axis) * kecepatan * Time.deltaTime;
        float gerakX = Input.GetAxis(axisX) * kecepatan * Time.deltaTime;

        Vector3 nextPos = transform.position + new Vector3(gerakX, gerakY, 0);

        nextPos.y = Mathf.Clamp(nextPos.y, batasBawah, batasAtas);
        nextPos.x = Mathf.Clamp(nextPos.x, batasKiri, batasKanan);

        transform.position = nextPos;
    }

    void JalankanAI()
    {
        if (bola == null) return;

        Vector3 targetPos = transform.position;

        // Gerakkan paddle hanya di sumbu Y (bisa diubah kalau mau gerakan X juga)
        if (bola.position.y > transform.position.y + 0.1f)
            targetPos.y += kecepatanAI * Time.deltaTime;
        else if (bola.position.y < transform.position.y - 0.1f)
            targetPos.y -= kecepatanAI * Time.deltaTime;

        targetPos.y = Mathf.Clamp(targetPos.y, batasBawah, batasAtas);
        targetPos.x = Mathf.Clamp(targetPos.x, batasKiri, batasKanan);

        transform.position = targetPos;
    }
}
