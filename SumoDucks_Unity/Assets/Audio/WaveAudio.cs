using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAudio : AkTriggerBase {

    public void Play()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
