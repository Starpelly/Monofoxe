using System.Reflection;

namespace Monofoxe.Engine.Android
{
	public class AlphaBlendEffectLoaderAndroid: AlphaBlendEffectLoader
	{
		protected override string _effectName => "Monofoxe.Engine.Android.AlphaBlend_gl.mgfxo";

		protected override Assembly _assembly => Assembly.GetAssembly(typeof(AlphaBlendEffectLoaderAndroid));

	}
}
