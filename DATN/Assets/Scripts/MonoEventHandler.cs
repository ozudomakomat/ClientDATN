using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoEventHandler : MonoBehaviour,INetEventHandler
{
    protected DataSender m_DataSender;
	private bool m_HasAddToListEventListener = false;
	protected virtual void Awake()
	{
		m_DataSender = DataCaculator.GetInstance().dataSender;
		//
		NetworkController nc = NetworkController.GetInstance();
		if (nc != null)
		{
			m_HasAddToListEventListener = true;
			nc.AddNetworkEventHandler(this);
		}
	}

	protected virtual void Start()
	{
		m_DataSender = DataCaculator.GetInstance().dataSender;
		//
		NetworkController nc = NetworkController.GetInstance();
		if (nc != null && !m_HasAddToListEventListener)
		{
			nc.AddNetworkEventHandler(this);
		}
	}
	public virtual void OnDestroy()
	{
		NetworkController nc = NetworkController.GetInstance();
		if (nc != null)
			nc.RemoveNetworkEventHandler(this);
	}

	public virtual void ProcessKEvent(int eventId, object data)
    {
    }
}
