using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public BaseFaction AIFaction;
    
    private List<Planet> myPlanets;
    private List<Planet> neutralPlanets;
    private List<Planet> enemyPlanets;
    private Planet[] allPlanets;

    private int myTotalPopulation;
    private Planet tempNeutralPlanet;
    private Planet tempEnemyPlanet;
    
    private void OnEnable()
    {
        Timer.AITick += Analyze;
    }

    private void OnDisable()
    {
        Timer.AITick -= Analyze;
    }

    private void Start()
    {
        myPlanets = new List<Planet>();
        neutralPlanets = new List<Planet>();
        enemyPlanets = new List<Planet>();
        allPlanets = FindObjectsOfType<Planet>();
        
        foreach (var planet in allPlanets)
        {
            if (planet.BaseFaction == BaseFaction.None)
            {
                neutralPlanets.Add(planet);
            }
            else if (planet.BaseFaction == AIFaction)
            {
                myPlanets.Add(planet);
            }
            else 
            {
                enemyPlanets.Add(planet);
            }
        }
    }

    private void Analyze()
    {
        AnalyzePlanetsFaction();
        tempNeutralPlanet = CheckForNeutralPlanets();
        myTotalPopulation = CalculateTotalPopulation();
        if (tempNeutralPlanet != null && tempNeutralPlanet.Population < myTotalPopulation)
        {
            SendUnitsFromBases(tempNeutralPlanet);
        }
        else if (enemyPlanets.Count == 0)
        {
            return;
        }
        else if (tempNeutralPlanet == null)
        {
            tempEnemyPlanet = FindLowerPopulationEnemyBase();
            if (tempEnemyPlanet.Population + 5 < myTotalPopulation)
            {
                SendUnitsFromBases(tempEnemyPlanet);
            }
        }
        
    }

    private void SendUnitsFromBases(Planet target)
    {
        for (int i = 0; i < myPlanets.Count; i++)
        {
            myPlanets[i].SendAllUnits(target.transform);
        }
    }
    
    private Planet FindLowerPopulationEnemyBase()
    {
        Planet temp = enemyPlanets[0];
        for (int i = 0; i < enemyPlanets.Count; i++)
        {
            if (enemyPlanets[i].Population < temp.Population)
            {
                temp = enemyPlanets[i];
            }
        }

        return temp;
    }

    private void AnalyzePlanetsFaction()
    {
        enemyPlanets.Clear();
        neutralPlanets.Clear();
        myPlanets.Clear();
        
        foreach (var planet in allPlanets)
        {
            if (planet.BaseFaction == BaseFaction.None)
            {
                neutralPlanets.Add(planet);
            }
            else if (planet.BaseFaction == AIFaction)
            {
                myPlanets.Add(planet);
            }
            else 
            {
                enemyPlanets.Add(planet);
            }
        }
    }
    
    private Planet CheckForNeutralPlanets()
    {
        for (int i = 0; i < allPlanets.Length; i++)
        {
            if (allPlanets[i].BaseFaction == BaseFaction.None)
            {
                return allPlanets[i];
            }
        }
        return null;
    }

    private int CalculateTotalPopulation()
    {
        int temp = 0;
        
        foreach (var t in myPlanets)
        {
            temp += t.Population;
        }

        return temp;
    }
}
