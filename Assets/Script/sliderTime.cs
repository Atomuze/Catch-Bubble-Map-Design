using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class sliderTime : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Slider slider;
    public TextMeshProUGUI sldtxt;
    public ScenceSystem sys = new ScenceSystem();
    public AudioSource audioSource;

    private bool pointerDown = false;

    void Start()
    {
        slider.onValueChanged.AddListener(
            delegate
            {
                sldtxt.text = slider.value.ToString();
            }
        );
    }

    void Update()
    {
        if (!pointerDown)
        {
            slider.SetValueWithoutNotify(audioSource.GetComponent<AudioSource>().time);
            sldtxt.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log(slider.maxValue);
        Debug.Log("slider " + name + " released ");
        pointerDown = false;
        sys.initialize();
        sys.setTheStartTime();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }
}
