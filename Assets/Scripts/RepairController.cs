﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairController : MonoBehaviour 
{
	[SerializeField] float m_RepairSpeed;
	[SerializeField] float m_InteractDistance;
	[SerializeField] Animator animator;
	[SerializeField] PlayerEffects effects;

	Repairable m_RepairableSystem;
	Transform m_LookTransform;
	RaycastHit m_Hit;
	bool m_Repairing;

	void Start()
	{
		m_LookTransform = transform.GetChild(0);
		m_Repairing = false;
	}

	/* Handle the repair behavior */
	void Update()
	{
		if (Input.GetMouseButtonDown(1) && 
			Physics.Raycast(
				m_LookTransform.position,
				m_LookTransform.forward,
				out m_Hit,
				m_InteractDistance,
				1 << 9))
		{
			// Start repairing
			m_RepairableSystem = m_Hit.transform.GetComponent<Repairable>();
			m_Repairing = (m_RepairableSystem != null);
			animator.SetBool("Repairing", m_Repairing);
			effects.PlayBlowtorch();
		}
		else if (Input.GetMouseButton(1) && m_Repairing)
		{
			// continue repairing
			m_RepairableSystem.Repair(m_RepairSpeed * Time.deltaTime);
		}
		else if (Input.GetMouseButtonUp(1) && m_Repairing)
		{
			m_RepairableSystem = null;
			m_Repairing = false;
			animator.SetBool("Repairing", false);
			effects.Stop();
		}
	}

}
