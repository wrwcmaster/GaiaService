using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmhySourceParser
{
    class RawResourceProvider
    {
        public static MongoCollection<RawResource> Collection
        {
            get {
                 return Database.Instance.GetCollection<RawResource>("resource");
            }
        }

        public static void SaveResource(RawResource res)
        {
            if (res == null) return;
            if (Collection.Count(Query<RawResource>.EQ(r => r.Link, res.Link)) == 0)
            {
                Collection.Insert(res);
            }
        }

        public static List<RawResource> FindResource(string keyword)
        {
            return Collection.Find(Query<RawResource>.Matches(r => r.Title, "/" + keyword + "/")).ToList();
        }

        public static DateTime GetLatestPublishTime()
        {
            RawResource res = Collection.FindAll().SetSortOrder(SortBy.Descending("PublishDate")).SetLimit(1).FirstOrDefault();
            return (res != null) ? res.PublishDate : DateTime.MinValue;
        }

        public static void SyncDmhyResource()
        {
            DateTime lastPublishDate = RawResourceProvider.GetLatestPublishTime();
            int i = 1;
            while (true)
            {
                Console.WriteLine("Syncing page " + i);
                List<RawResource> list = DmhyParser.Parse(i);
                if (list.Count == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Retrieved " + list.Count + " items - " + list[0].PublishDate.ToString());
                    if (!SaveResourceList(list, lastPublishDate)) break;
                    i++;
                }
            }
            Console.WriteLine("Sync Completed.");
        }

        private static bool SaveResourceList(List<RawResource> list, DateTime lastPublishDate)
        {
            foreach (RawResource res in list)
            {
                if (res.PublishDate > lastPublishDate)
                {
                    RawResourceProvider.SaveResource(res);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
