using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulLedgerUI : MonoBehaviour
{
    private PlayerActions controls;

	private int page = 1;
	private int pageMax = 1;
	#region Page Key
	//Page 1 | Ch.1 is Base Stats
	//Page 1 | Ch.2 is Hollow's character page
	//Page 2 | Ch.2 is unnamed character page
	#endregion
	private int chapter = 1;
	private int chapterMax = 2;
	#region Page Key
	//Chapter 1 is Stats
	//Chapter 2 is Characters
	//Chapter 3 will likely be a beastiary
	#endregion
	private int chapter1PageMax = 1;
	private int chapter2PageMax = 2;

	public Animator thisAnim;

	public SoulLedger soulLedgerInteract;

	public TextMeshProUGUI runCountText;

	public TitleScreen sceneManager;

	// Start is called before the first frame update
	void Start()
    {
		#region Control Actions
		controls = new PlayerActions();

		controls.UI.Negative.performed += ctx => GoBack();

		controls.UI.NextChapter.performed += ctx => SwitchChapter(1);
		controls.UI.PrevChapter.performed += ctx => SwitchChapter(-1);

		controls.UI.NextPage.performed += ctx => SwitchPage(1);
		controls.UI.PrevPage.performed += ctx => SwitchPage(-1);
		#endregion

		runCountText.text = sceneManager.runCount.ToString();
	}

	public void ToggleControls(bool state)
    {
		switch (state)
        {
			case true:
				controls.Enable();
				break;

			case false:
				controls.Disable();
				break;
        }
    }

	void SwitchChapter(int direction)
	{
		int newChapter = Mathf.Clamp(chapter + direction, 1, chapterMax);
		if (chapter == newChapter)
		{
			return;
		}

		chapter = newChapter;

		switch (chapter)
		{
			case 1: //Stats
				pageMax = chapter1PageMax;

				switch (page)
				{
					case 1: //Base Stats
						break;
				}
				break;

			case 2: //Characters
				pageMax = chapter2PageMax;

				switch (page)
				{
					case 1: //Hollow's Page
						break;

					case 2: //Unnamed Page
						break;
				}
				break;

			case 3: //Beastiary?
				break;
		}

		SwitchPage(-69);
		thisAnim.SetInteger("Chapter", chapter);
	}

	void SwitchPage(int direction)
	{
		int newPage = Mathf.Clamp(page + direction, 1, pageMax);
		if (page == newPage)
        {
			return;
        }
		

		switch (chapter)
		{
			case 1: //Stats
				switch (page)
				{
					case 1: //Base Stats
						break;
				}
				break;

			case 2: //Characters
				switch (page)
				{
					case 1: //Hollow's Page
						break;

					case 2: //Unnamed Page
						break;
				}
				break;

			case 3: //Beastiary?
				break;
		}

		page = newPage;
		thisAnim.SetInteger("Page", page);
	}

	void GoBack()
    {
		thisAnim.SetBool("Opened", false);
		soulLedgerInteract.ShutDownLedger();
		ToggleControls(false);

		switch (chapter)
        {
			case 1: //Stats
				switch (page)
                {
					case 1: //Base Stats
						break;
                }
				break;

			case 2: //Characters
				switch (page)
                {
					case 1: //Hollow's Page
						break;

					case 2: //Unnamed Page
						break;
                }
				break;

			case 3: //Beastiary?
				break;
        }
    }


}
