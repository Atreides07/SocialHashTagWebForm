using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialHashTagWebForm.Core.Repository
{
    public class VideoHashTag
    {
        [Key]
        public string UniqueId { get; set; }

        public string Id { get; set; }
        public string SourceProvider { get; set; }
        public string VideoUrl { get; set; }
        public string Tag { get; set; }

        public string AuthorName { get; set; }
        public string AuthorUrl { get; set; }

        public DateTime AddTime { get; set; }
    }
}