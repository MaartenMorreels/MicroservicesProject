using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Assessment.DAL.Entities
{
    public abstract class BaseEnt
    {
        #region Public Properties

        public int Id { get; set; }

        //Created
        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        //Updated
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        //Is Archived
        [Required]
        public bool Archived { get; set; }

        public int? ArchivedBy { get; set; }

        public DateTime? ArchivedOn { get; set; }

        ////Is Deleted
        [Required]
        public bool Deleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        #endregion Public Properties

        

        /// <summary>
        /// Compare property values 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object comparisonObject)
        {
            Type sourceType = this.GetType();
            Type destinationType = comparisonObject.GetType();
            if (sourceType == destinationType)
            {
                PropertyInfo[] sourceProperties = sourceType.GetProperties();
                foreach (PropertyInfo pi in sourceProperties)
                {
                    // if properties are null, skip the rest of this iteration
                    if ((sourceType.GetProperty(pi.Name).GetValue(this, null) is null &&
                        destinationType.GetProperty(pi.Name).GetValue(comparisonObject, null) is null))
                    {
                        continue;
                    }
                    // if property is a collection (but not a string), compare collection values  
                    if (pi.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))
                        && pi.PropertyType != typeof(String))
                    {
                        // eerste manier: sequenceEqual method gebruiken
                        var sourceColl = (IEnumerable<object>)sourceType.GetProperty(pi.Name).GetValue(this);
                        var destinationColl = (IEnumerable<object>)destinationType.GetProperty(pi.Name).GetValue(this);

                        if (!sourceColl.SequenceEqual(destinationColl))
                        {
                            return false;
                        }
                    }
                    else if (sourceType.GetProperty(pi.Name).GetValue(this, null).ToString() !=
                        destinationType.GetProperty(pi.Name).GetValue(comparisonObject, null).ToString())
                    {
                        // only need one property to be different to fail Equals.
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}