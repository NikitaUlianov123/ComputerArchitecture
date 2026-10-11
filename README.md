# ComputerArchitecture

Code from a computer architecture camp (summer 2021), written in C#.

## What's here

- **ConsoleApp/ClassLibrary**: an emulator for a custom 32-register ISA. Instructions are grouped into math (`Add`, `Sub`, `Mul`, `Div`, `Mod`), logic (`And`, `Or`, `Xor`, `Not`, `SHL`, `SHR`, `Eq`), memory (`Set`, `Load`, `Store`, `Push`, `Pull`, ...) and flow control (`Goto`, `Breq`, `Nop`, with label support). The register layout and opcodes are in `Plan.txt`.
- **EmulatorMMIO**: a WinForms front end that maps part of the emulator's memory to a 32x32 pixel display.
- **Week3**: a parser for Java `.class` files: constant pool, fields, methods, and attributes such as `Code`, `LineNumberTable` and `StackMapTable`. `JavaStuff` holds the sample Java program it reads.
- **ConsoleApp1**: bit-manipulation exercises with xUnit tests.
- **Logisim.circ**: a Logisim circuit from the camp.
