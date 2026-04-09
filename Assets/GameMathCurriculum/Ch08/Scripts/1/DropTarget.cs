using UnityEngine;

public class DropTarget : MonoBehaviour
{
    Terrain terrain;
    public bool full = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        terrain = Terrain.activeTerrain;

        float height = terrain.SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
}
