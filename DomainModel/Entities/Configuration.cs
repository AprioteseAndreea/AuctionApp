// <copyright file="Configuration.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DomainModel
{
    using System.ComponentModel.DataAnnotations;

    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the maximum auctions.
        /// </summary>
        /// <value>
        /// The maximum auctions.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int MaxAuctions { get; set; }

        /// <summary>
        /// Gets or sets the initial score.
        /// </summary>
        /// <value>
        /// The initial score.
        /// </value>
        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double InitialScore { get; set; }

        /// <summary>
        /// Gets or sets the minimum score.
        /// </summary>
        /// <value>
        /// The minimum score.
        /// </value>
        [Range(1, 5, ErrorMessage = "Please enter a value bigger than {1}")]
        public double MinScore { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>
        /// The days.
        /// </value>
        [Range(1, 365, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Days { get; set; }
    }
}
