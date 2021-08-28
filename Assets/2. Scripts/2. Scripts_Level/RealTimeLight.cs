// 2° MainLevel Script: Is in charge of manage the scary light of the dark room. 

using UnityEngine;

public class RealTimeLight : MonoBehaviour
{
    #region Variables to use:
    public enum WaveForm { sin, tri, sqr, saw, inv, noise };
    public WaveForm funcionDeOnda = WaveForm.sin;

    public float comienzo = 0.0f;
    public float amplitud = 1.0f;
    public float fase = 0.0f;
    public float frecuencia = 0.5f;

    private Color colorOriginal;
    private Light luz;
    #endregion

    #region Frames Methods: 
    void Start()
    {
        luz = GetComponent<Light>();
        colorOriginal = luz.color;
    }

    void Update()
    {
        luz.color = colorOriginal * (EvaluarOnda());
    }
    #endregion

    #region Methods to use:
    float EvaluarOnda()
    {
        float x = (Time.time + fase) * frecuencia;
        float y;
        x = x - Mathf.Floor(x);

        switch (funcionDeOnda)
        {
            case WaveForm.sin:
                y = Mathf.Sin(x * 2 * Mathf.PI);
                break;
            case WaveForm.tri:
                if (x < 0.5f)
                    y = 4.0f * x - 1.0f;
                else
                    y = -4.0f * x + 3.0f;
                break;
            case WaveForm.sqr:
                if (x < 0.5f)
                    y = 1.0f;
                else
                    y = -1.0f;
                break;
            case WaveForm.saw:
                y = x;
                break;
            case WaveForm.inv:
                y = 1.0f - x;
                break;
            case WaveForm.noise:
                y = 1f - (Random.value * 2);
                break;
            default:
                y = 1.0f;
                break;
        }
        return (y * amplitud) + comienzo;
    }
    #endregion
}
