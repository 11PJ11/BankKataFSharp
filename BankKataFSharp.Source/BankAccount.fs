namespace BankKataFSharp.Source

module BankAccount = 
    open System
    
    type IClock =
        abstract member today: Unit -> DateTime

    type IDisplay = 
        abstract member show: string -> Unit
    
    type StatementPrinter = 
        | StatementPrinter of IDisplay
    
    type BankAccount = 
        | BankAccount of IClock * StatementPrinter
    
    let deposit (amount : int) (account : BankAccount) : BankAccount = account

    let withdraw (amount : int) (account : BankAccount) : BankAccount = account

    let printStatement (account:BankAccount) = ()