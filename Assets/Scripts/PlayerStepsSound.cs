using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepsSound : MonoBehaviour
{
    private Player player;
    private float timeStepSoundMax = .3f;
    private float timeStepSound;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        timeStepSound -= Time.deltaTime;
        if (timeStepSound < 0f)
        {
            timeStepSound = timeStepSoundMax;
            if (player.IsWalking())
            {
                SoundManager.Instance.PlayPlayerStepsSound(player.transform.position);
            }
        }
    }
}
