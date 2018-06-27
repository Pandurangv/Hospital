using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    public class EnitiyRoomMaster
    {
        public EnitiyRoomMaster()
        {

        }
        #region Properties
        public int RoomId { get; set; }
        public int CategoryId { get; set; }
        public string RoomNo { get; set; }
        public int FloorNo { get; set; }
        #endregion



        public string CategoryName { get; set; }

        public string FloorName { get; set; }
    }

    
}