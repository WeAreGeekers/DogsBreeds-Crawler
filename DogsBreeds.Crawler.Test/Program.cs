using WeAreGeekers.DogsBreeds.Crawler;
using WeAreGeekers.DogsBreeds.Crawler.Responses;

// Create object
DogsBreedsCrawler dogsBreedsCrawler = new DogsBreedsCrawler();

// Get breeds
List<BreedDetails> listBreeds = dogsBreedsCrawler.GetBreeds();