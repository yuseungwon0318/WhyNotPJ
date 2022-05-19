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
		Black,

		Max //�ִ�ġ �뵵. �����̳� ����Ʈ ����⿡ ���.
	}
	public bool MixColor(ColorCodes baseC, ColorCodes catalyst, out ColorCodes result); //���Ұ��� ��ȯ. �̰ɷ� ���� �Ǵ�.
	public ColorCodes Replace(ColorCodes catalyst);
}
