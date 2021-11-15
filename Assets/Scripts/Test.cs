using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 relative;
            relative = transform.TransformDirection(Vector2.up);
            Debug.Log(relative);
            transform.position += (Vector3)relative;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector2 relative;
            relative = transform.TransformDirection(Vector2.right);
            Debug.Log(relative);
            transform.position += (Vector3)relative;
        }
    }
}
