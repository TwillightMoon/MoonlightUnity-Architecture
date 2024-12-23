using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PaleLuna.Attributes
{
    [CustomPropertyDrawer(typeof(RequireInterface))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        private Type _reqType;
        private RequireInterface _reqIAttribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _reqIAttribute = attribute as RequireInterface;
            _reqType = _reqIAttribute.RequireType;
            
            if (!IsFieldMonoBehaviour)
            {
                DrawError(position);
                return;
            }
            
            CheckDragAndDrop(position, _reqType);

            CheckValues(property, _reqType);

            DrawObjectField(property, label, position, _reqIAttribute.allowSceneObject);
        }

        private void CheckValues(SerializedProperty property, Type reqType)
        {
            if (!property.objectReferenceValue) return;
            if (IsValidObject(property.objectReferenceValue, reqType)) return;

            property.objectReferenceValue = null;
        }

        private void DrawObjectField(SerializedProperty property,
            GUIContent label,
            Rect position,
            bool allowSceneObject)
        {
            property.objectReferenceValue = EditorGUI.ObjectField(
                position,
                label,
                property.objectReferenceValue,
                typeof(MonoBehaviour),
                allowSceneObject);
        }

        private void CheckDragAndDrop(Rect position, Type reqType)
        {
            if (!position.Contains(Event.current.mousePosition)) return;

            foreach (Object objectReference in DragAndDrop.objectReferences)
            {
                if (IsValidObject(objectReference, reqType)) continue;

                DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                break;
            }
        }

        private bool IsValidObject(Object objectReference, Type reqType)
        {
            bool result = false;

            MonoBehaviour mono = objectReference as MonoBehaviour;

            if (mono)
                result = mono.GetComponent(reqType) != null;

            return result;
        }

        private void DrawError(Rect position)
        {
            EditorGUI.HelpBox(
                position,
                "RequireInterface works only with MonoBehaviour references",
                MessageType.Error);
        }

        private bool IsFieldMonoBehaviour =>
            fieldInfo.FieldType == typeof(MonoBehaviour) ||
            typeof(IEnumerable<MonoBehaviour>).IsAssignableFrom(fieldInfo.FieldType);
    }
}