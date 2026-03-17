using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TFManager : MonoBehaviour
{
	private Renderer bodyRenderer;
	private Renderer faceRenderer;
	[SerializeField]
 private GameObject bodyObj;
 [SerializeField]
 private GameObject faceObj;
 [SerializeField]
 private Transform anim2;
 [SerializeField] Animator anim;
 [SerializeField]
 private Vector3 muzzStart, muzzEnd;
	[SerializeField]
	public float lastFaceStage, timeSinceBlink, nextBlink, blinkTime, defaultNextBlink;
    [SerializeField]
    public int blinksTillReset;
    [SerializeField]
    private bool blinkOnOff;
    void Start()
    {
	    bodyRenderer = bodyObj.GetComponent<Renderer>();
	    faceRenderer = faceObj.GetComponent<Renderer>();
	    anim2.DOScale(muzzStart, 0.0f);
    }
    private void Update()
    {
        if (timeSinceBlink > defaultNextBlink && defaultNextBlink > 9.0f) //totally bodged together lmao
        {
            setPercent(-2.0f);
            lastFaceStage = 2.0f;
            changeFaceState(lastFaceStage);
			changeBodyState(2.0f);
        }

        if (blinkOnOff == true) {
            timeSinceBlink += Time.deltaTime;
            if (timeSinceBlink > nextBlink)
            {
                changeFaceState(lastFaceStage * 2.0f + 4.0f); //closed version
                timeSinceBlink = 0.0f;
                nextBlink = defaultNextBlink + Random.Range(0.0f, 1.0f);
                blinksTillReset -= 1;
                if (blinksTillReset <= 0)
                {
                    blinkTime = 0.08f;
                    defaultNextBlink = 3.0f;
                    blinksTillReset = 100;
                }
            }
            else if (timeSinceBlink > blinkTime * 2.0f)
            {
                changeFaceState(lastFaceStage); //open eyes
            }
            else if (timeSinceBlink < blinkTime * 2.0f && timeSinceBlink > blinkTime)
            {
                changeFaceState(lastFaceStage * 2.0f + 3.0f); //half-open eyes
            }
            else if (nextBlink - timeSinceBlink < blinkTime)
            {
                changeFaceState(lastFaceStage * 2.0f + 3.0f); //half-open eyes
            }
        }
    }

    public void changeBodyState(float stage)
	{
		bodyRenderer.sharedMaterial.SetFloat("_Index", stage);
	}
	public void changeFaceState(float stage)
	{
		faceRenderer.sharedMaterial.SetFloat("_Index", stage);
	}
	
	public void transition(float time, float target, bool includeFace = true)
	{
		bodyRenderer.sharedMaterial.DOFloat(target, "_Percent", time);
		if (includeFace)
		{
			faceRenderer.sharedMaterial.DOFloat(target, "_Percent", time);
		}
	}
	
	public void setPercent(float target)
	{
		bodyRenderer.sharedMaterial.SetFloat("_Percent", target);
		if (lastFaceStage < 1.1)
		{
            faceRenderer.sharedMaterial.SetFloat("_Percent", target);
        } else
		{
            faceRenderer.sharedMaterial.SetFloat("_Percent", -2.0f);
        }
	}

	public void ActivateBlink(bool truefalse)
	{
		blinkOnOff = truefalse;

    }
	
	public void playBoneAnim(int which)
	{
		switch (which)
		{
			//ears
		case 0:
			anim.Play("earsgrow");
			break;
			
			//antlers
		case 1:
			anim.Play("antlergrowth");
			break;
			
			//antlers
		case 2:
			anim.Play("muzzlegrow");
			anim2.DOScale(muzzEnd, 2.6f);
			break;
		case 3:
			anim.Play("muzzleoff");
			anim2.DOScale(muzzStart, 0.0f);
			anim.Play("earsoff");
			anim.Play("antlersoff");
			break;
		}
	}
	
	public void resetAndGo(float stage, float initial, float target, float time, bool includeFace = true)
	{
		setPercent(initial);
		changeBodyState(stage);
        lastFaceStage = Mathf.Min(stage, 2.0f);
        changeFaceState(lastFaceStage);
		transition(time, target, includeFace);
	}
}
