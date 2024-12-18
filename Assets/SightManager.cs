using UnityEngine;
using UnityEngine.UI;

public class SightManager : MonoBehaviour
{
    public GameObject blind;
    public GameObject vals;
    int[] schedule_chosen;
    float jitter_value;
    float timer;
    float period;
    float nextActionTime;
    int GameOver;
    RawImage ScreenshotOverlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
        period = 2.0f;
        nextActionTime = 0;

        blind = GameObject.Find("Blind");
        blind.SetActive(false);

        GameObject valObj = GameObject.Find("Values");
        Values vals = valObj.GetComponent<Values>();
       
        schedule_chosen = vals.getSchedule();
        
        jitter_value = vals.getJitter();

        GameObject scoverlay = GameObject.Find("Screenshot");
        ScreenshotOverlay = scoverlay.GetComponent<RawImage>();

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
        timer += Time.deltaTime;
        Debug.Log("time " + timer + "Atime " + nextActionTime + " jv " + jitter_value, this);

        //Debug.Log("tim" + timer);
        if (timer >= nextActionTime) {
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
                TakeScreenshot();
                ScreenshotOverlay.gameObject.SetActive(true);
                blind.SetActive(true);
            }
        }

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
