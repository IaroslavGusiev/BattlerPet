using System;
using System.Runtime.CompilerServices;
using LitMotion;
using UnityEngine.UI;

namespace CodeBase.Extensions
{
    public static class LitMotionExtensions
    {
        public static void CancelIfValid(this MotionHandle handle)
        {
            if (handle.IsActive())
                handle.Cancel();
        }
        
        public static MotionHandle BindToSlider<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Slider slider)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            IsNull(slider);
            return builder.BindWithState(slider, static (x, target) => { target.value= x; });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void IsNull<T>(T target)
        {
            if (target == null) 
                throw new ArgumentNullException(nameof(target));
        }
    }
}