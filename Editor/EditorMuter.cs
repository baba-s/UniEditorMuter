using System.Reflection;
using UnityEditor;
using UnityEngine.Events;

namespace UniEditorMuter
{
	internal static class EditorApplicationUtility
	{
		private const BindingFlags BINDING_ATTR =
			BindingFlags.Static |
			BindingFlags.Instance |
			BindingFlags.NonPublic;

		private static readonly FieldInfo m_info = typeof( EditorApplication )
			.GetField( "projectWasLoaded", BINDING_ATTR );

		public static UnityAction projectWasLoaded
		{
			get => m_info.GetValue( null ) as UnityAction;
			set
			{
				var functions = m_info.GetValue( null ) as UnityAction;
				functions += value;
				m_info.SetValue( null, functions );
			}
		}
	}

	[InitializeOnLoad]
	internal static class MuteWhenLoadedProject
	{
		static MuteWhenLoadedProject()
		{
			EditorApplicationUtility.projectWasLoaded += () => EditorUtility.audioMasterMute = true;
		}
	}
}