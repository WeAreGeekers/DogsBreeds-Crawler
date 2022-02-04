

using WeAreGeekers.DogsBreeds.Crawler.Extractors;

// Extract Fci data
var listFciBreedGroups = FciExtractor.ExtractFciGroup();
var listFciBreeds = FciExtractor.ExtractFciBreed(listFciBreedGroups);



// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
