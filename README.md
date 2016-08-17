# Twilio Xamarin Bindings

Twilio Bindings for Xamarin apps

Twilio.Common [![NuGet][common-nuget-img]][common-nuget-link]

Twilio.Conversations [![NuGet][conversations-nuget-img]][conversations-nuget-link]

## How to Build

You will need Xamarin installed on a mac.  Run the following:

```
sh ./bootstrapper.sh -t libs
```

This will download the necessary cake tooling in a `./tools/` folder, download the external dependencies, and run the build scripts.

[common-nuget-img]: https://img.shields.io/badge/nuget-0.3.1-blue.svg
[common-nuget-link]: https://www.nuget.org/packages/Twilio.Common.XamarinBinding
[conversations-nuget-img]: https://img.shields.io/badge/nuget-0.25.1-blue.svg
[conversations-nuget-link]: https://www.nuget.org/packages/Twilio.Conversations.XamarinBinding
