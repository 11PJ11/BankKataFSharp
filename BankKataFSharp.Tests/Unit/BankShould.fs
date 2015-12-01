module BankShould 

    open BankAccount.Source.Model
    open BankAccount.Source.Infrastructure
    open BankAccount.Source.Actions
    open NUnit.Framework
    open NodaTime
    open NodaTime.Testing
    open Swensen.Unquote
    open Swensen.Unquote.Assertions
    open FSharpx.State
    open NSubstitute

    [<Test>][<Timeout(500)>]
    let ``print account statement header`` () =
        let clock = FakeClock.FromUtc(2014, 4, 1)
        let account = BankAccount clock
        let printer = Substitute.For<IStatementPrinter>()
        
        printStatement printer account

        printer.Received().printHeader()

    [<Test>][<Timeout(500)>]
    let ``print account statement`` () =
        let clock = FakeClock.FromUtc(2014, 4, 1)
        let account = BankAccount clock
        let printer = Substitute.For<IStatementPrinter>()
        
        printStatement printer account

        printer.Received().print(Arg.Any<BankAccount>())