using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootSounds : MonoBehaviour
{
    public Animator playerAnimator;
    public AudioClip shootSound;
    AudioSource shootAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        shootAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!shootAudioSource.isPlaying) &&
            (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Stand") ||
            playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Run_Shot"))
            ) {
            shootAudioSource.PlayOneShot(shootSound);
        }
    }
}
