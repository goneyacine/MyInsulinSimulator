using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataManager : MonoBehaviour
{
    [Range(0f,3000f)]
    public float insulineInBlood = 0f;
    [Range(0f,3000f)]
    public float glucagonInBlood = 0f;
    [Range(0f,3f)]
    public float glucoseInBlood = 1.2f;
    [Range(.1f,3f)]
    public float updatingRate = 1f;
    
    private float lastUpdateTime;
    public List<float> glucoseData = new List<float>();
    public List<float> insulineData = new List<float>();
    public List<float> glucagonData = new List<float>();
    public Vector2 graphDimensions;
    public LineRenderer glucoseGraph;
    public LineRenderer insulineGraph;
    public LineRenderer glucagonGraph;
    public Slider glucoseSlider,secondGlucoseSlider,thirdGlucoseSlider;
    public Slider insulineSlider,secondInsulineSlider,thirdInsulineSlider;
    public Slider glucagonSlider,secondGlucagonSlider,thirdGlucagonSlider;
    public ParticleSystem glucosePartical;
    public ParticleSystem insulinePartical;
    public ParticleSystem glucagonPartical;
    private void Start(){
    UpdateData();
    lastUpdateTime = Time.time;
    }
    private void Update(){
    if(Time.time - lastUpdateTime >= updatingRate){
    UpdateData();
    lastUpdateTime = Time.time;
    }
   }


    public void UpdateData(){
    //first updating glucose value
       // first using insuline
    if(insulineInBlood > 0f && glucoseInBlood > 0f){
    if(insulineInBlood >= (3000f/60f)){
       insulineInBlood -= (3000f/60f);
       glucoseInBlood -= (3000f/60) * (1.8f/3000f);
    }else{
    	glucoseInBlood -= insulineInBlood * (1.8f/3000f);
    	insulineInBlood = 0;
    }
   }
      //second using glucagon 
      if(glucagonInBlood > 0f && glucoseInBlood < 3f){
    if(glucagonInBlood >= (3000f/60f)){
       glucagonInBlood -= (3000f/60f);
       glucoseInBlood += (3000f/60) * (.7f/3000f);
    }else{
    	glucoseInBlood += glucagonInBlood * (.7f/3000f);
    	glucagonInBlood = 0;
    }
   }
   if(glucoseInBlood < 0)
   glucoseInBlood = 0;
   else if(glucoseInBlood > 3)
   glucoseInBlood =3f;
   // Second, updating insuline value
   if(glucoseInBlood - (insulineInBlood * (1.8f / 3000f)) > 1.2f){
   	float neededInsuline = (glucoseInBlood - (insulineInBlood * (1.8f / 3000f))) / (1.8f/3000f);
   	if(neededInsuline > (3000f/15f))
   	insulineInBlood += (3000f/15f);
   	else 
   	insulineInBlood += neededInsuline;
   }
   // Third, updating glucagon value
     if(glucoseInBlood + (glucagonInBlood * (.7f/ 3000f)) < .7f){
   	float neededGlucagon = (glucoseInBlood + (glucagonInBlood * (.7f / 3000f))) / (.7f/3000f);
   	if(neededGlucagon > (3000f/15f))
   	glucagonInBlood += (3000f/15f);
   	else 
   	glucagonInBlood += neededGlucagon;
   }

   if(glucoseInBlood < 0)
   glucoseInBlood = 0;
   else if(glucoseInBlood > 3)
   glucoseInBlood =3f;

   if(insulineInBlood < 0)
   insulineInBlood = 0;
   else if(insulineInBlood > 3000f)
   insulineInBlood = 3000f;

   if(glucagonInBlood < 0)
   glucagonInBlood = 0;
   else if(glucagonInBlood > 3000f)
   glucagonInBlood = 3000f;
   //updating the graphs data
   glucoseData = UpdateGraphData(glucoseData,glucoseInBlood);
   insulineData = UpdateGraphData(insulineData,insulineInBlood);
   glucagonData = UpdateGraphData(glucagonData,glucagonInBlood);
   //updating the graphs visuals
   UpdateGraph(glucoseGraph,glucoseData,3f);
   UpdateGraph(insulineGraph,insulineData,3000f);
   UpdateGraph(glucagonGraph,glucagonData,3000f);
   //setting the sliders values
   glucoseSlider.value = glucoseInBlood;
   secondGlucoseSlider.value = glucoseInBlood;
   thirdGlucoseSlider.value = glucoseInBlood;

   glucagonSlider.value = glucagonInBlood;
   secondGlucagonSlider.value = glucagonInBlood;
   thirdGlucagonSlider.value = glucagonInBlood;

   insulineSlider.value = insulineInBlood;
   secondInsulineSlider.value = insulineInBlood;
   thirdInsulineSlider.value = insulineInBlood;
   //setting the glucose particals rate
   var glucoseParticalEmission = glucosePartical.emission;
   glucoseParticalEmission.rate = (30/3) * glucoseInBlood;
    //setting the insuline particals rate 
   var insulineParticalEmission = insulinePartical.emission;
   insulineParticalEmission.rate = (12 * insulineInBlood / 3000);
   //setting the glucagon particals rate
   var glucagonParticalEmission = glucagonPartical.emission;
   glucagonParticalEmission.rate = (12 * glucagonInBlood / 3000);
 }
 public List<float> UpdateGraphData(List<float> data,float newInput){
 	if(data.Count < 120){
 		data.Add(newInput);
 	}else{
 	 List<float> newData = new List<float>();
 	 for(int i = 1; i < 120;i++)
 	 newData.Add(data[i]);

 	 newData.Add(newInput);
 	 data = newData;
 	}
 	return data;
 }
 public void UpdateGraph(LineRenderer graph,List<float> data,float maxDataValue){
 	graph.positionCount = data.Count;
 	for(int i = 0;i <data.Count;i++){
 		graph.SetPosition(i,new Vector2( i * (graphDimensions.x /(120/updatingRate)),
 			                             data[i] * (graphDimensions.y /maxDataValue)));
 	}
 }
 public void OnGlucoseSliderValueChanged(int sliderIndex){
 	if(sliderIndex == 0)
 	glucoseInBlood = glucoseSlider.value;
 	else if(sliderIndex == 1)
 	glucoseInBlood = secondGlucoseSlider.value;
 	else 
 	glucoseInBlood = thirdGlucoseSlider.value;
 }
 public void OnInsulineSliderValueChanged(int sliderIndex){
 	if(sliderIndex == 0)
 	insulineInBlood = insulineSlider.value;
 	else if(sliderIndex == 1)
 	insulineInBlood = secondInsulineSlider.value;
 	else 
 	insulineInBlood = thirdInsulineSlider.value;
 }
 public void OnGlucagonSliderValueChanged(int sliderIndex){
 	if(sliderIndex == 0)
 	glucagonInBlood = glucagonSlider.value;
 	else if(sliderIndex == 1)
 	glucagonInBlood = secondGlucagonSlider.value;
 	else 
 	glucagonInBlood = thirdGlucagonSlider.value;
 }
}
