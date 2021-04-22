using UnityEngine;

public class SoundScript : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShoot(AudioClip ShootSound)
    {
        audioSource.PlayOneShot(ShootSound);
    }

    public void PlayALot()
    {
        audioSource.Play();
    }
}
