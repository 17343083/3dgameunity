using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//连珠游戏（井字棋）

public class Chess : MonoBehaviour
{
	//开始游戏和重新开始游戏没有区别，所以只需要调用重新开始游戏即可
	void Start()
	{
		restart();
	}

	//一个3*3的二维数组用于记录棋盘当前的状态
	int[,] board = new int[3, 3];
	//一个变量用于记录当前轮到哪一方下棋
	int turn = 1;

	void restart()
	{
		//开始游戏需要重置棋盘和turn变量
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				board[i, j] = 0;
		turn = 1;
	}

	void OnGUI()
	{
		//开始游戏按钮
		if (GUI.Button(new Rect(30, 100, 100, 60), "开始游戏"))
		{
			restart();
		}

		//判断游戏是否结束
		int flag = judge();
		//0代表游戏继续
		//1代表先手胜利
		//2代表后手胜利
		//3代表平局
		if (flag == 1)
		{
			GUI.Label(new Rect(30, 200, 100, 60), "先手胜利！");
		}
		else if (flag == 2)
		{
			GUI.Label(new Rect(30, 200, 100, 60), "后手胜利！");
		}
		else if (flag == 3)
		{
			GUI.Label(new Rect(30, 200, 100, 60), "平局");
		}

		//生成3*3棋盘
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (board[i, j] == 1)
				{
					GUI.Button(new Rect(200 + i * 100, 20 + j * 100, 100, 100), "o");
				}
				else if (board[i, j] == 2)
				{
					GUI.Button(new Rect(200 + i * 100, 20 + j * 100, 100, 100), "x");
				}
				else if (GUI.Button(new Rect(200 + i * 100, 20 + j * 100, 100, 100), "") && flag == 0)
				{
					if (turn == 1) board[i, j] = 1;
					else board[i, j] = 2;

					turn = -turn;
				}
			}
		}
	}

	int judge()
	{
		//横向连珠
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 1; j++)
			{
				if (board[i, j] != 0 &&
					board[i, j] == board[i, j + 1] &&
					board[i, j] == board[i, j + 2])
					return board[i, j];
			}
		}
		//纵向连珠
		for (int j = 0; j < 3; j++)
		{
			for (int i = 0; i < 1; i++)
			{
				if (board[i, j] != 0 &&
					board[i, j] == board[i + 1, j] &&
					board[i, j] == board[i + 2, j] )
					return board[i, j];
			}
		}
		//两种斜向连珠
		for (int i = 0; i < 1; i++)
		{
			for (int j = 0; j < 1; j++)
			{
				if (board[i, j] != 0 &&
					board[i, j] == board[i + 1, j + 1] &&
					board[i, j] == board[i + 2, j + 2])
					return board[i, j];
			}
		}
		for (int i = 0; i < 1; i++)
		{
			for (int j = 2; j < 3; j++)
			{
				if (board[i, j] != 0 &&
					board[i, j] == board[i + 1, j - 1] &&
					board[i, j] == board[i + 2, j - 2] )
					return board[i, j];
			}
		}
		//平局（棋盘满）
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (board[i, j] == 0)
					return 0;
			}
		}
		return 3;
	}
}