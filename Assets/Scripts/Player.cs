using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject OutLine;
    [SerializeField] private Camera MainCam;
    private List<Planet> chosenPlanets;
    private List<GameObject> outLines;
    
    private void Start()
    {
        chosenPlanets = new List<Planet>();
        outLines = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(MainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null)
            {
                chosenPlanets.Clear();
                ClearOutLines();
            }
            else if(hit.collider.gameObject.CompareTag("Player"))
            {
                Planet planet = hit.collider.gameObject.GetComponent<Planet>();
                
                GameObject newOutLine = Instantiate(OutLine, planet.transform.position, quaternion.identity);
                outLines.Add(newOutLine);
               
                if (chosenPlanets.Contains(planet))
                {
                    chosenPlanets.Remove(planet);
                    foreach (var chosenPlanet in chosenPlanets)
                    {
                        chosenPlanet.SendAllUnits(planet.transform);
                    }
                    chosenPlanets.Clear();
                    ClearOutLines();
                }
                else
                {
                    chosenPlanets.Add(planet);
                }
                
            }
            else
            {
                if (chosenPlanets.Count > 0)
                {
                    foreach (var planet in chosenPlanets)
                    {
                        planet.SendAllUnits(hit.transform);
                    }
                    chosenPlanets.Clear();
                    ClearOutLines();
                }
            }
        }
    }

    private void ClearOutLines()
    {
        if (outLines.Count > 0)
        {
            foreach (var outLine in outLines)
            {
                Destroy(outLine);
            }
        }
        outLines.Clear();
    }
}
