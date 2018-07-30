using System;
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
        public Vector3 scale=new Vector3(0.01f,0.01f,0.01f);
        //public Transform attach;
	[TextArea(4, 6)]
	public string jpos = "";
        // Avatar attributes
        public GameObject Avatar;
        [NamedArrayAttribute (new string[]{ "hips", 
                                            "thigh_R","shin_R","foot.R",
                                            "thigh.L", "shin.L","foot.L",
                                            "spine","chest","chest1",
                                            "neck","jaw","head",
                                            "upper_arm_L","forearm_L","handL",
                                            "upper_arm_R","forearm_R","handR"
                                            })]
        public GameObject[] Avatar_bones;
        private Vector3[]   Avatar_orientation_vectors=new Vector3[3];
        // MPII attributes
        private static Color[]    MPII_colors= {
                    new Color(0, 0, 255), new Color(0, 170, 255),new Color(0, 255, 170),new Color(0, 255, 0),
                    new Color(170, 255, 0), new Color(255, 170, 0),new Color(255, 0, 0),new Color(255, 0, 170),
                    new Color(170, 0, 255)
                    };
        private static int[]      MPII_limbs = { 0, 1, 2, 3, 3, 4, 5, 6, 6, 7, 8, 9, 9, 10, 11, 12, 12, 13 };
        [NamedArrayAttribute (new string[]{ "hips", 
                                            "thigh_R","shin_R","foot.R",
                                            "thigh.L", "shin.L","foot.L",
                                            "spine","neck","jaw","head",
                                            "upper_arm_L","forearm_L","handL",
                                            "upper_arm_R","forearm_R","handR"
                                            })]
        public GameObject[] MPII_parts;

        private Vector3[]   MPII_orientation_vectors=new Vector3[3];

	void BuildPosition(JSONNode J){
		JSONNode x = J [0];
		JSONNode z = J [1];
		JSONNode y = J [2];

                // Avatar
                // the Y=up direction is given by spine-hips
                Avatar_orientation_vectors[1]=(Avatar_bones [7].transform.position-Avatar_bones [0].transform.position);
                // the X=right direction would be given by ...thigh_R - thigh_L
                Avatar_orientation_vectors[0]=(Avatar_bones [4].transform.position-Avatar_bones [1].transform.position);
                // the Z=forward direction is given by cross product
                Avatar_orientation_vectors[2]=Vector3.Cross(Avatar_orientation_vectors[0],Avatar_orientation_vectors[1]);

                Quaternion   Avatar_Rotation=Quaternion.LookRotation(Avatar_orientation_vectors[2], Avatar_orientation_vectors[1]);

		// MPII 
                Vector3[]      MPII_pos =new Vector3[17];
                // compute x,y,z coordinates relative to hips
                Vector3  hips=new Vector3 (x [0] , y [0] , z [0] );
		for (int k = 0; k < 17; k++) {
			MPII_pos[k] = new Vector3 ((x [k]-hips.x) * scale[0], 
                                                   (y [k]-hips.y) * scale[1], 
                                                   (z [k]-hips.z) * scale[2]);
		}

                // the Y=up direction is given by spine-hips
                MPII_orientation_vectors[1]=(MPII_pos [7]-MPII_pos [0]);
                // the X=right direction would be given by ...thigh_R - thigh_L
                MPII_orientation_vectors[0]=(MPII_pos [1]-MPII_pos [4]);
                // the Z=forward direction is given by cross product
                MPII_orientation_vectors[2]=Vector3.Cross(MPII_orientation_vectors[0],MPII_orientation_vectors[1]);
                Quaternion   MPII_Rotation=Quaternion.LookRotation(MPII_orientation_vectors[2], MPII_orientation_vectors[1]);

                // rotate Avatar to match MPII orientation
                Quaternion relative =  Avatar_Rotation*MPII_Rotation ;
Avatar.transform.rotation=relative;
                //transform.Rotate(Vector3.up * Time.deltaTime, attach.position);
                //Quaternion Q=LookRotation(Vector3 forward, Vector3 upwards = Vector3.up);
		for (int k = 0; k < 17; k++) {
                        if (MPII_parts[k]!=null){
                            MPII_parts[k].transform.position=MPII_pos[k]+Avatar_bones[0].transform.position;
                        }
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

		try{
                        Gizmos.color =Color.yellow;
			Gizmos.DrawSphere(MPII_parts[0].transform.position, 0.01f);
                        Gizmos.color =MPII_colors[MPII_limbs[1]];
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
                        Gizmos.color =Color.green;
                        Gizmos.DrawLine (MPII_parts [0].transform.position, MPII_parts [0].transform.position+MPII_orientation_vectors[1]);
                        Gizmos.color =Color.red;
                        Gizmos.DrawLine (MPII_parts [0].transform.position, MPII_parts [0].transform.position+MPII_orientation_vectors[0]);
                        Gizmos.color =Color.yellow;
                        Gizmos.DrawLine (MPII_parts [0].transform.position, MPII_parts [0].transform.position+MPII_orientation_vectors[2]);
*/
                        Gizmos.color =Color.green;
                        Gizmos.DrawLine (Avatar_bones [0].transform.position, Avatar_bones [0].transform.position+Avatar_orientation_vectors[1]);
                        Gizmos.color =Color.red;
                        Gizmos.DrawLine (Avatar_bones [0].transform.position, Avatar_bones [0].transform.position+Avatar_orientation_vectors[0]);
                        Gizmos.color =Color.yellow;
                        Gizmos.DrawLine (Avatar_bones [0].transform.position, Avatar_bones [0].transform.position+Avatar_orientation_vectors[2]);
		}
		catch (Exception e)
		{
			Debug.LogException(e, this);
		}
	}
}
