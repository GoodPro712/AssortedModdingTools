﻿using AssortedModdingTools.Helpers;
using AssortedModdingTools.Systems.Reflection;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.Localization;

namespace AssortedModdingTools.Systems.Menu
{
	//TODO display somewhere amount of mods loaded
	public partial class MenuSystem : SystemBase
	{
		private static int previousMenuMode;

		public override void Load()
		{
			On_AddMenuButtons += Interface_AddMenuButtons;
			On.Terraria.Main.DrawMenu += Main_DrawMenu;
			IL.Terraria.Main.DrawMenu += MoveLogoLower;
		}

		private void Main_DrawMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
		{
			HookPreDrawMenu();
			orig(self, gameTime);
			//DrawMenu(gameTime);
			HookPostDrawMenu();
		}

		public override void OnUpdate()
		{
			if (previousMenuMode != Main.menuMode)
				HookOnMenuModeChange(previousMenuMode);

			previousMenuMode = Main.menuMode;
		}

		public override void Unload()
		{
			On_AddMenuButtons -= Interface_AddMenuButtons;
			previousMenuMode = -1;
			createMod = null;
		}

		//Remove when rewritting menu
		private static void Interface_AddMenuButtons(Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons)
		{
			MenuHelper.AddButton(Language.GetTextValue("tModLoader.MenuMods"), MenuMode.Mods, selectedMenu, buttonNames, ref buttonIndex, ref numButtons); //Mods

			if (ModCompileHelper.DeveloperMode)
			{
				MenuHelper.AddButton(Language.GetTextValue("tModLoader.MenuModSources"), delegate
				{
					bool ret = (bool)ReflectionSystem.DeveloperModeReady.Invoke(null, new object[1]);
					Main.menuMode = ret ? (int)MenuMode.ModSources : (int)MenuMode.DeveloperModeHelp;
				}, selectedMenu, buttonNames, ref buttonIndex, ref numButtons); //Mod Sources

				MenuHelper.AddButton("Modding Tools", MenuMode.ModdingTools, selectedMenu, buttonNames, ref buttonIndex, ref numButtons); //Modding Tools
			}

			MenuHelper.AddButton(Language.GetTextValue("tModLoader.MenuModBrowser"), MenuMode.ModBrowser, selectedMenu, buttonNames, ref buttonIndex, ref numButtons); //Mod Browser

			offY = 220; //Y offset with all buttons. Higher lowers the buttons on screen

			for (int i = 0; i < numButtons; i++)
				buttonScales[i] = 0.82f; //Button scale for all buttons

			spacing = 45; //The spacing between each button. Don't touch
		}

		//Remove when rewriting menu
		private static void MoveLogoLower(ILContext il)
		{
			ILCursor c = new ILCursor(il);

			// else if (this.logoScaleSpeed > -50f & this.logoScaleDirection == -1f)
			//IL_02cc: ldarg.0
			//IL_02cd: ldfld float32 Terraria.Main::logoScaleSpeed
			//IL_02d2: ldc.r4 - 50
			//IL_02d7: cgt
			//IL_02d9: ldarg.0
			//IL_02da: ldfld float32 Terraria.Main::logoScaleDirection
			//IL_02df: ldc.r4 - 1
			//IL_02e4: ceq
			//IL_02e6: and

			/*
			FieldInfo logoScaleSpeed = null;
			FieldInfo logoScaleDirection = null;

			try
			{
				logoScaleSpeed = typeof(Main).GetField("logoScaleSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
				logoScaleDirection = typeof(Main).GetField("logoScaleDirection", BindingFlags.NonPublic | BindingFlags.Instance);
			}
			catch (ReflectionTypeLoadException e) { }

			if (c.TryGotoNext(MoveType.After,
				e => e.MatchLdarg(0),
				e => e.MatchLdfld(logoScaleSpeed),
				e => e.MatchLdcR4(-50f),
				e => e.MatchCgt(),
				e => e.MatchLdarg(0),
				e => e.MatchLdfld(logoScaleDirection),
				e => e.MatchLdcR4(-1f),
				e => e.MatchCeq(),
				e => e.MatchAnd()))
			{
				c.Index -= 6;

				c.Emit(OpCodes.Pop);
				c.Emit(OpCodes.Ldc_R4, -20f);
			}*/

			c.Goto(0);

			for (int i = 0; i < 2; i++)
			{
				if (c.TryGotoNext(MoveType.After, e => e.MatchLdcR4(110f)))
				{
					c.Emit(OpCodes.Pop);
					//c.Remove();
					c.Emit(OpCodes.Ldc_R4, 125f);
				}
			}
		}
	}
}