﻿namespace FitnessProgram.ViewModels.BestResult
{
    public class BestResultViewModel
    {
        public int Id { get; init; }

        public List<string> BeforePhotos { get; init; }

        public List<string> AfterPhotos { get; init; }

        public string Story { get; init; }

        public DateTime CreatedOn { get; init; }
    }
}
