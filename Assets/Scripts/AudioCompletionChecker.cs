using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioCompletionChecker : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            SceneManager.LoadScene("MusicSelector");
        }
    }
}
