using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// skillroot Ȯ����Ű��.
/// �ڷᱸ�� �����غ���.
/// ��ų ��� ���� ����
/// </summary>
public class AllSkill 
{
    enum ColorCodes
	{
		None = -1,
		//�⺻�� + 2���� (��ȭ�� ����.)
		White,
		Red,
		Green,
		Blue,
		Yellow,
		Purple,
		Teal,
		//2+����, ���� (��ȭ�� ����.)


		Black
	}
	enum SkillCodes
	{
		None = -1,
		Ball,
		Smash
	}
	public abstract class SkillRoot
	{
		public string name
		{
			get;
			protected set;
		}
		public int skillCode
		{
			get;
			protected set;
		}
		public int colorCode
		{
			get{ return colorCode;}
			set{ colorCode = value;}
		}
		public int manaCost
		{
			get{ return manaCost;}
			set { manaCost = value;}
		}
		public float cd
		{
			get; set;
		}
		public abstract void Use();
	}
	class Ball : SkillRoot
	{
		
		public override void Use()
		{
			//�߻�
		}
	}
	class Smash : SkillRoot
	{
		public override void Use()
		{
			//���, ������
		}
	}
}
