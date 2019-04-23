using System.Collections.Generic;
using MessagePack;

namespace Michelangelo.Model.MichelangeloApi {
    [MessagePackObject(true)]
    public class SceneEnvironment {
        public double Ambient = 0.5;
        public uint AnimFps = 25;
        public uint AnimStart = 0;
        public uint AnimStop = 0;
        public double AssemblyAnimationMinVolume = -1.0;
        public int AssemblyLength = 30;
        public bool Bidirectional = false;
        public int Bounces = 3;
        public int GrammarsCount = 0;
        public bool HideEmitters = false;
        public bool IgnoreCams = true;
        public bool IgnoreDeforms = false;
        public bool LowRes = false;
        public uint MaxOtherCams = 0;
        public bool PathTrace = false;
        public int RR = 5;
        public int RulesCount = 0;
        public int SamplesPower = 3;
        public SkyMaterial Sky = null;
        public Dictionary<string, long> Statistics;
        public ulong StepsCount = 0;
        public int TimeLimit = int.MaxValue;
        public bool UseBlender = false;
        public bool UseRenderman = true;
    }
}
