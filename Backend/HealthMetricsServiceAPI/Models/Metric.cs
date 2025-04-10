﻿using System.ComponentModel.DataAnnotations;

namespace HealthMetricsServiceAPI.Models
{
    public class Metric
    {
        [Key]
        public int MetricId { get; set; }
        [Required]
        public string MetricName { get; set; } = string.Empty;
        [Required]
        public string Unit { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
