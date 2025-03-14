# Classic Calculator
A classic calculator engine that responds to pressing buttons and provides output including display value.

## Table of Contents
- [Description](#description)
- [Features](#features)
- [Usage](#usage)
- [License](#license)

## Description
It's a simple library that enables the user to create a calculator application or functionality without implementing any of the internal logic on their own. All they have to provide is a user interface that calls the library in response to user actions (e.g. pressing buttons) and presents the display value.

## Features
The following actions/buttons are currently supported:
- Digits (0-9)
- Decimal point (.)
- Toggle sign (+/-)
- Add (+)
- Subtract (-)
- Multiply (*)
- Divide (/)
- Calculate/Equals (=)
- Percent (%)
- Sqaure root (√)
- Clear (C)

At the moment the only output is the display value but more details will be returned in the future versions.

When creating the calculator object, the user must provide the display length in the constructor which specifies how many digits can be displayed. This will affect the calculator in the same way as it normally does on the physical devices:
1) User input is limited to the display length
2) An error occurs when the integer part of the result is too large to fit in the display
3) The least meaningful digits of the decimal part of the result are truncated to fit in the display 

The user can also provide a logger object in the constructor that will be used internally by the library.

Missing functionality:
- Memory features (M+, M-, etc.)
- CE (clear last entry)
- Other features of more advanced calculators

## Usage
An example usage:
```csharp
var calculator = new Calculator(9); // Display can show up to 9 digits (DisplayValue = 0)
calculator.PressButton(CalculatorButton.One) // Simulates pressing a button (DisplayValue = 1)
calculator.PressButton(CalculatorButton.Add) (DisplayValue = 1)
calculator.PressButton(CalculatorButton.Five) (DisplayValue = 5)
calculator.PressButton(CalculatorButton.Equals) (DisplayValue = 6)
var displayValue = calculator.DisplayValue; 
```

For more details, please refer to the provided documentation.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

