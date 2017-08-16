using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaController : MonoBehaviour {

    public string exercise;

    private Animator animator;
    private AnimatorStateInfo currentAnimatorState;

    static int pushUpState;
    static int sitUpState;
    
    void Start() {
        animator = GetComponent<Animator>();
        pushUpState = Animator.StringToHash("push_up");
        sitUpState = Animator.StringToHash("situps");
    }
    
    void FixedUpdate()
    {
        currentAnimatorState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentAnimatorState.shortNameHash == pushUpState ||
            currentAnimatorState.shortNameHash == sitUpState)
        {
            GameObject.Find("/UI/Canvas List/ContinueExerciseCanvas/Text")
                .GetComponent<ContinueExerciseText>().patnerCount = (int)currentAnimatorState.normalizedTime;
        }
    }

    public void readyExeciseAnimation() {
		animator.SetInteger (exercise, 1);
	}

	public void continueExerciseAnimation() {
		animator.SetInteger (exercise, 2);
	}

    private string step = "Step";
    public void nextStep()
    {
        animator.SetBool(step, true);
    }
    public void endStep()
    {
        animator.SetBool(step, false);
    }

	public void stopExerciseAnimation()
    {
        animator.SetBool(step, false);
        animator.SetInteger (exercise, 0);
	}
}
