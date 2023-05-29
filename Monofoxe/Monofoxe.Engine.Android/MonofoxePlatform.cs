using System;

namespace Monofoxe.Engine.Android
{
	public static class MonofoxePlatform
	{
		public static void Init()
		{
			GameMgr.CurrentPlatform = Platform.Android;

			GameMgr.CurrentGraphicsBackend = GraphicsBackend.OpenGL;

			StuffResolver.AddStuffAs<IAlphaBlendEffectLoader>(new AlphaBlendEffectLoaderAndroid());
			StuffResolver.AddStuffAs<ITextInputBinder>(new TextInputBInderAndroid());
		}
	}
}
