using UnityEngine;

public class DragTarget : MonoBehaviour
{
    Vector3 home;
    Terrain terrain;

    public float returnSpeed = 5f;
    bool returningHome = false;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;

        float height = terrain.SampleHeight(transform.position)+25f;
        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        home = transform.position;
    }

    public void Update()
    {
        if (returningHome)
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, home, Time.deltaTime * returnSpeed);
            nextPosition.y = terrain.SampleHeight(nextPosition)+25f;

            transform.position =nextPosition;

            if(Vector3.Distance(home, nextPosition) < 0.05f)
            {
                transform.position = home;
                returningHome = false;
            }
        }   
    }

    public void ReturnHome()
    {
        returningHome =true;
    }
    public void StopMove()
    {
        returningHome =false;
    }
}