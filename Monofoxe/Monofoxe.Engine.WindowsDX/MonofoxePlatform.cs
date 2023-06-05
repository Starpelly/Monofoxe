﻿namespace Monofoxe.Engine.WindowsDX
{
	public static class MonofoxePlatform
	{
		public static void Init(bool setCurrentPlatform = true)
		{
			if (setCurrentPlatform)
			{
				GameMgr.CurrentPlatform = Platform.Windows;
			}
			GameMgr.CurrentGraphicsBackend = GraphicsBackend.DirectX;

			StuffResolver.AddStuffAs<IAlphaBlendEffectLoader>(new AlphaBlendEffectLoaderWindowsDX());
			StuffResolver.AddStuffAs<ITextInputBinder>(new TextInputBinderWindowsDX());
		}
	}
}
