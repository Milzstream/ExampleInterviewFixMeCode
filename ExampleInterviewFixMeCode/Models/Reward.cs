using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExampleInterviewFixMeCode.Models
{
    //Reward
    public class Reward
    {
        [Required(ErrorMessage = "Id is required and must be Unique!")]
        public string Id = "1";
        [StringLength(500)]
        private string Description;
        [Required(ErrorMessage = "Display Name can't be empty!")]
        public string DisplayName;

        /// <summary>
        /// Required List of Associated UPCs that trigger the reward
        /// </summary>
        public List<long> AssociatedUpcs { get; set; }

        /// <summary>
        /// True if Model is Valid
        /// False if Model is InValid
        /// </summary>
        public bool isValid
        {
            get
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Id))
                        if (Description.Length < 501)
                            if (!String.IsNullOrWhiteSpace(DisplayName))
                                return true;

                }
                catch (Exception ex)
                {
                    return false;
                    throw ex;
                }
                return false;
            }
        }
    }
}
