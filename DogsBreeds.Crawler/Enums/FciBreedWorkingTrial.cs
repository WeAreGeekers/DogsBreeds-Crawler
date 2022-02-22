namespace WeAreGeekers.DogsBreeds.Crawler.Enums
{

    /// <summary>
    /// Status of working trial of breed defined by fci
    /// </summary>
    public enum FciBreedWorkingTrial
    {

        /// <summary>
        /// Not subject to working trial
        /// </summary>
        NotSubject = 0,

        /// <summary>
        /// Subject to working trial
        /// </summary>
        Subject = 1,

        /// <summary>
        /// Subject to working trial but only in country that applied
        /// </summary>
        SubjectLimitedOnCountryApplied = 2,

        /// <summary>
        /// Subject to working trial in some country
        /// </summary>
        SubjectOnSomeCountry = 3,

        /// <summary>
        /// Subject to working trial only in nordic countries
        /// </summary>
        SubjectOnNordicCountries = 4,

    }

}
