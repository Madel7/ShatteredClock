using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System.Linq;

public class OptionsMenuManager : MonoBehaviour
{
    public AudioMixer mainMixer;

    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        //transform.SetParent(null);
        //DontDestroyOnLoad(gameObject);

        //exposure = GameObject.Find("Brightness").GetComponent<AutoExposure>();
        brightness.TryGetSettings(out exposure);
        SetBrightness(brightnessSlider.value);

        //resolutionDropdown = GameObject.Find("ResolutionOption").GetComponent<TMP_Dropdown>();
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;
        
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        float db = Mathf.Log10(volume) * 20;
        mainMixer.SetFloat("volume", db);
    }

    public void SetBrightness(float value)
    {
        exposure.keyValue.value = value / 10;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
