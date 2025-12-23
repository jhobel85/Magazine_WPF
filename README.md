# Magazine Conveyor

A WPF application for managing and finding free positions in a piece magazine with configurable parameters.

## Problem Description

The application solves the problem of finding available positions in a magazine with "n" positions where some positions are occupied. The magazine can operate in two modes:

- **Linear**: Positions are arranged in a straight line (positions 0 to n-1)
- **Rotary**: Positions form a circle where the last position is adjacent to position 0

The core functionality is to find the first available position that can accommodate a workpiece of a specified size (requiring multiple consecutive positions).

## Features

### Core Functionality
- **FindFreePlace Algorithm**: Efficiently finds the first available position for a workpiece requiring multiple consecutive positions
- **Linear and Rotary Modes**: Support for both linear and rotary magazine configurations
- **Interactive UI**: WPF application for visualizing and testing the algorithm

### Technical Implementation
- ✅ **MVVM Pattern**: Clean separation of concerns using Model-View-ViewModel architecture
- ✅ **Dependency Injection**: Service-based architecture with `FindFreePlaceService` injected as a dependency
- ✅ **Unit Tests**: Comprehensive test coverage with NUnit
- ✅ **Custom WPF Controls**: `CircularPanel` for visual representation of rotary magazines

## Algorithm Examples

With a 12-position magazine where positions 1, 2, and 3 are occupied:

| Configuration | Result |
|--------------|--------|
| `isRotary = false, neededPlaces = 6` | Returns `-1` (not enough consecutive free positions) |
| `isRotary = false, neededPlaces = 4` | Returns `5` (positions 5-8 are available) |
| `isRotary = false, neededPlaces = 5` | Returns `-1` (not enough consecutive free positions) |
| `isRotary = true, neededPlaces = 5` | Returns `10` (positions 10-11 and 0 wrap around) |

## Building and Running

### Prerequisites
- .NET 8.0 SDK
- Windows OS (for WPF support)

### Build
```bash
dotnet build Converyor_magazine.sln
```

### Run
```bash
dotnet run --project Magazine-Conveyor/Magazine-Conveyor.csproj
```

### Run Tests
```bash
dotnet test
```

## Usage

1. **Configure Magazine**:
   - Set the number of positions
   - Toggle "Is Rotary" checkbox for rotary mode
   - Click "Update magazine" to apply changes

2. **Select Occupied Positions**:
   - Click on positions in the visual representation to mark them as occupied/free
   - Dark positions indicate occupied, light positions indicate free

3. **Find Free Place**:
   - Enter the number of needed places
   - Click "Find free place" to execute the algorithm
   - The first available position will be highlighted in green

## Testing

The project includes comprehensive unit tests covering:
- ✅ Single free place scenarios
- ✅ Multiple free places
- ✅ All free places / all needed
- ✅ Rotary mode with border wrapping
- ✅ Negative scenarios (no solution found)
- ✅ Edge cases (empty arrays, invalid inputs)

Run all tests with:
```bash
dotnet test Magazine-Conveyor/tests/UnitTests.csproj
```

## Architecture

### MVVM Pattern
- **Model**: `Magazine` class contains the business logic and data
- **View**: `MainWindow.xaml` defines the UI
- **ViewModel**: `Magazine` implements `INotifyPropertyChanged` for data binding

### Dependency Injection
- `FindFreePlaceService` is injected as a service implementing `IService`
- Services are initialized in the `MainWindow` constructor
- Clean separation allows for easy testing and maintainability

## License

See [LICENSE](Magazine-Conveyor/LICENSE) file for details.
