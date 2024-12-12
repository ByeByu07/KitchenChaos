using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECT_VOLUME = "SoundEffectVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipListSO audioClipListSO;
    private float volume = 1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, 1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeComplete += DeliveryManager_OnRecipeComplete;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        TrashCounter.OnDrop += TrashCounter_OnDrop;
        BaseCounter.OnObjectDrop += BaseCounter_OnObjectDrop;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPlayerPickObject += Player_OnPlayerPickObject;
    }

    private void Player_OnPlayerPickObject(object sender, System.EventArgs e)
    {
        PlaySound(audioClipListSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(audioClipListSO.chop, cuttingCounter.transform.position);
    }

    private void BaseCounter_OnObjectDrop(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(audioClipListSO.objectDrop, baseCounter.transform.position);
    }

    private void TrashCounter_OnDrop(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipListSO.trash, trashCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipListSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeComplete(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipListSO.deliverySuccess, deliveryCounter.transform.position);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    public void PlaySound(AudioClip[] audioClipArr, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArr[Random.Range(0, audioClipArr.Length)], position, volume);
    }

    public void PlayPlayerStepsSound(Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipListSO.footstep, position, volume * volumeMultiplier);
    }

    public void IncreaseVolume()
    {
        volume += .1f;

        if(volume > 1f)
        {
            volume = 0;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
