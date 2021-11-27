using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQWeb.ExcelCreate.Models
{
    public enum FileStatus
    {
        Creating,
        Completed
    } 
    public class UserFile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string filePath { get; set; }
        public DateTime? CreateDate { get; set; }
        public FileStatus FileStatus { get; set; }
        [NotMapped] // Veri tabanına bu columnu olusturma dedik.
        public string GetCreatedDate => CreateDate.HasValue ? CreateDate.Value.ToShortDateString() : "-";
    }
}
