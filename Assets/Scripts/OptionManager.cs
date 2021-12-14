using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions(); // clear template dropdown options

        int currentResolutionIndex = 0;

        // finding the actual available options
        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // find the current resolution
            if (
                (resolutions[i].width == Screen.width)
                &&
                (resolutions[i].height == Screen.height)
               )
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options); // re-adding the available options
        resolutionDropdown.value = currentResolutionIndex; // input the current resolution as current value
        resolutionDropdown.RefreshShownValue();
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        Debug.Log(isFullscreen ? "Going Fullscreen!" : "Fullscreen mode off");
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume (float volume)
    {
        Debug.Log("Value is at " + volume);
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Debug.Log("Screen resolution is at " + resolution.width + " x " + resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
