﻿using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using ZFrame.IO;

public class CardLoader : MonoBehaviour
{
	public UIGrid grid;
	public UIProgressBar progressBar;
	public UIPanel dockPanel;
	public UICamera cam;

	public void Load()
	{
		StartCoroutine(LoadCards());
	}

	private IEnumerator LoadCards()
	{
		grid.GetChildList().ForEach(t => Destroy(t.gameObject));

		progressBar.gameObject.SetActive(true);
		progressBar.value = 0;
		grid.gameObject.SetActive(false);
		SetData data = JsonConvert.DeserializeObject<SetData>(ResourceEngine.Instance.Load<TextAsset>("FRF").text);
		GameObject cardobj = ResourceEngine.Instance.Load<GameObject>("CardListItem");
		for (int i = 0; i < data.cards.Length; i++)
		{
			CardData card = data.cards[i];
			CardListItem item = NGUITools.AddChild(grid.gameObject, cardobj).GetComponent<CardListItem>();
			item.Data = card;
			yield return item;

			progressBar.value = (float) i/data.cards.Length;
		}

		grid.gameObject.SetActive(true);
		grid.Reposition();
		progressBar.gameObject.SetActive(false);
	}
}