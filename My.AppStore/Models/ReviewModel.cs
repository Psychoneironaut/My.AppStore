using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My.AppStore.Models
{
    public class ReviewModel
    {
        public int? ID { get; set; }

        public string UserEmail { get; set; }

        public int? Rating { get; set; }

        public string Body { get; set; }

        public DateTime DateModified { get; set; }
    }
}