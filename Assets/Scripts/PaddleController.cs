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

    void Update()
    {
        float gerakY = Input.GetAxis(axis) * kecepatan * Time.deltaTime;
        float gerakX = Input.GetAxis(axisX) * kecepatan * Time.deltaTime;

        Vector3 nextPos = transform.position + new Vector3(gerakX, gerakY, 0);

        // Clamp biar tidak menembus tepian
        nextPos.y = Mathf.Clamp(nextPos.y, batasBawah, batasAtas);
        nextPos.x = Mathf.Clamp(nextPos.x, batasKiri, batasKanan);

        transform.position = nextPos;
    }
}
