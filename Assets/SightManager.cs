using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SightManager : MonoBehaviour
{
    public GameObject blind;
    public GameObject vals;
    int schedule_chosen;
    int jitter_value;
    float timer;
    float period;
    float nextActionTime;
    int GameOver;
    RawImage ScreenshotOverlay;
    int ind;
    float nextSight;

    //public TMP_Text expValue;
    //public TMP_Text actValue;

    int [] Schedule1_RMS;
    int [] Schedule1_EDF;

    int [] sched;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Schedule1_RMS = new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 1, 1, 1, 1, 1, 4, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 7, 7, 7, -1, -1, -1, -1, -1, -1, -1, 5, 5, 5, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 7, 7, 7, -1, -1, -1, 1, 1, 1, 1, 1, -1, -1, -1, -1, -1, 5, 5, 5, 5, -1, -1, -1, -1, -1, -1, 3, 3, 3, 3, 3, 3, 3, 6, 6, 6, 6, 6, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Schedule1_EDF = new[] {1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,5,5,5,5,3,3,3,3,3,3,
                                         1,1,1,1,1,3,6,6,6,6,7,7,7,6,6,5,5,5,5,5,5,5,5,-1,-1,-1,-1,-1,-1,-1,5,5,5,5,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                                         1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,5,5,5,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,7,7,7,-1,-1,-1,
                                         1,1,1,1,1,-1,-1,-1,-1,-1,5,5,5,5,-1,-1,-1,-1,-1,-1,6,6,6,6,6,6,3,3,3,3,3,3,3,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                                         5,5,5,5,1,1,1,1,1,0,7,7,7,0,0,0,0,0,0,0,0};

        
        timer = 0f;
        period = 2.0f;
        nextActionTime = 0;
        ind = 0 ;
        //blind = GameObject.Find("Blind");
        //blind.SetActive(false);

        GameObject valObj = GameObject.Find("Values");
        Values vals = valObj.GetComponent<Values>();
       
        schedule_chosen = vals.getSchedule();

        if (schedule_chosen == 0){
            sched = Schedule1_RMS;
        }

        if (schedule_chosen == 1){
            sched = Schedule1_EDF;
        }        

        jitter_value = vals.getJitter();

        //GameObject scoverlay = GameObject.Find("Screenshot");
        //ScreenshotOverlay = scoverlay.GetComponent<RawImage>();

        if (jitter_value == 0 ){
            jitter_value += 1;
        }
        else{
           // jitter_value = 100/jitter_value;

        }
        

    }

    // Update is called once per frame


    void Update () 
    {
        //actValue.text = timer.ToString();
        if(sched[ind]==-10){
            ind = 0;
            //it loops
        }
        timer += Time.deltaTime;
      //  Debug.Log(nextActionTime);
       // Debug.Log(sched[ind]);
        if(timer>=nextActionTime){
            if(sched[ind] == 0){
                //sight!
                blind.SetActive(false);
            }
            else{
                blind.SetActive(true);
            }

            if(sched[ind+1] == 0 && sched[ind]!=0){
                nextSight = timer;
                float jitter = Random.Range(0, jitter_value);
                nextActionTime += jitter;
                //expValue = nextSight.ToString();
               // expValue.text = nextSight.ToString();
                //actValue.text = nextActionTime.ToString();

                //actValue = nextActionTime.ToString();
                //blind.GetComponent<UnityEngine.UI.Text>().text = nextSight.ToString();
                //  mytext = textobj.GetComponent<TMP_Text>();

                //txt = blind.gameObject.transform.GetChild(2).gameObject.text;

            }
            nextActionTime += .05f;
            ind ++;
        }




/*
        float jitter = Random.Range(0, jitter_value);
        
        if(timer>=nextActionTime){
            Debug.Log(jitter);
            nextActionTime += .05f + jitter;
            ind++;
        }
*/
       // Debug.Log(sched[ind]);


        
        
        /*
        
        Debug.Log("time " + timer + "Atime " + nextActionTime + " jv " + jitter_value, this);

        //Debug.Log("tim" + timer);
        if (sched[Math.floor(timer)] == 0) {
            float jitter = Random.Range(0, jitter_value);
            nextActionTime = nextActionTime + period + jitter;
            Debug.Log("j" + jitter, this);

            if (blind.activeSelf)
            {
                blind.SetActive(false);
            }
            else
            {
                // Take screenshot and then activate that image
  //              TakeScreenshot();
    //            ScreenshotOverlay.gameObject.SetActive(true);
                blind.SetActive(true);
            }
        }

    */
    }



    void TakeScreenshot()
    {
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();
        ScreenshotOverlay.texture = screenshot;
        return;
    }
}


