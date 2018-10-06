using UnityEngine;
using System.Collections;
/** this is class is static, should only be called by the gameController to display gui.
 *  May be deprecated later.
 */
public class GameGUI : MonoBehaviour {

	public static void TimeTableGUI(float currentTime) {
	}
	
	public static bool NextLevelGUI() {
		return GUI.Button(new Rect(150, 200, 300, 100), "Please retry!");
	}
	
	public static bool ReplayGUI() {
		return GUI.Button(new Rect(150, 200, 300, 100), "Please retry!");
	}
}
