# DogsBreeds-Crawler

## Introduction

[DogsBreeds](https://dogs-breeds.com) born with the goal to create the biggest world database about dog breeds information, show them in website and expose data with an organize API that makes you to access in an easy way.

For do that we create this crawler to track information from many source and create an all-in-one exposition with all data tracked.

You can retrieve a complete json updated daily at [this link](https://storage.googleapis.com/resources.dogs-breeds.com/crawler/breeds.json).

## Installation

You can use the crawler installing the [NuGet package](https://www.nuget.org/packages/WeAreGeekers.DogsBreeds.Crawler).

```
PM> Install-Package WeAreGeekers.DogsBreeds.Crawler
```

## Spiders

We create sub-project for every spider and every spider can be donwloaded and use separate from the crawler.

1. [FCI (Federation Cynologique Internationalle)](https://github.com/WeAreGeekers/DogsBreeds-Crawler-Spiders-FCI)


## Support

If you see untracked data, bugs, export errors or you want to suggest a new feature you can open an issue or submit a pull request.


## Roadmap & Changelog

If you want to see the roadmap of the main project [dogs-breeds.com](https://dogs-breeds.com) and its sub-projects like this one you can check out our [Roadmap and release notes](https://doc.clickup.com/d/2dqhp-608/dogs-breeds-roadmap).


## Authors

- [WeAreGeekers](https://github.com/WeAreGeekers) (Organization)
- [Fabrizio Pairone](https://github.com/FabrizioPairone) - [GitLab Profile](https://gitlab.com/FabrizioPairone)


## License

This project is under MIT license, it's free, use it as you want.
