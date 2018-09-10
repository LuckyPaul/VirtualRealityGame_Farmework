using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUIFW {
    class SingletonException :Exception{
        public SingletonException(string msg) :base(msg){
        }
    }
}
