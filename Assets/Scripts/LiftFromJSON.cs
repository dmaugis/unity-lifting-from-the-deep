﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SimpleJSON;



public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public NamedArrayAttribute(string[] names) { this.names = names; }
}

[CustomPropertyDrawer (typeof(NamedArrayAttribute))]public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.ObjectField(rect, property, new GUIContent(((NamedArrayAttribute)attribute).names[pos]));
        } catch {
            EditorGUI.ObjectField(rect, property, label);
        }
    }
}

public class LiftFromJSON : MonoBehaviour {
	private JSONNode J;
	private GameObject[] A=new GameObject[16];
        public Vector3 scale=new Vector3(0.01f,0.01f,0.01f);
	[TextArea(4, 6)]
	public string jpos = "";
        
        private static string[]    MPII_labels= {"hips", 
                                            "thigh.L", "shin.L","foot.L",
                                            "thigh.R","shin.R","foot.R",
                                            "spine","neck","jaw","head",
                                            "upperarm.R","forearm.R","hand.R",
                                            "upperarm_L","forearm_L","handL"
                                            };
        private static Color[]    MPII_colors= {
                    new Color(0, 0, 255), new Color(0, 170, 255),new Color(0, 255, 170),new Color(0, 255, 0),
                    new Color(170, 255, 0), new Color(255, 170, 0),new Color(255, 0, 0),new Color(255, 0, 170),
                    new Color(170, 0, 255)
                    };
        private static int[]      MPII_limbs = { 0, 1, 2, 3, 3, 4, 5, 6, 6, 7, 8, 9, 9, 10, 11, 12, 12, 13 };
        [NamedArrayAttribute (new string[]{ "hips", 
                                            "thigh.L", "shin.L","foot.L",
                                            "thigh_R","shin_R","foot.R",
                                            "spine","neck","jaw","head",
                                            "upper_arm_R","forearm_R","handR",
                                            "upper_arm_L","forearm_L","handL"
                                            })]
        public GameObject[] MPII_parts;

	void BuildPosition(JSONNode J){
		JSONNode x = J [0];
		JSONNode z = J [1];
		JSONNode y = J [2];

		for (int k = 0; k < 17; k++) {
			Vector3 vk = new Vector3 (x [k] * scale[0], y [k] * scale[1], z [k] * scale[2]);
                        if (MPII_parts[k]!=null){
                            MPII_parts[k].transform.position=vk;
                        }
			Debug.Log(vk);
		}
	}

	// Use this for initialization
	void Start () {
		J = JSON.Parse (jpos);
                //Debug.Log(J[0]);
                J=J[0];
                //Debug.Log(J[0]);
                J=J[0];
                Debug.Log(J[0]);
		BuildPosition(J);
	}
		

	void OnDrawGizmos(){

/*

    _LIMBS = np.array([ [0, 1], [2, 3], [3, 4], [5, 6], 6, 7, 8, 9,
                       9, 10, 11, 12, 12, 13]).reshape((-1, 2))
        for lid, (p0, p1) in enumerate(_LIMBS):
            if not (visible[oid][p0] and visible[oid][p1]):
                continue
            y0, x0 = pose_2d[oid][p0]
            y1, x1 = pose_2d[oid][p1]
            cv2.circle(image, (x0, y0), JOINT_DRAW_SIZE *_NORMALISATION_FACTOR +1, _COLORS[lid], -1)
            cv2.circle(image, (x1, y1), JOINT_DRAW_SIZE*_NORMALISATION_FACTOR +1, _COLORS[lid], -1)
            cv2.line(image, (x0, y0), (x1, y1),_COLORS[lid], LIMB_DRAW_SIZE*_NORMALISATION_FACTOR +1, 16)
*/
		try{
                        Gizmos.color =MPII_colors[MPII_limbs[0]];
			Gizmos.DrawLine (MPII_parts[0].transform.position, MPII_parts [1].transform.position);
			Gizmos.DrawLine (MPII_parts [1].transform.position, MPII_parts [2].transform.position);
			Gizmos.DrawLine (MPII_parts [2].transform.position, MPII_parts [3].transform.position);
			Gizmos.DrawLine (MPII_parts [0].transform.position, MPII_parts [4].transform.position);
			Gizmos.DrawLine (MPII_parts [4].transform.position, MPII_parts [5].transform.position);
			Gizmos.DrawLine (MPII_parts [5].transform.position, MPII_parts [6].transform.position);
			Gizmos.DrawLine (MPII_parts [0].transform.position, MPII_parts [7].transform.position);
			Gizmos.DrawLine (MPII_parts [7].transform.position, MPII_parts [8].transform.position);
			Gizmos.DrawLine (MPII_parts [8].transform.position, MPII_parts [9].transform.position);
			Gizmos.DrawLine (MPII_parts [9].transform.position, MPII_parts [10].transform.position);
			Gizmos.DrawLine (MPII_parts [8].transform.position, MPII_parts [11].transform.position);
			Gizmos.DrawLine (MPII_parts [11].transform.position, MPII_parts [12].transform.position);
			Gizmos.DrawLine (MPII_parts [12].transform.position, MPII_parts[13].transform.position);
			Gizmos.DrawLine (MPII_parts [8].transform.position, MPII_parts [14].transform.position);
			Gizmos.DrawLine (MPII_parts [14].transform.position, MPII_parts [15].transform.position);
			Gizmos.DrawLine (MPII_parts [15].transform.position, MPII_parts [16].transform.position);
/*
                        Gizmos.color =MPII_colors[MPII_limbs[0]];
			Gizmos.DrawLine (MPII_parts[0].transform.position, MPII_parts[1].transform.position);
                        //Gizmos.color =MPII_colors[MPII_limbs[1]];
			Gizmos.DrawLine (MPII_parts[2].transform.position, MPII_parts[3].transform.position);
                        //Gizmos.color =MPII_colors[MPII_limbs[2]];
			Gizmos.DrawLine (MPII_parts[3].transform.position, MPII_parts[4].transform.position);
                        //Gizmos.color =MPII_colors[MPII_limbs[3]];
			Gizmos.DrawLine (MPII_parts[5].transform.position, MPII_parts[6].transform.position);
                        //Gizmos.color =MPII_colors[MPII_limbs[4]];
			Gizmos.DrawLine (MPII_parts[6].transform.position, MPII_parts[7].transform.position);
                        //Gizmos.color =MPII_colors[MPII_limbs[5]];
			Gizmos.DrawLine (MPII_parts[8].transform.position, MPII_parts[9].transform.position);
                        //Gizmos.color =MPII_colors[MPII_limbs[6]];
			Gizmos.DrawLine (MPII_parts[9].transform.position, MPII_parts[10].transform.position);
                        //Gizmos.color =MPII_colors[MPII_limbs[7]];
			Gizmos.DrawLine (MPII_parts[11].transform.position, MPII_parts[12].transform.position);
                        //Gizmos.color =MPII_colors[MPII_limbs[8]];
			Gizmos.DrawLine (MPII_parts[12].transform.position, MPII_parts[13].transform.position);
  */                      
		}
		catch (Exception e)
		{
			Debug.LogException(e, this);
		}
	}
}
