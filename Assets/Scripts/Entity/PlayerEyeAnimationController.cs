using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyeAnimationController : MonoBehaviour {

    [SerializeField]
    Animator LeftEyeAnimator, RightEyeAnimator;
    [SerializeField] PlayerEyeAnimationTrigger[] triggers;

    [SerializeField]
    float animationCheckCooldownMin = 1f, animationCheckCooldownMax = 2.5f, animationCheckTime;

    bool doRightEye = true;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
        doRightEye = true;

        if (LeftEyeAnimator == null || RightEyeAnimator == null) {

            return;

        }

        if (Time.time > animationCheckTime) {

            float random;

            for (int i = 0; i < triggers.Length; i++) {

                random = Random.value * 100f;
                float nextPercentage = (i + 1 > triggers.Length - 1) ? 0f : triggers[i+1].ChanceToTrigger;

                if (random >= nextPercentage && random <= triggers[i].ChanceToTrigger) {

                    LeftEyeAnimator.SetTrigger(triggers[i].AnimationTriggerName);

                    if (triggers[i].RequiresBothEyes == true) {

                        RightEyeAnimator.SetTrigger(triggers[i].AnimationTriggerName);
                        doRightEye = false;

                    }

                    break;
                }
            }

            if (doRightEye == true) {

                for (int i = 0; i < triggers.Length; i++) {

                    random = Random.value * 100f;
                    float nextPercentage = (i + 1 > triggers.Length - 1) ? 0f : triggers[i + 1].ChanceToTrigger;

                    if (random >= nextPercentage && random <= triggers[i].ChanceToTrigger) {

                        if (triggers[i].RequiresBothEyes == true) {

                            continue;

                        }

                        RightEyeAnimator.SetTrigger(triggers[i].AnimationTriggerName);
                        break;
                    }
                }
            }

            animationCheckTime = Time.time + Random.Range(animationCheckCooldownMin, animationCheckCooldownMax);

        }
    }
}

[System.Serializable]
public class PlayerEyeAnimationTrigger {

    public string AnimationTriggerName;
    [Range(1f, 100f)]
    public float ChanceToTrigger = 1f;
    public bool RequiresBothEyes;

}