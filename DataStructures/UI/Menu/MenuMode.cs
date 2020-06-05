﻿namespace AssortedModdingTools.DataStructures.UI.Menu
{
	public enum MenuMode
	{
		None = -1,
		Main = 0,
		LanguageSelection = 1,
		//2-9
		WorldLoading = 10,
		Settings = 11,
		//12-24
		CursorColorSettings = 25,
		VolumeSettings = 26,
		//27
		ParallaxSetting = 28,
		//29-110
		ResolutionSetting = 111,
		GeneralSettings = 112,
		//113-251
		CursorBorderColorSettings = 252,
		//253-1110
		VideoSettings = 1111,
		InterfaceSettings = 1112,
		//1113-1124
		CursorSettings = 1125,
		//1126
		ControlsSettings = 1127,
		//1128-1212
		LanguageSettings = 1213,
		//1214-2007
		VideoEffectsSettings = 2008,

		//tModLoader
		Mods = 10000,
		ModSources = 10001,
		LoadingMods = 10002,
		BuildingMods = 10003,
		ErrorMessage = 10005,
		ReloadingMods = 10006,
		ModBrowser = 10007,
		ModInfo = 10008,
		DownloadMod = 10009,
		ModControls = 10010,
		ManagePublished = 10011,
		UpdateMessage = 10012,
		InfoMessage = 10013,
		EnterPassphraseMenu = 10015,
		ModPacks = 10016,
		TModLoaderSettings = 10017,
		EnterSteamIDMenu = 10018,
		ExtractMod = 10019,
		ModDownloadProgress = 10020,
		//10021
		DeveloperModeHelp = 10022,
		Progress = 10023,
		ModConfig = 10024,
		CreateMod = 10025,
		Exit = 10026,

		//This Mod
		ModdingTools = 20000,
		AdvancedCreateMod = 20001
	}
}