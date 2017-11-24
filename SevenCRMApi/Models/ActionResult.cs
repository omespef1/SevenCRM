using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
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
    public class ActionResult<T, T1> where T : class
    {
        public ActionResult() { }

        public ActionResult(bool state, T objresult, T1 objresultAux, string message)
        {
            State = state;
            ObjResult = objresult;
            Message = message;
            ObjResultAux = objresultAux;
        }
        [DataMember]
        public bool State { get; set; }
        [DataMember]
        public T ObjResult { get; set; }
        [DataMember]
        public T1 ObjResultAux { get; set; }
        [DataMember]
        public string Message { get; set; }
    }

    [Serializable, DataContract(IsReference = true)]
    public class ActionResult<T, T1, T2> where T : class
    {
        public ActionResult() { }

        public ActionResult(bool state, T objresult, T1 objresultAux, T2 objresultAux2, string message)
        {
            State = state;
            ObjResult = objresult;
            Message = message;
            ObjResultAux = objresultAux;
            ObjResultAux2 = objresultAux2;
        }
        [DataMember]
        public bool State { get; set; }
        [DataMember]
        public T ObjResult { get; set; }
        [DataMember]
        public T1 ObjResultAux { get; set; }
        [DataMember]
        public T2 ObjResultAux2 { get; set; }
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