using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu ("UIHelper/Grid Pool Group", 3)]
public class GridPoolGroup : BasePoolGroup
{
	//
	// Constructors
	//
	public GridPoolGroup ()
	{
	}

	[SerializeField]
	private UIHelperEnum.GridAlignment m_ChildAlignment;

	[SerializeField]
	private UIHelperEnum.StartAxis m_StartAxis;

	[SerializeField]
	private int m_ConstraintCount;

	//
	// Overide
	//
	public override void setAdapter (List<object> adapter, bool toFirst = true)
	{
		base.setAdapter (adapter, toFirst);
		//==
		calcSizeDelta ();
		resetPool ();
		updateData ();
		//==
		if (toFirst)
			scrollToFirst ();		
	}

	protected override void calcSizeDelta ()
	{
		base.calcSizeDelta ();
		//==
		int num = 0;

		//calculate number of group cell
		for (int i = 0; i < adapter.Count; i += m_ConstraintCount) {
			num++;
		}

		//add cell size
		for (int i = 0; i < adapter.Count; i++) {
			listCellSize.Add (getCellSize ());
		}

		float sizeX = m_ScrollRect.viewport.rect.width;
		float sizeY = m_ScrollRect.viewport.rect.height;

		float sizeConstraintX = 0f;
		float sizeConstaintY = 0f;

		//calculate content size
		if (m_StartAxis == UIHelperEnum.StartAxis.Horizontal) {
			sizeConstraintX = (getCellSize ().x + m_Spacing.x) * m_ConstraintCount - m_Spacing.x;
			if (sizeConstraintX > sizeX)
				sizeX = sizeConstraintX;
			sizeY = (getCellSize ().y + m_Spacing.y) * num - m_Spacing.y;
		} else if (m_StartAxis == UIHelperEnum.StartAxis.Vertical) {
			sizeX = (getCellSize ().x + m_Spacing.x) * num - m_Spacing.x;
			sizeConstaintY = (getCellSize ().y + m_Spacing.y) * m_ConstraintCount - m_Spacing.y;
			if (sizeConstaintY > sizeY)
				sizeY = sizeConstaintY;
		}

		//set size delta
		m_ScrollRect.content.sizeDelta = new Vector2 (sizeX, sizeY);
		checkCancelDragIfFits ();

		/*
		 * calculate init local position of each cell in group.		 
		 * anchors min, max, pivot is at (0, 1).
		 * start corner is always upper left.
		 */
		int index = -1;

		if (m_StartAxis == UIHelperEnum.StartAxis.Horizontal) {
			for (int i = 0; i < num; i++) {
				float posX = 0f;
				float posY = -i * (getCellSize ().y + m_Spacing.y);
				//==
				if (m_ChildAlignment == UIHelperEnum.GridAlignment.UpperLeft)
					posX = 0f;
				else if (m_ChildAlignment == UIHelperEnum.GridAlignment.UpperCenter)
					posX = sizeX * 0.5f - sizeConstraintX * 0.5f;
				else if (m_ChildAlignment == UIHelperEnum.GridAlignment.UpperRight)
					posX = sizeX - sizeConstraintX;				
				//==
				for (int j = 0; j < m_ConstraintCount; j++) {
					index++;
					if (index > (adapter.Count - 1))
						break;
					listCellPos.Add (new Vector2 (posX, posY));
					posX = posX + (getCellSize ().x + m_Spacing.x);
				}			
			}
		} else if (m_StartAxis == UIHelperEnum.StartAxis.Vertical) {
			for (int i = 0; i < num; i++) {
				float posX = i * (getCellSize ().x + m_Spacing.x);
				float posY = 0f;
				//==
				if (m_ChildAlignment == UIHelperEnum.GridAlignment.UpperLeft)
					posY = 0f;
				else if (m_ChildAlignment == UIHelperEnum.GridAlignment.MiddleLeft)
					posY = sizeConstaintY * 0.5f - sizeY * 0.5f;
				else if (m_ChildAlignment == UIHelperEnum.GridAlignment.LowerLeft)
					posY = sizeConstaintY - sizeY;
				//==
				for (int j = 0; j < m_ConstraintCount; j++) {
					index++;
					if (index > (adapter.Count - 1))
						break;
					listCellPos.Add (new Vector2 (posX, posY));
					posY = posY - (getCellSize ().y + m_Spacing.y);
				}
			}
		}
	}

