using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip blastSound;
    [Range(0f, 1f)] public float volume = 1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (blastSound != null)
                AudioSource.PlayClipAtPoint(blastSound, Camera.main.transform.position, volume);

            GameManager.instance.EndGame("BOOM! You Hit a Bomb!");
        }
    }
}