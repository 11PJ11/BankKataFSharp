namespace BankKataFSharp.Source

    open System


    type Amount = Amount of int

    type Transaction = 
        | Credit of DateTime * Amount
        | Debit of DateTime * Amount

    type Transactions(transactions: Transaction list) =
        let _transactions = transactions

    type IStatementPrinter =
        abstract member PrintHeader: Unit -> Unit
        abstract member Print: Transactions -> Unit

    type StatementPrinter() =
        interface IStatementPrinter with
            member x.PrintHeader() = ()
            member x.Print transactions = ()

    type BankAccount(statementPrinter: IStatementPrinter,
                     transactions: Transactions) =
        
        let _statementPrinter = statementPrinter
        let _transactions = transactions

        member x.deposit amount = ()
        member x.withdraw amount = ()
        member x.printStatement = 
            statementPrinter.PrintHeader()
            statementPrinter.Print _transactions
            

    type IDisplay =
        abstract member Show: string -> Unit 