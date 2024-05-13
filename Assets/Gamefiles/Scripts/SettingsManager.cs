using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{


    [Header("Elements")]
    [SerializeField] private GameObject resetPrompt;
    [SerializeField] private Slider mergeForceSlider;
    [SerializeField] private Toggle sfxToggle;
    private bool canSave;


    [Header("Action")]
    public static Action<float> OnMergeForceChange;
    public static Action<bool> OnSFXValueChanged;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Initialize();
        LoadData();
        yield return new WaitForSeconds(.5f);
        canSave = true;

    }

    private void Initialize()
    {
        OnMergeForceChange?.Invoke(mergeForceSlider.value);
        OnSFXValueChanged?.Invoke(sfxToggle.isOn);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetButtonCallback()
    {
        resetPrompt.SetActive(true);
    }


    public void ResetProgressYes()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void ResetProgressNo()
    {
        resetPrompt.SetActive(false);
    }

    public void SliderValueChanged()
    {
        OnMergeForceChange?.Invoke(mergeForceSlider.value);
        SaveData();
    }
    public void ToggleCallback(bool sfxActive)
    {
        OnSFXValueChanged?.Invoke(sfxActive);

        SaveData();
    }

    private void LoadData()
    {
        mergeForceSlider.value = PlayerPrefs.GetFloat("MergeForceValue");
        sfxToggle.isOn = PlayerPrefs.GetInt("SfxActive") == 0;
    }

    private void SaveData()
    {
        if (!canSave)
            return;
        PlayerPrefs.SetFloat("MergeForceValue", mergeForceSlider.value);
        int sfxValue = sfxToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("SfxActive", sfxValue);
    }
}
