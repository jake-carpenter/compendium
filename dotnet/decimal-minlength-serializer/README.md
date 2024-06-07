# JSON Converters for decimal

These are just my musings dealing with trailing zeros in `decimal` types within dotnet when serializing with *System.Text.Json*. Tests are included for expected output and the main project is a benchmark to compare the performance of serialization.

## Default

This is what I *believe* the default behavior is. This is a baseline for the benchmarks.

## Attempt 1

Format decimals using the `"G0"`` string format specifier to minimize the number of trailing zeroes. I wanted this to be in there to know how bad converting to a string is.

## Attempt 2

"Normalize" by dividing by 1 trailed by 28 zeroes as the decimal (max decimal scale).
