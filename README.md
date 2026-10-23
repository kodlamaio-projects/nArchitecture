> [!WARNING]
> **Breaking change: nArchitecture 2.0 starter.** This now targets .NET 10 with `NArchitecture.Core.Mediator` and [Mapperly](https://mapperly.riok.app/) instead of MediatR/AutoMapper. If your project was created from the 1.x starter, don't pull these changes directly into it. Read the [migration guide](https://github.com/kodlamaio-projects/nArchitecture.Core/blob/main/docs/migration-guide-2.0.md) ([Türkçe](https://github.com/kodlamaio-projects/nArchitecture.Core/blob/main/docs/migration-guide-2.0.tr.md)) or run `nArchGen upgrade` in your project.

<p align="center">
  <a href="https://github.com/kodlamaio-projects/nArchitecture/graphs/contributors"><img src="https://img.shields.io/github/contributors/kodlamaio-projects/nArchitecture.svg?style=for-the-badge"></a>
  <a href="https://github.com/kodlamaio-projects/nArchitecture/network/members"><img src="https://img.shields.io/github/forks/kodlamaio-projects/nArchitecture.svg?style=for-the-badge"></a>
  <a href="https://github.com/kodlamaio-projects/nArchitecture/stargazers"><img src="https://img.shields.io/github/stars/kodlamaio-projects/nArchitecture.svg?style=for-the-badge"></a>
  <a href="https://github.com/kodlamaio-projects/nArchitecture/issues"><img src="https://img.shields.io/github/issues/kodlamaio-projects/nArchitecture.svg?style=for-the-badge"></a>
  <a href="https://github.com/kodlamaio-projects/nArchitecture/blob/master/LICENSE"><img src="https://img.shields.io/github/license/kodlamaio-projects/nArchitecture.svg?style=for-the-badge"></a>
</p><br />

<p align="center">
  <a href="https://github.com/kodlamaio-projects/nArchitecture"><img src="https://user-images.githubusercontent.com/53148314/194872467-827dc967-acee-4bca-88a2-59ed5695bebf.png" height="125"></a>
  <h3 align="center">nArchitecture Project
</h3>
  <p align="center">
    <!-- PROJECT_DESCRIPTION -->
    <!-- <br />
    <a href="https://github.com/kodlamaio-projects/nArchitecture"><strong>Explore the docs »</strong></a>
    <br /> -->
    <!-- <br />
    <a href="https://github.com/kodlamaio-projects/nArchitecture">View Demo</a>
    · -->
    <a href="https://github.com/kodlamaio-projects/nArchitecture/issues">Report Bug</a>
    ·
    <a href="https://github.com/kodlamaio-projects/nArchitecture/discussions">Request Feature</a>
  </p>
</p>

## 💻 About The Project

As Kodlama.io, we decided to share examples of completed projects. Inspired by Clean Architecture, nArchitecture is a monolith project that showcases advanced development techniques. The project includes Clean Architecture, CQRS, Advanced Repository, Dynamic Querying, JWT, OTP, Google & Microsoft Auth, Role-Based Management, Distributed Caching (Redis), Logging (Serilog), Elastic Search, [Code Generator](https://github.com/kodlamaio-projects/nArchitecture.Gen) and much more. By contributing, you can support the project and learn new things.

### Built With

[![](https://img.shields.io/badge/.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/tr-tr/dotnet/welcome)

## ⚙️ Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

- .NET 10 SDK (see `global.json`)

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/kodlamaio-projects/nArchitecture.git
   ```
2. Configure `appsettings.json` in WebAPI.
3. Run `Update-Database` command with Package Manager Console in WebAPI to create tables in sql server.

## 🚀 Usage

1. Run example WebAPI project `dotnet run --project src\starterProject\WebAPI`
2. Run tests `dotnet test`

### Analysis

1. If not, Install dotnet tool `dotnet tool restore`.
2. Run anaylsis command `dotnet roslynator analyze`

### Format

1. If not, Install dotnet tool `dotnet tool restore`.
2. Run format command `dotnet csharpier format .`

## 🆕 What's new in 2.0

- **.NET 10**, package versions managed centrally in `Directory.Packages.props`, shared project settings in `Directory.Build.props`.
- **NArchitecture.Core 2.0**: MediatR is replaced by `NArchitecture.Core.Mediator` (`using NArchitecture.Core.Mediator;`, `services.AddMediator(...)`), AutoMapper by [Mapperly](https://mapperly.riok.app/) static mappers under each feature's `Mappers` folder.
- Swashbuckle 10 / Microsoft.OpenApi 2, EF Core 10, xunit v3 with the Microsoft Testing Platform runner.
- Generate new features with [nArchitecture.Gen](https://github.com/kodlamaio-projects/nArchitecture.Gen) 2.0: `dotnet tool restore` then `dotnet nArchGen generate crud`.
- Upgrading a project created from the 1.x starter? See the [migration guide](https://github.com/kodlamaio-projects/nArchitecture.Core/blob/main/docs/migration-guide-2.0.md) or run `nArchGen upgrade` in your solution folder.

## 🚧 Roadmap

See the [open issues](https://github.com/kodlamaio-projects/nArchitecture/issues) for a list of proposed features (and known issues).

## 🤝 Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the project and clone your local machine
2. Create your Feature Branch (`git checkout -b <Feature>/<AmazingFeature>'`)
3. Develop
4. Commit your Changes (`git add . && git commit -m '<SemanticCommitType>(<Scope>): <AmazingFeature>'`)
   💡 Check [Semantic Commit Messages](./docs/Semantic%20Commit%20Messages.md)
5. Push to the Branch (`git push origin <Feature>/<AmazingFeature>`)
6. Open a Pull Request

Contributing on Core Packages With This Repo:

1. Fork the [nArchitecture.Core](https://github.com/kodlamaio-projects/nArchitecture.Core) project
2. Locate to `src/corePackages` path (`cd .\src\corePackages\`)
3. Add your forked nArchitecture.Core repository remote address (`git remote add <YourUserName> https://github.com/<YourUserName>/nArchitecture.Core.git`)
4. Create your Feature Branch (`git checkout -b <Feature>/<AmazingFeature>'`)
5. Develop
6. Commit your changes (`git add . && git commit -m '<SemanticCommitType>(<Scope>): <AmazingFeature>'`)
   💡 Check [Semantic Commit Messages](./docs/Semantic%20Commit%20Messages.md)
7. Push to the branch (`git push <YourUserName> --set-upstream HEAD:refs/heads/<Feature>/<AmazingFeature>`)
8. Open a Pull Request

If your pull request is accepted and merged:

9. Locate to `src/corePackages` path (`cd .\src\corePackages\`)
10. Switch to main branch `git checkout main`
11. Locate root path `/` path (`cd ..\..\`)
12. Pull repo and submodule `git submodule update --remote`
13. Commit your changes (`git add . && git commit -m 'build(corePackages): update submodule'`)
14. Push to the Branch (`git push origin <Feature>/<AmazingFeature>`)
15. Open a Pull Request

## ⚖️ License

Distributed under the MIT License. See `LICENSE` for more information.

## 📧 Contact

**Project Link:** [https://github.com/kodlamaio-projects/nArchitecture](https://github.com/kodlamaio-projects/nArchitecture)

<!-- ## 🙏 Acknowledgements
- []() -->