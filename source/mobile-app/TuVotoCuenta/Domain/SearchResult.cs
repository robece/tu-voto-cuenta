using System;
using TuVotoCuenta.Classes;

namespace TuVotoCuenta.Domain
{
    public class SearchResult : ObservableObject
    {
        private int upVotes;

        public int UpVotes
        {
            get => upVotes;
            set => SetProperty(ref upVotes, value);
        }


        private int downVotes;

        public int DownVotes
        {
            get => downVotes;
            set => SetProperty(ref downVotes, value);
        }

        private Uri photoUrl;

        public Uri PhotoUrl
        {
            get => photoUrl;
            set => SetProperty(ref photoUrl, value);
        }

        private RecordItem recordItem;

        public RecordItem RecordItem
        {
            get => recordItem;
            set => SetProperty(ref recordItem, value);
        }

    }
}
