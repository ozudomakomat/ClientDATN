﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent, ExecuteInEditMode, RequireComponent (typeof(RectTransform))]
public abstract class BasePoolGroup : MonoBehaviour
{
	//
	// Constructors
	//
	protected BasePoolGroup ()
	{				
	}

	//
	// Field
	//
	[SerializeField]
	protected ScrollRect m_ScrollRect;

    //Set in inspector 
	[SerializeField]
	protected GameObject[] m_CellPrefabs;

    //Size cua cell - set in inspector (neu cac cell chi co 1 kich thuoc)
    //neu set "cellSizeCallback" thi get size cua cell = delegate nay chu
    //ko dung m_CellSize nay nua - lam lang nhang vai
	[SerializeField]
	protected Vector2 m_CellSize;

    //spacing giua cac cell - set in inspector
	[SerializeField]
	protected Vector2 m_Spacing;

    //set in inspector
	[SerializeField]
	protected bool m_CancelDragIfFits;

	//
	// Delegates
	//
	public delegate GameObject CellPrefabDelegate (int index);

	public delegate Vector2 CellSizeDelegate (int index);

	public delegate void CellDataDelegate (GameObject go, object data);

	public delegate void OnValueChangedDelegate ();

	public delegate void OnScrollFinished ();


    //delegate tra ve prefab cua item o 1 index nao day (danh cho list
    // co nhieu kieu cell khac nhau - khi duoc set thi m_CellPrefabs ko duoc dung nua)
	protected CellPrefabDelegate cellPrefabCallback = null;

    //delegate de tinh size cua item (khi set thi size mac dinh m_CellSize
    //set trong inspector se ko duoc dung nua)
	protected CellSizeDelegate cellSizeCallback = null;

    //delegate de fill data vao cell gameobject
	protected CellDataDelegate cellDataCallback = null;

	protected OnValueChangedDelegate onValueChangedCallback = null;

	protected OnScrollFinished onScrollFinishedCallback = null;

	//
	// Values
	//
	
    //list chua pool game object
    protected List<PoolObject> listPool = new List<PoolObject> ();

    //list size cua item
	protected List<Vector2> listCellSize = new List<Vector2> ();

    //list chua vi tri (x,y) cua item
	protected List<Vector2> listCellPos = new List<Vector2> ();

    //list item data
	protected List<object> adapter = new List<object> ();

	//
	// Scrolling
	//
	protected bool horizontal;

	protected bool vertical;

	protected Vector2 pointFrom;

	protected Vector2 pointTo;

	protected float duration = 0f;

	private float time = 0f;

	//
	// Method
	//

	/// <summary>
	/// <para>Define the way how you use multiple prefabs.</para>
	/// <para></para>
	/// <para>Param: delegate (int 'index of element in adapter')</para>
	/// </summary>
	public void howToUseCellPrefab (CellPrefabDelegate func)
	{
		cellPrefabCallback = func;
	}

	/// <summary>
	/// <para>Define the way how you use each cell data.</para>
	/// <para></para>
	/// <para>Param: delegate (GameObject go, object data)</para>
	/// </summary>
	public void howToUseCellData (CellDataDelegate func)
	{
		cellDataCallback = func;
	}

	/// <summary>
	/// <para>Callback executed when the scroll position of the slider is changed.</para>
	/// <para></para>
	/// <para>Param: delegate()</para>
	/// </summary>
	public void onValueChanged (OnValueChangedDelegate func)
	{
		onValueChangedCallback = func;
	}

	/// <summary>
	/// <para>Return the cell prefab you set in inspector.</para>
	/// <para></para>
	/// <para>Param: Index of element in inspector</para>
	/// </summary>
	public GameObject getCellPrefab (int index)
	{		
		return m_CellPrefabs [index];
	}

	/// <summary>
	/// <para>Return the prefab corresponding each element in adapter.</para>
	/// <para></para>
	/// <para>Param: Index of element in adapter.</para>
	/// </summary>
	protected GameObject getElementPrefab (int index)
	{
		if (cellPrefabCallback != null)
			return cellPrefabCallback (index);
		return getCellPrefab (0);
	}

	/// <summary>
	/// <para>Return size of cell that you set in inspector.</para>
	/// </summary>
	public Vector2 getCellSize ()
	{
		return m_CellSize;
	}



