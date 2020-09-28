using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour {
	public Dropdown resolutionDropdown;
	public Slider musicVolumeSlider;
	public Button applyButton;

	public AudioSource musicSource;
	public Resolution[] resolutions;
	public GameSettings gameSettings;

	void OnEnable()
	{
		gameSettings = new GameSettings();

		resolutionDropdown.onValueChanged.AddListener (delegate { OnResolutionChange(); });
		musicVolumeSlider.onValueChanged.AddListener (delegate { OnMusicVolumeChange(); });
		applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

		resolutions = Screen.resolutions;
		foreach (Resolution resolution in resolutions)
		{
			resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
		}
	}

	public void OnResolutionChange()
	{
		Screen.SetResolution(resolutions [resolutionDropdown.value].width, resolutions [resolutionDropdown.value].height, false);
	}

	public void OnMusicVolumeChange()
	{
		musicSource.volume = gameSettings.musicVolume = musicVolumeSlider.value;
	}

	public void OnApplyButtonClick()
	{
		SaveSettings();
	}

	public void SaveSettings()
	{
		string jsonData = JsonUtility.ToJson(gameSettings, true);
		File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
	}

	public void LoadSettings()
	{
		
	}
}
