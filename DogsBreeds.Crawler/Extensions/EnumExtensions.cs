using WeAreGeekers.DogsBreeds.Crawler.Enums;
using WeAreGeekers.DogsBreeds.Crawler.Spiders.FCI.Enums;

namespace WeAreGeekers.DogsBreeds.Crawler.Extensions
{

    /// <summary>
    /// Static class with extension methods of enums
    /// </summary>
    public static class EnumExtensions
    {

        /// <summary>
        /// Map BreedStatus enum of FCI Spider into private enum
        /// </summary>
        /// <param name="breedStatus"></param>
        /// <returns></returns>
        public static FciBreedStatus ToFciBreedStatus(this BreedStatus breedStatus)
        {
            switch (breedStatus)
            {
                // Provisional
                case BreedStatus.Provisional:
                    return FciBreedStatus.Provisional;

                // Definitive
                case BreedStatus.Definitive:
                    return FciBreedStatus.Definitive;

                // Not implemented
                default:
                    throw new NotImplementedException(
                        string.Format(
                            "Warning! entry of breed status equals to '{0}' not supported in mapping yet.",
                            breedStatus.ToString()
                        )
                    );
            }
        }

        /// <summary>
        /// Map BreedWorkingTrial enum of FCI Spider into private enum
        /// </summary>
        /// <param name="breedWorkingTrial"></param>
        /// <returns></returns>
        public static FciBreedWorkingTrial ToFciWorkingTrial(this BreedWorkingTrial breedWorkingTrial)
        {
            switch (breedWorkingTrial)
            {
                // Not subject 
                case BreedWorkingTrial.NotSubject:
                    return FciBreedWorkingTrial.NotSubject;

                // Subject 
                case BreedWorkingTrial.Subject:
                    return FciBreedWorkingTrial.Subject;

                // Subject limited on country applied
                case BreedWorkingTrial.SubjectLimitedOnCountryApplied:
                    return FciBreedWorkingTrial.SubjectLimitedOnCountryApplied;

                // Subject on some countries
                case BreedWorkingTrial.SubjectOnSomeCountry:
                    return FciBreedWorkingTrial.SubjectOnSomeCountry;

                // Subject to nordic countries
                case BreedWorkingTrial.SubjectOnNordicCountries:
                    return FciBreedWorkingTrial.SubjectOnNordicCountries;

                // Not implemented
                default:
                    throw new NotImplementedException(
                        string.Format(
                            "Warning! entry of breed working trial equals to '{0}' not supported in mapping yet.",
                            breedWorkingTrial.ToString()
                        )
                    );
            }
        }

    }

}
