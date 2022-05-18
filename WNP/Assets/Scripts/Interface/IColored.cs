using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColored
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
	public void Init();
	public ColorCodes MixColor(ColorCodes Base, ColorCodes Catalyst);
	public ColorCodes Replace(ColorCodes Catalyst);
}
