﻿using WeAreGeekers.DogsBreeds.Crawler.Extractors;
using WeAreGeekers.DogsBreeds.Crawler.Responses;

// Extract fci data
List<ResponseBreedDetailData> listBreedDetailData = FciExtractor.ExtractFromFciBreedDetailData();

// Write breeds.json & csv (how to write array in csv? With '|'? Idk)
// TODO: 