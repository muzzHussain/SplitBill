export default interface UpdateExpenseDTO {
  ExpenseId: string;

  Title: string;

  UsersList: { [userEmail: string]: number };
}
