using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLevel : MonoBehaviour
{
    public Button[] LevelButtons;

    private void Start()
    {
        for (int i = 0; i < Data.Instance.MaxSceneId; i++)
        {
            LevelButtons[i].interactable = true;
        }
    }

    public void LoadLevel(int id)
    {
        SceneManager.LoadScene(id);
    }
}
