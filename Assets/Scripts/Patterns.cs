using UnityEngine;
using System.Collections;
using System.Reflection;

public class Patterns : MonoBehaviour{

	public static Patterns p;
	public static int patternCount = 61;

	void Start(){
		p = this;
	}

	public static void execute(int id){
		if(p != null)
			p.StartCoroutine ("p" + id);
	}

	/**
	 * Check if the pattern can be spawned
	 * @return true if you CANNOT spawn this pattern
	 */
	public static bool Check(int id){
		MethodInfo method = typeof(Patterns).GetMethod ("check"+ id);
		if (method == null)
			return false;
		bool r = (bool) method.Invoke (null, null);
		return r;
	}

	public static GameObject Spawn(GameObject type, object[] args){
		GameObject g = Instantiate(type) as GameObject;
		g.SendMessage("Init", args);
		return g;
	}
	
	public static GameObject SpawnSpikes(float x, float y){
		return Spawn(Values.v.Spikes, new object[]{x, y});
	}
	
	public static GameObject SpawnBird(float y, float speed, SideDirection direction){
		return Spawn(Values.v.Bird, new object[]{y, speed, direction});
	}
	
	public static GameObject SpawnPlane(float y, float speed, SideDirection direction){
		return Spawn(Values.v.Plane, new object[]{y, speed, direction});
	}
	
	public static GameObject SpawnBonusChest(Bonus bonus, float x, float y){
		return Spawn (Values.v.BonusChest, new object[]{bonus, x, y});
	}
	
	public static GameObject SpawnMalusChest(Malus malus, float x, float y){
		return Spawn (Values.v.MalusChest, new object[]{malus, x, y});
	}
	
	public static GameObject SpawnBulle(float x, float y){
		return Spawn (Values.v.Bulle, new object[]{x, y});
	}
	
	public static GameObject SpawnTrampo(float x, float y, SideDirection direction){
		return Spawn (Values.v.Trampo, new object[]{x, y, direction});
	}
	
	public static GameObject SpawnChargedCloud(float x, float y){
		return Spawn (Values.v.ChargedCloud, new object[]{x, y});
	}
	
	public static GameObject SpawnCloud(float x, float y){
		return Spawn (Values.v.Cloud, new object[]{x, y});
	}
	
	public static GameObject SpawnLaser(float x, float y, int size, float angle){
		return Spawn (Values.v.Laser, new object[]{x, y, size, angle});
	}
	
	public static GameObject SpawnBlackHole(float x, float y, float radius){
		return Spawn (Values.v.BlackHole, new object[]{x, y, radius});
	}

	public static GameObject SpawnSpookie(int difficulty){
		return Spawn (Values.v.Spookie, new object[]{ difficulty });
	}

	public static GameObject SpawnSpookieProjectile(Vector2 startPos, float startAngle, float speed){
		return Spawn (Values.v.SpookieProjectile, new object[]{startPos, startAngle, speed});
	}

	public static GameObject SpawnKrougObject(Vector2 position, float startAngle, float speed){
		return Spawn (Values.v.KrougObject, new object[]{ position, startAngle, speed });
	}

	public static GameObject SpawnKroug(int difficulty){
		return Spawn (Values.v.Kroug, new object[]{ difficulty });
	}

	IEnumerator p1(){
		SpawnSpikes(3.5f, 6.5f + Values.offsetY);
		SpawnSpikes(2.4f, 6.5f + Values.offsetY);
		SpawnBulle(2.4f, 5.5f + Values.offsetY);
		SpawnBulle(2.4f, 4.5f + Values.offsetY);
		SpawnBulle(2.4f, 3.5f + Values.offsetY);
		yield return null;
	}

