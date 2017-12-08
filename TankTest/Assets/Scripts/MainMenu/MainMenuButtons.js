import UnityEngine.SceneManagement;
import UnityEngine.Debug;
function LoadDesertScene()
 {
     Application.LoadLevel(1);
 }
 function LoadBeachScene()
 {
     Application.LoadLevel(2);
 }
function QuitGame() {
	Application.Quit();
}