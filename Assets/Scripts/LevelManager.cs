using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int SceneIndex;
    public float CoinsToAdd;

    private Planet[] allPlanets;
    
    private void OnEnable()
    {
        Planet.PlanetSateChanged += CheckLoseCondition;
        Planet.PlanetSateChanged += CheckWinCondition;
    }

    private void OnDisable()
    {
        Planet.PlanetSateChanged -= CheckLoseCondition;
        Planet.PlanetSateChanged -= CheckWinCondition;
    }

    private void Start()
    {
        allPlanets = FindObjectsOfType<Planet>();
    }

    private void CheckWinCondition()
    {
        for (int i = 0; i < allPlanets.Length; i++)
        {
            if (allPlanets[i].BaseFaction == BaseFaction.Player)
            {
                continue;
            }
            else
            {
                return;
            }
        }

        SetSceneId();
        SceneManager.LoadScene(0);
    }

    private void CheckLoseCondition()
    {
        for (int i = 0; i < allPlanets.Length; i++)
        {
            if (allPlanets[i].BaseFaction == BaseFaction.Player)
            {
                return;
            }
            else
            {
                Debug.Log("Поражение");
            }
        }
    }

    private void SetSceneId()
    {
        SceneIndex++;
        if (Data.Instance.MaxSceneId < SceneIndex)
        {
            Data.Instance.MaxSceneId = SceneIndex;
        }

    }
}
