using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_InsulineManager : MonoBehaviour
{
  int needFullInsulineResevers;
  public List<InsulineResever> emptyInsulineResevers = new List<InsulineResever>();
  public List<InsulineResever> fullInsulineResevers = new List<InsulineResever>();
  float lastUpdateTime;
  public DataManager dataManager;
  [Range(0f,20f)]
  public float insulineValueInCell = 0f;
  public float glucoseValueInCell = 0f;
  public List<GlucoseResever> enabledGlucoseResevers = new List<GlucoseResever>();
  public List<GlucoseResever> disabledGlucoseResevers = new List<GlucoseResever>();
  public List<CenterGlucoseResever> aliveCenterResevers = new List<CenterGlucoseResever>();
  public Transform glucoseBank;
  private void Update(){
  if(Time.time - lastUpdateTime >= .15f){
   needFullInsulineResevers = (int)(dataManager.insulineInBlood / 300f );
   if(fullInsulineResevers.Count < needFullInsulineResevers){
   	InsulineResever randomEmptyResever = emptyInsulineResevers[(int)Random.Range(0,emptyInsulineResevers.Count - 1)];
   	emptyInsulineResevers.Remove(randomEmptyResever);
   	randomEmptyResever.canSpawn = true;
   	fullInsulineResevers.Add(randomEmptyResever);
   }else if(fullInsulineResevers.Count > needFullInsulineResevers){
    InsulineResever randomFullResever = fullInsulineResevers[(int)Random.Range(0,fullInsulineResevers.Count - 1)];
   	fullInsulineResevers.Remove(randomFullResever);
   	randomFullResever.canSpawn = false;
   	emptyInsulineResevers.Add(randomFullResever);
   }
   insulineValueInCell -= .175f;
   if(insulineValueInCell < 0f)
   insulineValueInCell = 0f;
   else if(insulineValueInCell > 20f)
   insulineValueInCell = 20f;
   if(glucoseValueInCell < 0)
   glucoseValueInCell = 0;
   else if(glucoseValueInCell > 50f)
   glucoseValueInCell = 50f;
 
  glucoseBank.localScale = glucoseValueInCell * (.45f / 50f) * Vector2.one;

   if(enabledGlucoseResevers.Count < (int)insulineValueInCell){
   	CenterGlucoseResever randomCenterResever = aliveCenterResevers[(int)Random.Range(0,aliveCenterResevers.Count -1)];
   	aliveCenterResevers.Remove(randomCenterResever);
   	GlucoseResever randomGlucoseResever = disabledGlucoseResevers[(int)Random.Range(0,disabledGlucoseResevers.Count -1)];
   	disabledGlucoseResevers.Remove(randomGlucoseResever);
   	enabledGlucoseResevers.Add(randomGlucoseResever);
   	randomCenterResever.target = randomGlucoseResever.transform;
   }else if (enabledGlucoseResevers.Count > (int)insulineValueInCell)
   {
   	GlucoseResever randomGlucoseResever = enabledGlucoseResevers[(int)Random.Range(0,enabledGlucoseResevers.Count -1)];
   	enabledGlucoseResevers.Remove(randomGlucoseResever);
   	disabledGlucoseResevers.Add(randomGlucoseResever);
   	randomGlucoseResever.gameObject.SetActive(false);
   }
   lastUpdateTime = Time.time;
  }
 }  
}
