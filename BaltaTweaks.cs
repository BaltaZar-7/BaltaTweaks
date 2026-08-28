#nullable disable
using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;

namespace BaltaTweaks
{
	public class Main : MelonMod
	{
		public override void OnInitializeMelon()
		{           
			MelonLogger.Msg("[BaltaTweaks] BaltaTweaks initialized");
            BaltaTweaksSettings.OnLoad();
        }	
    }
}