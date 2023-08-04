using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class LightSaberController : MonoBehaviour
{
    [SerializeField] AudioClip clipOn;
    [SerializeField] AudioClip clipOff;

    private Animator animator;
    private AudioSource audioSource;

    private bool isOn = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerBeam()
    {
        if (!isOn)
        {
            // Iþýnýn açýlma animasyonunu çalýþtýr
            animator.SetTrigger("On");
            audioSource.PlayOneShot(clipOn);
        }
        else
        {
            animator.SetTrigger("On");
            audioSource.PlayOneShot(clipOff);
        }

        isOn = !isOn;
        animator.SetBool("On", isOn);
    }
}
