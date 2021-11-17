using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
   public static event Action<TickType> SpawnTick;
   public static event Action AITick;

   public Data Data;
   
   private float playerSpawnTime;
   private float playerDeltaTick;
   private float botSpawnTime;
   private float botDeltaTick;

   private float botAnalyzeTime;
   private float botTime;
   
   private void Start()
   {
      botTime = Time.time;
      botAnalyzeTime = Data.BotAnalyzeTime;
      
      playerSpawnTime = Time.time;
      botSpawnTime = Time.time;
      playerDeltaTick = Data.ReproductionTime;
      botDeltaTick = Data.EnemyReproductionTime;
   }

   private void Update()
   {
      if (Time.time - playerSpawnTime > playerDeltaTick)
      {
         SpawnTick?.Invoke(TickType.Player);
         playerSpawnTime = Time.time;
      }
      
      if (Time.time - botSpawnTime > botDeltaTick)
      {
         SpawnTick?.Invoke(TickType.AI);
         botSpawnTime = Time.time;
      }
      
      if (Time.time - botTime > botAnalyzeTime)
      {
         AITick?.Invoke();
         botTime = Time.time;
      }
   }
}

public enum TickType
{
   Player = 0,
   AI = 1
}