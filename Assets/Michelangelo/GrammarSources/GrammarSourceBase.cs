using System;
using UnityEngine;

namespace Michelangelo.GrammarSources {
    public class GrammarSourceBase : MonoBehaviour {
        protected object Position(params object[] args) {
            throw new NotImplementedException();
        }
        protected object Size(params object[] args) {
            throw new NotImplementedException();
        }
    }
}
