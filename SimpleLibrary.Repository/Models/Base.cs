using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Base
    {
        public Base()
        {
            CreateDate = DateTime.UtcNow;
        }
        [Required]
        public int Id { get; set; }
        public bool Versioned { get; set; }
        public bool Archived { get; set; }
        public bool Deleted { get; set; }

        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        //public DateTime CreateDate { get; set; }

        private DateTime createDate;
        public DateTime CreateDate		// * careful: this propertyName used for stirng check in Mehtods.PropertyNameValuePairString
        {
            get { return createDate; }
            set { createDate = value; }

            /// convert client <-> UTC when get/set
            /// 
            //get	{ return Methods.ConvertTime(createDate, Constants.TimeTo.Client); }
            //set
            //{
            //	// if vlaue close to UTC time then assign it, else subtract client time to make it UTC
            //	if (value <= DateTime.UtcNow && value >= DateTime.UtcNow.AddSeconds(-2))
            //		createDate = value;
            //	else
            //		createDate = Methods.ConvertTime(createDate, Constants.TimeTo.Utc);
            //}
        }

        private DateTime? lastModifiedDate;
        public DateTime? LastModifiedDate		// * careful: this propertyName used for stirng check in Mehtods.PropertyNameValuePairString
        {
            get { return lastModifiedDate; }
            set { lastModifiedDate = value; }

            
        }

    }
}
