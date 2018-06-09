using System;
using TuVotoCuenta.Enums;

namespace TuVotoCuenta.Domain
{
    public abstract class HttpResponseBase
    {
        public ResponseStatus Status
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

    }
}
