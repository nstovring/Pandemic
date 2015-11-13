using UnityEngine;
using System.Collections;

public class __roleCards : MonoBehaviour {

	public List<String> createRoleCards () {
		
		//System.out.println("test");
		
		List<String> RoleCards = new ArrayList<String>();
		
		RoleCards.add ("WHITE_MEDIC");
		RoleCards.add ("WHITE_DISPATCHER");
		RoleCards.add ("WHITE_QURANTINE SPECIALIST");
		RoleCards.add ("WHITE_CONTINGENCY");
		RoleCards.add ("WHITE_RESEARCHER");
		RoleCards.add ("WHITE_SCIENTIST");
		RoleCards.add ("WHITE_OPERATIONS EXPERT");
		
		
		//My shuffle function... damn it all
		/*int n = RoleCards.size();
   		Random random = new Random ();
		List<String> tmpRoleCards = new ArrayList<String>();

		for (int i = 0; i <n; i++) {  		
   			int r = random.nextInt(RoleCards.size());
   			tmpRoleCards.add (RoleCards.get(r));
   			RoleCards.remove(r);
	    }		
		
		for (int i = 0; i < tmpRoleCards.size(); i++) {
			//System.out.println("tmpList: " + tmpRoleCards.get(i));
	    }*/
		
		return RoleCards;
	}
	
}
