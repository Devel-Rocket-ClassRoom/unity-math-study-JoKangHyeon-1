using UnityEngine;

public class BezierBullet : MonoBehaviour
{
    public float lifeTime = 0f;
    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    public float timer = 0f;

    private TrailRenderer trailRenderer;
    private new Renderer renderer;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        renderer = GetComponent<Renderer>();

        Color c = Random.ColorHSV();
        c.a = 1;

        trailRenderer.material.color = c;
        renderer.material.color = c;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = CubicBezier(p0, p1, p2, p3, timer / lifeTime);
        }
    }

    private Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }
}