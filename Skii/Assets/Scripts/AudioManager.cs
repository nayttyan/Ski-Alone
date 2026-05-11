using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip collisionSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        Obstacles.OnPlayerHit += PlayCollisionSound;
    }

    private void OnDisaable()
    {
        Obstacles.OnPlayerHit -= PlayCollisionSound;
    }

    private void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }
}
