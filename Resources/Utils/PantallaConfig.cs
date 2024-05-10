using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace UltimateMatch.Resources.Utils
{
    public class PantallaConfig
    {
        public static async Task SetDimensionesPantallaAsync(int width, int height)
        {
            await Device.InvokeOnMainThreadAsync(() =>
            {
                App.Current.MainPage.WidthRequest = width;
                App.Current.MainPage.HeightRequest = height;
            });
        }
    }
}
