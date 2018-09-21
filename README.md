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

## Authors

* **Paul Leathwood** - *Initial work* - https://github.com/ple16

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
