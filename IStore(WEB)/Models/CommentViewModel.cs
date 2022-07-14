using Domain.EF_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IStore_WEB_.Models
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            Answers = new List<Comment>();
            LikesTotal = "0";
            DislikesTotal = "0";
        }
        public string Id { get; set; }
        public string CurrentUserId { get; set; }
        public string DateShort { get; set; }
        public string TimeShort { get; set; }
        public string Nick { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LikesTotal { get; set; } 
        public string DislikesTotal { get; set; }
        public string Raiting { get; set; }
        public IEnumerable<Comment> Answers { get; set; }
    }
}
