namespace BankKataFSharp.Source

    open System

    type Amount = 
        | Amount of int

    type Transaction = 
        { date : DateTime
          amount : Amount }

    type ITransactions = 
        abstract Add : Transaction -> Unit

    type Transactions(transactions : Transaction list) = 
        let _transactions = transactions
        interface ITransactions with
            member x.Add transaction = ()

    type IDisplay = 
        abstract Show : string -> Unit

    type IStatementPrinter = 
        abstract PrintHeader : Unit -> Unit
        abstract Print : ITransactions -> Unit

    type StatementPrinter(display : IDisplay) = 
        let HEADER = "DATE | AMOUNT | BALANCE"
        let _display = display
        interface IStatementPrinter with
            member x.PrintHeader() = _display.Show HEADER
            member x.Print(transactions) = ()

    type BankAccount(statementPrinter : IStatementPrinter, transactions : ITransactions) = 
        let _statementPrinter = statementPrinter
        let _transactions = transactions
    
        member x.deposit (amount:int) = 
            _transactions.Add({ date = DateTime.Now
                                amount = Amount(amount) })
    
        member x.withdraw amount = ()
        member x.printStatement = 
            statementPrinter.PrintHeader()
            statementPrinter.Print _transactions