	/// <summary>
	/// <para>Return size of cell is working with element at 'index' of adapter.</para>
	/// <para>Return size of cell that you set in inspector if not found.</para>
	/// <para></para>
	/// <para>Param: Index of element in adapter.</para>
	/// <para>Notice: Make sure you call function setAdapter first.</para>
	/// </summary>
    /// 
    //Cai nay ko thay dung

//	public Vector2 getCellSize (int index)
//	{
//		if (index > 0 && index < listCellSize.Count)
//			return listCellSize [index];
//		return m_CellSize;
//	}

	/// <summary>
	/// <para>Return data of element at 'index' of adapter.</para>
	/// <para></para>
	/// <para>Param: Index of element in adapter</para>
	/// <para>Notice: Make sure you call function setAdapter first.</para>
	/// </summary>
	public object getCellData (int index)
	{		
		return adapter [index];
	}

	protected void setCellData (GameObject go, object data)
	{
		if (cellDataCallback != null)
			cellDataCallback (go, data);
	}

	/// <summary>
	/// <para>Return gameObject is working with element at 'index' of adapter. Return null if not found.</para>
	/// <para></para>
	/// <para>Param: Index of element in adapter.</para>
	/// </summary>
    /// 
    /// dek thay dung
	public GameObject getGameObject (int index)
	{
		GameObject prefab = getElementPrefab (index);

		if (prefab == null)
			return null;

		foreach (PoolObject po in listPool) {
			if (!po.isAvailable && po.prefabName.Equals (prefab.name) && po.index == index) {
				return po.gameObj;
			}
		}

		return null;
	}

	//giang added;
	//reload lai data cho cac cell dang visible
	public void reloadDataToVisibleCell()
	{
		foreach (PoolObject poolObj in listPool)
		{
			if (!poolObj.isAvailable)
			{
				GameObject goCell = poolObj.gameObj;
				int indexCell = poolObj.index;
				//
				setCellData(goCell, getCellData(indexCell));
			}
		}
	}

	/// <summary>
	/// <para>Return index of object in adapter. Return -1 if not found</para>
	/// </summary>
	/// <returns>The index.</returns>
	/// <param name="data">Data.</param>
	public int getIndex (object data)
	{
		if (data == null)
			return -1;
		
		for (int i = 0; i < adapter.Count; i++) {
			if (data.Equals (adapter [i]))
				return i;
		}

		return -1;
	}

	private void addCell (GameObject go, int index)
	{		
		go.name = "item_" + index;
		go.SetActive (true);
		//
		normalizeCellUI (go, index);
		setCellData (go, getCellData (index));
	}

	protected void normalizeAnchor (RectTransform rect)
	{
		rect.anchorMin = Vector2.up;
		rect.anchorMax = Vector2.up;
		rect.pivot = Vector2.up;
	}

	protected void normalizeCellUI (GameObject go, int index)
	{
		//anchor
		RectTransform rect = go.GetComponent<RectTransform> ();
		normalizeAnchor (rect);

		//size
		rect.sizeDelta = listCellSize [index];

		//pos
		rect.anchoredPosition = listCellPos [index];
	}

	protected bool isFitInMaskWidth (int index)
	{
		float offsetX = m_ScrollRect.content.anchoredPosition.x;

		float xLeft = listCellPos [index].x + offsetX;
		float xRight = xLeft + listCellSize [index].x;

		if (xLeft >= 0f && xRight <= m_ScrollRect.viewport.rect.width)
			return true;

		return false;
	}

	protected bool isFitInMaskHeight (int index)
	{
		float offsetY = m_ScrollRect.content.anchoredPosition.y;

		float yTop = listCellPos [index].y + offsetY;
		float yBot = yTop - listCellSize [index].y;

		if (yTop <= 0 && yBot >= -m_ScrollRect.viewport.rect.height)
			return true;

		return false;
	}

	//
	// Work with pool
	//
	protected void resetPool ()
	{
		foreach (PoolObject po in listPool) {
			po.recycleObject ();
		}
	}

    // khi scrollList update
    // -> biet item o 'index' da visible roi thi ko show ra nua
    // neu ko thi lay item trong pool ra roi set data .....
	protected bool isCellVisible (int index)
	{
		foreach (PoolObject po in listPool) {
			if (!po.isAvailable && po.index == index)
				return true;
		}
		return false;
	}

