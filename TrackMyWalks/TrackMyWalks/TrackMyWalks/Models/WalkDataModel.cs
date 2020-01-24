using System;
using System.Collections.Generic;
using System.Text;

namespace TrackMyWalks.Models
{
    public class WalkDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public string Difficulty { get; set; }
        public string ImageUrl { get; set; }
    }
}
