using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
	public int textIndex = 0;
	[SerializeField] private TMP_Text nameBox;
	[SerializeField] private TMP_Text textBox;
	[SerializeField] private RawImage bgImage;
	[SerializeField] private Image blackoutBehindUI;
	[SerializeField] private Image blackoutAll;
	[SerializeField] private TFManager tfManager;
	[SerializeField] private Image textBG;
	[SerializeField] private RawImage holdiaysIMG1, holdiaysIMG2,holdiaysIMG3;
	[SerializeField] private GameObject debugMenu;
	[SerializeField] private AudioSource door, outside, inside, parade;
	private string textToChange;
	private string nameToChange;
	private float nextDelay = 7.0f;
	public bool locked = true;

    void Start()
	{
		//fade out blackout
		Invoke("DelayClicks", nextDelay);
		Sequence mySequence1 = DOTween.Sequence();
		mySequence1.Append(blackoutAll.DOFade(0.0f, 3.0f));
		mySequence1.Insert(3, nameBox.DOFade(1.0f, 2.0f));
		mySequence1.Insert(3, textBox.DOFade(1.0f, 2.0f));
		mySequence1.Insert(3, textBG.DOFade(1.0f, 2.0f));
		mySequence1.PrependInterval(1);
		nextDelay = 0.0f;
		outside.volume = 0.7f;
    }

    void Update()
	{
		if (!locked)
		{
			if (Input.GetMouseButtonDown(0))
			{
				locked = true;
				Invoke("DelayClicks", nextDelay);
				NextText();
			}
		}
	    
    }
    
	public void NextText()
	{
		textIndex += 1;
		
		switch (textIndex)
		{
		case 1:
			nameBox.SetText("Katherine");
			textBox.SetText("Would you be a dear and put some fresh hay in the empty stall while I'm out?");
			break;
		case 2:
			nameBox.SetText("Anna");
			textBox.SetText("Sure, I'll get right on that.");
			nextDelay = 9.0f;
			break;
		case 3:
			textBox.SetText("");
			nameBox.SetText("");
			nameToChange = "Anna";
			textToChange = "Looks like Kathy has been storing furniture in here, I should probably move all of this before putting the fresh hay in.";
			Invoke("TextSet", 9);
			inside.Play();
			outside.DOFade(0.0f, 4.0f);
			inside.DOFade(0.7f, 4.0f);
			//play door creak sfx
			door.volume = 0.6f;
			door.PlayDelayed(6);
			Sequence mySequence = DOTween.Sequence();
			mySequence.Append(blackoutAll.DOFade(1.0f, 4.0f));
			mySequence.Append(bgImage.DOFade(0.0f, 0.0f));
			mySequence.Append(textBG.DOFade(0.0f, 0.0f));
			mySequence.Append(blackoutBehindUI.DOFade(1.0f, 0.0f));
			mySequence.AppendInterval(2);
			mySequence.Append(blackoutAll.DOFade(0.0f, 2.0f));
			mySequence.Append(textBG.DOFade(1.0f, 2.0f));
			nextDelay = 4.0f;
			break;
		case 4:
			//dizzy camera effects of some sort?
			textBox.SetText("<wiggle a=0.3 f=0.4>Woaaah....</wiggle><br>Why am I suddenly so dizzy?<br>I really need to sit down for a bit.");
			blackoutBehindUI.DOFade(0.0f, 4.0f);
			nextDelay = 0.0f;
			break;
		case 5:
			textBox.SetText("The delivery shouldn't be for a while, I'll have plenty of time to bring the hay in after I rest without Kathy getting on my case about slacking off again.");
			break;
		case 6:
			textBox.SetText("My ears are burning up, do I have a fever?");
			nextDelay = 4.5f;
			break;
		case 7:
			textBox.DOFade(0.0f, 2.0f);
			textBG.DOFade(0.0f, 2.0f);
			nameBox.DOFade(0.0f, 2.0f);
			tfManager.playBoneAnim(0);
			tfManager.resetAndGo(0.0f,0.0f,8.0f, 2.0f);
			nextDelay = 2.5f;
			//Fade out textbox and BG
			//Trigger ear TF
			break;
		case 8:
			textBox.SetText("");
			textBox.DOFade(1.0f, 2.0f);
			textBG.DOFade(1.0f, 2.0f);
			nameBox.DOFade(1.0f, 2.0f);
			//Fade in text box
			nameToChange = "Anna";
			textToChange = "Something's definitely off. The heat felt like it was moving to the top of my head.";
			Invoke("TextSet", 2.0f);
			nextDelay = 4.5f;
			break;
		case 9:
			textBox.DOFade(0.0f, 2.0f);
			textBG.DOFade(0.0f, 2.0f);
			nameBox.DOFade(0.0f, 2.0f);
			tfManager.playBoneAnim(1);
			nextDelay = 2.5f;
			//Fade out textbox and BG
			//Trigger antler TF
			break;
		case 10:
			//Fade in text box
			textBox.SetText("");
			textBox.DOFade(1.0f, 2.0f);
			textBG.DOFade(1.0f, 2.0f);
			nameBox.DOFade(1.0f, 2.0f);
			nameToChange = "Anna";
			textToChange = "...was I wearing a deer headband?<br>I can't quite remember.";
			Invoke("TextSet", 2.0f);
			nextDelay = 0.0f;
			break;
		case 11:
			textBox.SetText("The warmth is spreading. <br>Honestly though, it feels rather pleasant, like a blanket slowly enveloping my whole body.");
			nextDelay = 9.0f;
			break;
		case 12:
			//Fade out textbox and BG
			//Trigger fur growth
			textBox.DOFade(0.0f, 2.0f);
			textBG.DOFade(0.0f, 2.0f);
			nameBox.DOFade(0.0f, 2.0f);
			tfManager.playBoneAnim(2);
			tfManager.resetAndGo(1.0f,0.0f,5.5f, 9.0f);
			nextDelay = 2.5f;
			break;
		case 13:
			//Fade in text box
			textBox.SetText("");
			textBox.DOFade(1.0f, 2.0f);
			textBG.DOFade(1.0f, 2.0f);
			nameBox.DOFade(1.0f, 2.0f);
			nameToChange = "Anna";
			textToChange = "<pend>Mmmm</pend>, definitely a nice change of pace from the winter weather.<br>I could just bask in this feeling forever.";
			Invoke("TextSet", 2.0f);
			nextDelay = 0.0f;
			break;
		case 14:
			textBox.SetText("...I just can't stop feeling like something's off though.");
			nextDelay = 5.0f;
			break;
		case 15:
			tfManager.resetAndGo(2.0f,-0.0f,2.0f, 5.0f);
			//fade out shoes
			textBox.SetText("Oh no, how was I unable to see it before? The mirror... my reflection!<br>I'm becoming a <shake>deer!</shake>");
			nextDelay = 0.0f;
			break;
		case 16:
			textBox.SetText("I can't seem to muster the will to move from this chair...<br>I'm acting like a deer caught in headlights.");
			break;
		case 17:
			textBox.SetText("...not that <i>that's</i> the most pressing way that I'm beginning to feel like a deer.<br>");
			break;
		case 18:
			//fade pants to 30%
			textBox.SetText("What is Kathy going to think if she comes back to me like this?");
			nextDelay = 8.2f;
			break;
		case 19:
			nameBox.SetText("");
			textBox.SetText("");
			textToChange =  "...looks like Anna left the hay outside the stall instead of actually setting it up for our guest.";
			nameToChange =  "Katherine";
			//fade in blackout
			//fade out blackout
			//further away, panned, door opening sfx
			//footsteps
			//fade pants to 60%
			Invoke("TextSet", 8);
			//play door creak sfx
			Sequence mySequence2 = DOTween.Sequence();
			mySequence2.Append(blackoutAll.DOFade(1.0f, 4.0f));
			mySequence2.AppendInterval(1);
			mySequence2.Append(blackoutAll.DOFade(0.0f, 4.0f));
			nextDelay = 0.0f;
			break;
		case 20:
			nameBox.SetText("Anna");
			textBox.SetText("Kathy's back! I don't know how I'm going to explain this, but I have to at least hope she'll be able to get me out of this chair.");
			nextDelay = 0.0f;
			break;
		case 21:
			//fully fade out pants
			//tfManager.resetAndGo(3.0f,0.0f,4.2f, 7.0f);
			nameBox.SetText("Anna");
			textBox.SetText("<i>*Bleat*</i><br>\"Kathy, help! I'm turning into a deer!\"");
			nextDelay = 0.0f;
			break;
		case 22:
			//fade out sweater
			//tfManager.resetAndGo(4.0f,-1.0f,8.0f, 7.0f);
			nameBox.SetText("Katherine");
			textBox.SetText("Bleating? Was the reindeer already delivered?");
			nextDelay = 0.0f;
			break;
		case 23:
			//door sfx
			door.Play();
			nameBox.SetText("Katherine");
			textBox.SetText("Aww, I'm sorry for not moving my furniture out of your stall, and that my assistaint didn't bring much new hay in for you.");
			break;
		case 24:
			nameBox.SetText("Anna");
			textBox.SetText("<i>*Bleating*</i><br>\"You can't understand me?\"");
			break;
		case 25:
			nameBox.SetText("Katherine");
			textBox.SetText("Calm down girl, I'll finish setting up your stall, and tomorrow you'll have a nice time in our town's little parade.<br>Huh, I've never seen one in person before, but I thought reindeer were usually bigger than this.");
			break;
		case 26:
			//door sfx
			nameBox.SetText("Anna");
			textBox.SetText("She doesn't recognize me at all!");
			break;
		case 27:
			//door sfx
			nameBox.SetText("Anna");
			textBox.SetText("...oh deer.");
			nextDelay = 4.0f;
			break;
		case 28:
			blackoutBehindUI.DOFade(1.0f, 4.0f);
			textToChange =  "It looks like the the road has been closed due to snow and strong winds, preventing us from making the delivery on time.<br>We'll issue a refund, sorry for any inconvience this may cause.";
			nameToChange =  "Kathy's Phone";
			Invoke("TextSet", 5);
			nextDelay = 4.0f;
			break;
		case 29:
			
			nameBox.SetText("Katherine");
			textBox.SetText("...?");
			nextDelay = 6.0f;
			
			break;
		case 30:
			textToChange =  "";
			nameToChange =  "";
			parade.PlayDelayed(6);
			parade.volume = 0.6f;
			inside.DOFade(0.0f, 5.0f);
			Invoke("TextSet", 6);
			textBG.enabled = false;
			Sequence mySequence3 = DOTween.Sequence();
			mySequence3.Append(blackoutAll.DOFade(1.0f, 5.0f));
			mySequence3.AppendInterval(2);
			inside.DOFade(0.0f, 5.0f);
			mySequence3.Append(blackoutAll.DOFade(0.0f, 3.0f));
			mySequence3.Append(holdiaysIMG1.DOFade(1.0f, 4.0f));
			nextDelay = 4.0f;
			break;
		case 31:
			holdiaysIMG2.DOFade(1.0f, 4.0f);
			break;
		case 32:
			holdiaysIMG3.DOFade(1.0f, 4.0f);
			nextDelay = 5.0f;
			break;
		case 33:
			holdiaysIMG1.DOFade(0.0f,0.0f);
			holdiaysIMG2.DOFade(0.0f,0.0f);
			holdiaysIMG3.DOFade(0.0f,4.0f);
			blackoutBehindUI.DOFade(1.0f,0.0f);
			textToChange =  "This game was made in 7 days for 7DFPS, and I think it turned out pretty decent for being made in such a short timeframe.<br>If you enjoyed this and want to be notified when my next game (most likely Dogress Bar) is finished, feel free to follow me on Itch!";
			nameToChange =  "SnepShark";
			Invoke("TextSet", 3);
			break;
		case 34:
			textBox.SetText("I'll let you access the animation viewer now. If you'd like to skip to it from a fresh boot, press shift+M to skip to the ending!<br>Thanks for playing!");
			break;
		case 35:
			textToChange = "";
			nameToChange = "";
			TextSet();
			tfManager.resetAndGo(0.0f,0.0f,0.0f,0.0f);
			tfManager.playBoneAnim(3);
			holdiaysIMG1.DOFade(0.0f,0.0f);
			holdiaysIMG2.DOFade(0.0f,0.0f);
			holdiaysIMG3.DOFade(0.0f,0.0f);
			bgImage.DOFade(0.0f,0.0f);
			outside.Stop();
			textBG.enabled = false;
			blackoutBehindUI.DOFade(0.0f,4.0f);
			inside.DOFade(0.6f, 4.0f);
			debugMenu.SetActive(true);
			blackoutAll.DOFade(0.0f,0.0f);
			break;
		case 36:
			TextSet();
			break;
		}
		
	}
	
	private void TextSet()
	{
		nameBox.SetText(nameToChange);
		textBox.SetText(textToChange);
	}
	
	private void DelayClicks()
	{
		locked = false;
	}
	public int CurrentStage()
	{
		return textIndex;
	}
}