	protected override void updateData ()
	{
		//Calculate distance between current pivot's position and init pivot's position of layput group
		//init pos is at (0, 0) (local)
		float offsetX = m_ScrollRect.content.anchoredPosition.x;
		float offsetY = m_ScrollRect.content.anchoredPosition.y;

		//check pool, inactive object if it's out of bound
		foreach (PoolObject po in listPool) {
			if (!po.isAvailable) {
				float xLeft = listCellPos [po.index].x + offsetX;
				float xRight = xLeft + listCellSize [po.index].x;
				float yTop = listCellPos [po.index].y + offsetY;
				float yBot = yTop - listCellSize [po.index].y;

				if (xRight < 0 || xLeft > m_ScrollRect.viewport.rect.width
				    || yBot > 0 || yTop < -m_ScrollRect.viewport.rect.height)
					po.recycleObject ();				
			}
		}

		//data
		for (int i = 0; i < adapter.Count; i++) {
			float xLeft = listCellPos [i].x + offsetX;
			float xRight = xLeft + listCellSize [i].x;
			float yTop = listCellPos [i].y + offsetY;
			float yBot = yTop - listCellSize [i].y;

			if (xRight < 0 || xLeft > m_ScrollRect.viewport.rect.width
			    || yBot > 0 || yTop < -m_ScrollRect.viewport.rect.height
			    || isCellVisible (i))
				continue;

			//add cell
			getPooledObject (i);
		}
	}

	/// <summary>
	/// <para>Scroll pool group to position of element at 'index' of adapter.</para>\
	/// <para></para>
	/// <para>Param1: Index of element in adapter.</para>
	/// <para>Param2: Duration for scrolling from beginning to end.</para>
	/// <para>Param3: If value is true and element at 'index' fits in mask width or mask height, scroll will cancel.</para>
	/// </summary>
	public void scrollTo (int index, float duration = 0, bool cancelIfFitInMaskWidth = false, bool cancelIfFitInMaskHeight = false)
	{
		if (cancelIfFitInMaskWidth && isFitInMaskWidth (index))
			return;
		if (cancelIfFitInMaskHeight && isFitInMaskHeight (index))
			return;

		pointFrom = m_ScrollRect.content.anchoredPosition;
		pointTo = pointFrom;

		if (horizontal) {
			float maxAbsX = (m_ScrollRect.content.rect.width > m_ScrollRect.viewport.rect.width) ? 
				(m_ScrollRect.content.rect.width - m_ScrollRect.viewport.rect.width) : 0f;

			if (index >= 0 && index < listCellPos.Count) {
				float newX = -listCellPos [index].x;
				if (Mathf.Abs (newX) > maxAbsX)
					newX = -maxAbsX;				
				pointTo = new Vector2 (newX, pointTo.y);
			}
		}

		if (vertical) {
			float maxAbsY = (m_ScrollRect.content.rect.height > m_ScrollRect.viewport.rect.height) ?
				(m_ScrollRect.content.rect.height - m_ScrollRect.viewport.rect.height) : 0f;

			if (index >= 0 && index < listCellPos.Count) {				
				float newY = -listCellPos [index].y;
				if (Mathf.Abs (newY) > maxAbsY)
					newY = maxAbsY;
				pointTo = new Vector2 (pointTo.x, newY);
			}
		}

		if ((pointFrom.x != pointTo.x) || (pointFrom.y != pointTo.y)) {
			if (duration > 0f) {
				this.duration = duration;
			} else {
				m_ScrollRect.content.anchoredPosition = pointTo;
				updateData ();
				if (onScrollFinishedCallback != null)
					onScrollFinishedCallback ();
			}
		}	
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
		scrollTo (adapter.Count - 1, duration, false, false);
	}

	protected override void checkCancelDragIfFits ()
	{
		if (m_CancelDragIfFits) {
			if (horizontal) {
				if (m_ScrollRect.content.rect.width <= m_ScrollRect.viewport.rect.width)
					m_ScrollRect.enabled = false;
				else
					m_ScrollRect.enabled = true;
			}

			if (vertical) {
				if (m_ScrollRect.content.rect.height <= m_ScrollRect.viewport.rect.height)
					m_ScrollRect.enabled = false;
				else
					m_ScrollRect.enabled = true;
			}
		}
	}
}
//end class