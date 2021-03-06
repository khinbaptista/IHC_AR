﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatalogItem : MonoBehaviour {

	[SerializeField]
	private Color colorSelected;
	private Color colorUnselected;
	private Image image;

	[SerializeField]
	private Transform item;
	public Transform Item { get { return item; } }

	[SerializeField]
	private Catalog container;

	private bool _isSelected;
	public bool isSelected { get { return _isSelected; } }

	void Start () {
		image = GetComponentInChildren<Image>();
		colorUnselected = image.color;
	}

	public void OnClick() {
		SetSelection(!_isSelected);
	}

	public void SetSelection(bool selected) {
		_isSelected = selected;
		image.color = isSelected ? colorSelected : colorUnselected;

		if (isSelected)
			container.SetSelected(this);
		else
			container.IgnoreSelected();
	}

}
