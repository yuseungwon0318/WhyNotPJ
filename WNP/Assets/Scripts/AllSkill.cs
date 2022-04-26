using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// <summary>
/// ������ ��Ŀ���� ����. ���� �����ϸ� �������� ����.
/// skillroot Ȯ����Ű��.
/// �ڷᱸ�� �����غ���.
/// ��ų ��� ���� ����
/// </summary>
public class AllSkill 
{
    public enum ColorCodes
	{
		None = -1,
		//�⺻�� (��ȭ�� ����.)
		White,

		Red,
		Green,
		Blue,
		//1��
		Yellow, //����
		Purple, //��û
		Teal, //û��
		//2��
		Monza, // ��û + ��(��ȭö ��)
		Lime, //���� + ��
		Cobalt, // û�� + û
		//3��
		Lotus, //���� + ��û (����)
		Goblin, //���� + û��
		EastBay, //��û + û��
		//����
		Black
	}
	public enum SkillCodes
	{
		None = -1,
		Ball,
		Smash
	}

	private static Dictionary<SkillCodes, string> skillCodeNamePair = new Dictionary<SkillCodes, string>()
	{
		{SkillCodes.Ball, "Ball" },
		{SkillCodes.Smash, "Smash" },
	};
	private static Dictionary<ColorCodes, string> colorCodeDescPair = new Dictionary<ColorCodes, string>()
	{
		{ColorCodes.White, "�ٸ� ���� �� ���� �� �ֽ��ϴ�." },
		{ColorCodes.Red, "" },
		{ColorCodes.Green, "" },
		{ColorCodes.Blue, "" },
		{ColorCodes.Yellow, "" },
		{ColorCodes.Purple, "" },
		{ColorCodes.Teal, "" },
		{ColorCodes.Monza, "" },
		{ColorCodes.Lime, "" },
		{ColorCodes.Cobalt,"" },
		{ColorCodes.Lotus, "" },
		{ColorCodes.Goblin, "" },
		{ColorCodes.EastBay, "" },
		{ColorCodes.Black, "�ƹ� ȿ���� �����ϴ�." },
	}; //�׳� �س�����.

	public abstract class SkillRoot
	{
		//�߻� ������Ƽ. ��� ��ų���� �ݵ�� �����ؾ� �ϴ� ��ҵ�.
		public abstract string name
		{
			get;
			protected set;
		}
		public abstract SkillCodes skillCode
		{
			get;
			protected set;
		}
		

		public abstract void Init(); //���� �ʱ�ȭ
		public abstract void Use();

		//�� �߻� ������Ƽ. Init���� ���� �ʱ�ȭ��.
		public ColorCodes colorCode
		{
			get => colorCode;
			protected set => colorCode = value;
		}
		public string colorDesc
		{
			get => colorDesc; protected set => colorDesc = colorCodeDescPair[colorCode];
		} //�� ���� �ؽ�Ʈ�� ���� ����.
		public int manaCost
		{
			get => manaCost;
			set => manaCost = value;
		}
		public float coolDown
		{
			get => coolDown; set => coolDown = value;
		}
		public float skillAtk
		{
			get;
			set;
		}

		public void AddColor(ColorCodes color)
		{
			//�����տ����� ��. ���� ���?
		}
	}
	public class Ball : SkillRoot
	{
		public override string name { get => name; protected set => name = skillCodeNamePair[(int)SkillCodes.Ball]; }
		public override SkillCodes skillCode { get => skillCode; protected set => skillCode = SkillCodes.Ball; }
		public override void Use()
		{
			//�߻�
		}
		public override void Init()
		{
			colorCode = ColorCodes.White;
			colorDesc = "";
			manaCost = 20;
			coolDown = 2;
			skillAtk = 10;
		}
	}
	public class Smash : SkillRoot
	{
		public override string name { get => name; protected set => name = "Smash"; }
		public override SkillCodes skillCode { get => skillCode; protected set => skillCode = SkillCodes.Smash; }
		public override void Use()
		{
			//���, ������
		}
		public override void Init()
		{
			colorCode = ColorCodes.White;
			colorDesc = "";
			manaCost = 10;
			coolDown = 3;
			skillAtk = 15;
		}
	}
}
