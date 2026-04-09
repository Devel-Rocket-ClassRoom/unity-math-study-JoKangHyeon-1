using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class CharacterMover : MonoBehaviour
{
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime*speed, 0f, Input.GetAxis("Vertical")*Time.deltaTime*speed);
        transform.Translate(move);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * speed*20f,0f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.rotation *= Quaternion.Euler(0f, -Time.deltaTime * speed*20f, 0f);
        }
    }
}
