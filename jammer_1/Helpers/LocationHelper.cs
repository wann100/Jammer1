using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using System.Diagnostics;

namespace Jammer_1.Helpers
{
    class LocationHelper
    {
        Position position;
        public Position Myposition{ get { return position; } set { position = value; } }
        public LocationHelper()
        {
            GetCurrentLocation();
        }
        public async void GetCurrentLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;


                if (locator.IsGeolocationAvailable || locator.IsGeolocationEnabled)
                {
                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                }

                

            }
            catch (Exception ex)
            {
                //Display error as we have timed out or can't get location.
            }
        }



    }
}
