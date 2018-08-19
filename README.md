# sample-zoo-dotnet

A simple ASP.NET MVC 5.0 app that leverages RAPID for authoring

_Note that MVC 5.0 is *NOT* a requirement of RAPID_
_RAPID can be used with virtually any .NET application_

## To run this app

- Ensure .NET v4.5+ installed
- Run AEM
- Install RAPID
- This app uses custom AEM components:
    - The AEM package that contains those components is located under the `./aem_content` directory
    - Upload and install the AEM package (e.g. using AEM's package manager)
- Open Microsoft Visual Studio (2013 or newer is recommended)
- Navigate to `File -> Open -> Project/Solution` and open the `sample-zoo-dotnet.sln` file
- Select `sample-zoo-dotnet` from the Solution Explorer, then press `F5`
- Navigate to `http://localhost:51857` if your browser does not open automatically
- You may edit `sample-zoo-dotnet/Web.config` to adjust default properties (e.g. AEM port)

## Get started with RAPID

Check out the [RAPID getting started docs here](https://rapid.aandes.io).