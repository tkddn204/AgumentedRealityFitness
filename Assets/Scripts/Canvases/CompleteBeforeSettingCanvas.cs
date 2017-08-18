using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteBeforeSettingCanvas : MonoBehaviour {

    public bool isBeforeSettingAnimationToExercise = false;
    
    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            if (!isBeforeSettingAnimationToExercise)
            {
                isBeforeSettingAnimationToExercise = true;
                GameObject.Find("/ImageTarget/Beta")
                    .GetComponent<BetaController>().readyExeciseAnimation();
            }
        }
    }
}
