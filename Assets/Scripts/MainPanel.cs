
using UnityEngine;

public class MainPanel : MonoBehaviour
{

	[SerializeField]
	private GroupPresenter presenter;

	private GroupModel model;
	
	// Use this for initialization
	void Start()
	{
		Initialize();
		Prepare();
	}
	public void Initialize()
	{
		//model
		model = new GroupModel();
		presenter.Initialize(model);
	}
	public void Prepare()
	{
		//modelは先
		model.Prepare();
		//presenterはこの後
		presenter.Prepare();

	}
	// Update is called once per frame
	void Update()
	{
	}
}
