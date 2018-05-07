using System;

namespace TuVotoCuenta.MasterDetail
{
    public class MasterPageItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }

        public object[] TargetTypeParameters { get; set; }
    }
}