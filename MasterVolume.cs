using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterVolume : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float masterVolume = 1.0f;

    public void OnValueChanged(float newValue)
    {
        masterVolume = newValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = masterVolume;
    }
}
