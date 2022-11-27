using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    public float secondsPerMinute = 0.625f;
    public float startTime = 12f;
    public bool showGUI = true;
    public float latitudeAngle = 45f;
    public Transform sunTilt;
    public WeatherManager weatherManager;

    private float day;
    private float min;
    private float smoothMin;

    private float texOffset;
    private Material skyMat;
    private Transform sunOrbit;
    public Light sun;

    private float sunInitialIntensity;

    [SerializeField]
    private TextMeshProUGUI dayLabel;
    [SerializeField]
    private TextMeshProUGUI timeLabel;

    private void Start()
    {
        skyMat = GetComponent<Renderer>().sharedMaterial;
        sunOrbit = sunTilt.GetChild(0);

        sunTilt.eulerAngles = new Vector3(Mathf.Clamp(latitudeAngle, 0, 90), 0, 0);

        sunInitialIntensity = sun.intensity;

        if (secondsPerMinute == 0)
        {
            Debug.LogError("Error! Can't have a time of zero, changed to 0.01 instead.");
            secondsPerMinute = 0.01f;
        }
    }

    private void Update()
    {
        UpdateSky();
        UpdateSun();

        dayLabel.text = "Day " + day.ToString();
        timeLabel.text = digitalDisplay(Mathf.Floor(min / 60).ToString()) + ":" + digitalDisplay((min - Mathf.Floor(min / 60) * 60).ToString());
    }
    void UpdateSky()
    {
        smoothMin = (Time.time / secondsPerMinute) + (startTime * 60);
        day = Mathf.Floor(smoothMin / 1440) + 1;
        if (smoothMin % 1440 == 0)
        {
            Debug.Log("Next Day");
            weatherManager.ChangeWeather();
        } 

        smoothMin = smoothMin - (Mathf.Floor(smoothMin / 1440) * 1440);
        min = Mathf.Round(smoothMin);

        texOffset = Mathf.Cos((((smoothMin) / 1440) * 2) * Mathf.PI) * 0.25f + 0.25f;
        skyMat.mainTextureOffset = new Vector2(Mathf.Round((texOffset - (Mathf.Floor(texOffset / 360) * 360)) * 1000) / 1000, 0);
    }
    void UpdateSun()
    {
        sunOrbit.localEulerAngles = new Vector3(0, smoothMin / 4, 0);

        //sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    string digitalDisplay(string num)
    {
        if (num.Length == 2)
        {
            return num;
        }
        else
        {
            return "0" + num;
        }
    }
}