	protected void getPooledObject (int index)
	{
		GameObject prefab = getElementPrefab (index);

		if (prefab == null) {
			Debug.LogWarning ("The prefab you get with index " + index + " in adapter should not be NULL.");
			return;
		}

		foreach (PoolObject po in listPool) {
			if (po.isAvailable && po.prefabName.Equals (prefab.name)) {
				po.index = index;
				po.isAvailable = false;
				addCell (po.gameObj, index);
				return;
			}
		}

		//no pool object inactive --> create new
		GameObject go = UnityEngine.Object.Instantiate (prefab) as GameObject;
		go.transform.SetParent (m_ScrollRect.content);
		go.transform.localScale = Vector3.one;
        //
        Vector3 tmp = go.transform.localPosition;
        tmp.z = 0;
        go.transform.localPosition = tmp;
		addCell (go, index);

		//add to list pool
		PoolObject po2 = new PoolObject ();
		po2.index = index;
		po2.prefabName = prefab.name;
		po2.isAvailable = false;
		po2.gameObj = go;
		listPool.Add (po2);
		//
		return;
	}

	//
	// Virtual
	//

	/// <summary>
	/// <para>Set Adapter</para>
	/// <para>Param1: List<object> of dataset.</para>
	/// <para>Param2: boolean - true if you want to scroll to first, false if not.</para>
	/// </summary>
	public virtual void setAdapter (List<object> adapter, bool toFirst = true)
	{	
		checkError ();
		stop ();
		this.adapter.Clear ();
		this.adapter.AddRange (adapter);
	}

	protected virtual void calcSizeDelta ()
	{
		listCellPos.Clear ();
		listCellSize.Clear ();
	}

	protected virtual void updateData ()
	{
	}

	/// <summary>
	/// Make velocity = (0, 0) and then pool group will be stop movement.
	/// </summary>
	public void stop ()
	{
		m_ScrollRect.velocity = Vector2.zero;
	}

	/// <summary>
	/// <para>Call when <ScrollTo> function finished.</para>
	/// <para>Param: delegate()</para>
	/// </summary>
	public void onScrollFinished (OnScrollFinished func)
	{
		onScrollFinishedCallback = func;
	}

	/// <summary>
	/// <para>Scroll pool group to position of first element of adapter</para>
	/// <para></para>
	/// <para>Param: Duration for scrolling from beginning to end.</para>
	/// </summary>
	public virtual void scrollToFirst (float duration = 0f)
	{
		pointFrom = m_ScrollRect.content.anchoredPosition;
		pointTo = Vector2.zero;

		if ((pointFrom.x != pointTo.x) || (pointFrom.y != pointTo.y)) {
			if (duration > 0f) {
				duration = duration;
			} else {
				m_ScrollRect.content.anchoredPosition = pointTo;
				updateData ();
				if (onScrollFinishedCallback != null)
					onScrollFinishedCallback ();
			}
		}
	}

	public virtual void scrollToLast (float duration = 0f)
	{
	}

	protected virtual void scrolling ()
	{
		if (duration > 0) {
			m_ScrollRect.content.anchoredPosition = Vector2.Lerp (pointFrom, pointTo, time / duration);
			updateData ();
			time += Time.deltaTime;
			if (time >= duration) {
				m_ScrollRect.content.anchoredPosition = pointTo;
				updateData ();

				//stop scrolling
				duration = 0f;
				time = 0f;
				stop ();
				if (onScrollFinishedCallback != null)
					onScrollFinishedCallback ();
			}
		}
	}

	protected virtual void checkCancelDragIfFits ()
	{
	}

	protected virtual void onValueChanged ()
	{
		m_ScrollRect.onValueChanged.AddListener (delegate {
			updateData ();
		});

		if (onValueChangedCallback != null)
			onValueChangedCallback ();
	}

	protected void checkError ()
	{
		if (m_ScrollRect == null) {		
			Debug.LogError ("ScrollRect field of Pool Group is null or empty!");
			return;
		}
			
		if (m_ScrollRect.content == null) {
			Debug.LogError ("Content field of ScrollRect is null or empty!");
			return;
		}

		if (m_ScrollRect.viewport == null) {
			Debug.LogError ("Viewport field of ScrollRect is null or empty!");
			return;
		}
	}

	protected virtual void Awake ()
	{
		if (m_ScrollRect == null)
			return;		
		onValueChanged ();
		horizontal = m_ScrollRect.horizontal;
		vertical = m_ScrollRect.vertical;
		normalizeAnchor (m_ScrollRect.content);
	}

	// Use this for initialization
	protected virtual void Start ()
	{
		
	}

	// Update is called once per frame
	protected virtual void Update ()
	{
		
	}

	protected virtual void FixedUpdate ()
	{
		//scrolling pool group
		scrolling ();
	}
}
//end of class
