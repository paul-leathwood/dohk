# Project D.O.H.K.

This is collection of containerised applications to be used as a training tutorial
for integrating [Docker](https://www.docker.com/) containers with [OpenTracing](http://opentracing.io/) on [Kubernetes](https://kubernetes.io/) using [Helm](https://helm.sh/) charts

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

## How To Use

To clone and run this application, you'll need
- [Git](https://git-scm.com)
- [DotNet Core 2.1 SDK](https://www.microsoft.com/net/learn/get-started)
- [Docker](https://docs.docker.com/install/)
- [Docker Compose CLI](https://docs.docker.com/compose/install/)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)
- [Kubernetes CLI](https://kubernetes.io/docs/tasks/tools/install-kubectl/)
- [Helm CLI](https://docs.helm.sh/using_helm/#installing-helm)

```bash
# Clone this repository
$ git clone https://github.com/pgl/dohk

# Update brew
brew update

# Install the dotnetcore sdk
brew install dotnet-sdk

# Install the azure-cli
brew install azure-cli

# Install kubectl using the azure-cli
brew install kubernetes-cli

# Install helm
brew install kubernetes-helm
```

### Installing

A step by step series of examples that tell you how to get a development env running

Say what the step will be

```
docker run -d -p 27017:27107 --name mongodb bitnami/mongodb:4.1.1
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
