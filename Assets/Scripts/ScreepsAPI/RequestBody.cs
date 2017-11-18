using System;

namespace Screeps3D {
    
    public class RequestBody : JSONObject {
    
        public string ToQueryString() {
            var count = 0;
            var str = "?";
            foreach (var key in keys) {
                str += string.Format("{0}={1}", key, this[key].str);
                count++;
                if (count < this.Count) {
                    str += "&";
                }
            }
            return str;
        }
    }
}