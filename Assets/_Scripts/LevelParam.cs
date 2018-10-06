using UnityEngine;
using System.Collections;

public class Levels
{
	public int ach_complete_num = 1;
	public int ach_collect_num = 2;
	public float ach_fast_second = 30f;
	public int ach_score = 200;
	
	public Levels (int _complete_num, int _collect_num, float _second, int _score) {
		this.ach_complete_num = _complete_num;
		this.ach_collect_num = _collect_num;
		this.ach_fast_second = _second;
		this.ach_score = _score;
	}
}

public class LevelParam : MonoBehaviour {
	static public Levels[,] level = new Levels[5, 11];
	
	static public void init() {
		level[1, 1 ] = new Levels(4, 6, 40.0f, 1400);
		level[1, 2 ] = new Levels(4, 5, 35.0f, 1200);
		level[1, 3 ] = new Levels(4, 6, 50.0f, 1400);
		level[1, 4 ] = new Levels(3, 5, 50.0f, 1400);
	    level[1, 5 ] = new Levels(4, 7, 40.0f, 1700);
	    level[1, 6 ] = new Levels(3, 4, 40.0f, 1100);
	    level[1, 7 ] = new Levels(4, 6, 40.0f, 1400);
	    level[1, 8 ] = new Levels(3, 5, 50.0f, 1300);
	    level[1, 9 ] = new Levels(4, 6, 65.0f, 1300);
	    level[1, 10] = new Levels(4, 6, 70.0f, 1300);
	    level[2, 1 ] = new Levels(4, 7, 40.0f, 1500);
	    level[2, 2 ] = new Levels(4, 6, 55.0f, 1300);
		level[2, 3 ] = new Levels(4, 6, 55.0f, 1500);
		level[2, 4 ] = new Levels(4, 6, 60.0f, 1500);
		level[2, 5 ] = new Levels(4, 6, 50.0f, 1500);
	    level[2, 6 ] = new Levels(4, 6, 60.0f, 1500);
	    level[2, 7 ] = new Levels(3, 6, 60.0f, 1500);
	    level[2, 8 ] = new Levels(3, 6, 70.0f, 1500);
	    level[2, 9 ] = new Levels(2, 4, 80.0f, 1100);
	    level[2, 10] = new Levels(3, 5, 55.0f, 1000);
	    level[3, 1 ] = new Levels(4, 6, 40.0f, 1500);
	    level[3, 2 ] = new Levels(4, 6, 55.0f, 1500);
	    level[3, 3 ] = new Levels(3, 5, 50.0f, 1200);
	    level[3, 4 ] = new Levels(4, 6, 55.0f, 1000);
	    level[3, 5 ] = new Levels(4, 6, 55.0f, 1100);
	    level[3, 6 ] = new Levels(4, 6, 70.0f, 1500);
	    level[3, 7 ] = new Levels(4, 6, 80.0f, 1500);
	    level[3, 8 ] = new Levels(4, 7, 80.0f, 1500);
	    level[3, 9 ] = new Levels(4, 5, 80.0f, 1200);
	    level[3, 10] = new Levels(4, 6, 80.0f, 1300);
	    level[4, 1 ] = new Levels(4, 5, 50.0f, 1200);
	    level[4, 2 ] = new Levels(3, 6, 60.0f, 1300);
	    level[4, 3 ] = new Levels(4, 6, 50.0f, 1200);
		level[4, 4 ] = new Levels(4, 6, 50.0f, 1400);
		level[4, 5 ] = new Levels(3, 5, 55.0f, 1100);
		level[4, 6 ] = new Levels(3, 5, 60.0f, 1000);
		level[4, 7 ] = new Levels(3, 5, 85.0f, 1200);
		level[4, 8 ] = new Levels(4, 6, 90.0f, 1000);
		level[4, 9 ] = new Levels(4, 6, 85.0f, 1400);
		level[4,10 ] = new Levels(4, 6, 120.0f, 1200);
	}
}
