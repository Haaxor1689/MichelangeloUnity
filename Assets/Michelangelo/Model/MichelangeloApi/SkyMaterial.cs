using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
    [MessagePackObject(true)]
    public class SkyMaterial {
        public bool ActiveSky = true;
        public bool ActiveSun = true;
        public double Albedo = 0.15;
        public int Day = 06;
        public int Hour = 15;
        public double Latitude = 48.1486;
        public double Longitude = 17.1077;
        public int Minute = 00;
        public int Month = 05;
        public double? Scale = null;
        public int Second = 00;
        public double? SkyScale = null;
        public double[] SunDirection = null;
        public double? SunScale = null;
        public int Timezone = 1;
        public double Turbidity = 0.3;
        public int Year = 2016;
    }
}
