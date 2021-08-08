using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class HorizontalOrVerticalPoolGroup : BasePoolGroup
{
	//
	// Constructors
	//
	protected HorizontalOrVerticalPoolGroup ()
	{
	}

	//
	// Method
	//

	/// <summary>
	/// <para>Define the way how you use multiple cell size.</para>
	/// <para></para>
	/// <para>Param: delegate (int 'index of element in adapter')</para>
	/// </summary>
	public void howToUseCellSize (CellSizeDelegate func)
	{
		cellSizeCallback = func;
	}

	public virtual void scrollTo (int index, float duration = 0f, bool cancelIfFitInMask = false)
	{
	}

	/// <summary>
	/// <para>Return size of element at 'index' of adapter.</para>
	/// <para></para>
	/// <para>Param: Index of element in adapter</para>
	/// </summary>
	protected Vector2 getElementSize (int index)
	{
		if (cellSizeCallback != null)
			return cellSizeCallback (index);
		return getCellSize ();
	}

	protected void checkToAddAtleast3Cells ()
	{
		if (adapter.Count == 0)
			return;
		int _1stIndex = 0;
		int _2ndIndex = 1;
		int _3rdIndex = 2;

		bool _1stFound = false;
		bool _2ndFound = false;
		bool _3rdFound = false;

		foreach (PoolObject po in listPool) {
			if (po.index == _1stIndex)
				_1stFound = true;
			else if (po.index == _2ndIndex)
				_2ndFound = true;
			else if (po.index == _3rdIndex)
				_3rdFound = true;
		}

		if (_2ndIndex > (adapter.Count - 1))
			_2ndFound = true;
		if (_3rdIndex > (adapter.Count - 1))
			_3rdFound = true;

		if (!_1stFound)
			getPooledObject (_1stIndex);
		if (!_2ndFound)
			getPooledObject (_2ndIndex);
		if (!_3rdFound)
			getPooledObject (_3rdIndex);
	}

	//
	// Overide
	//
	public override void setAdapter (List<object> adapter, bool toFirst = true)
	{
		base.setAdapter (adapter, toFirst);
		//==
		calcSizeDelta ();
		resetPool ();
		checkToAddAtleast3Cells ();
		updateData ();
		//==
		if (toFirst)
			scrollToFirst ();		
	}

	/// <summary>
	/// <para>Scroll pool group to position of last element of adapter.</para>
	/// <para></para>
	/// <para>Param: Duration for scrolling from beginning to end.</para>
	/// </summary>
	public override void scrollToLast (float duration = 0)
	{
		if (adapter.Count <= 0)
			return;
		scrollTo (adapter.Count - 1, duration, false);
	}
}
//end of class