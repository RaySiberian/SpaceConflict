using UnityEngine;

public class Test : MonoBehaviour
{
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Data.Instance.Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Data.Instance.Load();
        }
    }
}