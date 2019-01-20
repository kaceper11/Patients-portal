using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsPortalModel.Entities
{
    [TrackChanges]
    public class Conversation
    {
        public Conversation()
        {
            Status = MessageStatus.Sent;
        }

        public enum MessageStatus
        {
            Sent,
            Delivered
        }

        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Message { get; set; }

        public MessageStatus Status { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
