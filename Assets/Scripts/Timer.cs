using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
   public static event Action Tick;
   public static event Action AITick;
   
   private float tickTime;
   private float AItickTime;
   private float t;
   
   private void Start()
   {
      tickTime = Time.time;
      AItickTime = Time.time;
   }

   private void Update()
   {
      if (Time.time - tickTime > 0.02)
      {
         Tick?.Invoke();
         tickTime = Time.time;
      }

      if (Time.time - AItickTime > 3)
      {
         AITick?.Invoke();
         AItickTime = Time.time;
      }
   }
}
