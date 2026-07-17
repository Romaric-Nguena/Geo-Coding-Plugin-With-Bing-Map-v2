# Dataverse Geo-Coding Plugin with Bing Maps

A simple .NET geocoding project for Dataverse that converts a human-readable address into geographic coordinates using the Bing Maps REST API.

This repository can be used as a starting point for a Dataverse geocoding plugin or integration scenario where address fields need to be converted into latitude and longitude values.

## Overview

The project prompts the user for an address, sends it to the Bing Maps Locations API, reads the JSON response, and returns the first matching latitude and longitude.

Typical use cases include:

- Converting customer, account, contact, or location addresses into coordinates.
- Preparing address data for map visualization.
- Building a Dataverse plugin or workflow that automatically fills latitude and longitude fields.
- Testing Bing Maps geocoding logic before integrating it into Microsoft Dataverse.

## Current Project Status

At the moment, this repository contains a .NET 8 console application that demonstrates the geocoding logic.

It is not yet a complete Dataverse plugin package. The current implementation can be refactored into a Dataverse plugin by moving the geocoding logic into a plugin class and executing it when a Dataverse record is created or updated.

## Features

- Accepts an address as user input.
- Calls the Bing Maps REST Locations API.
- Parses the API response using `Newtonsoft.Json`.
- Extracts latitude and longitude from the first geocoding result.
- Displays the coordinates in the console.
- Provides a simple base for future Dataverse integration.

## Technologies Used

- C#
- .NET 8
- Bing Maps REST API
- Newtonsoft.Json
- Visual Studio

## Repository Structure

```text
Geo-Coding-Plugin-With-Bing-Map-v2/
├── GeoCoding.sln
├── GeoCoding/
│   ├── GeoCoding.csproj
│   └── Program.cs
├── .gitignore
└── .gitattributes
```

## Prerequisites

Before running the project, make sure you have:

- .NET 8 SDK installed.
- Visual Studio 2022 or another compatible C# IDE.
- A valid Bing Maps API key.

## Bing Maps API Key

The project requires a Bing Maps API key to call the geocoding service.

For security reasons, do not hard-code your API key directly in the source code, especially if the repository is public.

Recommended options:

- Use environment variables.
- Use user secrets during local development.
- Use secure configuration in your deployment environment.

Example using an environment variable:

```csharp
private static readonly string BingMapsApiKey =
    Environment.GetEnvironmentVariable("BING_MAPS_API_KEY");
```

Then define the environment variable before running the application.

## Installation

Clone the repository:

```bash
git clone https://github.com/your-username/Geo-Coding-Plugin-With-Bing-Map-v2.git
```

Navigate into the project folder:

```bash
cd Geo-Coding-Plugin-With-Bing-Map-v2
```

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

## Usage

Run the console application:

```bash
dotnet run --project GeoCoding
```

Enter an address when prompted:

```text
Enter the address:
1600 Amphitheatre Parkway, Mountain View, CA
```

Example output:

```text
Geo Coding Results:
----------------------------------------------------------------------------
Latitude: 37.422
Longitude: -122.084
```

## How It Works

1. The application asks the user to enter an address.
2. The address is URL-encoded.
3. A request is sent to the Bing Maps REST Locations API.
4. The JSON response is parsed.
5. The first result is selected.
6. The latitude and longitude are extracted and displayed.

Main request format:

```text
http://dev.virtualearth.net/REST/v1/Locations?q={address}&key={BingMapsApiKey}
```

## Dataverse Integration Idea

This project can be extended into a Microsoft Dataverse plugin.

A possible Dataverse scenario:

1. A user creates or updates a record containing address fields.
2. The plugin reads the address fields from the Dataverse record.
3. The plugin sends the full address to the Bing Maps geocoding API.
4. The plugin receives latitude and longitude.
5. The plugin updates the Dataverse record with the returned coordinates.

Example fields that could be used:

- `address1_line1`
- `address1_city`
- `address1_stateorprovince`
- `address1_postalcode`
- `address1_country`
- `latitude`
- `longitude`

## Important Security Note

If an API key was previously committed to this repository, it should be considered exposed.

Recommended actions:

1. Revoke or regenerate the exposed key.
2. Remove the key from the source code.
3. Store the new key securely outside the repository.
4. Avoid committing secrets in future changes.

## Possible Improvements

- Convert the console app into a real Dataverse plugin.
- Add configuration support for the Bing Maps API key.
- Add error handling for API limits, invalid addresses, and network failures.
- Add logging and tracing.
- Return confidence level and formatted address from the Bing Maps response.
- Support multiple geocoding results instead of only the first result.
- Add unit tests for the geocoding logic.
- Add retry logic for temporary API failures.
- Use HTTPS for the Bing Maps API request.

## License

No license has been specified yet.

Consider adding a license file if you want others to reuse, modify, or contribute to this project.

## Author

Created as a geocoding project using Bing Maps, with the goal of supporting address-to-coordinate scenarios and future Dataverse integration.
