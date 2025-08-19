using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public static float ScreenHalfWidth;
    public static float ScreenHalfHeight;

    //set screen dimensions - derived from camera's orthographic size and aspect ratio
    void Awake()
    {
        Camera mainCam = Camera.main;
        ScreenHalfHeight = mainCam.orthographicSize;
        ScreenHalfWidth = ScreenHalfHeight * mainCam.aspect;
    }
}