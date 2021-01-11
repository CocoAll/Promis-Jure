using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	public enum FadeType
	{
		None = 0,
		FadeIn,
		FadeOut
	};

	[SerializeField]
	private FadeType type;
	[SerializeField]
	public float delayToStart = 0f;
	[SerializeField]
	private float delayToFade = 1f;
	[SerializeField]
	private float maxAlphaSprite = 1f;

	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		Debug.Log (spriteRenderer);
	}

	void Start()
	{
		switch (type)
		{
		case FadeType.FadeIn:
			StartCoroutine(FadeInSprite());
			break;
		case FadeType.FadeOut:
			StartCoroutine(FadeOutSprite());
			break;
		case FadeType.None:
		default:
			break;
		}
	}

	IEnumerator FadeInSprite()
	{

		Debug.Log ("FadeIn");
		bool hideColor = true;

		foreach (Fade otherFaders in GetComponents<Fade>())
		{
			if (otherFaders.delayToStart < delayToStart && otherFaders.type == FadeType.FadeOut)
			{
				hideColor = false;
			}
		}

		Color color = spriteRenderer.material.color;
		color.a = hideColor ? 0f : maxAlphaSprite;
		spriteRenderer.material.color = color;

		yield return new WaitForSeconds(delayToStart);

		float waitTime = delayToFade / 100;
		for (uint i = 0; i <= 100; ++i)
		{
			Color colorMid = spriteRenderer.material.color;
			colorMid.a = i / 100f * maxAlphaSprite;
			spriteRenderer.material.color = colorMid;

			yield return new WaitForSeconds(waitTime);
		}
	}

	IEnumerator FadeOutSprite()
	{

		Debug.Log ("FadeOut");

		yield return new WaitForSeconds(delayToStart);

		float waitTime = delayToFade / 100;
		for (uint i = 100; i > 0; --i)
		{
			Color colorMid = spriteRenderer.material.color;
			colorMid.a = i / 100f * maxAlphaSprite;
			spriteRenderer.material.color = colorMid;

			yield return new WaitForSeconds(waitTime);
		}
	}
}
