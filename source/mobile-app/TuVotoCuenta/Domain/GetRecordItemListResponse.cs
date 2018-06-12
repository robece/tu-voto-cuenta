using System;
using System.Collections.ObjectModel;

namespace TuVotoCuenta.Domain
{
    public class GetRecordItemListResponse : HttpResponseBase
    {
        public ObservableCollection<RecordItem> Records { get; set; }
    }
}
