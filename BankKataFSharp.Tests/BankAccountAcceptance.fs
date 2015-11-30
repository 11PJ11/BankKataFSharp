namespace BankKataFSharp.Tests

module BankAccountAcceptance =
    open NUnit.Framework
    open BankKataFSharp.Source.BankAccount
    open NSubstitute
    open System

    let clock = Substitute.For<IClock>()
    let console = Substitute.For<IDisplay>()
    let statementPrinter = StatementPrinter console
    let clockedBankAccount = BankAccount (clock, statementPrinter)

    [<Test>]
    let ``it outputs the bank statement of the operations``() =
        clock.today().Returns
            ( new DateTime(2014, 4,  1)
            , new DateTime(2014, 4,  2)
            , new DateTime(2014, 4, 10)
            ) |> ignore

        let bankAccountToBePrinted = 
            clockedBankAccount
            |> deposit 1000
            |> withdraw 100
            |> deposit 500

        bankAccountToBePrinted
        |> printStatement 

        console.Received().show "DATE | AMOUNT | BALANCE" 
        console.Received().show "10/04/2014 | 500.00 | 1400.00" 
        console.Received().show "02/04/2014 | -100.00 | 900.00" 
        console.Received().show "01/04/2014 | 1000.00 | 1000.00" 