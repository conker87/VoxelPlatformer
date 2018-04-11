using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectAnimator : MonoBehaviour {

    public bool isCurrentlyAnimating = false;

    [SerializeField]
    float animationCheckCooldownMin = 1f, animationCheckCooldownMax = 2.5f, animationCheckTime;

    Coroutine animationCoroutine;

    [SerializeField]
    List<GameObjectAnimationFrames> animationFrameObjects = new List<GameObjectAnimationFrames>();

	void Start () {
		
        foreach (GameObjectAnimationFrames anims in animationFrameObjects) {

            if (anims.IsDefault == true) {

                anims.GameObjectFrames[0].gameObject.SetActive(true);
                break;

            }

        }

	}
	
	void Update () {
		
        if (Time.time > animationCheckTime) {

            if (isCurrentlyAnimating == true) {
                return;
            }

            float random = Random.value * 100f;
            int i = 0;

            foreach (GameObjectAnimationFrames anims in animationFrameObjects) {

                if (anims.AnimationChance > random) {

                    if (anims.LinkedAnimator == null) {

                        BeginAnimation(anims.GameObjectFrames, anims.AnimationFrameLength);

                    } else {

                        if (anims.LinkedAnimator.isCurrentlyAnimating == false) {

                            BeginAnimation(i);
                            anims.LinkedAnimator.BeginAnimation(i);

                        }

                    }

                    break;

                }

                i++;

            }

            animationCheckTime = Time.time + Random.Range(animationCheckCooldownMin, animationCheckCooldownMax);

        }

	}

    public void BeginAnimation(GameObject[] frames, float frameLength) {

        if (animationCoroutine != null) {

            StopCoroutine(animationCoroutine);
            animationCoroutine = null;

        }

        animationCoroutine = StartCoroutine(DoAnimation(frames, frameLength));

    }

    public void BeginAnimation(string animationName) {

        foreach (GameObjectAnimationFrames anims in animationFrameObjects) {

            if(anims.AnimationName == animationName) {

                BeginAnimation(anims.GameObjectFrames, anims.AnimationFrameLength);
                break;

            }

        }

    }

    public void BeginAnimation(int index) {

        BeginAnimation(animationFrameObjects[index].GameObjectFrames,
            animationFrameObjects[index].AnimationFrameLength);


    }

    IEnumerator DoAnimation(GameObject[] frames, float frameLength) {

        int totalFrames = frames.Length;

        isCurrentlyAnimating = true;

        for (int i = 0; i < totalFrames; i++) {

            if (i != 0) {
                frames[i-1].SetActive(false);
            }

            frames[i].SetActive(true);

            if (i == 0) {
                animationFrameObjects[0].GameObjectFrames[0].SetActive(false);
            }

            yield return new WaitForSeconds(frameLength);
        }

        frames[totalFrames-1].gameObject.SetActive(false);

        isCurrentlyAnimating = false;
        animationFrameObjects[0].GameObjectFrames[0].SetActive(true);

    }

}

[System.Serializable]
public class GameObjectAnimationFrames {

    public string AnimationName;
    public float AnimationFrameLength = .1f;

    public float AnimationChance;

    public GameObjectAnimator LinkedAnimator;

    public bool IsDefault;

    public GameObject[] GameObjectFrames;

    public GameObjectAnimationFrames() {

    }

    public GameObjectAnimationFrames(GameObjectAnimationFrames value) {

        AnimationName = value.AnimationName;
        AnimationFrameLength = value.AnimationFrameLength;
        AnimationChance = value.AnimationChance;
        LinkedAnimator = value.LinkedAnimator;
        IsDefault = value.IsDefault;
        GameObjectFrames = value.GameObjectFrames;

    }

    public GameObjectAnimationFrames(string animationName, float animationFrameLength, float animationChance,
        GameObjectAnimator linkedAnimator, bool isDefault, GameObject[] gameObjectFrames) {

        AnimationName = animationName;
        AnimationFrameLength = animationFrameLength;
        AnimationChance = animationChance;
        LinkedAnimator = linkedAnimator;
        IsDefault = isDefault;
        GameObjectFrames = gameObjectFrames;

    }

}