using System;
using System.Collections.Generic;

namespace TuVotoCuenta.MasterDetail
{
    public class Group : List<MasterPageItem>
    {
        public String Name { get; private set; }

        public Group()
        {
        }

        public Group(String Name)
        {
            this.Name = Name;
        }

        public static List<Group> ConvertListInGroups(List<MasterPageItem> list)
        {
            List<Group> result = new List<Group>();

            List<string> distinct = new List<string>();
            for (int i = 0; i <= list.Count - 1; i++)
            {
                if (!distinct.Contains(list[i].Title))
                    distinct.Add(list[i].Title);
            }

            for (int i = 0; i <= distinct.Count - 1; i++)
            {
                Group group = new Group();
                group.Name = distinct[i];

                List<MasterPageItem> list_distinct = list.FindAll(x => x.Title == distinct[i]);
                list_distinct.Sort();

                for (int j = 0; j <= list_distinct.Count - 1; j++)
                    group.Add(list_distinct[j]);

                result.Add(group);
            }
            return result;
        }

        public static List<MasterPageItem> ConvertGroupsInList(List<Group> groups)
        {
            List<MasterPageItem> result = new List<MasterPageItem>();

            for (int i = 0; i <= groups.Count - 1; i++)
            {
                for (int j = 0; j <= groups[i].Count - 1; j++)
                    result.Add(groups[i][j]);
            }

            result.Sort();

            return result;
        }
    }
}