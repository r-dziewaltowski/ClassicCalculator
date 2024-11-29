# Classic Calculator
It's a simple calculator giving a feel of using an old school physical calculator device.

## Table of Contents
- [Description](#description)
- [Features](#features)
- [Purpose](#purpose)
- [Usage](#usage)
- [License](#license)

## Description
The solution consists of 3 projects:
- ClassicCalculator - a class library containing all the busines logic of the calculator that is shared by the applications
- ClassicCalculatorBlazorApp - a Blazor SPA web calculator application
- ClassicCalculatorWpfApp - a WPF desktop calculator application

The business logic is completely decoupled from the presentation layer provided by the applications. Any new application (e.g. a mobile app) can just reuse the existing logic. Also, the logic implementation could be easily replaced as long as an alternative adheres to the same interface.

Unfortunately the project requires more work to make the results always correct, e.g.:
- It doesn't support big numbers both in terms of calculations and display
- It is prone to numerical problems

## Features
Both applications provide the functionality of a simple calculator with a display showing just one number and the following buttons:
- Digits
- Decimal point
- Toggle sign
- Add
- Subtract
- Multiply
- Divide
- Calculate
- Percent
- Sqaure root
- Clear

The behaviour follows quite closely my own physical calculator device.

Missing functionality:
- Memory features (M+, M-, etc.)
- CE (clear last entry)
- Other features of more advanced calculators
- There's no OFF button... :)

## Usage
```bash
git clone https://github.com/r-dziewaltowski/ClassicCalculator.git
cd .\ClassicCalculator\
```
All the commands below should be run from the root folder.

<h3>Web application</h3>

The application is deployed and avaialble at the following address: https://classiccalculator.onrender.com/. Plase note that when you try to access it it may be asleep and needs around a minute to wake up.

If you prefer to run it locally there's two options:
- Run in Docker container:
```bash
docker-compose up --build
```
Access via http://localhost/ in your browser
- Run directly:
```bash
dotnet run --project ClassicCalculatorBlazorApp\
```
Access via http://localhost:5116/ or https://localhost:7201 in your browser

<h3>Desktop application</h3>

```bash
dotnet run --project .\ClassicCalculatorWpfApp\
```

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

