namespace BankKataFSharp.Source

module BankAccount = 
    open System
    
    type IClock =
        abstract member today: Unit -> DateTime

    type IDisplay = 
        abstract member show: string -> Unit
    
    type StatementPrinter(display:IDisplay) =
        let HEADER = "DATE | AMOUNT | BALANCE" 
        let _display = display
        member x.PrintHeader =
            _display.show HEADER
    
    type BankAccount = 
        | BankAccount of IClock
    
    let deposit (amount : int) (account : BankAccount) : BankAccount = account

    let withdraw (amount : int) (account : BankAccount) : BankAccount = account

    let printStatement (printer:StatementPrinter) (account:BankAccount) = 
        printer.PrintHeader