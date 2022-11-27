using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public enum Weathers
    {
        Sunny,
        Rain,
        Snow
    }

    public Weathers weather = Weathers.Sunny;

    [SerializeField] ParticleSystem rainEffect;
    [SerializeField] ParticleSystem snowEffect;

    private void Start()
    {
        rainEffect.Stop();
        snowEffect.Stop();
    }

    public void ChangeWeather()
    {
        weather = (Weathers)Random.Range(0, System.Enum.GetValues(typeof(Weathers)).Length);
        // System.Enum.GetValues(typeof(Weathers)).Length is define the length of the enum

        switch (weather)
        {
            case Weathers.Sunny:
                rainEffect.Stop();
                snowEffect.Stop();
                Debug.Log("Sun is shining");
                break;
            case Weathers.Rain:
                rainEffect.Play();
                snowEffect.Stop();
                Debug.Log("Start raining");
                break;
            case Weathers.Snow:
                rainEffect.Stop();
                snowEffect.Play();
                Debug.Log("Start snowing");
                break;
            default:
                Debug.LogWarning("Incorrect weather name!");
                break;
        }
    }
}
