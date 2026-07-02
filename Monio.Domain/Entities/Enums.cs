namespace Monio.Domain.Entities;

public enum TransactionType
{
    Income,
    Expense
}

public enum WalletType
{
    Cash,
    Bank,
    EWallet,
    Investment,
    Other
}

public enum CategoryType
{
    Income,
    Expense
}

public enum BudgetPeriodType
{
    Weekly,
    Monthly,
    Custom
}

public enum RecurringFrequency
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}
