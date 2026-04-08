using System.Timers;
using UnityEngine;

public class BezierRandomMover : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float pointMaxRadious = 10f;
    public Vector2 lifeTime;

    public BezierBullet bulletPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dir = endPoint.position - startPoint.position;
            Quaternion q1 = Quaternion.AngleAxis(Random.Range(0, 360), dir);
            Quaternion q2 = Quaternion.AngleAxis(Random.Range(0, 360), dir);

            float range1 = Random.value;
            float range2 = Random.value;
            if (range2 > range1)
            {
                float temp = range1;
                range1 = range2;
                range2= temp;
            }

            float radious1 = Random.Range(0, pointMaxRadious);
            float radious2 = Random.Range(0, pointMaxRadious);

            Vector3 p1 = Vector3.Lerp(startPoint.position, endPoint.position, range1) + q1 * (Vector3.up * radious1);
            Vector3 p2 = Vector3.Lerp(startPoint.position, endPoint.position, range2) + q2 * (Vector3.up * radious2);

            BezierBullet bullet = Instantiate(bulletPrefab);
            bullet.lifeTime = Random.Range(lifeTime.x, lifeTime.y);
            bullet.p0 = startPoint.position;
            bullet.p1 = p1;
            bullet.p2 = p2;
            bullet.p3 = endPoint.position;
            bullet.transform.position = bullet.p0;
        }
    }
}
