# Instruction Math

This plugin's purpose is to provide users with NINA instructions that operate based on a math equation

## Expressions

Each instruction is controlled by a math expressions.  This plugin uses mXparser to evaluate the expressions.

- [Supported built-in functions of mXparser can be found here](https://mathparser.org/mxparser-math-collection/)

## Instructions

Instructions added by this plugin

### Math Loop

This will loop while the expression evaluates to true

- The expression is compared against a target value to determine truthfulness
- Available comparisons are >, <, >= (greater than or equal), <=, =, != (not equal)

## Keywords *or* Constants, Variables & Functions

In addition to the built in constants and variables this plugin adds some useful ones:

Note: all times are represented as [milliseconds since epoch](https://en.wikipedia.org/wiki/Unix_time)

- **[count]** - The number of times an instruction has been executed
- **[time]** - the current time
- **[astro_dawn]** - The next astronomical dawn
- **[astro_dusk]** - The next astronomical dusk

## Future

The next steps of this plugin are to add more Keywords so that users can come up with more advanced expressions

## Suggestions / Pull Requests

If you would like for me to add a feature [please create an issue with an enhancement label](https://github.com/FlyingKiwis/NINA.InstructionMath/issues)

I'm also open to pull requests if you want to work on new features as well.

# 3rd party Licenses

mXparser has a dual license agreement as this is a free and open source project (FOSS) it is licensed under the non-commercial use license.

The full license can be accessed here:

https://mathparser.org/mxparser-license/
