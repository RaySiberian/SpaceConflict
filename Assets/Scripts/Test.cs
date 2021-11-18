using UnityEngine;

public class Test : MonoBehaviour
{
    public Data Data;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Data.Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Data.Load();
        }
    }
}