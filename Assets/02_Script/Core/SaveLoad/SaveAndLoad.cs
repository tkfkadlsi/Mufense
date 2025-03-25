using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private bool _canSave;
    private string _optionPath;

    private void Awake()
    {
        _optionPath = Path.Combine(Application.persistentDataPath, "OptionData.json");
        _canSave = false;

        Managers instance = GetComponent<Managers>();
        OptionData optionData;

        if (File.Exists(_optionPath))
        {
            string jsonData = File.ReadAllText(_optionPath);

            if (string.IsNullOrEmpty(jsonData))
            {
                optionData = new OptionData();
                optionData.Language = Language.English;
                optionData.MasterVolume = 0;
                optionData.MusicVolume = 0;
                optionData.EffectVolume = 0;
                optionData.FrameLimit = false;
                optionData.LowDetailMod = false;
            }
            else
            {
                optionData = JsonUtility.FromJson<OptionData>(jsonData);
            }

        }
        else
        {
            optionData = new OptionData();
            optionData.Language = Language.English;
            optionData.MasterVolume = 0;
            optionData.MusicVolume = 0;
            optionData.EffectVolume = 0;
            optionData.FrameLimit = false;
            optionData.LowDetailMod = false;
        }

        instance.Game.Language = optionData.Language;
        instance.Game.MasterVolume = optionData.MasterVolume;
        instance.Game.MusicVolume = optionData.MusicVolume;
        instance.Game.EffectVolume = optionData.EffectVolume;
        instance.Game.FrameLimit = optionData.FrameLimit;
        instance.Game.LowDetailMod = optionData.LowDetailMod;

        Application.targetFrameRate = instance.Game.FrameLimit ? 60 : -1;
    }

    private void OnApplicationQuit()
    {
        Managers instance = GetComponent<Managers>();

        OptionData optionData = new OptionData();
        optionData.Language = instance.Game.Language;
        optionData.MasterVolume = instance.Game.MasterVolume;
        optionData.MusicVolume = instance.Game.MusicVolume;
        optionData.EffectVolume = instance.Game.EffectVolume;
        optionData.FrameLimit = instance.Game.FrameLimit;
        optionData.LowDetailMod = instance.Game.LowDetailMod;

        string jsondata = JsonUtility.ToJson(optionData, true);
        File.WriteAllText(_optionPath, jsondata);
    }
}
