using System.Collections;
using UnityEngine;

public interface ITimestoppable
{
    Color OriginalColour { get; set; }

    void Stop();
    void Resume();

    IEnumerator ResumeMove();
}