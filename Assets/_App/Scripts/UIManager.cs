using UnityEngine;
using System.Collections;

public delegate void Callback();

public class UIManager : MonoBehaviour {

	private static UIManager instance;

	public ScanPrompt scan;
	public ConfirmationBox confirmationBox;
	public MenuActions menu;
	public Catalog catalog;
	public Toolbox tools;

	void Start() {
		instance = this;
	}

	public static void ShowMenu() {
		instance.menu.Show();
	}

	public static void ShowScan() {
		instance.menu.Hide();
		instance.scan.Show();
	}

	public static void HideScan() {
		instance.scan.Hide();
		instance.menu.Show();
	}

	public static void ShowCatalog(Callback onSelected, Callback onCancel = null) {
		instance.menu.Hide();
		instance.catalog.Show(onSelected, onCancel);
	}

	public static void HideCatalog() {
		instance.catalog.Hide();
		instance.menu.Show();
	}

	public static void Information(string message) {
		instance.menu.Hide();
		InformationBox.Show(message);
	}

	public static void Confirmation(string message, Callback onConfirm, Callback onCancel) {
		instance.menu.Hide();
		ConfirmationBox.Show(message, onConfirm, onCancel);
	}

	public static void ShowToolbox() {
		instance.menu.Hide();
		instance.tools.Show();
	}

	public static void HideToolbox() {
		instance.tools.Hide();
		instance.menu.Show();
	}

	public static void NewItemAdded() {
		instance.tools.NewItem(instance.catalog.GetSelected().Item);
		ShowToolbox();
	}

	public static Transform GetCatalogSelectedItem() {
		return instance.catalog.GetSelected().Item;
	}
}
