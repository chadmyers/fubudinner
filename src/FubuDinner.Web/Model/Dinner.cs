using System;

namespace FubuDinner.Web.Model
{
    public class Dinner : Entity
    {
        /*[Required]*/
        public string Title { get; set; }
        /*[Required, MaximumLength(256)]*/
        public string Description { get; set; }
        /*[Required]*/
        public DateTime EventDate { get; set; }
        /*[Required]*/
        public string Address { get; set; }
        //TODO: COUNTRIES
        /*[Required]*/
        public string ContactPhone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}