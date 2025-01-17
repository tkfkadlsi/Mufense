using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private bool _canSave;

    private void Awake()
    {
        _canSave = false;

        Managers instance = GetComponent<Managers>();

        OptionData optionData = JsonUtility.FromJson<OptionData>(
            Path.Combine(Application.persistentDataPath, "Data/OptionData.json"));

        instance.Game.Language = optionData.Language;
        instance.Game.MasterVolume = optionData.MasterVolume;
        instance.Game.MusicVolume = optionData.MusicVolume;
        instance.Game.EffectVolume = optionData.EffectVolume;
    }
}
