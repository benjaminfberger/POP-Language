# POP Language (under development)

A programming language and transpiler built from scratch using **C#**. 

This language attempts to solve the problem that modern languages present through the complexity of multithreading. POP simplifies multithreading by using a concurrent actor model, in which each `program()` is an actor that will run on a separate thread to the program that created it. The `run` keyword creates a new thread for the program specified to execute in (e.g. `run example()` would create a new thread and run the program `example()` on it). However, without the `run` keyword, the program will execute on the same thread as the program it is within (e.g. `example()` inside `main()` would run the program `example()` on the same thread as `main()`. 

## Roadmap
- [x] Tokenizer (converts source code into a stream of valid tokens)
- [x] Parser (parses tokens into the ast)
- [ ] Type Checker (ensures ast is type safe)
- [ ] Code Generation (translates ast into c# source code and compiles it using the .NET compiler)
- [ ] Runtime (manages thread dispatching and execution)

## Example
```
program main{
  init{
    let x = int()
    let y = int()
  }
  print(x + y)
  run example(5) // this will create a new thread and run the program example on it
  example() // this will run the program example on the same thread as the program main
}

program example{
  init{
    let message = arg(0) // this will create a string() with the value given by the first argument passed into example()
  }
  print("example")
  print(message)
}
```

## Testing
Since the backend of the transpiler is not yet complete, you can verify the tokenizer and parser handle the language syntax correctly by running the unit tests via the .NET CLI. 

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/) (9.0 or later required)

### Tests
To run all the automated tests, run the following command in the root directory
```bash
cd pop/tests
dotnet test
```

[docs](https://github.com/benjaminfberger/POP-Language/blob/master/pop/docs/docs.md)
