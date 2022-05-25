using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{
	public int slotNum = 3;
	int currentSlot;
	public List<ActiveSkillBasic> skills = new List<ActiveSkillBasic>();
	List<ActiveSkillSlot> slots = new List<ActiveSkillSlot>();
	private void Awake()
	{
		slots.Capacity = slotNum;
		skills.Capacity = slotNum;
		currentSlot = 0;
		for (int i = 0; i < slotNum; i++)
		{
			slots.Add(new ActiveSkillSlot());
			slots[i].Init(skills[i]);
		}
		slots[currentSlot].isActive = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			slots[currentSlot].isActive = false;
			currentSlot +=1;
			slots[currentSlot].isActive = true;
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			slots[currentSlot].Use();
		}
	}
}
