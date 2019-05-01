using System;
using SprinklingApp.Model.Entities.Abstract;

namespace SprinklingApp.Model.Entities.Concrete {

    public class ValveLog : BaseEntity {
        public int ValveId { get; set; }

        public string ValveName { get; set; }

        /// <summary>
        ///     opened or closed
        /// </summary>
        public string Operation { get; set; } //open or close

        /// <summary>
        ///     successful or fail
        /// </summary>
        public string Status { get; set; }

        public DateTime OperationTime { get; set; }

        public float Tonnage { get; set; }

        public long RaspberryId { get; set; }
        public string RaspberryName { get; set; }
    }

}