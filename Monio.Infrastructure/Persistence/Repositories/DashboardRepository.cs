using Dapper;
using Monio.Application.Features.Dashboard.DTOs;
using Monio.Application.Interfaces.Persistence;
using Monio.Infrastructure.Persistence.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Infrastructure.Persistence.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DapperConnectionFactory _connectionFactory;

        public DashboardRepository(
            DapperConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<DashboardSummaryResponse> GetSummaryAsync(Guid userId, DateOnly? fromDate, DateOnly? toDate)
        {
            const string sql = """
                SELECT
                    COALESCE(SUM(CASE WHEN "Type" = 'Income' THEN "Amount" ELSE 0 END),0) AS "TotalIncome",
                    COALESCE(SUM(CASE WHEN "Type" = 'Expense' THEN "Amount" ELSE 0 END),0) AS "TotalExpense"
                FROM "Transactions"
                WHERE "UserId" = @UserId
                    AND (
                        CAST(@FromDate AS date) IS NULL 
                        OR "TransactionDate" >= CAST(@FromDate AS date)
                    )
                    AND (
                        CAST(@ToDate AS date) IS NULL 
                        OR "TransactionDate" <= CAST(@ToDate AS date)
                    );
                """;

            using var connection = _connectionFactory.CreateConnection();

            var result = await connection.QuerySingleAsync<DashboardSummaryResponse>(
                sql,
                new
                {
                    UserId = userId,
                    FromDate = fromDate?.ToDateTime(TimeOnly.MinValue),
                    ToDate = toDate?.ToDateTime(TimeOnly.MinValue)
                });

            result.Balance = result.TotalIncome - result.TotalExpense;

            return result;
        }
    }
}
