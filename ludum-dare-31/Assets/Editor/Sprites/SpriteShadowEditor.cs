using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpriteShadow))]
[CanEditMultipleObjects]
public class SpriteShadowEditor : Editor 
{
    private SpriteShadow spriteShadow;
    
    private SerializedProperty sizeProperty;
    
    private SerializedProperty tintProperty;
    
    private SerializedProperty angleProperty;
    
    private SerializedProperty offsetProperty;
    
    private SerializedProperty flipBetweenAnglesProperty;
    
    private SerializedProperty minFlippedProperty;
    
    private SerializedProperty maxFlippedProperty;
    
    private SerializedProperty useParentsSpriteProperty;
    
    private SerializedProperty replacementSpriteProperty;
    
    private void OnEnable()
    {
        sizeProperty = serializedObject.FindProperty("size");
        tintProperty = serializedObject.FindProperty("tint");
        angleProperty = serializedObject.FindProperty("angle");
        offsetProperty = serializedObject.FindProperty("offset");
        flipBetweenAnglesProperty = serializedObject.FindProperty("flipBetweenAngles");
        minFlippedProperty = serializedObject.FindProperty("minFlipped");
        maxFlippedProperty = serializedObject.FindProperty("maxFlipped");
        useParentsSpriteProperty = serializedObject.FindProperty("useParentsSprite");
        replacementSpriteProperty = serializedObject.FindProperty("replacementSprite");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        DrawFields();
        
        serializedObject.ApplyModifiedProperties();
    }
    
    private void DrawFields()
    {
        EditorGUILayout.PropertyField(angleProperty, new GUIContent("Angle"));
        
        EditorGUILayout.PropertyField(tintProperty, new GUIContent("Tint"));
        
        EditorGUILayout.PropertyField(sizeProperty, new GUIContent("Size"));
        
        EditorGUILayout.PropertyField(offsetProperty, new GUIContent("Offset"));
        
        DrawFlipBetweenAngles();
        
        EditorGUILayout.PropertyField(useParentsSpriteProperty, new GUIContent("Use Parent's Sprite"));
        
        if (!useParentsSpriteProperty.boolValue)
        {
            EditorGUILayout.PropertyField(replacementSpriteProperty, new GUIContent("Sprite"));
        }
    }
    
    private void DrawFlipBetweenAngles()
    {
        EditorGUILayout.PropertyField(flipBetweenAnglesProperty, new GUIContent("Flip Between Angles"));
        
        if (flipBetweenAnglesProperty.boolValue)
        {
            float minFlippedRef = minFlippedProperty.floatValue;
            float maxFlippedRef = maxFlippedProperty.floatValue;
            
            EditorGUILayout.MinMaxSlider(new GUIContent("Flip Between"), ref minFlippedRef, ref maxFlippedRef, 0, 359f, null);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(" ");
            EditorGUILayout.LabelField(minFlippedRef + " - " + maxFlippedRef + " Degrees");
            EditorGUILayout.EndHorizontal();
            
            minFlippedProperty.floatValue = Mathf.Floor(minFlippedRef);
            maxFlippedProperty.floatValue = Mathf.Ceil(maxFlippedRef);
        }
    }
}