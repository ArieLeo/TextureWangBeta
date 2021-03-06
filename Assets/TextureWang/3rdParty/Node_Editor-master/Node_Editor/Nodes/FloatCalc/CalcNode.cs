﻿#if REPLACED
using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node (false, "Float/Calculation")]
public class CalcNode : Node 
{
	public enum CalcType { Add, Substract, Multiply, Divide }
	public CalcType type = CalcType.Add;

	public const string ID = "calcNode";
	public override string GetID { get { return ID; } }

//	public float Input1Val = 1f;
//	public float Input2Val = 1f;

    public FloatRemap m_Value1;
    public FloatRemap m_Value2;

    public override Node Create (Vector2 pos) 
	{
		CalcNode node = CreateInstance <CalcNode> ();
		
		node.name = "Calc Node";
		node.rect = new Rect (pos.x, pos.y, 200, 100);
        node.m_Value1=new FloatRemap(0,-1,1);
        node.m_Value2 = new FloatRemap(0, -1, 1);


        node.CreateOutput ("Output 1", "Float");

		return node;
	}
    protected internal override void CopyScriptableObjects(System.Func<ScriptableObject, ScriptableObject> replaceSerializableObject)
    {
        TextureNode.ConnectRemapFloats(this, replaceSerializableObject);
    }

    protected internal override void NodeGUI () 
	{
		GUILayout.BeginHorizontal ();
		GUILayout.BeginVertical ();
        GUILayout.Label("Val1:"+(float)m_Value1+" Val2:"+(float)m_Value2);
        //		if (Inputs [0].connection != null)
        //			GUILayout.Label (Inputs [0].name);
        //		else
        //			Input1Val = RTEditorGUI.FloatField (GUIContent.none, Input1Val);
        //		InputKnob (0);
        // --
        //		if (Inputs [1].connection != null)
        //			GUILayout.Label (Inputs [1].name);
        //		else
        //			Input2Val = RTEditorGUI.FloatField (GUIContent.none, Input2Val);
        //		InputKnob (1);

        GUILayout.EndVertical ();
		GUILayout.BeginVertical ();

		Outputs [0].DisplayLayout ();

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();

#if UNITY_EDITOR
		type = (CalcType)UnityEditor.EditorGUILayout.EnumPopup (new GUIContent ("Calculation Type", "The type of calculation performed on Input 1 and Input 2"), type);
#else
		GUILayout.Label (new GUIContent ("Calculation Type: " + type.ToString (), "The type of calculation performed on Input 1 and Input 2"));
#endif

		if (GUI.changed)
			NodeEditor.RecalculateFrom (this);
	}


    public override void DrawNodePropertyEditor()
    {
#if UNITY_EDITOR
        type = (CalcType)UnityEditor.EditorGUILayout.EnumPopup(new GUIContent("Calculation Type", "The type of calculation performed on Input 1 and Input 2"), type);
#else
		GUILayout.Label (new GUIContent ("Calculation Type: " + type.ToString (), "The type of calculation performed on Input 1 and Input 2"));
#endif

        m_Value1.SliderLabel(this, "Value1");
        m_Value2.SliderLabel(this, "Value2");

    }
    public override bool Calculate () 
	{
/*
		if (Inputs[0].connection != null)
			Input1Val = Inputs[0].connection.GetValue<float> ();
		if (Inputs[1].connection != null)
			Input2Val = Inputs[1].connection.GetValue<float> ();
*/
		switch (type) 
		{
		case CalcType.Add:
			Outputs[0].SetValue<float> (m_Value1 + m_Value2);
			break;
		case CalcType.Substract:
			Outputs[0].SetValue<float> (m_Value1 - m_Value2);
			break;
		case CalcType.Multiply:
			Outputs[0].SetValue<float> (m_Value1 * m_Value2);
			break;
		case CalcType.Divide:
			Outputs[0].SetValue<float> (m_Value1 / m_Value2);
			break;
		}

		return true;
	}
}
#endif