using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DmhySourceParser
{
    [DataContract]
    public class RawResource
    {
        public ObjectId Id { get; set; }
        
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public string Link { get; set; }
        
        [DataMember]
        public DateTime PublishDate { get; set; }

        public override string ToString()
        {
            return "Title: " + Title + ", Link: " + Link + "PublishDate: " + PublishDate.ToString();
        }
    }
}
