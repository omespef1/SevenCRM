using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCentralizacion.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class ActionResult<T> where T : class
    {
        public ActionResult() { }

        public ActionResult(bool state, T objresult, string message)
        {
            State = state;
            ObjResult = objresult;
            Message = message;
        }
        [DataMember]
        public bool State { get; set; }
        [DataMember]
        public T ObjResult { get; set; }
        [DataMember]
        public string Message { get; set; }
    }

    [Serializable, DataContract(IsReference = true)]
    public class ActionResult
    {
        public ActionResult() { }

        public ActionResult(bool state, string message)
        {
            State = state;
            Message = message;
        }
        [DataMember]
        public bool State { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}