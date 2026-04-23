using Unity.VisualScripting;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public LayerMask dropZoneMask;
    public LayerMask floorZoneMask;

    public float maxDistance = 800f;

    Terrain terrain;
    new Camera camera;
    GameObject dragTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        terrain = Terrain.activeTerrain;
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out var hit))
            {
                if (hit.collider.CompareTag("Unit"))
                {
                    dragTarget = hit.collider.gameObject;
                    dragTarget.GetComponent<DragTarget>().StopMove();
                }
            }

        }
        
        if (Input.GetMouseButton(0))
        {
            if (dragTarget == null)
            {
                return;
            }

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, maxDistance, floorZoneMask))
            {
                Vector3 targetPos = hit.point;
                targetPos.y += 25f;
                dragTarget.transform.position = targetPos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (dragTarget == null)
            {
                return;
            }

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, maxDistance, dropZoneMask))
            {
                dragTarget.GetComponent<DragTarget>().StopMove();
            }
            else
            {
                dragTarget.GetComponent<DragTarget>().ReturnHome();
            }
            dragTarget = null;
        }
    }
}