	IEnumerator p2(){
		SpawnSpikes (1.5f, 2.9f + Values.offsetY);
		SpawnSpikes (2.7f, 3.7f + Values.offsetY);
		SpawnSpikes (3.9f, 4.5f + Values.offsetY);
		SpawnBulle (1.5f, 0.7f + Values.offsetY);
		SpawnBulle (2.7f, 1.5f + Values.offsetY);
		SpawnBulle (3.9f, 2.3f + Values.offsetY);
		SpawnBulle (5.16f, 3.5f + Values.offsetY);
		SpawnBulle (1.5f, 6.3f + Values.offsetY);
		SpawnBulle (2.7f, 7.3f + Values.offsetY);
		SpawnBulle (3.9f, 8.3f + Values.offsetY);
		SpawnMalusChest ((Malus)Random.Range (0, (int) Malus.END), 5.16f, 6.2f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird (8.2f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}
	
	IEnumerator p3(){
		SpawnBulle (1.6f, 3.8f + Values.offsetY);
		SpawnBulle (0.9f, 2.7f + Values.offsetY);
		SpawnBulle (1.6f, 1.6f + Values.offsetY);
		SpawnBulle (2.6f, 1.6f + Values.offsetY);
		SpawnBulle (4.6f, 1.6f + Values.offsetY);
		SpawnBulle (5.3f, 2.7f + Values.offsetY);
		SpawnBulle (4.6f, 3.8f + Values.offsetY);
		SpawnBulle (3.6f, 1.6f + Values.offsetY);
		SpawnMalusChest ((Malus)Random.Range (0, (int) Malus.END), 1.18f, 5.75f + Values.offsetY);
		SpawnMalusChest ((Malus)Random.Range (0, (int) Malus.END), 3.24f, 5.75f + Values.offsetY);
		SpawnMalusChest ((Malus)Random.Range (0, (int) Malus.END), 5.31f, 5.75f + Values.offsetY);
		SpawnSpikes (1.57f, 8.8f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p4(){
		SpawnBulle (1.6f, 6.3f + Values.offsetY);
		SpawnBulle (1.6f, 4.8f + Values.offsetY);
		SpawnBulle (1.6f, 3.3f + Values.offsetY);
		SpawnBulle (5f, 9f + Values.offsetY);
		SpawnSpikes (1.6f, 7.8f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p5(){
		SpawnBulle(2.6f, 8.9f + Values.offsetY);
		SpawnBulle(3.7f, 8.1f + Values.offsetY);
		SpawnBulle(2.6f, 7.3f + Values.offsetY);
		SpawnBulle(3.7f, 6.5f + Values.offsetY);
		SpawnBulle(1.57f, 3.87f + Values.offsetY);
		SpawnBulle(1.57f, 4.87f + Values.offsetY);
		SpawnBulle(4.65f, 3.87f + Values.offsetY);
		SpawnBulle(4.65f, 4.87f + Values.offsetY);
		SpawnCloud(0.57f, 5.8f + Values.offsetY);
		SpawnCloud(1.57f, 5.8f + Values.offsetY);
		SpawnCloud(5.65f, 5.8f + Values.offsetY);
		SpawnCloud(4.65f, 5.8f + Values.offsetY);
		SpawnCloud(3.5f, 3.2f + Values.offsetY);
		SpawnCloud(2.5f, 3.2f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p6(){
		SpawnSpikes(4.6f, 7.5f + Values.offsetY);
		SpawnSpikes(1.6f, 7.5f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 1.6f, 5.3f + Values.offsetY);
		SpawnBulle(1.6f, 4f + Values.offsetY);
		SpawnBulle(1.6f, 3f + Values.offsetY);
		SpawnCloud(2.6f, 1.5f + Values.offsetY);
		SpawnCloud(1.6f, 1.5f + Values.offsetY);
		SpawnCloud(0.6f, 1.5f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnPlane(9.1f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
	}
	
	IEnumerator p7(){
		SpawnCloud(2.5f, 6.8f + Values.offsetY);
		SpawnCloud(1.5f, 6.8f + Values.offsetY);
		SpawnCloud(0.5f, 6.8f + Values.offsetY);
		SpawnBulle(1.5f, 5.7f + Values.offsetY);
		SpawnBulle(2.5f, 4.7f + Values.offsetY);
		SpawnBulle(1.5f, 3.7f + Values.offsetY);
		SpawnBulle(2.5f, 2.7f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p8(){
		SpawnBulle(2.3f, 9.6f + Values.offsetY);
		SpawnBulle(3.3f, 8.6f + Values.offsetY);
		SpawnBulle(4.3f, 7.6f + Values.offsetY);
		SpawnBulle(4.3f, 5.8f + Values.offsetY);
		SpawnBulle(3.3f, 4.8f + Values.offsetY);
		SpawnBulle(2.3f, 3.8f + Values.offsetY);
		SpawnTrampo(5.4f, 6.85f + Values.offsetY, SideDirection.GOING_LEFT);
		SpawnTrampo(0.7f, 2.75f + Values.offsetY, SideDirection.GOING_RIGHT);
		yield return null;
	}
	
	IEnumerator p9(){
		SpawnChargedCloud(1.5f, 9f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 4.8f, 8.6f  + Values.offsetY);
		SpawnBulle(3.7f, 6.2f + Values.offsetY);
		SpawnSpikes(3.7f, 7.33f + Values.offsetY);
		SpawnBulle(3.7f, 5.2f + Values.offsetY);
		SpawnBulle(2.4f, 3.8f + Values.offsetY);
		SpawnBulle(1.5f, 2.7f + Values.offsetY);
		SpawnCloud(1.5f, 3.4f + Values.offsetY);
		SpawnCloud(0.5f, 3.4f + Values.offsetY);
		SpawnTrampo(4.8f, 0.7f + Values.offsetY, SideDirection.GOING_LEFT);
		yield return null;
	}
	
	IEnumerator p10(){
		SpawnBulle(4.7f, 2.75f + Values.offsetY);
		SpawnBulle(4.7f, 3.75f + Values.offsetY);
		SpawnBulle(4.7f, 4.75f + Values.offsetY);
		SpawnSpikes(5.7f, 4.75f + Values.offsetY);
		SpawnSpikes(3.7f, 4.75f + Values.offsetY);
		SpawnSpikes(1.7f, 4.75f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 1.7f, 6.5f  + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(8.6f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}
	
	IEnumerator p11(){
		SpawnLaser(0f, 1f + Values.offsetY, 4, 0f);
		SpawnLaser(6.25f, 3f + Values.offsetY, 4, 180f);
		SpawnSpikes(4f, 7.9f + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 3f, 7.9f  + Values.offsetY);
		SpawnSpikes(2f, 7.9f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 2f, 5.8f  + Values.offsetY);
		SpawnBulle(2f, 4.2f + Values.offsetY);
		SpawnBulle(2f, 3.2f + Values.offsetY);
		SpawnBulle(3f, 2.2f + Values.offsetY);
		SpawnBulle(4f, 1.2f + Values.offsetY);
		yield return null;
	}

	public static bool check12(){
		if (Values.GetPointCounter ().GetPoints () < Values.minPointToSpawnBlackHole * Values.scoreMultiplier)
			return true;
		return false;
	}
	
	IEnumerator p12(){
		SpawnBulle(4.8f, 10.73f + Values.offsetY);
		SpawnBulle(3.8f, 9.73f + Values.offsetY);
		SpawnBulle(2.8f, 8.73f + Values.offsetY);
		SpawnTrampo(1f, 7.75f + Values.offsetY, SideDirection.GOING_RIGHT);
		SpawnBulle(0.95f, 6.39f + Values.offsetY);
		SpawnBulle(0.95f, 5.39f + Values.offsetY);
		SpawnBulle(1.9f, 4.11f + Values.offsetY);
		SpawnBulle(2.85f, 2.83f + Values.offsetY);
		SpawnBulle(3.8f, 1.55f + Values.offsetY);
		SpawnBulle(5.05f, 0.27f + Values.offsetY);
		SpawnBlackHole(1.7f, 1.75f + Values.offsetY, 3f);
		SpawnSpikes(2.5f, 5.39f + Values.offsetY);
		SpawnSpikes(3.45f, 4.11f + Values.offsetY);
		SpawnSpikes(4.4f, 2.83f + Values.offsetY);
		SpawnSpikes(5.35f, 1.55f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p13(){
		SpawnCloud(4.75f, 5f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 4.75f, 8.2f  + Values.offsetY);
		SpawnBulle(4.75f, 7f + Values.offsetY);
		SpawnBulle(4.75f, 6f + Values.offsetY);
		SpawnSpikes(5.8f, 6.45f + Values.offsetY);
		SpawnSpikes(3.6f, 6.45f + Values.offsetY);
		SpawnSpikes(1.5f, 6.45f + Values.offsetY);
		SpawnBulle(1.5f, 5.5f + Values.offsetY);
		SpawnBulle(1.5f, 4.5f + Values.offsetY);
		SpawnBulle(1.5f, 3.5f + Values.offsetY);
		SpawnCloud(2.5f, 2.3f + Values.offsetY);
		SpawnCloud(1.5f, 2.3f + Values.offsetY);
		SpawnCloud(0.5f, 2.3f + Values.offsetY);
		yield return null;
	}

	public static bool check14(){
		if (Values.GetPointCounter ().GetPoints () < Values.minPointToSpawnBlackHole * Values.scoreMultiplier)
			return true;
		return false;
	}
	
	IEnumerator p14(){
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 1.5f, 6.71f  + Values.offsetY);
		SpawnBlackHole(2.5f, 4.72f + Values.offsetY, 3f);
		SpawnCloud(4.67f, 6.71f + Values.offsetY);
		SpawnCloud(5.67f, 6.71f + Values.offsetY);
		SpawnBulle(5.67f, 5.4f + Values.offsetY);
		SpawnBulle(4.37f, 4.1f + Values.offsetY);
		SpawnBulle(3.07f, 2.8f + Values.offsetY);
		SpawnBulle(1.77f, 1.5f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p15(){
		SpawnBulle(5.1f, 8.8f + Values.offsetY);
		SpawnBulle(5.1f, 7.8f + Values.offsetY);
		SpawnSpikes(3.8f, 8.8f + Values.offsetY);
		SpawnSpikes(2.7f, 7.9f + Values.offsetY);
		SpawnSpikes(1.6f, 7f + Values.offsetY);
		SpawnSpikes(0.5f, 6.1f + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 2.1f, 5f  + Values.offsetY);
		SpawnBulle(2.1f, 4f + Values.offsetY);
		SpawnBulle(3.1f, 3f + Values.offsetY);
		SpawnBulle(4.1f, 2f + Values.offsetY);
		SpawnBulle(2.8f, 1.3f + Values.offsetY);
		SpawnLaser(6.45f, 4f + Values.offsetY, 4, 180f);
		SpawnLaser(-0.2f, 0f + Values.offsetY, 4, 0f);
		yield return null;
	}
	
	IEnumerator p16(){
		SpawnBulle(1.6f, 8.7f + Values.offsetY);
		SpawnBulle(2.6f, 7.4f + Values.offsetY);
		SpawnBulle(3.6f, 6.3f + Values.offsetY);
		SpawnBulle(4.6f, 5.2f + Values.offsetY);
		SpawnSpikes(2.6f, 8.7f + Values.offsetY);
		SpawnSpikes(3.6f, 7.6f + Values.offsetY);
		SpawnCloud(5.7f, 6.5f + Values.offsetY);
		SpawnSpikes(4.6f, 6.5f + Values.offsetY);
		SpawnTrampo(1.6f, 3.6f + Values.offsetY, SideDirection.GOING_RIGHT);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 1.6f, 1.8f  + Values.offsetY);
		yield return null;
	}
	IEnumerator p17(){
		SpawnSpikes(4.7f, 9.3f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 4.7f, 6.1f  + Values.offsetY);
		SpawnBulle(0.7f, 4.8f + Values.offsetY);
		SpawnBulle(0.7f, 3.8f + Values.offsetY);
		SpawnBulle(0.7f, 2.8f + Values.offsetY);
		SpawnBulle(0.7f, 1.8f + Values.offsetY);
		SpawnSpikes(3.7f, 2.8f + Values.offsetY);
		SpawnSpikes(2.7f, 2.8f + Values.offsetY);
		SpawnSpikes(1.7f, 2.8f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnPlane(6.1f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
	}

	public static bool check18(){
		if (Values.GetPointCounter ().GetPoints () < Values.minPointToSpawnBlackHole * Values.scoreMultiplier)
			return true;
		return false;
	}
	
	IEnumerator p18(){
		SpawnBlackHole(3.8f, 7.4f + Values.offsetY, 3f);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 1.5f, 9f  + Values.offsetY);
		SpawnBulle(1.5f, 8f + Values.offsetY);
		SpawnBulle(1.5f, 7f + Values.offsetY);
		SpawnBulle(1.5f, 6f + Values.offsetY);
		SpawnBulle(1.5f, 5f + Values.offsetY);
		SpawnCloud(2.5f, 4f + Values.offsetY);
		SpawnCloud(1.5f, 4f + Values.offsetY);
		SpawnCloud(0.5f, 4f + Values.offsetY);
		SpawnBulle(1.5f, 3f + Values.offsetY);
		SpawnBulle(1.5f, 2f + Values.offsetY);
		SpawnBulle(1.5f, 1f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p19(){
		SpawnBulle(3.5f, 8f + Values.offsetY);
		SpawnBulle(3.5f, 7f + Values.offsetY);
		SpawnBulle(3.5f, 6f + Values.offsetY);
		SpawnChargedCloud(4.55f, 7.7f + Values.offsetY);
		SpawnSpikes(2.5f, 6f + Values.offsetY);
		SpawnChargedCloud(1.5f, 4.9f + Values.offsetY);
		SpawnBulle(1.5f, 2.5f + Values.offsetY);
		SpawnBulle(1.5f, 1.5f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p20(){
		SpawnBulle(2.75f, 6.5f + Values.offsetY);
		SpawnBulle(3.75f, 5.5f + Values.offsetY);
		SpawnBulle(4.75f, 4.5f + Values.offsetY);
		SpawnCloud(3.75f, 6.8f + Values.offsetY);
		SpawnCloud(4.75f, 6.8f + Values.offsetY);
		SpawnCloud(5.75f, 6.8f + Values.offsetY);
		SpawnLaser(1.3f, 3f + Values.offsetY, 4, 0f);
		SpawnBulle(2.8f, 0.47f + Values.offsetY);
		SpawnBulle(2.8f, 1.47f + Values.offsetY);
		SpawnSpikes(5.7f, 1.5f + Values.offsetY);
		SpawnSpikes(3.9f, 1.5f + Values.offsetY);
		SpawnSpikes(1.7f, 1.5f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p21(){
		SpawnBulle(1.5f, 9f + Values.offsetY);
		SpawnBulle(1.5f, 8f + Values.offsetY);
		SpawnSpikes(4.3f, 6.95f + Values.offsetY);
		SpawnSpikes(5.3f, 6.95f + Values.offsetY);
		SpawnBulle(5.3f, 5.8f + Values.offsetY);
		SpawnBulle(5.3f, 4.8f + Values.offsetY);
		SpawnBulle(5.3f, 3.8f + Values.offsetY);
		SpawnTrampo(1.5f, 1.5f + Values.offsetY, SideDirection.GOING_RIGHT);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(5f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
	}

	public static bool check22(){
		if (Values.GetPointCounter ().GetPoints () < Values.minPointToSpawnBlackHole * Values.scoreMultiplier)
			return true;
		return false;
	}
	
	IEnumerator p22(){
		SpawnSpikes(3.5f, 9.5f + Values.offsetY);
		SpawnSpikes(2.5f, 9.5f + Values.offsetY);
		SpawnBlackHole(5f, 6.5f + Values.offsetY, 3f);
		SpawnBlackHole(1.6f, 6.5f + Values.offsetY, 3f);
		SpawnBulle(3.5f, 3.5f + Values.offsetY);
		SpawnBulle(2.5f, 3.5f + Values.offsetY);
		SpawnBulle(4.3f, 1.7f + Values.offsetY);
		SpawnBulle(4.3f, 2.6f + Values.offsetY);
		SpawnBulle(1.7f, 2.6f + Values.offsetY);
		SpawnBulle(1.7f, 1.7f + Values.offsetY);
		SpawnCloud(3.5f, 2.2f + Values.offsetY);
		SpawnCloud(2.5f, 2.2f + Values.offsetY);
		yield return null;
	}

	public static bool check23(){
		if (Values.playerSpeed < Values.cloudSlowPosPerTick * 2f)
			return true;
		return false;
	}
	
	IEnumerator p23(){
		SpawnBulle(3.5f, 4.43f + Values.offsetY);
		SpawnBulle(4.5f, 5.43f + Values.offsetY);
		SpawnBulle(5.7f, 6.43f + Values.offsetY);
		SpawnBulle(0.5f, 8.43f + Values.offsetY);
		SpawnBulle(1.5f, 8.43f + Values.offsetY);
		SpawnBulle(2.5f, 8.43f + Values.offsetY);
		SpawnBulle(3.5f, 8.43f + Values.offsetY);
		SpawnBulle(4.5f, 8.43f + Values.offsetY);
		SpawnBulle(5.7f, 8.43f + Values.offsetY);
		SpawnBulle(5.7f, 7.43f + Values.offsetY);
		SpawnLaser(6.25f, 9.2f + Values.offsetY, 4, 180f);
		SpawnChargedCloud(4.5f, 7.43f + Values.offsetY);
		SpawnChargedCloud(3.5f, 7.43f + Values.offsetY);
		SpawnChargedCloud(2.5f, 7.43f + Values.offsetY);
		SpawnChargedCloud(1.5f, 7.43f + Values.offsetY);
		SpawnChargedCloud(0.5f, 7.43f + Values.offsetY);
		SpawnTrampo(0.6f, 2.5f + Values.offsetY, SideDirection.GOING_RIGHT);
		yield return null;
	}

	public static bool check24(){
		if (Values.GetPointCounter ().GetPoints () < Values.minPointToSpawnBlackHole * Values.scoreMultiplier)
			return true;
		return false;
	}

	IEnumerator p24(){
		SpawnBulle(0.7f, 9.7f + Values.offsetY);
		SpawnBulle(1.7f, 8.7f + Values.offsetY);
		SpawnChargedCloud(4.8f, 7.7f + Values.offsetY);
		SpawnSpikes(1.7f, 7.7f + Values.offsetY);
		SpawnSpikes(0.7f, 7.7f + Values.offsetY);
		SpawnBulle(3.5f, 5.1f + Values.offsetY);
		SpawnBulle(3.5f, 4.1f + Values.offsetY);
		SpawnBulle(3.1f, 2.6f + Values.offsetY);
		SpawnBulle(2f, 1.6f + Values.offsetY);
		SpawnBlackHole(1.8f, 4.3f + Values.offsetY, 3f);
		yield return null;
	}
	
	IEnumerator p25(){
		SpawnBulle(4.7f, 8.2f + Values.offsetY);
		SpawnBulle(3.7f, 7.2f + Values.offsetY);
		SpawnTrampo(5.6f, 8.7f + Values.offsetY, SideDirection.GOING_LEFT);
		SpawnTrampo(2.6f, 6.7f + Values.offsetY, SideDirection.GOING_RIGHT);
		SpawnTrampo(5.5f, 4.5f + Values.offsetY, SideDirection.GOING_LEFT);
		SpawnLaser(0.5f, 6.1f + Values.offsetY, 6, -45f);
		SpawnBulle(0.7f, 4.8f + Values.offsetY);
		SpawnBulle(0.7f, 3.8f + Values.offsetY);
		SpawnBulle(0.7f, 2.8f + Values.offsetY);
		SpawnBulle(0.7f, 1.8f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p26(){
		SpawnBulle(3.65f, 9.25f + Values.offsetY);
		SpawnBulle(2.65f, 8.25f + Values.offsetY);
		SpawnBulle(1.65f, 7.25f + Values.offsetY);
		SpawnBulle(2.65f, 6.25f + Values.offsetY);
		SpawnBulle(3.65f, 5.25f + Values.offsetY);
		SpawnBulle(4.65f, 4.25f + Values.offsetY);
		SpawnBulle(3.65f, 3.25f + Values.offsetY);
		SpawnBulle(2.65f, 2.25f + Values.offsetY);
		SpawnBulle(1.65f, 1.25f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(9.6f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
		SpawnBird(7.2f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
		SpawnPlane(4.5f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
	}

	IEnumerator p27(){
		SpawnBulle(4.6f, 9.9f + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 3.6f, 5.7f  + Values.offsetY);
		SpawnSpikes(5.99f, 3.84f + Values.offsetY);
		SpawnBulle(3.6f, 4.7f + Values.offsetY);
		SpawnBulle(3.6f, 3.7f + Values.offsetY);
		SpawnBulle(3.6f, 2.7f + Values.offsetY);
		SpawnChargedCloud(2.6f, 2f + Values.offsetY);
		SpawnChargedCloud(4.6f, 2f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnPlane(4.18f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
		SpawnBird(10.21f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}

	IEnumerator p28(){
		SpawnBulle(5.1f, 9.2f + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 5.962279f, 4.518843f  + Values.offsetY);
		SpawnBulle(5.1f, 8.2f + Values.offsetY);
		SpawnBulle(6.1f, 7.2f + Values.offsetY);
		SpawnChargedCloud(4.9f, 7.2f + Values.offsetY);
		SpawnChargedCloud(3.6f, 6.2f + Values.offsetY);
		SpawnChargedCloud(2.3f, 5.2f + Values.offsetY);
		SpawnChargedCloud(1.2f, 4.2f + Values.offsetY);
		SpawnBulle(0.3f, 3f + Values.offsetY);
		SpawnBulle(0.3f, 2f + Values.offsetY);
		SpawnSpikes(0.3f, 1f + Values.offsetY);
		yield return null;
	}

	IEnumerator p29(){
		SpawnBulle(5.1f, 0.1f + Values.offsetY);
		SpawnBulle(4.1f, 1.1f + Values.offsetY);
		SpawnBulle(3.1f, 2.1f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 4f, 4.3f  + Values.offsetY);
		SpawnChargedCloud(5.5f, 6.4f + Values.offsetY);
		SpawnChargedCloud(2.6f, 6.4f + Values.offsetY);
		SpawnBulle(2.6f, 8.3f + Values.offsetY);
		SpawnBulle(2.6f, 9.3f + Values.offsetY);
		SpawnBulle(5.5f, 9.3f + Values.offsetY);
		SpawnBulle(5.5f, 8.3f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(3f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}

	IEnumerator p30(){
		SpawnBulle(5f, 9f + Values.offsetY);
		SpawnBulle(5f, 8f + Values.offsetY);
		SpawnBulle(5f, 7f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 5f, 5.7f  + Values.offsetY);
		SpawnCloud(6f, 4.6f + Values.offsetY);
		SpawnCloud(5f, 4.6f + Values.offsetY);
		SpawnCloud(4f, 4.6f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnPlane(9.66f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}
	
	IEnumerator p31(){
		SpawnBulle(3.3f, 2.3f + Values.offsetY);
		SpawnBulle(3.3f, 1.3f + Values.offsetY);
		SpawnSpikes(4.3f, 1.3f + Values.offsetY);
		SpawnSpikes(2.3f, 1.3f + Values.offsetY);
		SpawnSpikes(0.3f, 1.3f + Values.offsetY);
		SpawnBulle(3.3f, 3.3f + Values.offsetY);
		SpawnBulle(2.3f, 4.3f + Values.offsetY);
		SpawnLaser(6.1f, 5.8f + Values.offsetY, 4, 180f);
		SpawnTrampo(5.3f, 8.8f + Values.offsetY, SideDirection.GOING_LEFT);
		SpawnTrampo(1.4f, 5.8f + Values.offsetY, SideDirection.GOING_RIGHT);
		SpawnLaser(0.1f, 9f + Values.offsetY, 4, 0f);
		yield return null;	
	}
	
	IEnumerator p32(){
		SpawnBulle(2.5f, 0.2f + Values.offsetY);
		SpawnBulle(2.5f, 1.2f + Values.offsetY);
		SpawnBulle(2.5f, 3.2f + Values.offsetY);
		SpawnBulle(2.5f, 2.2f + Values.offsetY);
		SpawnBulle(2.5f, 4.2f + Values.offsetY);
		SpawnChargedCloud(3.8f, 1.47f + Values.offsetY);
		SpawnChargedCloud(4.8f, 1.47f + Values.offsetY);
		SpawnLaser(0.17f, 5.2f + Values.offsetY, 4, 0f);
		SpawnBulle(2.5f, 8.2f + Values.offsetY);
		SpawnBulle(2.5f, 9.2f + Values.offsetY);
		SpawnBulle(2.5f, 7.2f + Values.offsetY);
		SpawnBulle(2.5f, 6.2f + Values.offsetY);
		SpawnLaser(6.1f, 10f + Values.offsetY, 4, 180f);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(7.73f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
	}
	
	IEnumerator p33(){
		SpawnCloud(0.4f, 1.8f + Values.offsetY);
		SpawnCloud(2.6f, 1.8f + Values.offsetY);
		SpawnCloud(3.6f, 1.8f + Values.offsetY);
		SpawnCloud(5.85f, 1.8f + Values.offsetY);
		SpawnBulle(4.7f, 2.15f + Values.offsetY);
		SpawnBulle(3.6f, 3.15f + Values.offsetY);
		SpawnBulle(2.6f, 3.15f + Values.offsetY);
		SpawnBulle(1.55f, 2.15f + Values.offsetY);
		SpawnSpikes(1.42f, 4.42f + Values.offsetY);
		SpawnSpikes(0.42f, 4.42f + Values.offsetY);
		SpawnSpikes(4.84f, 4.42f + Values.offsetY);
		SpawnSpikes(5.84f, 4.42f + Values.offsetY);
		SpawnBulle(1.42f, 8.55f + Values.offsetY);
		SpawnBulle(1.42f, 9.55f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(6.53f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}
	
	IEnumerator p34(){
		SpawnLaser(6.22131f, 0.9004107f + Values.offsetY, 5, 135f);
		SpawnLaser(0.1364694f, 3.475287f + Values.offsetY, 5, 45f);
		SpawnBulle(1.43f, 3.05f + Values.offsetY);
		SpawnBulle(2.43f, 4.05f + Values.offsetY);
		SpawnBulle(3.43f, 5.05f + Values.offsetY);
		SpawnBulle(4.43f, 6.05f + Values.offsetY);
		SpawnSpikes(5.9f, 9.5f + Values.offsetY);
		SpawnSpikes(4.9f, 9.5f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p35(){
		SpawnCloud(0.5f, 1.8f + Values.offsetY);
		SpawnBulle(5.128233f, 2f + Values.offsetY);
		SpawnBulle(5.128233f, 3f + Values.offsetY);
		SpawnBulle(5.138233f, 4f + Values.offsetY);
		SpawnBulle(5.128233f, 5f + Values.offsetY);
		SpawnLaser(4.2f, 2.62f + Values.offsetY, 4, 135f);
		SpawnSpikes(4.2f, 5.3f + Values.offsetY);
		SpawnSpikes(3.2f, 5.3f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 0.5f, 6.3f  + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p36(){
		SpawnBulle(3.6f, 2.35f + Values.offsetY);
		SpawnBulle(2.6f, 3.35f + Values.offsetY);
		SpawnBulle(1.6f, 4.35f + Values.offsetY);
		SpawnTrampo(4.6f, 1.4f + Values.offsetY, SideDirection.GOING_LEFT);
		SpawnSpikes(5.8f, 8.3f + Values.offsetY);
		SpawnSpikes(4.8f, 8.3f + Values.offsetY);
		SpawnSpikes(0.4f, 8.3f + Values.offsetY);
		SpawnSpikes(1.4f, 8.3f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnPlane(6f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
	}
	
	IEnumerator p37(){
		SpawnBulle(1.6f, 1.25f + Values.offsetY);
		SpawnBulle(1.6f, 2.25f + Values.offsetY);
		SpawnBulle(1.6f, 3.25f + Values.offsetY);
		SpawnBulle(4.02f, 5.84f + Values.offsetY);
		SpawnBulle(4.02f, 4.84f + Values.offsetY);
		SpawnBulle(4.02f, 3.84f + Values.offsetY);
		SpawnSpikes(5.8f, 8.4f + Values.offsetY);
		SpawnSpikes(4.8f, 8.4f + Values.offsetY);
		SpawnChargedCloud(1.6f, 4.8f + Values.offsetY);
		SpawnChargedCloud(4.02f, 7.39f + Values.offsetY);
		SpawnTrampo(1.6f, 9.3f + Values.offsetY, SideDirection.GOING_RIGHT);
		yield return null;
	}
	
	IEnumerator p38(){
		SpawnCloud(3.9f, 3.7f + Values.offsetY);
		SpawnCloud(2.9f, 3.7f + Values.offsetY);
		SpawnLaser(0.03567648f, 7.2f + Values.offsetY, 5, 0f);
		SpawnBulle(4.9f, 7.2f + Values.offsetY);
		SpawnBulle(3.9f, 6.2f + Values.offsetY);
		SpawnBulle(2.9f, 9.2f + Values.offsetY);
		SpawnBulle(3.9f, 8.2f + Values.offsetY);
		SpawnBulle(2.9f, 10.2f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(2.67f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}
	
	IEnumerator p39(){
		SpawnLaser(0.023f, 8.6f + Values.offsetY, 5, -45f);
		SpawnBulle(5f, 1.38f + Values.offsetY);
		SpawnBulle(5f, 2.38f + Values.offsetY);
		SpawnBulle(5f, 3.38f + Values.offsetY);
		SpawnCloud(6f, 4.38f + Values.offsetY);
		SpawnCloud(5f, 4.38f + Values.offsetY);
		SpawnCloud(4f, 4.38f + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 1.7f, 5f  + Values.offsetY);
		SpawnBulle(5f, 8.52f + Values.offsetY);
		SpawnBulle(5f, 9.52f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p40(){
		SpawnSpikes(1.58f, 2.67f + Values.offsetY);
		SpawnSpikes(3.58f, 2.67f + Values.offsetY);
		SpawnSpikes(5.58f, 2.67f + Values.offsetY);
		SpawnBulle(0.42f, 2.67f + Values.offsetY);
		SpawnBulle(0.4f, 1.67f + Values.offsetY);
		SpawnBulle(0.41f, 3.67f + Values.offsetY);
		SpawnBulle(1.58f, 4.94f + Values.offsetY);
		SpawnCloud(2.58f, 7.65f + Values.offsetY);
		SpawnCloud(3.58f, 7.63f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnPlane(6.32f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
		SpawnBird(9.72f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}

	IEnumerator p41(){
		SpawnCloud(0.43f, 1.13f + Values.offsetY);
		SpawnCloud(1.5f, 2.3f + Values.offsetY);
		SpawnCloud(2.5f, 3.47f + Values.offsetY);
		SpawnCloud(6.03f, 4.42f + Values.offsetY);
		SpawnCloud(4.96f, 5.59f + Values.offsetY);
		SpawnCloud(3.89f, 6.76f + Values.offsetY);
		SpawnBulle(4.96f, 1.47f + Values.offsetY);
		SpawnBulle(4.96f, 2.47f + Values.offsetY);
		SpawnBulle(4.96f, 3.47f + Values.offsetY);
		SpawnBulle(1.56f, 6.76f + Values.offsetY);
		SpawnBulle(2.56f, 7.96f + Values.offsetY);
		SpawnBulle(3.56f, 8.96f + Values.offsetY);
		SpawnCloud(2.5f, 9.98f + Values.offsetY);
		SpawnCloud(1.5f, 8.81f + Values.offsetY);
		SpawnCloud(0.43f, 7.64f + Values.offsetY);
		yield return null;
	}

	public static bool check42(){
		if (Values.GetPointCounter ().GetPoints () < Values.minPointToSpawnBlackHole * Values.scoreMultiplier)
			return true;
		return false;
	}

	IEnumerator p42(){
		SpawnBlackHole(2.66f, 1.7f + Values.offsetY, 3f);
		SpawnBlackHole(4.93f, 5.55f + Values.offsetY, 3f);
		SpawnBlackHole(1.77f, 8.85f + Values.offsetY, 3f);
		SpawnBulle(3.52f, 0.3f + Values.offsetY);
		SpawnBulle(4.52f, 1.57f + Values.offsetY);
		SpawnBulle(4.52f, 2.57f + Values.offsetY);
		SpawnBulle(3.45f, 3.73f + Values.offsetY);
		SpawnBulle(2.42f, 5f + Values.offsetY);
		SpawnBulle(2.42f, 6.3f + Values.offsetY);
		SpawnBulle(3.63f, 7.4f + Values.offsetY);
		SpawnBulle(4.55f, 8.7f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p43(){
		SpawnLaser(-0.0146966f, 0.232223f + Values.offsetY, 5, 0f);
		SpawnLaser(4.723568f, 0.9958994f + Values.offsetY, 10, 90f);
		SpawnBulle(3.821042f, 1f + Values.offsetY);
		SpawnBulle(2.69f, 1f + Values.offsetY);
		SpawnBulle(1.6f, 1f + Values.offsetY);
		SpawnBulle(0.51f, 1f + Values.offsetY);
		SpawnBulle(0.51f, 2f + Values.offsetY);
		SpawnBulle(1.6f, 2f + Values.offsetY);
		SpawnBulle(2.71f, 2f + Values.offsetY);
		SpawnBulle(3.8f, 2f + Values.offsetY);
		SpawnBulle(3.8f, 3f + Values.offsetY);
		SpawnBulle(1.6f, 3f + Values.offsetY);
		SpawnBulle(0.51f, 3f + Values.offsetY);
		SpawnBulle(0.51f, 4f + Values.offsetY);
		SpawnBulle(2.71f, 4f + Values.offsetY);
		SpawnBulle(3.8f, 5f + Values.offsetY);
		SpawnBulle(2.71f, 5f + Values.offsetY);
		SpawnBulle(1.6f, 5f + Values.offsetY);
		SpawnBulle(0.51f, 5f + Values.offsetY);
		SpawnBulle(0.51f, 6f + Values.offsetY);
		SpawnBulle(2.71f, 6f + Values.offsetY);
		SpawnBulle(2.71f, 7f + Values.offsetY);
		SpawnBulle(0.51f, 7f + Values.offsetY);
		SpawnBulle(0.51f, 8f + Values.offsetY);
		SpawnBulle(1.6f, 8f + Values.offsetY);
		SpawnBulle(2.71f, 8f + Values.offsetY);
		SpawnBulle(3.8f, 8f + Values.offsetY);
		SpawnBulle(3.8f, 9f + Values.offsetY);
		SpawnBulle(2.71f, 9f + Values.offsetY);
		SpawnBulle(1.6f, 9f + Values.offsetY);
		SpawnBulle(0.51f, 9f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 1.6f, 4f  + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 1.6f, 6f  + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 1.6f, 7f  + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 2.71f, 3f  + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 3.8f, 4f  + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 3.8f, 6f  + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 3.8f, 7f  + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 6f, 9.67f  + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p44(){
		SpawnBulle(2.55f, 1.16f + Values.offsetY);
		SpawnBulle(4f, 2.16f + Values.offsetY);
		SpawnBulle(2.55f, 3.16f + Values.offsetY);
		SpawnBulle(4f, 4.16f + Values.offsetY);
		SpawnCloud(3.76f, 5.88f + Values.offsetY);
		SpawnCloud(2.76f, 5.88f + Values.offsetY);
		SpawnCloud(4.76f, 5.88f + Values.offsetY);
		SpawnCloud(1.76f, 5.88f + Values.offsetY);
		SpawnSpikes(0.5f, 8.5f + Values.offsetY);
		SpawnSpikes(6f, 8.5f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p45(){
		SpawnChargedCloud(3.77f, 1.65f + Values.offsetY);
		SpawnBulle(5.28f, 1.68f + Values.offsetY);
		SpawnBulle(5.28f, 2.68f + Values.offsetY);
		SpawnBulle(5.28f, 3.68f + Values.offsetY);
		SpawnBulle(4.17f, 4.68f + Values.offsetY);
		SpawnChargedCloud(5.28f, 5.9f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 1.66f, 7.46f  + Values.offsetY);
		SpawnSpikes(2.66f, 4.08f + Values.offsetY);
		SpawnSpikes(1.66f, 4.08f + Values.offsetY);
		SpawnSpikes(0.66f, 4.08f + Values.offsetY);
		yield return null;
	}

	IEnumerator p46(){
		SpawnLaser(4.897131f, 0.3710732f + Values.offsetY, 4, 135f);
		SpawnBulle(3.56f, 0.07f + Values.offsetY);
		SpawnBulle(2.56f, 1.07f + Values.offsetY);
		SpawnBulle(1.56f, 2.07f + Values.offsetY);
		SpawnBulle(2.6f, 3.96f + Values.offsetY);
		SpawnBulle(3.8f, 5.28f + Values.offsetY);
		SpawnCloud(0.6f, 5.9f + Values.offsetY);
		SpawnCloud(1.6f, 5.9f + Values.offsetY);
		SpawnCloud(2.6f, 5.9f + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 1.6f, 4.9f  + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 5.22f, 9.13f  + Values.offsetY);
		SpawnBulle(1.6f, 9.75f + Values.offsetY);
		SpawnBulle(1.6f, 8.75f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p47(){
		SpawnCloud(3f, 1.32f + Values.offsetY);
		SpawnCloud(4f, 1.32f + Values.offsetY);
		SpawnCloud(5f, 2.32f + Values.offsetY);
		SpawnCloud(6f, 3.51f + Values.offsetY);
		SpawnCloud(4f, 4.8f + Values.offsetY);
		SpawnCloud(3f, 4.8f + Values.offsetY);
		SpawnCloud(1.8f, 5.6f + Values.offsetY);
		SpawnCloud(0.6f, 6.49f + Values.offsetY);
		SpawnBulle(6f, 0.32f + Values.offsetY);
		SpawnBulle(6f, 1.32f + Values.offsetY);
		SpawnBulle(6f, 2.32f + Values.offsetY);
		SpawnBulle(5.15f, 7f + Values.offsetY);
		SpawnBulle(4.15f, 8f + Values.offsetY);
		SpawnBulle(4.15f, 9f + Values.offsetY);
		SpawnBulle(3.15f, 10f + Values.offsetY);
		SpawnChargedCloud(4.15f, 10f + Values.offsetY);
		yield return null;
	}

	public static bool check48(){
		if (Values.GetPointCounter ().GetPoints () < Values.minPointToSpawnBlackHole * Values.scoreMultiplier)
			return true;
		return false;
	}
	
	IEnumerator p48(){
		SpawnBlackHole(3.99f, 2.32f + Values.offsetY, 3f);
		SpawnBulle(1.12f, 2f + Values.offsetY);
		SpawnBulle(2.12f, 3f + Values.offsetY);
		SpawnBulle(3.12f, 4f + Values.offsetY);
		SpawnBulle(4.12f, 5f + Values.offsetY);
		SpawnSpikes(2f, 4.5f + Values.offsetY);
		SpawnSpikes(1f, 4.5f + Values.offsetY);
		SpawnBlackHole(4.94f, 8.51f + Values.offsetY, 3f);
		SpawnLaser(0.2f, 9.44f + Values.offsetY, 4, 0f);
		yield return null;
	}
	
	IEnumerator p49(){
		SpawnBulle(5.8f, 1.01f + Values.offsetY);
		SpawnBulle(5.8f, 2.5f + Values.offsetY);
		SpawnBulle(5.8f, 4f + Values.offsetY);
		SpawnBulle(3.58f, 7.8f + Values.offsetY);
		SpawnLaser(0.2042835f, 7.101869f + Values.offsetY, 4, 0f);
		SpawnBulle(2.58f, 8.8f + Values.offsetY);
		SpawnBulle(1.58f, 9.8f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(8.28f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
		SpawnBird(2.7f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
		SpawnPlane(5.21f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
	}
	
	IEnumerator p50(){
		SpawnLaser(3.797423f, 0.1459184f + Values.offsetY, 6, 135f);
		SpawnLaser(5.42f, 1.5f + Values.offsetY, 6, 135f);
		SpawnBulle(4.7f, 0.87f + Values.offsetY);
		SpawnBulle(3.7f, 1.87f + Values.offsetY);
		SpawnBulle(2.7f, 2.87f + Values.offsetY);
		SpawnBulle(1.7f, 3.87f + Values.offsetY);
		SpawnBulle(0.7f, 4.87f + Values.offsetY);
		SpawnLaser(2.046919f, 5.643116f + Values.offsetY, 5, 45f);
		SpawnLaser(0.3731916f, 7.16329f + Values.offsetY, 5, 45f);
		SpawnBulle(1.371286f, 6.533723f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 4.764807f, 9.881178f  + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p51(){
		SpawnTrampo(0.76f, 1.67f + Values.offsetY, SideDirection.GOING_RIGHT);
		SpawnBulle(2.34f, 2.7f + Values.offsetY);
		SpawnBulle(3.9f, 3.7f + Values.offsetY);
		SpawnBulle(2.34f, 4.7f + Values.offsetY);
		SpawnSpikes(5.9f, 5.96f + Values.offsetY);
		SpawnSpikes(4.9f, 5.96f + Values.offsetY);
		SpawnSpikes(3.9f, 5.96f + Values.offsetY);
		SpawnSpikes(0.36f, 7.43f + Values.offsetY);
		SpawnSpikes(1.36f, 7.43f + Values.offsetY);
		SpawnSpikes(2.36f, 7.43f + Values.offsetY);
		SpawnLaser(6.039297f, 7.716081f + Values.offsetY, 4, 135f);
		yield return null;
	}
	
	IEnumerator p52(){
		SpawnCloud(2.69f, 0.88f + Values.offsetY);
		SpawnCloud(3.69f, 0.88f + Values.offsetY);
		SpawnCloud(1.43f, 3.09f + Values.offsetY);
		SpawnCloud(0.43f, 3.09f + Values.offsetY);
		SpawnCloud(4.87f, 3.09f + Values.offsetY);
		SpawnCloud(5.87f, 3.09f + Values.offsetY);
		SpawnCloud(3.69f, 5.52f + Values.offsetY);
		SpawnCloud(2.69f, 5.52f + Values.offsetY);
		SpawnCloud(5.87f, 7.77f + Values.offsetY);
		SpawnCloud(4.87f, 7.77f + Values.offsetY);
		SpawnCloud(0.43f, 7.77f + Values.offsetY);
		SpawnCloud(1.43f, 7.77f + Values.offsetY);
		SpawnCloud(2.69f, 9.7f + Values.offsetY);
		SpawnCloud(3.69f, 9.7f + Values.offsetY);
		SpawnBulle(1.69f, 0.11f + Values.offsetY);
		SpawnBulle(1.69f, 1.4f + Values.offsetY);
		SpawnBulle(2.69f, 2.6f + Values.offsetY);
		SpawnBulle(3.69f, 3.65f + Values.offsetY);
		SpawnBulle(4.69f, 4.63f + Values.offsetY);
		SpawnBulle(2.69f, 7.78f + Values.offsetY);
		SpawnBulle(2.69f, 8.78f + Values.offsetY);
		SpawnBulle(1.69f, 9.78f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p53(){
		SpawnBulle(1.68f, 0.93f + Values.offsetY);
		SpawnBulle(2.68f, 1.93f + Values.offsetY);
		SpawnBulle(3.68f, 2.93f + Values.offsetY);
		SpawnBulle(4.68f, 3.93f + Values.offsetY);
		SpawnBulle(5.68f, 4.93f + Values.offsetY);
		SpawnBulle(3.42f, 10.07f + Values.offsetY);
		SpawnBulle(2.42f, 9.07f + Values.offsetY);
		SpawnBulle(1.42f, 8.07f + Values.offsetY);
		SpawnBulle(0.42f, 7.07f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnPlane(3.4f, Values.playerSpeed * (2f/3), SideDirection.GOING_RIGHT);
		SpawnBird(7.81f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
		yield return null;
	}
	
	IEnumerator p54(){
		SpawnSpikes(0.35f, 3.73f + Values.offsetY);
		SpawnSpikes(1.35f, 3.73f + Values.offsetY);
		SpawnSpikes(2.35f, 3.73f + Values.offsetY);
		SpawnBulle(4.96f, 3.89f + Values.offsetY);
		SpawnBulle(4.96f, 4.89f + Values.offsetY);
		SpawnBulle(4.96f, 5.89f + Values.offsetY);
		SpawnCloud(5.91f, 7f + Values.offsetY);
		SpawnCloud(4.91f, 7f + Values.offsetY);
		SpawnCloud(3.91f, 7f + Values.offsetY);
		SpawnCloud(2.91f, 7f + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 1.18f, 8.68f  + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p55(){
		SpawnSpikes(0.69f, 3.27f + Values.offsetY);
		SpawnSpikes(1.69f, 4.27f + Values.offsetY);
		SpawnSpikes(3.69f, 4.27f + Values.offsetY);
		SpawnSpikes(4.69f, 3.27f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 2.69f, 3.79f  + Values.offsetY);
		SpawnBulle(2.69f, 5.27f + Values.offsetY);
		SpawnBulle(3.69f, 6.34f + Values.offsetY);
		SpawnBulle(4.69f, 7.34f + Values.offsetY);
		SpawnBulle(3.69f, 8.34f + Values.offsetY);
		SpawnChargedCloud(2.69f, 6.34f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p56(){
		SpawnTrampo(1.2f, 1.68f + Values.offsetY, SideDirection.GOING_RIGHT);
		SpawnBulle(2.41f, 3.08f + Values.offsetY);
		SpawnBulle(3.41f, 4.08f + Values.offsetY);
		SpawnBulle(4.41f, 5.08f + Values.offsetY);
		SpawnCloud(4.7f, 8.5f + Values.offsetY);
		SpawnCloud(3.7f, 8.51f + Values.offsetY);
		SpawnCloud(1.7f, 8.48f + Values.offsetY);
		SpawnCloud(0.7f, 8.49f + Values.offsetY);
		yield return new WaitForSeconds(Values.offsetY/Values.playerSpeed);
		SpawnBird(5.75f, Values.playerSpeed * (2f/3), SideDirection.GOING_LEFT);
	}
	
	IEnumerator p57(){
		SpawnLaser(0.1512301f, 3.14f + Values.offsetY, 5, 0f);
		SpawnBulle(2.27f, 0.3f + Values.offsetY);
		SpawnBulle(2.27f, 1.3f + Values.offsetY);
		SpawnBulle(2.27f, 2.3f + Values.offsetY);
		SpawnCloud(0.8f, 8.48f + Values.offsetY);
		SpawnMalusChest((Malus) Random.Range(0, (int) Malus.END), 5f, 5.5f  + Values.offsetY);
		SpawnSpikes(6f, 6.6f + Values.offsetY);
		SpawnSpikes(5f, 6.6f + Values.offsetY);
		SpawnSpikes(4f, 6.6f + Values.offsetY);
		SpawnCloud(1.8f, 8.48f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p58(){
		SpawnTrampo(4.96f, 0.56f + Values.offsetY, SideDirection.GOING_LEFT);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 2f, 3.56f  + Values.offsetY);
		SpawnBulle(2f, 5.62f + Values.offsetY);
		SpawnBulle(3f, 6.62f + Values.offsetY);
		SpawnBulle(4f, 7.62f + Values.offsetY);
		SpawnBulle(5f, 8.62f + Values.offsetY);
		SpawnChargedCloud(2f, 6.89f + Values.offsetY);
		SpawnChargedCloud(4f, 8.7f + Values.offsetY);
		SpawnChargedCloud(5.87f, 9.7f + Values.offsetY);
		yield return null;
	}
	
	IEnumerator p59(){
		SpawnCloud(1.79f, 2f + Values.offsetY);
		SpawnCloud(2.79f, 2f + Values.offsetY);
		SpawnCloud(3.79f, 2f + Values.offsetY);
		SpawnCloud(4.79f, 2f + Values.offsetY);
		SpawnCloud(5.78f, 5.51f + Values.offsetY);
		SpawnCloud(4.5f, 6.51f + Values.offsetY);
		SpawnCloud(2f, 6.51f + Values.offsetY);
		SpawnCloud(0.63f, 5.51f + Values.offsetY);
		SpawnCloud(3.67f, 8.8f + Values.offsetY);
		SpawnCloud(2.67f, 8.8f + Values.offsetY);
		SpawnCloud(1.67f, 8.8f + Values.offsetY);
		SpawnCloud(0.67f, 8.8f + Values.offsetY);
		SpawnBulle(2.79f, 0.1f + Values.offsetY);
		SpawnBulle(3.79f, 0.14f + Values.offsetY);
		SpawnBulle(4.79f, 1.1f + Values.offsetY);
		SpawnBulle(1.79f, 1.1f + Values.offsetY);
		SpawnBulle(0.79f, 2f + Values.offsetY);
		SpawnBulle(5.79f, 2f + Values.offsetY);
		SpawnBulle(4.79f, 3.34f + Values.offsetY);
		SpawnBulle(1.79f, 3.34f + Values.offsetY);
		SpawnBulle(2.79f, 4.5f + Values.offsetY);
		SpawnBulle(3.79f, 4.5f + Values.offsetY);
		SpawnBonusChest((Bonus) Random.Range(0, (int) Bonus.END), 5.41f, 9.95f  + Values.offsetY);
		yield return null;
	}

	public static bool check60(){
		if (Values.GetPointCounter ().GetPoints () < Values.minPointToSpawnBlackHole * Values.scoreMultiplier)
			return true;
		return false;
	}
	
	IEnumerator p60(){
		SpawnBlackHole(2.74f, 2f + Values.offsetY, 3f);
		SpawnBlackHole(2.74f, 5f + Values.offsetY, 3f);
		SpawnBlackHole(2.74f, 8f + Values.offsetY, 3f);
		SpawnBulle(5.2f, 2f + Values.offsetY);
		SpawnBulle(5.16f, 3.5f + Values.offsetY);
		SpawnBulle(5.17f, 5f + Values.offsetY);
		SpawnBulle(5.17f, 6.5f + Values.offsetY);
		SpawnBulle(5.17f, 8f + Values.offsetY);
		SpawnLaser(6f, 0.5035214f + Values.offsetY, 3, 180f);
		SpawnLaser(6f, 10.17572f + Values.offsetY, 3, 180f);
		yield return null;
	}
	
	IEnumerator p61(){
		SpawnBulle(3.77f, 1.36f + Values.offsetY);
		SpawnBulle(2.77f, 2.36f + Values.offsetY);
		SpawnBulle(1.77f, 3.36f + Values.offsetY);
		SpawnBulle(0.77f, 4.36f + Values.offsetY);
		SpawnLaser(6.1f, 5.187817f + Values.offsetY, 5, 180f);
		SpawnLaser(6.1f, 7.39f + Values.offsetY, 3, 180f);
		SpawnLaser(0.2f, 7.39f + Values.offsetY, 3, 0f);
		SpawnLaser(0.2f, 9.36f + Values.offsetY, 3, 0f);
		SpawnLaser(6.1f, 9.36f + Values.offsetY, 3, 180f);
		yield return null;
	}
}
