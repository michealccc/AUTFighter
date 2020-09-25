using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameRateCounter : MonoBehaviour
{
    public bool IsShowing => textField.gameObject.activeSelf;

    [Header("References")]
    [SerializeField, Tooltip("The text field displaying the frame rate.")]
    private TextMeshProUGUI textField = default;

    [Header("Frame Rate")]
    [SerializeField, Tooltip("The delay in seconds between updates of the displayed frame rate.")]
    private float pollingTime = 0.5f;

    float mTime;
    int mFrameCount;

    public void Show(bool show)
    {
        textField.gameObject.SetActive(show);
    }
        
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Update time.
        mTime += Time.deltaTime;

        // Count this frame.
        mFrameCount++;

        if (mTime >= pollingTime)
        {
            // Update frame rate.
            int frameRate = Mathf.RoundToInt((float) mFrameCount / mTime);
            textField.text = frameRate.ToString();

            // Reset time and frame frame count.
            mTime -= pollingTime;
            mFrameCount = 0;
        }
    }
}